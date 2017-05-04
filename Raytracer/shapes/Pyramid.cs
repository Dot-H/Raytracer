using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Raytracer.utils;

namespace Raytracer.shapes
{
    class Pyramid : Shape
    {
        private PyramidFace[] triangle_faces_;
        private PyramidFace inter_face_;
        private CubeFace base_face_;
        private Vector3 pos_;

        public Pyramid(Material mat, Vector3 pos, float base_size, float height) : base(mat)
        {
            pos_ = pos;
            base_face_ = new CubeFace(mat, new Vector3(pos.X + base_size / 2, pos.Y, pos.Z - base_size / 2),
                                           new Vector3(pos.X - base_size / 2, pos.Y, pos.Z - base_size / 2),
                                           new Vector3(pos.X + base_size / 2, pos.Y, pos.Z + base_size / 2));



            // Contains all the triangles faces belonging to the pyramid
            triangle_faces_ = new PyramidFace[4];
            triangle_faces_[0] = new PyramidFace(mat, new Vector3(pos.X + base_size / 2, pos.Y, pos.Z - base_size / 2), //Front
                                                      new Vector3(pos.X - base_size / 2, pos.Y, pos.Z - base_size / 2),
                                                      new Vector3(pos.X, pos.Y + base_size / 2, pos.Z)); //Sommet
            triangle_faces_[1] = new PyramidFace(mat, new Vector3(pos.X - base_size / 2, pos.Y, pos.Z - base_size / 2), //left
                                                      new Vector3(pos.X - base_size / 2, pos.Y, pos.Z + base_size / 2),
                                                      new Vector3(pos.X, pos.Y + base_size / 2, pos.Z)); //Sommet
            triangle_faces_[2] = new PyramidFace(mat, new Vector3(pos.X - base_size / 2, pos.Y, pos.Z + base_size / 2), //behind
                                                      new Vector3(pos.X + base_size / 2, pos.Y, pos.Z + base_size / 2),
                                                      new Vector3(pos.X, pos.Y + base_size / 2, pos.Z)); //Sommet
            triangle_faces_[3] = new PyramidFace(mat, new Vector3(pos.X + base_size / 2, pos.Y, pos.Z + base_size / 2), //right
                                                      new Vector3(pos.X + base_size / 2, pos.Y, pos.Z - base_size / 2),
                                                      new Vector3(pos.X, pos.Y + base_size / 2, pos.Z)); //Sommet

        }

        public override Vector3 intersect(Ray ray)
        {
            Vector3 res = null;
            double distance = double.MaxValue;
            foreach (PyramidFace face in triangle_faces_)
            {
                Vector3 intersection_point = face.intersect(ray);
                if (intersection_point != null)
                {
                    double distance_ = Vector3.distance(intersection_point, ray.Origin);
                    if (distance_ < distance)
                    {
                        distance = distance_;
                        inter_face_ = face;
                        res = intersection_point;
                    }
                }
            }

            Vector3 intersection_pointBis = base_face_.intersect(ray);
            if (intersection_pointBis != null)
            {
                double distanceTer = Vector3.distance(intersection_pointBis, ray.Origin);
                if (distanceTer < distance)
                    return intersection_pointBis;
            }
            return res;
        }

        public override Vector3 normal_at_point(Vector3 point)
        {
            if (inter_face_ == null)
                return base_face_.normal_at_point(point);
            return inter_face_.normal_at_point(point);
        }
    }
}
