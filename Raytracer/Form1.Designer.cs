namespace Raytracer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChosenShape = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txt_Width = new System.Windows.Forms.Label();
            this.txt_Height = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.colorDialog3 = new System.Windows.Forms.ColorDialog();
            this.Color1 = new System.Windows.Forms.Button();
            this.Color2 = new System.Windows.Forms.Button();
            this.Color3 = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aliasingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.shapeName = new System.Windows.Forms.Label();
            this.labelPerlin = new System.Windows.Forms.Label();
            this.buttonPerlin = new System.Windows.Forms.Button();
            this.labelMarbre = new System.Windows.Forms.Label();
            this.buttonMarbre = new System.Windows.Forms.Button();
            this.buttonClassic = new System.Windows.Forms.Button();
            this.labelClassic = new System.Windows.Forms.Label();
            this.buttonBois = new System.Windows.Forms.Button();
            this.labelBois = new System.Windows.Forms.Label();
            this.SliderReflected = new System.Windows.Forms.TrackBar();
            this.labelReflected = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderReflected)).BeginInit();
            this.SuspendLayout();
            // 
            // ChosenShape
            // 
            this.ChosenShape.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ChosenShape.Name = "ChosenShape";
            this.ChosenShape.Size = new System.Drawing.Size(73, 29);
            this.ChosenShape.Text = "Shape";
            this.ChosenShape.Click += new System.EventHandler(this.ChosenShape_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(374, 5);
            this.textBox1.MaxLength = 4;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "500";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(492, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(56, 26);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "500";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txt_Width
            // 
            this.txt_Width.AutoSize = true;
            this.txt_Width.BackColor = System.Drawing.Color.SteelBlue;
            this.txt_Width.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txt_Width.Location = new System.Drawing.Point(440, 8);
            this.txt_Width.Name = "txt_Width";
            this.txt_Width.Size = new System.Drawing.Size(46, 20);
            this.txt_Width.TabIndex = 3;
            this.txt_Width.Text = "width";
            this.txt_Width.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt_Height
            // 
            this.txt_Height.AutoSize = true;
            this.txt_Height.BackColor = System.Drawing.Color.SteelBlue;
            this.txt_Height.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txt_Height.Location = new System.Drawing.Point(554, 8);
            this.txt_Height.Name = "txt_Height";
            this.txt_Height.Size = new System.Drawing.Size(53, 20);
            this.txt_Height.TabIndex = 4;
            this.txt_Height.Text = "height";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 104);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(761, 520);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 7;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(644, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(426, 29);
            this.progressBar.TabIndex = 8;
            this.progressBar.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // Color1
            // 
            this.Color1.Location = new System.Drawing.Point(123, 38);
            this.Color1.Name = "Color1";
            this.Color1.Size = new System.Drawing.Size(94, 35);
            this.Color1.TabIndex = 9;
            this.Color1.Text = "Color1";
            this.Color1.UseVisualStyleBackColor = true;
            this.Color1.Click += new System.EventHandler(this.Color1_Click);
            // 
            // Color2
            // 
            this.Color2.Location = new System.Drawing.Point(296, 40);
            this.Color2.Name = "Color2";
            this.Color2.Size = new System.Drawing.Size(94, 34);
            this.Color2.TabIndex = 10;
            this.Color2.Text = "Color2";
            this.Color2.UseVisualStyleBackColor = true;
            this.Color2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Color3
            // 
            this.Color3.Location = new System.Drawing.Point(477, 40);
            this.Color3.Name = "Color3";
            this.Color3.Size = new System.Drawing.Size(94, 34);
            this.Color3.TabIndex = 11;
            this.Color3.Text = "Color3";
            this.Color3.UseVisualStyleBackColor = true;
            this.Color3.Click += new System.EventHandler(this.button3_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSceneToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loadSceneToolStripMenuItem
            // 
            this.loadSceneToolStripMenuItem.Name = "loadSceneToolStripMenuItem";
            this.loadSceneToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.loadSceneToolStripMenuItem.Text = "Load Scene";
            this.loadSceneToolStripMenuItem.Click += new System.EventHandler(this.loadSceneToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aliasingToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(88, 29);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // aliasingToolStripMenuItem
            // 
            this.aliasingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.aliasingToolStripMenuItem.Name = "aliasingToolStripMenuItem";
            this.aliasingToolStripMenuItem.Size = new System.Drawing.Size(159, 30);
            this.aliasingToolStripMenuItem.Text = "Aliasing";
            this.aliasingToolStripMenuItem.Click += new System.EventHandler(this.aliasingToolStripMenuItem_Click);
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(123, 30);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(123, 30);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
            // 
            // renderToolStripMenuItem
            // 
            this.renderToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.renderToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.renderToolStripMenuItem.Name = "renderToolStripMenuItem";
            this.renderToolStripMenuItem.Size = new System.Drawing.Size(79, 29);
            this.renderToolStripMenuItem.Text = "Render";
            this.renderToolStripMenuItem.Click += new System.EventHandler(this.renderToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.renderToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.ChosenShape});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1104, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // shapeName
            // 
            this.shapeName.AutoSize = true;
            this.shapeName.Location = new System.Drawing.Point(804, 104);
            this.shapeName.Name = "shapeName";
            this.shapeName.Size = new System.Drawing.Size(129, 20);
            this.shapeName.TabIndex = 12;
            this.shapeName.Text = "Choose a shape.";
            this.shapeName.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // labelPerlin
            // 
            this.labelPerlin.AutoSize = true;
            this.labelPerlin.Location = new System.Drawing.Point(804, 156);
            this.labelPerlin.Name = "labelPerlin";
            this.labelPerlin.Size = new System.Drawing.Size(69, 20);
            this.labelPerlin.TabIndex = 13;
            this.labelPerlin.Text = "Is Perlin:";
            // 
            // buttonPerlin
            // 
            this.buttonPerlin.Location = new System.Drawing.Point(892, 151);
            this.buttonPerlin.Name = "buttonPerlin";
            this.buttonPerlin.Size = new System.Drawing.Size(75, 31);
            this.buttonPerlin.TabIndex = 14;
            this.buttonPerlin.Text = "False";
            this.buttonPerlin.UseVisualStyleBackColor = true;
            this.buttonPerlin.Click += new System.EventHandler(this.buttonPerlin_Click);
            // 
            // labelMarbre
            // 
            this.labelMarbre.AutoSize = true;
            this.labelMarbre.Location = new System.Drawing.Point(804, 230);
            this.labelMarbre.Name = "labelMarbre";
            this.labelMarbre.Size = new System.Drawing.Size(80, 20);
            this.labelMarbre.TabIndex = 15;
            this.labelMarbre.Text = "Is marbre:";
            // 
            // buttonMarbre
            // 
            this.buttonMarbre.Location = new System.Drawing.Point(890, 225);
            this.buttonMarbre.Name = "buttonMarbre";
            this.buttonMarbre.Size = new System.Drawing.Size(75, 31);
            this.buttonMarbre.TabIndex = 16;
            this.buttonMarbre.Text = "False";
            this.buttonMarbre.UseVisualStyleBackColor = true;
            this.buttonMarbre.Click += new System.EventHandler(this.buttonMarbre_Click);
            // 
            // buttonClassic
            // 
            this.buttonClassic.Location = new System.Drawing.Point(892, 188);
            this.buttonClassic.Name = "buttonClassic";
            this.buttonClassic.Size = new System.Drawing.Size(75, 31);
            this.buttonClassic.TabIndex = 17;
            this.buttonClassic.Text = "False";
            this.buttonClassic.UseVisualStyleBackColor = true;
            this.buttonClassic.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelClassic
            // 
            this.labelClassic.AutoSize = true;
            this.labelClassic.Location = new System.Drawing.Point(804, 193);
            this.labelClassic.Name = "labelClassic";
            this.labelClassic.Size = new System.Drawing.Size(77, 20);
            this.labelClassic.TabIndex = 18;
            this.labelClassic.Text = "Is classic:";
            this.labelClassic.Click += new System.EventHandler(this.labelClassic_Click);
            // 
            // buttonBois
            // 
            this.buttonBois.Location = new System.Drawing.Point(890, 262);
            this.buttonBois.Name = "buttonBois";
            this.buttonBois.Size = new System.Drawing.Size(75, 31);
            this.buttonBois.TabIndex = 19;
            this.buttonBois.Text = "False";
            this.buttonBois.UseVisualStyleBackColor = true;
            this.buttonBois.Click += new System.EventHandler(this.buttonBois_Click);
            // 
            // labelBois
            // 
            this.labelBois.AutoSize = true;
            this.labelBois.Location = new System.Drawing.Point(805, 267);
            this.labelBois.Name = "labelBois";
            this.labelBois.Size = new System.Drawing.Size(68, 20);
            this.labelBois.TabIndex = 20;
            this.labelBois.Text = "Is wood:";
            // 
            // SliderReflected
            // 
            this.SliderReflected.LargeChange = 1;
            this.SliderReflected.Location = new System.Drawing.Point(794, 348);
            this.SliderReflected.Maximum = 100;
            this.SliderReflected.Name = "SliderReflected";
            this.SliderReflected.Size = new System.Drawing.Size(276, 69);
            this.SliderReflected.TabIndex = 21;
            this.SliderReflected.Scroll += new System.EventHandler(this.SliderReflected_Scroll);
            // 
            // labelReflected
            // 
            this.labelReflected.AutoSize = true;
            this.labelReflected.Location = new System.Drawing.Point(804, 316);
            this.labelReflected.Name = "labelReflected";
            this.labelReflected.Size = new System.Drawing.Size(121, 20);
            this.labelReflected.TabIndex = 22;
            this.labelReflected.Text = "Reflected coef: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1104, 942);
            this.Controls.Add(this.labelReflected);
            this.Controls.Add(this.SliderReflected);
            this.Controls.Add(this.labelBois);
            this.Controls.Add(this.buttonBois);
            this.Controls.Add(this.labelClassic);
            this.Controls.Add(this.buttonClassic);
            this.Controls.Add(this.buttonMarbre);
            this.Controls.Add(this.labelMarbre);
            this.Controls.Add(this.buttonPerlin);
            this.Controls.Add(this.labelPerlin);
            this.Controls.Add(this.shapeName);
            this.Controls.Add(this.Color3);
            this.Controls.Add(this.Color2);
            this.Controls.Add(this.Color1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.txt_Height);
            this.Controls.Add(this.txt_Width);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "MyRaytracer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderReflected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem ChosenShape;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label txt_Width;
        private System.Windows.Forms.Label txt_Height;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.ColorDialog colorDialog3;
        private System.Windows.Forms.Button Color1;
        private System.Windows.Forms.Button Color2;
        private System.Windows.Forms.Button Color3;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aliasingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label shapeName;
        private System.Windows.Forms.Label labelPerlin;
        private System.Windows.Forms.Button buttonPerlin;
        private System.Windows.Forms.Label labelMarbre;
        private System.Windows.Forms.Button buttonMarbre;
        private System.Windows.Forms.Button buttonClassic;
        private System.Windows.Forms.Label labelClassic;
        private System.Windows.Forms.Button buttonBois;
        private System.Windows.Forms.Label labelBois;
        private System.Windows.Forms.TrackBar SliderReflected;
        private System.Windows.Forms.Label labelReflected;
    }
}

