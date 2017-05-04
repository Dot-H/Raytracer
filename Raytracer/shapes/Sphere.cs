using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Raytracer.utils;

namespace Raytracer.shapes
{
    class Sphere : Shape
    {
        #region Attributes
        private float radius_;
        private Vector3 pos_;
        #endregion

        #region Constructor
        public Sphere(Material mat, Vector3 position, float radius) : base(mat)
        {
            pos_ = position;
            radius_ = radius;
        }
        #endregion

        #region Methods
        public override Vector3 intersect(Ray ray)
        {
            double a = ray.Dir | ray.Dir;
            double b = 2d * (ray.Dir | (ray.Origin - pos_));
            double c = ((ray.Origin - pos_) | (ray.Origin - pos_)) - (radius_ * radius_);
            double[] coef = new double[] { a, b, c};
            Polynomial t_eq = new Polynomial(coef, 2);
            double[] res = new double[2];
            uint nbSol = t_eq.resolve_quadratic(ref res);
          
            if (nbSol == 1 && res[0] >= 0)
                return ray.Origin + ray.Dir * res[0];
            else if (nbSol == 2)
            {
                double min = Math.Min(res[0], res[1]);
                if (min < 0)
                {
                    Double max = Math.Max(res[0], res[1]);
                    if (max < 0)
                        return null;
                    return ray.Origin + ray.Dir * max;
                }
                return ray.Origin + ray.Dir * min;
            }
            return null;
        }

        public override Vector3 normal_at_point(Vector3 point)
        {
            Vector3 normal = point - pos_;
            normal.normalize();
            return normal;
        }

    
        #endregion
    }
}
