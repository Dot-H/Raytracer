using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.utils
{
    class Camera
    {
        #region Attributes
        private Vector3 pos_;
        private Vector3 u_;
        private Vector3 v_;
        #endregion

        #region Constructor
        public Camera(Vector3 pos, Vector3 u, Vector3 v)
        {
            pos_ = pos;
            u_ = u;
            v_ = v;
        }
        #endregion

        #region Getters
        public Vector3 Pos
        {
            get { return pos_; }
        }

        public Vector3 U
        {
            get { return u_; }
        }

        public Vector3 V
        {
            get { return v_; }
        }

        #endregion
    }
}
