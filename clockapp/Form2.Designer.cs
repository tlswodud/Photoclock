namespace clockapp
{
    partial class Form2
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            panel2 = new Panel();
            maxwindow = new FontAwesome.Sharp.IconButton();
            iconButton9 = new FontAwesome.Sharp.IconButton();
            exitbotton = new FontAwesome.Sharp.IconButton();
            panel4 = new Panel();
            textBox1 = new TextBox();
            panel5 = new Panel();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            backgroundbutton = new FontAwesome.Sharp.IconButton();
            colorbutton = new FontAwesome.Sharp.IconButton();
            sizebutton = new FontAwesome.Sharp.IconButton();
            trackBar2 = new TrackBar();
            label3 = new Label();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            timebutton = new FontAwesome.Sharp.IconButton();
            timer1 = new System.Windows.Forms.Timer(components);
            panel3 = new Panel();
            optionbutton = new FontAwesome.Sharp.IconButton();
            trackbutton = new FontAwesome.Sharp.IconButton();
            btnmenu = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            color2 = new ColorDialog();
            fontDialog1 = new FontDialog();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonFace;
            panel2.Controls.Add(maxwindow);
            panel2.Controls.Add(iconButton9);
            panel2.Controls.Add(exitbotton);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(98, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 36);
            panel2.TabIndex = 1;
            panel2.MouseClick += panel2_MouseClick;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // maxwindow
            // 
            maxwindow.Dock = DockStyle.Right;
            maxwindow.FlatAppearance.BorderSize = 0;
            maxwindow.FlatStyle = FlatStyle.Flat;
            maxwindow.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            maxwindow.IconColor = Color.Black;
            maxwindow.IconFont = FontAwesome.Sharp.IconFont.Auto;
            maxwindow.IconSize = 25;
            maxwindow.Location = new Point(258, 0);
            maxwindow.Name = "maxwindow";
            maxwindow.Size = new Size(46, 36);
            maxwindow.TabIndex = 4;
            maxwindow.UseVisualStyleBackColor = true;
            maxwindow.Visible = false;
            maxwindow.MouseClick += maxwindow_MouseClick;
            // 
            // iconButton9
            // 
            iconButton9.BackColor = SystemColors.AppWorkspace;
            iconButton9.FlatAppearance.BorderSize = 0;
            iconButton9.FlatStyle = FlatStyle.Flat;
            iconButton9.Font = new Font("Lucida Sans Typewriter", 9F);
            iconButton9.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButton9.IconColor = Color.Black;
            iconButton9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton9.Location = new Point(6, 7);
            iconButton9.Name = "iconButton9";
            iconButton9.Size = new Size(95, 23);
            iconButton9.TabIndex = 3;
            iconButton9.Text = "CLOCK";
            iconButton9.UseVisualStyleBackColor = false;
            // 
            // exitbotton
            // 
            exitbotton.Dock = DockStyle.Right;
            exitbotton.FlatAppearance.BorderSize = 0;
            exitbotton.FlatStyle = FlatStyle.Flat;
            exitbotton.IconChar = FontAwesome.Sharp.IconChar.CircleLeft;
            exitbotton.IconColor = Color.Black;
            exitbotton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            exitbotton.IconSize = 25;
            exitbotton.Location = new Point(304, 0);
            exitbotton.Name = "exitbotton";
            exitbotton.Size = new Size(46, 36);
            exitbotton.TabIndex = 2;
            exitbotton.UseVisualStyleBackColor = true;
            exitbotton.MouseClick += exitbotton_MouseClick;
            // 
            // panel4
            // 
            panel4.AllowDrop = true;
            panel4.BackColor = Color.Snow;
            panel4.BackgroundImageLayout = ImageLayout.Stretch;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(textBox1);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(trackBar2);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(pictureBox2);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(98, 36);
            panel4.Name = "panel4";
            panel4.Size = new Size(350, 177);
            panel4.TabIndex = 2;
            panel4.DragDrop += panel4_DragDrop;
            panel4.DragEnter += panel4_DragEnter;
            panel4.MouseClick += panel4_MouseClick;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Lavender;
            textBox1.Location = new Point(135, 145);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // panel5
            // 
            panel5.BackColor = Color.LightGray;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(iconButton1);
            panel5.Controls.Add(backgroundbutton);
            panel5.Controls.Add(colorbutton);
            panel5.Controls.Add(sizebutton);
            panel5.Location = new Point(-1, 27);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 122);
            panel5.TabIndex = 6;
            // 
            // iconButton1
            // 
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Lucida Sans", 7F);
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            iconButton1.IconColor = Color.Black;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 25;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(-1, 87);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(112, 28);
            iconButton1.TabIndex = 9;
            iconButton1.Tag = "Fullscreen";
            iconButton1.Text = "startTime";
            iconButton1.TextAlign = ContentAlignment.MiddleLeft;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // backgroundbutton
            // 
            backgroundbutton.FlatAppearance.BorderSize = 0;
            backgroundbutton.FlatStyle = FlatStyle.Flat;
            backgroundbutton.Font = new Font("Lucida Sans", 7F);
            backgroundbutton.IconChar = FontAwesome.Sharp.IconChar.DesktopAlt;
            backgroundbutton.IconColor = Color.Black;
            backgroundbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            backgroundbutton.IconSize = 25;
            backgroundbutton.ImageAlign = ContentAlignment.MiddleLeft;
            backgroundbutton.Location = new Point(0, 59);
            backgroundbutton.Name = "backgroundbutton";
            backgroundbutton.Size = new Size(112, 28);
            backgroundbutton.TabIndex = 9;
            backgroundbutton.Tag = "Background";
            backgroundbutton.Text = "startTime";
            backgroundbutton.TextAlign = ContentAlignment.MiddleLeft;
            backgroundbutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            backgroundbutton.UseVisualStyleBackColor = true;
            backgroundbutton.Click += iconButton4_Click_1;
            // 
            // colorbutton
            // 
            colorbutton.FlatAppearance.BorderSize = 0;
            colorbutton.FlatStyle = FlatStyle.Flat;
            colorbutton.Font = new Font("Lucida Sans", 7F);
            colorbutton.IconChar = FontAwesome.Sharp.IconChar.Paintbrush;
            colorbutton.IconColor = Color.Black;
            colorbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            colorbutton.IconSize = 25;
            colorbutton.ImageAlign = ContentAlignment.MiddleLeft;
            colorbutton.Location = new Point(0, 31);
            colorbutton.Name = "colorbutton";
            colorbutton.Size = new Size(112, 28);
            colorbutton.TabIndex = 8;
            colorbutton.Tag = "Color";
            colorbutton.Text = "startTime";
            colorbutton.TextAlign = ContentAlignment.MiddleLeft;
            colorbutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            colorbutton.UseVisualStyleBackColor = true;
            colorbutton.Click += colorbutton_Click;
            // 
            // sizebutton
            // 
            sizebutton.FlatAppearance.BorderSize = 0;
            sizebutton.FlatStyle = FlatStyle.Flat;
            sizebutton.Font = new Font("Lucida Sans", 7F);
            sizebutton.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            sizebutton.IconColor = Color.Black;
            sizebutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            sizebutton.IconSize = 25;
            sizebutton.ImageAlign = ContentAlignment.MiddleLeft;
            sizebutton.Location = new Point(0, 3);
            sizebutton.Name = "sizebutton";
            sizebutton.Size = new Size(101, 28);
            sizebutton.TabIndex = 7;
            sizebutton.Tag = "Initialize";
            sizebutton.Text = "startTime";
            sizebutton.TextAlign = ContentAlignment.MiddleLeft;
            sizebutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            sizebutton.UseVisualStyleBackColor = true;
            sizebutton.Click += sizebutton_Click;
            // 
            // trackBar2
            // 
            trackBar2.AutoSize = false;
            trackBar2.BackColor = Color.LightGray;
            trackBar2.Dock = DockStyle.Right;
            trackBar2.Location = new Point(303, 0);
            trackBar2.Maximum = 100;
            trackBar2.Minimum = 30;
            trackBar2.Name = "trackBar2";
            trackBar2.Orientation = Orientation.Vertical;
            trackBar2.Size = new Size(45, 175);
            trackBar2.TabIndex = 5;
            trackBar2.Value = 30;
            trackBar2.ValueChanged += trackBar2_ValueChanged;
            trackBar2.MouseDown += trackBar2_MouseDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("경기천년제목 Bold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(68, 21);
            label3.TabIndex = 3;
            label3.Text = "Clock";
            label3.MouseClick += label3_MouseClick;
            label3.MouseDoubleClick += label3_MouseDoubleClick;
            label3.MouseDown += label3_MouseDown;
            label3.MouseMove += label3_MouseMove;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("경기천년제목V Bold", 30F, FontStyle.Bold);
            label1.Location = new Point(8, 19);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(207, 40);
            label1.TabIndex = 0;
            label1.Text = "Loading...";
            label1.MouseClick += label1_MouseClick;
            label1.MouseDown += label1_MouseDown;
            label1.MouseMove += label1_MouseMove;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(348, 175);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // timebutton
            // 
            timebutton.FlatAppearance.BorderSize = 0;
            timebutton.FlatStyle = FlatStyle.Flat;
            timebutton.Font = new Font("Lucida Sans", 7F);
            timebutton.IconChar = FontAwesome.Sharp.IconChar.Hourglass1;
            timebutton.IconColor = Color.Black;
            timebutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            timebutton.IconSize = 25;
            timebutton.Location = new Point(0, 80);
            timebutton.Name = "timebutton";
            timebutton.Size = new Size(110, 30);
            timebutton.TabIndex = 10;
            timebutton.Tag = "Stopwatch";
            timebutton.Text = "startTime";
            timebutton.TextAlign = ContentAlignment.MiddleLeft;
            timebutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            timebutton.UseVisualStyleBackColor = true;
            timebutton.Click += timebutton_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightGray;
            panel3.Controls.Add(timebutton);
            panel3.Controls.Add(optionbutton);
            panel3.Controls.Add(trackbutton);
            panel3.Controls.Add(btnmenu);
            panel3.Controls.Add(pictureBox1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(100, 213);
            panel3.TabIndex = 0;
            panel3.MouseClick += panel3_MouseClick;
            // 
            // optionbutton
            // 
            optionbutton.FlatAppearance.BorderSize = 0;
            optionbutton.FlatStyle = FlatStyle.Flat;
            optionbutton.Font = new Font("Lucida Sans", 7F);
            optionbutton.IconChar = FontAwesome.Sharp.IconChar.ListCheck;
            optionbutton.IconColor = Color.Black;
            optionbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            optionbutton.IconSize = 25;
            optionbutton.Location = new Point(0, 144);
            optionbutton.Name = "optionbutton";
            optionbutton.Size = new Size(110, 30);
            optionbutton.TabIndex = 8;
            optionbutton.Tag = "Option";
            optionbutton.Text = "startTime";
            optionbutton.TextAlign = ContentAlignment.MiddleLeft;
            optionbutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            optionbutton.UseVisualStyleBackColor = true;
            optionbutton.Click += optionbutton_Click;
            // 
            // trackbutton
            // 
            trackbutton.FlatAppearance.BorderSize = 0;
            trackbutton.FlatStyle = FlatStyle.Flat;
            trackbutton.Font = new Font("Lucida Sans", 7F);
            trackbutton.IconChar = FontAwesome.Sharp.IconChar.Egg;
            trackbutton.IconColor = Color.Black;
            trackbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            trackbutton.IconSize = 25;
            trackbutton.Location = new Point(0, 112);
            trackbutton.Name = "trackbutton";
            trackbutton.Size = new Size(110, 30);
            trackbutton.TabIndex = 7;
            trackbutton.Tag = "Opacity";
            trackbutton.Text = "startTime";
            trackbutton.TextAlign = ContentAlignment.MiddleLeft;
            trackbutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            trackbutton.UseVisualStyleBackColor = true;
            trackbutton.Click += trackbutton_Click;
            // 
            // btnmenu
            // 
            btnmenu.FlatAppearance.BorderSize = 0;
            btnmenu.FlatStyle = FlatStyle.Flat;
            btnmenu.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            btnmenu.IconColor = Color.Black;
            btnmenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnmenu.IconSize = 25;
            btnmenu.Location = new Point(0, 40);
            btnmenu.Name = "btnmenu";
            btnmenu.Size = new Size(110, 22);
            btnmenu.TabIndex = 1;
            btnmenu.Tag = "home";
            btnmenu.UseVisualStyleBackColor = true;
            btnmenu.MouseClick += btnmenu_MouseClick;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 34);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Honeydew;
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(98, 213);
            panel1.TabIndex = 0;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Gray;
            ClientSize = new Size(448, 213);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form2";
            TopMost = true;
            FormClosed += Form2_FormClosed;
            Load += Form1_Load;
            Resize += Form1_Resize;
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Panel panel4;
        private FontAwesome.Sharp.IconButton exitbotton;
        private FontAwesome.Sharp.IconButton iconButton9;
        private FontAwesome.Sharp.IconButton maxwindow;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Label label3;
        private TrackBar trackBar2;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton trackbutton;
        private FontAwesome.Sharp.IconButton btnmenu;
        private PictureBox pictureBox1;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton optionbutton;
        private Panel panel5;
        private FontAwesome.Sharp.IconButton colorbutton;
        private FontAwesome.Sharp.IconButton sizebutton;
        private FontAwesome.Sharp.IconButton backgroundbutton;
        private FontAwesome.Sharp.IconButton timebutton;
        private TextBox textBox1;
        private PictureBox pictureBox2;
        private ColorDialog color2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontDialog fontDialog1;
    }
}
