using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using Raytracer.shapes;
using Raytracer.utils;
using System.IO;


namespace Raytracer
{
    class Raytracer
    {
        #region Attributes
        private static Raytracer instance_;
        private Bitmap image_;        
        private PerlinNoise perlin = new PerlinNoise();

        private Boolean aliasing;

        #endregion

        #region Constructor
        private Raytracer()
        {
            Aliasing = false;
            //Au cas où l'utilisateur ne choisit aucune couleur
        }
        #endregion

        #region Method
        /// <summary>
        /// Generate a bitmap from a 3D scene.
        /// </summary>
        /// <param name="cam">The camera through which the scene is seen</param>
        /// <param name="screen">The screen to render on</param>
        /// <param name="nodes">The list of shapes belonging to the scene</param>
        /// <param name="lights">The list of light belonging to the scene</param>
        /// <param name="ambient">The scene ambient color</param>
        /// <param name="pb">The progess bar to increase during the rendering</param>
        /// <returns></returns>
        public Bitmap render_image(Camera cam, utils.Screen screen, List<Shape> nodes,
                                   List<Light> lights, NormalizedColor ambient,
                                   ProgressBar pb)
        {
            image_ = new Bitmap(screen.Width, screen.Height);
            Ray ray = null;

            NormalizedColor pixel_color = null;

            Vector3 intersection_point = null;
            Shape intersection_shape = null;

            int x = 0;
            int y = screen.Height - 1; 
            
            if (!aliasing)
            {
                for (int height = -screen.Height / 2; height < screen.Height / 2; ++height, --y, x = 0)
                {
                    for (int width = -screen.Width / 2; width < screen.Width / 2; ++width, ++x)
                    {
                        ray = Ray.get_ray(x, y, screen, cam);
                        get_intersection(ray, nodes, ref intersection_point, ref intersection_shape);
                        NormalizedColor newColor = new NormalizedColor();
                        if (intersection_point != null)
                        {
                            get_newColor(ray, nodes, lights, ambient, intersection_point, intersection_shape, ref newColor);
                            pixel_color = newColor;
                            image_.SetPixel(x, y, pixel_color.to_color());
                        }
                    }
                    pb.Value++;
                }
            }
            else
            {
                for (int height = -screen.Height / 2; height < screen.Height / 2; ++height, --y, x = 0)
                {
                    for (int width = -screen.Width / 2; width < screen.Width / 2; ++width, ++x)
                    {
                        NormalizedColor newColor = new NormalizedColor();
                        pixel_color = new NormalizedColor();
                        for (double i = x - 0.5d; i <= x + 0.6d; i += 0.25d)
                            for (double j = y - 0.5d; j <= y + 0.6d; j += 0.25d)
                            {
                                ray = Ray.get_ray(i, j, screen, cam);

                                get_intersection(ray, nodes, ref intersection_point, ref intersection_shape);
                                if (intersection_point != null)
                                {
                                    get_newColor(ray, nodes, lights, ambient, intersection_point, intersection_shape, ref newColor);
                                }
                                pixel_color += new NormalizedColor(newColor.R / 25, newColor.G / 25, newColor.B / 25);
                                image_.SetPixel(x, y, pixel_color.to_color());
                            }
                    }
                    pb.Value++;
                }
            }          

            return image_;
        }

