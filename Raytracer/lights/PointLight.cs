using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer.utils;
using System.Drawing;
using Raytracer.shapes;

namespace Raytracer.lights
{
    class PointLight : AttenuationLight
    {

        public PointLight(NormalizedColor c, Vector3 pos,
                          float cst, float linear, float quadra)
                          : base(c, pos, cst, linear, quadra)
        { }

        public override NormalizedColor apply_lightning(NormalizedColor ambient,Shape intersectionShape,
                                                        Ray ray, Vector3 normal, Vector3 point,PerlinNoise perlin)
        {
            Material material = intersectionShape.Mat;
            if (intersectionShape.IsPerlin)
            {
                double min = -1d;
                double max = 1d;
                if (intersectionShape.Perlin_marbre)
                    color_ = perlin.marbre(PerlinNoise.Noise(point, 10, ref min, ref max), point, intersectionShape.Color1, intersectionShape.Color2);
                else if (intersectionShape.Perlin_bois)
                    color_ = perlin.bois(PerlinNoise.Noise(point, 10, ref min, ref max), 0.25, intersectionShape.Color1, intersectionShape.Color2);
                else
                    color_ = perlin.newWorld(PerlinNoise.Noise(point, 4, ref min, ref max), 0.01d, 0.05d, 0.15d, intersectionShape.Color1, intersectionShape.Color2, intersectionShape.Color3);
            }
            Vector3 direction = pos_ - point;
            direction.normalize();

            // Diffuse
            NormalizedColor diffuse = color_ * Math.Max(direction | normal, 0.0d);

            // Specular
            double spec_factor = specular_get(ray, direction, point, normal, material.shininess);
            NormalizedColor specular_color = material.specular * spec_factor * color_;

            // Light attenuation
            double attenuation = attenuation_get(point);

            ambient = ambient * attenuation;
            diffuse = diffuse * attenuation;
            specular_color = specular_color * attenuation;

            return (diffuse + specular_color) * material.diffuse * ambient;
        }

        public override Vector3 get_direction(Vector3 point)
        {
            Vector3 direction = pos_ - point;
            direction.normalize();

            return direction;
        }

        #region getters/setters
        public Vector3 position_get
        {
            get { return pos_; }
        }
        #endregion
    }
}
