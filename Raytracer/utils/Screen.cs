using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.utils
{
    class Screen
    {
        private const double PI_4 = 0.7853981633d;

        #region Attributes
        private int width_;
        private int height_;
        private Vector3 center_;
        #endregion

        #region Constructor
        public Screen(int width, int height)
        {
            width_ = width;
            height_ = height;
        }
        #endregion

        #region Method
        /// <summary>
        /// Centers the current screen instance according to a given camera
        /// </summary>
        /// <param name="cam">The camera used as a "point of reference"</param>
        public void set_center(Camera cam)
        {
            Vector3 u = cam.U;
            Vector3 v = cam.V;

            u.normalize();
            v.normalize();

            Vector3 w = u * v;
            double dist = (width_ / 2.0d) / (Math.Tan(PI_4 / 2.0d));
            w = w * dist;

            center_ = cam.Pos + w;
        }
        #endregion

        #region Getters & Setters
        public int Width
        {
            get { return width_; }
            set { width_ = value; }
        }

        public int Height
        {
            get { return height_; }
            set { height_ = value; }
        }

        public Vector3 Center
        {
            get { return center_; }
        }
        #endregion
    }
}
