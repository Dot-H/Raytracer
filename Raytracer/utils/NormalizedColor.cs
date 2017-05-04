using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Raytracer.utils
{
    /// <summary>
    /// Handle the color information between 0 and 1
    /// </summary>
    /// <remarks>
    /// This is useful to make computations on colors, such as adding them, multiply them,...
    /// </remarks>
    class NormalizedColor
    {
        #region Attributes
        private double r_;
        private double g_;
        private double b_;
        #endregion

        #region Constructors
        public NormalizedColor()
        {
            r_ = 0;
            g_ = 0;
            b_ = 0;
        }

        public NormalizedColor(double r, double g, double b)
        {
            r_ = r;
            g_ = g;
            b_ = b;
        }

        public NormalizedColor(Color c)
        {
            r_ = (double)c.R / 255d;
            g_ = (double)c.G / 255d;
            b_ = (double)c.B / 255d;

        }
        #endregion

        #region Method
        public Color to_color()
        {
            if (r_ < 0)
                r_ = 0;
            if (g_ < 0)
                g_ = 0;
            if (b_ < 0)
                b_ = 0;
            return Color.FromArgb((int)(255.0d * r_ > 255.0d ? 255:255.0d*r_),(int)(255.0d * g_ > 255.0d ? 255 : 255.0d * g_), (int)(255.0d * b_ > 255.0d ? 255 : 255.0d * b_));
        }
        #endregion

        #region Operators
        public static NormalizedColor operator *(NormalizedColor c1, double d)
        {
            return new NormalizedColor(c1.R * d, c1.G * d, c1.B * d);
        }

        public static NormalizedColor operator *(NormalizedColor c1, NormalizedColor c2)
        {
            double a = c1.R * c2.R;
            double b = c1.G * c2.G;
            double c = c1.B * c2.B;
            return new NormalizedColor(a,b ,c );
        }

        public static NormalizedColor operator +(NormalizedColor c1, NormalizedColor c2)
        {
            return new NormalizedColor(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
        }
        #endregion

        #region Getters
        public double R
        {
            get { return r_; }
        }

        public double G
        {
            get { return g_; }
        }

        public double B
        {
            get { return b_; }
        }
        #endregion
    }
}
