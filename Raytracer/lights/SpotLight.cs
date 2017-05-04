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
    class SpotLight : AttenuationLight
    {
        private Vector3 dir_;
        private float outer_cut_off_;
        private float cut_off_;

        public SpotLight(NormalizedColor c, Vector3 pos, Vector3 dir,
                         float cut_off = 0.984f, float outer_cut_off = 0.965f,
                         float cst = 0, float linear = 0, float quadratic = 0)
                         : base(c, pos, cst, linear, quadratic)
        {
            dir_ = dir;
            dir_.normalize();
            outer_cut_off_ = outer_cut_off;
            cut_off_ = cut_off;
        }

        public override NormalizedColor apply_lightning(NormalizedColor ambient, Shape intersectionShape,
                                                        Ray ray, Vector3 normal, Vector3 point, PerlinNoise perlin)
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

            double attenuation = attenuation_get(point);
            Vector3 yolo = (pos_ - point);
            yolo.normalize();
            double b = ((-1d * dir_ )| yolo);
            double pL_intensity = (b - outer_cut_off_) / (cut_off_ - outer_cut_off_);
            pL_intensity = Math.Max(Math.Min(1, pL_intensity), 0);
            NormalizedColor newDiffuse = material.diffuse * color_ * Math.Max((-1d * dir_) | normal, 0d) * pL_intensity * attenuation;
            NormalizedColor specular = color_ * specular_get(ray, get_direction(point), point, normal, material.shininess) * material.specular * pL_intensity *attenuation;
            NormalizedColor res = (newDiffuse + specular) * ambient * material.diffuse * attenuation * pL_intensity;
            return res;
        }

        public override Vector3 get_direction(Vector3 point)
        {
            Vector3 dir = pos_ - point;
            dir.normalize();
            return dir;
        }
    }
}
