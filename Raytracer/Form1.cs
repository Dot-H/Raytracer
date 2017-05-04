using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Globalization;

using Raytracer;
using Raytracer.shapes;
using Raytracer.lights;
using Raytracer.utils;

namespace Raytracer
{
    public partial class Form1 : Form
    {
        private Raytracer raytracer_;
        private List<Shape> nodes_;
        private List<Light> lights_;
        private NormalizedColor ambient_;
        private Camera camera_;
        private utils.Screen screen_;
        private Shape lookingShape;
        private bool alreadyClicked = false;

        public Form1()
        {
            InitializeComponent();

            raytracer_ = Raytracer.Instance;
            nodes_ = new List<Shape>();
            lights_ = new List<Light>();
            ambient_ = null;

            camera_ = new Camera(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0));
            screen_ = new utils.Screen(500, 500);

            progressBar.Maximum = Convert.ToInt32(textBox2.Text);

            Color1.Visible = false;
            Color2.Visible = false;
            Color3.Visible = false;

            labelPerlin.Visible = false;
            labelMarbre.Visible = false;
            labelBois.Visible = false;
            labelClassic.Visible = false;
            labelReflected.Visible = false;

            buttonPerlin.Visible = false;
            buttonMarbre.Visible = false;
            buttonBois.Visible = false;
            buttonClassic.Visible = false;
            SliderReflected.Visible = false;
        }

        #region personal methods
        /// <summary>
        /// Opens a dialog to load an XML scene file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog.InitialDirectory + openFileDialog.FileName;
                load_scene(path);
                alreadyClicked = false;
                ChosenShape.DropDownItems.Clear();

                for (int i = 0; i < nodes_.Count; i++)
                {
                    ToolStripMenuItem shape = new ToolStripMenuItem();
                    shape.ForeColor = System.Drawing.SystemColors.ControlLight;
                    shape.Name = "" + i;
                    shape.Size = new System.Drawing.Size(73, 29);
                    shape.Text = setName(nodes_[i].GetType().ToString());
                    shape.Click += new System.EventHandler(shape_Click);
                    ChosenShape.DropDownItems.AddRange(new ToolStripItem[] { shape });
                }

