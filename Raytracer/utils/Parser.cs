using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;

using Raytracer.shapes;
using Raytracer.lights;

namespace Raytracer.utils
{
    class Parser
    {
        #region Tags
        const string UNKNOWN_ID = "tag unknown id";
        const string UNKNOWN_TAG = "has unknown tag";
        #endregion

        #region Camera Parsing
        public static Camera parse_camera(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "pos", "u", "v");

            Dictionary<string, object> objects = parse_tags(node);

            foreach (XmlNode subnode in node)
                objects.Add(subnode.Attributes["id"].Value, parse_tag_vector(subnode));

            return new Camera((Vector3)objects["pos"], (Vector3)objects["u"], (Vector3)objects["v"]);
        }
        #endregion

        #region Shape Parsing
        /// <summary>
        /// Parses XML language enclosed between two 'shape id="sphere"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'shape id="sphere"' tag</param>
        /// <returns></returns>
        public static Sphere parse_sphere(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "material", "attribute", "vector");
            assert_id_exception(node, "attribute", "radius");

            Dictionary<string, object> objects = parse_tags(node);

            return new Sphere((Material)objects["mat"], (Vector3)objects["vec"], (float)objects["radius"]);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'shape id="plane"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'shape id="plane"' tag</param>
        /// <returns></returns>
        public static Plane parse_plane(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "material", "attribute");
            assert_id_exception(node, "attribute", "a", "b", "c", "d");

            Dictionary<string, object> objects = parse_tags(node);

            return new Plane((Material)objects["mat"], (float)objects["a"], (float)objects["b"], (float)objects["c"], (float)objects["d"]);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'shape id="cube"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'shape id="cube"' tag</param>
        /// <returns></returns>
        public static Cube parse_cube(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "material", "attribute", "vector");
            assert_id_exception(node, "attribute", "size");

            Dictionary<string, object> objects = parse_tags(node);

            return new Cube((Material)objects["mat"], (Vector3)objects["vec"], (float)objects["size"]);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'shape id="pyramid"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'shape id="pyramid"' tag</param>
        /// <returns></returns>
        public static Pyramid parse_pyramid(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "material", "attribute", "vector");
            assert_id_exception(node, "attribute", "base", "height");

            Dictionary<string, object> objects = parse_tags(node);

