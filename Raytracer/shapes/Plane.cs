using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer.utils;

namespace Raytracer.shapes
{
    class Plane : Shape
    {
        private const double EPSILON = 0.00001;
        private double a_;
        private double b_;
        private double c_;
        private double d_;


        private Vector3 point_a_;
        private Vector3 point_b_;
        private Vector3 point_c_;

        private Vector3 normal_;

        public Plane(Material mat, double a, double b,
                     double c, double d) : base(mat)
        {
            a_ = a;
            b_ = b;
            c_ = c;
            d_ = d;

            normal_ = new Vector3(a, b, c);
            normal_.normalize();
            //
            point_a_ = new Vector3(0, -2, 0);
            point_b_ = new Vector3(5, -2, 0);
            point_c_ = new Vector3(0, 0, 5);
        }
        /*
        public override Vector3 intersect(Ray ray)
        {
            double num = (normal_ | ray.Origin) + d_;
            double den = normal_ | ray.Dir;

            if (Math.Abs(den) <= EPSILON)
                return null;

            double t = -(num / den);
            //double t = (ray.Origin | normal_) / den;

            if (t >= 0)
                return ray.Origin + ray.Dir * t;
            else
                return null;

            /*if (abs(denom) > 0.0001f) // your favorite epsilon
            {
                float t = (center - ray.origin).dot(normal) / denom;
                if (t >= 0) return true; // you might want to allow an epsilon here too
            }
            return false;
        }

        public override Vector3 normal_at_point(Vector3 point)
        {
            return normal_;
        }*/

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


