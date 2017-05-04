using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer.utils;

namespace Raytracer.shapes
{
    class PyramidFace : Shape
    {
        private Vector3 point_a_;
        private Vector3 point_b_;
        private Vector3 point_c_;
        private const double epsilon = 0.00000001;
        public PyramidFace(Material mat, Vector3 point_a, Vector3 point_b, Vector3 point_c) : base(mat)
        {
            point_a_ = point_a;
            point_b_ = point_b;
            point_c_ = point_c;
        }

        public override Vector3 intersect(Ray ray)
        {
            Vector3 AB = point_b_ - point_a_;
            Vector3 AC = point_c_ - point_a_;
            Vector3 pvec = ray.Dir * AC;
            double det = AB | pvec;
            if (det < epsilon)
                return null;
            double invDet = 1 / det;

            Vector3 tvec = ray.Origin - point_a_;
            double u = (tvec | pvec) * invDet;
            if (u < 0 || u > 1)
                return null;

            Vector3 qvec = tvec * AB;
            double v = (ray.Dir | qvec) * invDet;
            if (v < 0 || u + v > 1)
                return null;

            double t = (AC | qvec) * invDet;
            return ray.Origin + t * ray.Dir; 
        }

        public override Vector3 normal_at_point(Vector3 point)
        {
            Vector3 normal = (point_b_ - point_a_) * (point_c_ - point_a_);
            normal.normalize();
            return normal;
        }

    }
}
