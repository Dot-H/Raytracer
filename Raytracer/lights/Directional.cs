using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Raytracer.utils;
using Raytracer.shapes;

namespace Raytracer.lights
{
    class Directional : Light
    {
        #region Attribute
        private Vector3 dir_;
        #endregion

        #region Constructor
        public Directional(NormalizedColor c, Vector3 dir) : base(c)
        {
            dir_ = dir;
            dir_.normalize();
        }
        #endregion

        #region Methods
        public override NormalizedColor apply_lightning(NormalizedColor ambient,Shape intersectionShape,
                                                        Ray ray, Vector3 normal, Vector3 point, PerlinNoise perlin)
        {
            Material material = intersectionShape.Mat;
            if (intersectionShape.IsPerlin)
            {
                double min = -10d;
                double max = 10d;
                if (intersectionShape.Perlin_marbre)
                    material.diffuse = perlin.marbre(PerlinNoise.Noise(point, 10, ref min, ref max), point, intersectionShape.Color1, intersectionShape.Color2);
                else if (intersectionShape.Perlin_bois)
                    material.diffuse = perlin.bois(PerlinNoise.Noise(point, 10, ref min, ref max), 0.25, intersectionShape.Color1, intersectionShape.Color2);
                else
                    material.diffuse = perlin.newWorld(PerlinNoise.Noise(point, 4, ref min, ref max), 0.01d, 0.05d, 0.15d, intersectionShape.Color1, intersectionShape.Color2, intersectionShape.Color3);
            }
            NormalizedColor newDiffuse = color_ * Math.Max((-1d * dir_) | normal, 0d);
            NormalizedColor specular = color_ * specular_get(ray, dir_, point, normal, material.shininess) * material.specular;
            NormalizedColor res = (newDiffuse + specular) * ambient * material.diffuse;
            return res;
        }

        public override Vector3 get_direction(Vector3 point)
        {
            dir_.normalize();
            return dir_;
        }
        #endregion

        #region Getter
        public Vector3 Dir
        {
            get { return dir_; }
        }
        #endregion
    }
}