                lookingShape = null;
                changeShape();
            }
        }

        /// <summary>
        /// Renders the loaded scene into a new image of size width * height
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reinit the progress bar
            progressBar.Value = 0;

            if (screen_.Width == 0 || screen_.Height == 0)
            {
                MessageBox.Show("One of the image composant is null.");
                return;
            }
            else if (nodes_.Count == 0)
            {
                MessageBox.Show("There is no loaded scene or the scene is empty.");
                return;
            }
            else if (ambient_ == null)
            {
                MessageBox.Show("Impossible to render the scene : no ambient light found.");
                return;
            }

            // Reload the screen center
            screen_.set_center(camera_);

            // Clear the previous picture
            pictureBox.Image = null;
            pictureBox.Image = raytracer_.render_image(camera_, screen_, nodes_, lights_, ambient_, progressBar);
        }

        /// <summary>
        /// Saves the rendered image to the asset folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog.OpenFile();
                raytracer_.Img.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                screen_.Width = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Please provide number only.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                screen_.Height = Convert.ToInt32(textBox2.Text);
                progressBar.Maximum = screen_.Height;
            }
            catch
            {
                MessageBox.Show("Please provide number only");
            }
        }

        /// <summary>
        /// Loads the XML file located at "path" inside the node lists
        /// </summary>
        /// <param name="path">The path where the XML file is located</param>
        private void load_scene(string path)
        {
            // Clean previous loading
            nodes_.Clear();
            lights_.Clear();
            ambient_ = new NormalizedColor();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList nl = doc.SelectNodes("root");
            XmlNode root = nl[0];

            try
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.LocalName.Equals("camera"))
                        camera_ = Parser.parse_camera(node);
                    else if (node.LocalName.Equals("shape"))
                    {
                        switch (node.Attributes["id"].Value)
                        {
                            case "sphere":
                                nodes_.Add(Parser.parse_sphere(node));
                                break;
                            case "plane":
                                nodes_.Add(Parser.parse_plane(node));
                                break;
                            case "cube":
                                nodes_.Add(Parser.parse_cube(node));
                                break;
                            case "pyramid":
                                nodes_.Add(Parser.parse_pyramid(node));
                                break;
                            default:
                                throw new ParsingException("'<shape>' tag unknown id");
                        }
                    }
                    else if (node.LocalName.Equals("light"))
                    {
                        switch (node.Attributes["id"].Value)
                        {
                            case "ambient":
                                ambient_ = Parser.parse_ambient(node);
                                break;
                            case "directional":
                                lights_.Add(Parser.parse_directional(node));
                                break;
                            case "point":
                                lights_.Add(Parser.parse_point_light(node));
                                break;
                            case "spot":
                                lights_.Add(Parser.parse_spot_light(node));
                                break;
                            default:
                                throw new ParsingException("'<light>' tag unknown id");
                        }
                    }
                    else
                        throw new ParsingException("'<root>' has unknown tag '<" + node.LocalName + ">'");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void changeShape()
        {
            if (lookingShape == null)
            {
                resetAllButtons();
            }
            else if (lookingShape.IsPerlin)
            {
                Color1.BackColor = lookingShape.Color1;
                Color2.BackColor = lookingShape.Color2;
                if (!lookingShape.Perlin_classic)
                    Color3.Visible = false;
                Color3.BackColor = lookingShape.Color3;

                labelPerlin.Visible = true;
                labelMarbre.Visible = true;
                labelBois.Visible = true;
                labelClassic.Visible = true;
                labelReflected.Visible = true;

                buttonPerlin.Visible = true;
                buttonMarbre.Visible = true;
                buttonBois.Visible = true;
                buttonClassic.Visible = true;
                SliderReflected.Visible = true;
            }
            else
            {
                Color1.Visible = false;
                Color2.Visible = false;
                Color3.Visible = false;

                labelPerlin.Visible = true;
                labelMarbre.Visible = false;
                labelBois.Visible = false;
                labelClassic.Visible = false;
                labelReflected.Visible = true;

                buttonPerlin.Visible = true;
                buttonMarbre.Visible = false;
                buttonBois.Visible = false;
                buttonClassic.Visible = false;
                SliderReflected.Visible = true;
            }
            if(lookingShape != null)
            actualizePerlinButtons();
        }

        private void resetAllButtons()
        {
            Color1.Visible = false;
            Color2.Visible = false;
            Color3.Visible = false;

            labelPerlin.Visible = false;
            labelMarbre.Visible = false;
            labelBois.Visible = false;
            labelClassic.Visible = false;
            labelReflected.Visible = false;
            labelReflected.Text = "Reflected coef: " + '0';

            buttonPerlin.Visible = false;
            buttonPerlin.Text = "False";
            buttonMarbre.Visible = false;
            buttonPerlin.Text = "False";
            buttonBois.Visible = false;
            buttonBois.Text = "False";
            buttonClassic.Visible = false;
            buttonClassic.Text = "False";

            SliderReflected.Visible = false;
            SliderReflected.Value = 0;

            shapeName.Text = "Choose a Shape";
        }

        private void actualizePerlinButtons()
        {
            buttonPerlin.Text = "" + lookingShape.IsPerlin;
            buttonBois.Text = "" + lookingShape.Perlin_bois;
            buttonClassic.Text = "" + lookingShape.Perlin_classic;
            buttonMarbre.Text = "" + lookingShape.Perlin_marbre;

            labelReflected.Text = "Reflected coef: " + lookingShape.Mat.ReflectedCoef;
            SliderReflected.Value = (int)(lookingShape.Mat.ReflectedCoef * 100);
        }

        private void setColorButtons()
        {
            if (lookingShape.Perlin_bois || lookingShape.Perlin_marbre)
            {
                Color1.Visible = true;
                Color2.Visible = true;
                Color3.Visible = false;
            }
            else
            {
                Color1.Visible = true;
                Color2.Visible = true;
                Color3.Visible = true;
            }
        }

        private string setName(string type)
        {
            string name = "";
            int i = type.Length - 1;
            while (type[i] != '.')
            {
                name = type[i] + name;
                i--;
            }
            return name;
        }

        #endregion

        #region Event Click
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void aliasingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            raytracer_.Aliasing = true;
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            raytracer_.Aliasing = false;
        }

        private void perlinToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void boisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lookingShape != null)
            {
                lookingShape.IsPerlin = true;
                lookingShape.Perlin_marbre = false;
                lookingShape.Perlin_bois = true;
                lookingShape.Perlin_classic = false;
            }
            setColorButtons();
        }

        private void Color1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color1.BackColor = colorDialog1.Color;
                lookingShape.Color1 = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color2.BackColor = colorDialog1.Color;
                lookingShape.Color2 = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color3.BackColor = colorDialog1.Color;
                lookingShape.Color3 = colorDialog1.Color;
            }

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ChosenShape_Click(object sender, EventArgs e)
        {
            if (!alreadyClicked)
            {

                alreadyClicked = true;
            }
        }

        private void shape_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem shapeControl = (ToolStripMenuItem)sender;
            int i = (int)shapeControl.Name[0] - 48;
            lookingShape = nodes_[i];
            shapeName.Text = "Shape: " + setName(lookingShape.GetType().ToString());
            changeShape();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonPerlin_Click(object sender, EventArgs e)
        {
            lookingShape.IsPerlin = !lookingShape.IsPerlin;
            buttonPerlin.Text = "" + lookingShape.IsPerlin;
            if (!lookingShape.IsPerlin)
            {
                labelPerlin.Visible = true;
                labelMarbre.Visible = false;
                labelBois.Visible = false;
                labelClassic.Visible = false;

                buttonPerlin.Visible = true;
                buttonMarbre.Visible = false;
                buttonBois.Visible = false;
                buttonClassic.Visible = false;
            }
            actualizePerlinButtons();
            changeShape();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lookingShape.Perlin_classic = !lookingShape.Perlin_classic;

            if (lookingShape.Perlin_classic)
            {
                lookingShape.Perlin_marbre = false;
                lookingShape.Perlin_bois = false;
                lookingShape.Perlin_classic = true;
            }
            setColorButtons();
            actualizePerlinButtons();
        }

        private void buttonMarbre_Click(object sender, EventArgs e)
        {
            lookingShape.Perlin_marbre = !lookingShape.Perlin_marbre;

            if (lookingShape.Perlin_marbre)
            {
                lookingShape.Perlin_marbre = true;
                lookingShape.Perlin_bois = false;
                lookingShape.Perlin_classic = false;
            }
            setColorButtons();
            actualizePerlinButtons();
        }

        private void buttonBois_Click(object sender, EventArgs e)
        {
            lookingShape.Perlin_bois = !lookingShape.Perlin_bois;

            if (lookingShape.Perlin_bois)
            {
                lookingShape.Perlin_marbre = false;
                lookingShape.Perlin_bois = true;
                lookingShape.Perlin_classic = false;
            }
            setColorButtons();
            actualizePerlinButtons();
        }

        #endregion

        private void labelClassic_Click(object sender, EventArgs e)
        {

        }

        private void SliderReflected_Scroll(object sender, EventArgs e)
        {
            lookingShape.Mat.ReflectedCoef = (float)SliderReflected.Value / 100F;
            labelReflected.Text = "Reflected coef: " + lookingShape.Mat.ReflectedCoef;
        }
    }
}
