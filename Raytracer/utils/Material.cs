using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.utils
{
    class Material
    {
        #region Attributes
        private NormalizedColor diffuse_color_;
        private NormalizedColor spec_color_;
        private float           shininess_;
        private float           reflected_coef_;//Rajouté ici, dans les getters_setters et dans le contructeur.
        #endregion

        #region Constructor
        public Material(NormalizedColor diff, NormalizedColor spec, float shin,float reflected_coef = 0.8f)
        {
            diffuse_color_ = diff;
            spec_color_ = spec;
            shininess_ = shin;
            reflected_coef_ = reflected_coef; 
        }
        #endregion

        #region Getters & Setters
        public NormalizedColor diffuse
        {
            get { return diffuse_color_; }
            set { diffuse_color_ = value; }
        }

        public NormalizedColor specular
        {
            get { return spec_color_; }
            set { spec_color_ = value; }
        }

        public float shininess
        {
            get { return shininess_; }
            set { shininess_ = value; }
        }

        public float ReflectedCoef
        {
            get { return reflected_coef_; }
            set { reflected_coef_ = value; }
        }

        #endregion
    }
}