        private void get_newColor(Ray ray, List<Shape> nodes, List<Light> lights, NormalizedColor ambient, Vector3 intersection_point, Shape intersection_shape, ref NormalizedColor newColor)
        {
            newColor = ambient * intersection_shape.Mat.diffuse * 0.22;
            foreach (Light light in lights)
                if (!is_shadowed_by_light(nodes, intersection_shape, intersection_point, light))
                    newColor += light.apply_lightning(ambient, intersection_shape, ray,
                                      intersection_shape.normal_at_point(intersection_point), intersection_point, perlin);
            if (intersection_shape.Mat.ReflectedCoef > 0.00001f)
                  {
                      NormalizedColor reflectedColor = get_reflected_Color(nodes, intersection_shape, intersection_point, lights, ray, ambient);
                      if (reflectedColor != null)
                          newColor = newColor * (1d - intersection_shape.Mat.ReflectedCoef)
                                     + reflectedColor * intersection_shape.Mat.ReflectedCoef;
                  }
        }
        /// <summary>
        /// Check wheter there is an intersection for a particular rays
        /// </summary>
        /// <param name="ray">The ray on which the intersection is applied</param>
        /// <param name="nodes">The shape list in which we are looking for an intersection</param>
        /// <param name="point">The variable containing the resulting point of intersection if exists, NULL otherwise</param>
        /// <param name="shape">The variable containing the resulting insertected shape if exists, NULL otherwise</param>
        private void get_intersection(Ray ray, List<Shape> nodes,
                                      ref Vector3 point, ref Shape shape)
        {
            point = null;
            shape = null;

            foreach (Shape n in nodes)
            {
                Vector3 intersection_point = n.intersect(ray);

                if (intersection_point != null
                    && (point == null
                        || Vector3.distance(new Vector3(), intersection_point) < Vector3.distance(new Vector3(), point)))
                {
                    point = intersection_point;
                    shape = n;
                }
            }
        }

        /// <summary>
        /// Check whether a surface is submitted to shadows or not.
        /// </summary> 
        /// <param name="nodes">The shape list in which we are looking for the shadowing</param>
        /// <param name="intersection_shape">The current shape on which the shadowing is checked</param>
        /// <param name="intersection_point">The current point of intersection of the shape on which the shadowing is checked</param>
        /// <param name="l">The emitting light that could produce the shadow</param>
        /// <returns></returns>
        private Boolean is_shadowed_by_light(List<Shape> nodes, Shape intersection_shape,
                                    Vector3 intersection_point, Light l)
        {
            // Compute shadows
            foreach (Shape n in nodes)
            {
                if (intersection_shape == n)
                    continue;

                Vector3 direction = l.get_direction(intersection_point) * -1;
                if (n.intersect(new Ray(intersection_point, direction)) != null)
                    return true;
            }
            return false;
        }


        private NormalizedColor get_reflected_Color(List<Shape> nodes, Shape intersection_shape, Vector3 intersection_point, List<Light> lights, Ray ray, NormalizedColor ambient)
        {
            Vector3 normal = intersection_shape.normal_at_point(intersection_point);
            double theta = -1d * (normal | ray.Dir);
            if (theta == 0)
                return null;
            Vector3 rayDir = ray.Dir + (2d * normal * theta);
            rayDir.normalize();
            Ray reflectedRay = new Ray(intersection_point, rayDir);

            Vector3 intersection_Reflected_point = null;
            Shape intersection_Reflected_shape = null;
            NormalizedColor reflectedColor = new NormalizedColor();
            get_intersection(reflectedRay, nodes, ref intersection_Reflected_point, ref intersection_Reflected_shape);
            if (intersection_Reflected_point != null)
            {
                reflectedColor = ambient * intersection_Reflected_shape.Mat.diffuse * 0.22;
                foreach (Light light in lights)
                {
                   if (!is_shadowed_by_light(nodes, intersection_Reflected_shape, intersection_Reflected_point, light))
                        reflectedColor += light.apply_lightning(ambient, intersection_Reflected_shape, reflectedRay,
                                                intersection_Reflected_shape.normal_at_point(intersection_Reflected_point), 
                                                intersection_Reflected_point, perlin);
                }
            }
            else
                return null;
            return reflectedColor;
        }
                
        #endregion

        #region Getter
        public Bitmap Img
        {
            get { return image_; }
        }
        #endregion

        #region Singleton
        public static Raytracer Instance
        {
            get
            {
                if (instance_ == null)
                    instance_ = new Raytracer();
                return instance_;
            }
        }

        public bool Aliasing
        {
            set
            {
                aliasing = value;
            }
        }

        
        #endregion
    }
}
