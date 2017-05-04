using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.utils
{
    class Polynomial
    {
        #region Attributes
        private double[] coefs_;
        private uint n_;
        #endregion

        #region Constructor
        public Polynomial(double[] coefs, uint n)
        {
            n_= n;
            coefs_ = coefs;
        }
        #endregion  

        #region Methods
        /// <summary>
        /// Solves a second degree polynomial equation
        /// </summary>
        /// <param name="squares">The array representing the different coefficients</param>
        /// <returns>1 if the solution is unique, 2 if the equation has two roots, 0 otherwise</returns>
        public uint resolve_quadratic(ref double[] squares)
        {
            double d = coefs_[1] * coefs_[1] - 4 * coefs_[2] * coefs_[0];
            if (d > 0)
            {
                squares[0] = (-1d * coefs_[1] - Math.Sqrt(d)) / (2d * coefs_[0]);
                squares[1] = (-1d * coefs_[1] + Math.Sqrt(d)) / (2d * coefs_[0]);
                return 2;
            }

            else if (d == 0)
            {
                squares[0] = -1d * coefs_[1] / (2d * coefs_[0]); 
                return 1;
            } 
            else
                return 0;
        }

        /// <summary>
        /// Solves a third degree polynomial equation
        /// </summary>
        /// <param name="squares">The array representing the different coefficients</param>
        /// <returns>1 if the solution is unique, 3 if the equation has three roots</returns>
        public uint resolve_cubic(ref double[] squares)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