            return new Pyramid((Material)objects["mat"], (Vector3)objects["vec"], (float)objects["base"], (float)objects["height"]);
        }
        #endregion

        #region Light Parsing
        /// <summary>
        /// Parses XML language enclosed between two 'light id="ambient"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'light id="ambient"' tag</param>
        /// <returns></returns>
        public static NormalizedColor parse_ambient(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "color");
            assert_id_exception(node, "color", "diffuse");

            NormalizedColor color = null;

            foreach (XmlNode subnode in node)
            {
                if (subnode.LocalName.Equals("color"))
                    color = parse_tag_color(subnode);
                else
                    throw new ParsingException("id='" + node.Attributes["id"].Value + "' " + UNKNOWN_TAG + " '<" + subnode.LocalName + ">'");
            }
            return color;
        }

        /// <summary>
        /// Parses XML language enclosed between two 'light id="directional"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'light id="directional"' tag</param>
        /// <returns></returns>
        public static Directional parse_directional(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "color", "vector");
            assert_id_exception(node, "color", "diffuse");

            Vector3 direction = null;
            NormalizedColor color = null;

            foreach (XmlNode subnode in node)
            {
                if (subnode.LocalName.Equals("color"))
                    color = parse_tag_color(subnode);
                else
                    direction = parse_tag_vector(subnode);
            }
            return new Directional(color, direction);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'light id="point"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'light id="point"' tag</param>
        /// <returns></returns>
        public static PointLight parse_point_light(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "color", "vector", "attribute");
            assert_id_exception(node, "attribute", "constant", "linear", "quadratic");
            assert_id_exception(node, "color", "diffuse");

            Dictionary<string, object> objects = parse_tags(node);

            return new PointLight((NormalizedColor)objects["color"], (Vector3)objects["vec"],
                                  (float)objects["constant"], (float)objects["linear"], (float)objects["quadratic"]);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'light id="point"' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'light id="point"' tag</param>
        /// <returns></returns>
        public static SpotLight parse_spot_light(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "color", "vector", "attribute");
            assert_id_exception(node, "attribute",
                                "cutOff", "outerCutOff",
                                "constant", "linear", "quadratic");
            assert_id_exception(node, "color", "diffuse");

            Dictionary<string, object> objects = new Dictionary<string, object>();

            foreach (XmlNode subnode in node)
            {
                switch (subnode.LocalName)
                {
                    case "attribute":
                        objects.Add(subnode.Attributes["id"].Value, float.Parse(subnode.InnerText, CultureInfo.InvariantCulture));
                        break;
                    case "color":
                        objects.Add("color", parse_tag_color(subnode));
                        break;
                    case "vector":
                        objects.Add("vec_" + subnode.Attributes["id"].Value, parse_tag_vector(subnode));
                        break;
                }
            }
            return new SpotLight((NormalizedColor)objects["color"], (Vector3)objects["vec_pos"], (Vector3)objects["vec_direction"],
                                  (float)objects["cutOff"], (float)objects["outerCutOff"],
                                  (float)objects["constant"], (float)objects["linear"], (float)objects["quadratic"]);
        }
        #endregion

        #region Tags Parsing
        /// <summary>
        /// Parses all subtags of a given node
        /// </summary>
        /// <param name="node">The parent node</param>
        /// <returns>A dictionnary containing each parsed subtag objects</returns>
        private static Dictionary<string, Object> parse_tags(XmlNode node)
        {
            Dictionary<string, object> objects = new Dictionary<string, object>();

            foreach (XmlNode subnode in node)
            {
                switch (subnode.LocalName)
                {
                    case "attribute":
                        objects.Add(subnode.Attributes["id"].Value, float.Parse(subnode.InnerText, CultureInfo.InvariantCulture));
                        break;
                    case "material":
                        objects.Add("mat", parse_tag_material(subnode));
                        break;
                    case "vector":
                        objects.Add("vec", parse_tag_vector(subnode));
                        break;
                    case "color":
                        objects.Add("color", parse_tag_color(subnode));
                        break;
                }
            }

            return objects;
        }

        /// <summary>
        /// Parses XML language enclosed between two 'material' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'material' tag</param>
        /// <returns></returns>
        public static Material parse_tag_material(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "color", "attribute");
            assert_id_exception(node, "color", "diffuse", "specular");
            assert_id_exception(node, "attribute", "shininess");

            NormalizedColor diffuse = null;
            NormalizedColor specular = null;
            float shininess = 0.0f;

            foreach (XmlNode subnode in node.ChildNodes)
            {
                switch (subnode.LocalName)
                {
                    case "color":
                        if (subnode.Attributes["id"].Value.Equals("diffuse"))
                            diffuse = parse_tag_color(subnode);
                        else
                            specular = parse_tag_color(subnode);
                        break;
                    case "attribute":
                        shininess = float.Parse(subnode.InnerText, CultureInfo.InvariantCulture);
                        break;
                }
            }

            return new Material(diffuse, specular, shininess);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'color' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'color' tag</param>
        /// <returns></returns>
        public static NormalizedColor parse_tag_color(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "composant");
            assert_id_exception(node, "composant", "r", "g", "b");

            byte r = 0;
            byte g = 0;
            byte b = 0;

            foreach (XmlNode subnode in node.ChildNodes)
            {
                switch (subnode.Attributes["id"].Value)
                {
                    case "r":
                        byte.TryParse(subnode.InnerText, out r);
                        break;
                    case "g":
                        byte.TryParse(subnode.InnerText, out g);
                        break;
                    case "b":
                        byte.TryParse(subnode.InnerText, out b);
                        break;
                }
            }

            return new NormalizedColor(r / 255.0d, g / 255.0d, b / 255.0d);
        }

        /// <summary>
        /// Parses XML language enclosed between two 'vector' tags
        /// </summary>
        /// <param name="node">The XMLNode pointing the 'vector' tag</param>
        /// <returns></returns>
        public static Vector3 parse_tag_vector(XmlNode node)
        {
            // Checks for parsing exceptions
            assert_tag_exception(node, "composant");
            assert_id_exception(node, "composant", "x", "y", "z");

            Vector3 position = new Vector3();

            foreach (XmlNode subnode in node.ChildNodes)
            {
                switch (subnode.Attributes["id"].Value)
                {
                    case "x":
                        position.X = double.Parse(subnode.InnerText, CultureInfo.InvariantCulture);
                        break;
                    case "y":
                        position.Y = double.Parse(subnode.InnerText, CultureInfo.InvariantCulture);
                        break;
                    case "z":
                        position.Z = double.Parse(subnode.InnerText, CultureInfo.InvariantCulture);
                        break;
                }
            }

            return position;
        }
        #endregion

        #region Parsing Exceptions
        /// <summary>
        /// Throws an exception if one of the given symbol tags is not a child of the given XmlNode
        /// of if one of the given expected tag is missings
        /// </summary>
        /// <param name="node">The XmlNode that should contain the tag</param>
        /// <param name="values">All the symbols that should be child of the given XmlNode</param>
        private static void assert_tag_exception(XmlNode node, params string[] values)
        {
            foreach (XmlNode subnode in node)
            {
                if (Array.IndexOf(values, subnode.LocalName) == -1)
                    throw new ParsingException("id='" + node.Attributes["id"].Value + "'\nunknown tag '<" + subnode.LocalName + ">'");
            }

            foreach (string n in values)
            {
                if (node[n] == null)
                    throw new ParsingException("id='" + node.Attributes["id"].Value + "'\nmissing '<" + n + ">' tag");
            }
        }

        /// <summary>
        /// Throws an exception if the given tag_name does not match the given values(missing ids, unknown ids, ...)
        /// </summary>
        /// <param name="node">The XmlNode that should contain the tag</param>
        /// <param name="tag_name">The tag to check</param>
        /// <param name="values">All the symbols that should be present in the given tag ids</param>
        private static void assert_id_exception(XmlNode node, string tag_name, params string[] values)
        {
            // Raises an exception if tag_name has an unknown id field
            assert_unknown_id(node, tag_name, values);
            // Raises an exception if tag_name has a missing id field in comparison to the values array
            assert_missing_id(node, tag_name, values);
        }

        /// <summary>
        /// Throws an exception if the given sub tag has an unknown or a missing id
        /// </summary>
        /// <param name="node">The XmlNode that should contain the tag</param>
        /// <param name="values">All the symbols that should be present in the given tag ids</param>
        private static void assert_unknown_id(XmlNode node, string tag_name, params string[] values)
        {
            foreach (XmlNode subnode in node)
            {
                if (!subnode.LocalName.Equals(tag_name))
                    continue;

                if (Array.IndexOf(values, subnode.Attributes["id"].Value) == -1)
                    throw new ParsingException("id='" + node.Attributes["id"].Value + "'\n'<" + tag_name
                                               + ">' tag has unknown id '" + subnode.Attributes["id"].Value + "'");
            }
        }

        /// <summary>
        /// Throws an exception if the attributes does not have all the the given ids in values
        /// </summary>
        /// <param name="node">The XmlNode that should contain the tag</param>
        /// <param name="values">All the symbols that should be id of the given XmlNode</param>
        private static void assert_missing_id(XmlNode node, string tag_name, params string[] values)
        {
            bool found = false;
            foreach (string n in values)
            {
                foreach (XmlNode subnode in node)
                {
                    if (!subnode.LocalName.Equals(tag_name))
                        continue;

                    if (n.Equals(subnode.Attributes["id"].Value))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    throw new ParsingException("id='" + node.Attributes["id"].Value + "'\n'<" + tag_name
                                               + ">' tag has missing id(s) '");

                found = false;
            }
        }
        #endregion
    }
}
