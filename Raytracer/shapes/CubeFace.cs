using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer.utils;

namespace Raytracer.shapes
{
    class CubeFace : Shape
    {
        private Vector3 point_a_;
        private Vector3 point_b_;
        private Vector3 point_c_;

        public CubeFace(Material mat ,Vector3 point_a, Vector3 point_b, Vector3 point_c) : base(mat)
        {
            point_a_ = point_a;
            point_b_ = point_b;
            point_c_ = point_c;
        }

        public override Vector3 intersect(Ray ray)
        {
            Vector3 u = point_b_ - point_a_;
            Vector3 v = point_c_ - point_a_;
            Vector3 w = ray.Origin - point_a_;

            double r = ((w * v) | ray.Dir) / ((u * v) | ray.Dir);
            if (r <= 0 || r >= 1)
                return null;
            double s = ((u * w) | ray.Dir) / ((u * v) | ray.Dir);
            if (s <= 0 || s >= 1)
                return null;
            double t = -((u * v) | w) / ((u * v) | ray.Dir);
            return ray.Origin + t * ray.Dir;
        }


        public override Vector3 normal_at_point(Vector3 point)
        {
            //Vector3 normal = point_a_ * point_b_;
            Vector3 normal = (point_b_ - point_a_) * (point_c_ - point_a_);
            normal.normalize();
            return normal;
        }
        

        private void swap(ref double a, ref double b)
        {
            double inter = a;
            a = b;
            b = inter;
        }
    }
}
