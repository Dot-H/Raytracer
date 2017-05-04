using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Raytracer.utils;

namespace Raytracer.shapes
{
    abstract class Shape
    {
        #region Attribute
        private Material mat_;
        protected bool isPerlin = false;
        protected bool perlin_marbre = false;
        protected bool perlin_bois = false;
        protected bool perlin_classic = false;
        protected Color color1 = System.Drawing.Color.White;
        protected Color color2 = System.Drawing.Color.Gray;
        protected Color color3 = System.Drawing.Color.Black;
     

        #endregion

        #region Constructor
        public Shape(Material mat)
        {
            mat_ = mat;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the intersection point as a Vector3 relative to a given ray.
        /// </summary>
        /// <param name="ray">The 3D ray intersecting or not the shape instance</param>
        /// <returns>A Vector3 representing the intersection point. NULL otherwise</returns>
        public abstract Vector3 intersect(Ray ray);

        /// <summary>
        /// Computes the normal to the surface at a given 3D point
        /// </summary>
        /// <param name="point">The Vector3 point on the shape surface</param>
        /// <returns>The normalized Vector3 representing the normal</returns>
        public abstract Vector3 normal_at_point(Vector3 point);
        #endregion

        #region Getters
        public Material Mat
        {
            get { return mat_; }
        }

        public bool IsPerlin
        {
            get
            {
                return isPerlin;
            }

            set
            {
                isPerlin = value;
            }
        }

        public bool Perlin_marbre
        {
            get { return perlin_marbre; }
            set { perlin_marbre = value; }
        }

        public bool Perlin_bois
        {
            get { return perlin_bois; }
            set { perlin_bois = value; }
        }

        public bool Perlin_classic
        {
            get { return perlin_classic; }
            set { perlin_classic = value; }
        }

        public Color Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        public Color Color2
        {
            get { return color2; }
            set { color2 = value; }
        }

        public Color Color3
        {
            get { return color3; }
            set { color3 = value; }
        }
        #endregion
    }
}
