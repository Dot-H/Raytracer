using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raytracer.utils;

namespace Raytracer.lights
{
    abstract class AttenuationLight : Light
    {
        protected   Vector3 pos_;
        private     float   constant_;
        private     float   linear_;
        private     float   quadratic_;

        public AttenuationLight(NormalizedColor c, Vector3 pos, 
                                float cst, float linear, float quadratic) : base(c)
        {
            pos_ = pos;
            constant_ = cst;
            linear_ = linear;
            quadratic_ = quadratic;
        }

        protected double attenuation_get(Vector3 point)
        {
            double distance = Vector3.distance(point, pos_);
            return Math.Min(1 / (constant_ + linear_ + quadratic_ * distance * distance), 1);
        }
    }
}
