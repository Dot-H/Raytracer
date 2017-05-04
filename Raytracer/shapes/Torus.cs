using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer.utils;

namespace Raytracer.shapes
{
    class Torus : Shape
    {
        public Torus(Material mat) : base (mat)
        {

        }

        public override Vector3 intersect(Ray ray)
        {
            throw new NotImplementedException();
        }

        public override Vector3 normal_at_point(Vector3 point)
        {
            throw new NotImplementedException();
        }


    }
}
