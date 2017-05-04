using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.utils
{
    class Ray
    {
        #region Attributes
        private Vector3 origin_;
        private Vector3 dir_;
        #endregion

        #region Constructor
        public Ray(Vector3 origin, Vector3 dir)
        {
            origin_ = origin;
            dir_ = dir;
        }
        #endregion

        #region Method
        /// <summary>
        /// Computes a Ray from the x and y position on the 3D projection screen and the camera
        /// </summary>
        /// <param name="x">The position of the 3D pixel along the x-axis</param>
        /// <param name="y">The position of the 3D pixel along the y-axis</param>
        /// <param name="screen">The 3D screen</param>
        /// <param name="cam">The camera representing the viewer point of view</param>
        /// <returns></returns>
        public static Ray get_ray(double x, double y, Screen screen, Camera cam)
        {
            double dx = (double)x - (double)(screen.Width) / 2.0d;
            double dy = (double)y - (double)(screen.Height) / 2.0d;

            Vector3 u = cam.U * dx;
            Vector3 v = cam.V * dy;

            Vector3 point = screen.Center + u + v;
            Vector3 dir = point - cam.Pos;
            dir.normalize();
            
            return new Ray(cam.Pos, dir);
        }
        #endregion

        #region Getters
        public Vector3 Origin
        {
            get { return origin_; }
        }

        public Vector3 Dir
        {
            get { return dir_; }
        }

        #endregion
    }
}



