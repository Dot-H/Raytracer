using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer
{
    /// <summary>
    /// Vector class containg 3 coordinates
    /// </summary>
    /// <remarks>
    /// Allows to perform computation on vectors easily
    /// </remarks>
    class Vector3
    {
        #region Attributes
        private double x_;
        private double y_;
        private double z_;
        #endregion

        #region Constructors
        public Vector3()
        {
            x_ = 0;
            y_ = 0;
            z_ = 0;
        }

        public Vector3(double x, double y, double z)
        {
            x_ = x;
            y_ = y;
            z_ = z;
        }
        #endregion

        #region Methods
        public double norm()
        {
            return Math.Sqrt(x_ * x_ + y_ * y_ + z_ * z_);
        }

        /// <summary>
        /// Transforms the the current instance of the Vector3 to its normalized form
        /// </summary>
        public void normalize()
        {
            double norm = this.norm();
            x_ /= norm;
            y_ /= norm;
            z_ /= norm;
        }

        /// <summary>
        /// Computes the Euclidian distance between two Vector3
        /// </summary>
        /// <param name="p1">The first vector</param>
        /// <param name="p2">The second vector</param>
        /// <returns>A double representing the euclidian distance between the two vectors</returns>
        public static double distance(Vector3 p1, Vector3 p2)
        {
            double xAB = (p2.X - p1.X);
            double yAB = (p2.Y - p1.Y);
            double zAB = (p2.Z - p1.Z);
            return Math.Sqrt(xAB * xAB + yAB * yAB + zAB * zAB);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Computes the dot product of the two given Vector3
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>A double representing the dot product between the two vectors</returns>
        public static double operator |(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public static Vector3 operator *(Vector3 v, double d)
        {
            return new Vector3(v.X * d, v.Y * d, v.Z * d);
        }

        public static Vector3 operator *(double d, Vector3 v)
        {
            return new Vector3(v.X * d, v.Y * d, v.Z * d);
        }
        #endregion

        #region Getters & Setters
        public double X
        {
            get { return x_; }
            set { x_ = value; }
        }

        public double Y
        {
            get { return y_; }
            set { y_ = value; }
        }

        public double Z
        {
            get { return z_; }
            set { z_ = value; }
        }
        #endregion
    }
}
