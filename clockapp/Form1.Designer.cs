namespace clockapp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel2 = new Panel();
            iconButton9 = new FontAwesome.Sharp.IconButton();
            exitbotton = new FontAwesome.Sharp.IconButton();
            panel4 = new Panel();
            panel6 = new Panel();
            secondDash = new FontAwesome.Sharp.IconButton();
            secondExit = new FontAwesome.Sharp.IconButton();
            trackBar2 = new TrackBar();
            panel5 = new Panel();
            BGButton1 = new FontAwesome.Sharp.IconButton();
            saveButton = new FontAwesome.Sharp.IconButton();
            timebutton = new FontAwesome.Sharp.IconButton();
            colorbutton = new FontAwesome.Sharp.IconButton();
            sizebutton = new FontAwesome.Sharp.IconButton();
            label4 = new Label();
            label1 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            pictureBox2 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            panel3 = new Panel();
            optionbutton = new FontAwesome.Sharp.IconButton();
            trackbutton = new FontAwesome.Sharp.IconButton();
            pauserun = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            btnmenu = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            toolTip1 = new ToolTip(components);
            color = new ColorDialog();
            timer2 = new System.Windows.Forms.Timer(components);
            timer3 = new System.Windows.Forms.Timer(components);
            fontDialog1 = new FontDialog();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            clickThroughToolStripMenuItem = new ToolStripMenuItem();
            stopwatchToolStripMenuItem = new ToolStripMenuItem();
            clockToolStripMenuItem = new ToolStripMenuItem();
            titlebarhiddenToolStripMenuItem = new ToolStripMenuItem();
            stopwatchToolStripMenuItem1 = new ToolStripMenuItem();
            clockToolStripMenuItem1 = new ToolStripMenuItem();
            slidingTimer = new System.Windows.Forms.Timer(components);
            mousetimer = new System.Windows.Forms.Timer(components);
            slidingTimer2 = new System.Windows.Forms.Timer(components);
            timer4 = new System.Windows.Forms.Timer(components);
            exitPhotoclockToolStripMenuItem = new ToolStripMenuItem();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonFace;
            panel2.Controls.Add(iconButton9);
            panel2.Controls.Add(exitbotton);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(98, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(336, 34);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            panel2.MouseClick += panel2_MouseClick;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // iconButton9
            // 
            iconButton9.BackColor = Color.Transparent;
            iconButton9.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            iconButton9.FlatAppearance.BorderSize = 0;
            iconButton9.FlatStyle = FlatStyle.Flat;
            iconButton9.Font = new Font("Lucida Sans Typewriter", 9F);
            iconButton9.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButton9.IconColor = Color.Black;
            iconButton9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton9.Location = new Point(8, 6);
            iconButton9.Name = "iconButton9";
            iconButton9.Size = new Size(95, 23);
            iconButton9.TabIndex = 3;
            iconButton9.TabStop = false;
            iconButton9.Text = "DASHBOARD";
            iconButton9.UseVisualStyleBackColor = false;
            iconButton9.Click += iconButton9_Click;
            iconButton9.MouseClick += iconButton9_MouseClick;
            // 
            // exitbotton
            // 
            exitbotton.Dock = DockStyle.Right;
            exitbotton.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            exitbotton.FlatAppearance.BorderSize = 0;
            exitbotton.FlatStyle = FlatStyle.Flat;
            exitbotton.IconChar = FontAwesome.Sharp.IconChar.CircleLeft;
            exitbotton.IconColor = Color.Black;
            exitbotton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            exitbotton.IconSize = 25;
            exitbotton.Location = new Point(290, 0);
            exitbotton.Name = "exitbotton";
            exitbotton.Size = new Size(46, 34);
            exitbotton.TabIndex = 2;
            exitbotton.TabStop = false;
            exitbotton.UseVisualStyleBackColor = true;
            exitbotton.MouseClick += exitbotton_MouseClick;
            // 
            // panel4
            // 
            panel4.AllowDrop = true;
            panel4.BackColor = Color.Snow;
            panel4.BackgroundImageLayout = ImageLayout.Stretch;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(trackBar2);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(textBox1);
            panel4.Controls.Add(pictureBox2);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(98, 34);
            panel4.Name = "panel4";
            panel4.Size = new Size(336, 272);
            panel4.TabIndex = 2;
            panel4.DragDrop += panel4_DragDrop;
            panel4.DragEnter += panel4_DragEnter;
            panel4.MouseClick += panel4_MouseClick;
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.ButtonFace;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(secondDash);
            panel6.Controls.Add(secondExit);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(289, 34);
            panel6.TabIndex = 14;
            panel6.Visible = false;
            panel6.MouseDown += panel6_MouseDown;
            // 
            // secondDash
            // 
            secondDash.BackColor = Color.Transparent;
            secondDash.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            secondDash.FlatAppearance.BorderSize = 0;
            secondDash.FlatStyle = FlatStyle.Flat;
            secondDash.Font = new Font("Lucida Sans Typewriter", 9F);
            secondDash.IconChar = FontAwesome.Sharp.IconChar.None;
            secondDash.IconColor = Color.Black;
            secondDash.IconFont = FontAwesome.Sharp.IconFont.Auto;
            secondDash.Location = new Point(4, 4);
            secondDash.Name = "secondDash";
            secondDash.Size = new Size(95, 23);
            secondDash.TabIndex = 3;
            secondDash.TabStop = false;
            secondDash.Text = "DASHBOARD";
            secondDash.UseVisualStyleBackColor = false;
            secondDash.MouseClick += secondDash_MouseClick;
            // 
            // secondExit
            // 
            secondExit.Dock = DockStyle.Right;
            secondExit.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            secondExit.FlatAppearance.BorderSize = 0;
            secondExit.FlatStyle = FlatStyle.Flat;
            secondExit.IconChar = FontAwesome.Sharp.IconChar.CircleLeft;
            secondExit.IconColor = Color.Black;
            secondExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            secondExit.IconSize = 25;
            secondExit.Location = new Point(247, 0);
            secondExit.Name = "secondExit";
            secondExit.Size = new Size(40, 32);
            secondExit.TabIndex = 2;
            secondExit.TabStop = false;
            secondExit.UseVisualStyleBackColor = true;
            secondExit.MouseClick += secondExit_MouseClick;
            // 
            // trackBar2
            // 
            trackBar2.BackColor = Color.LightGray;
            trackBar2.Dock = DockStyle.Right;
            trackBar2.Location = new Point(289, 0);
            trackBar2.Maximum = 100;
            trackBar2.Minimum = 30;
            trackBar2.Name = "trackBar2";
            trackBar2.Orientation = Orientation.Vertical;
            trackBar2.Size = new Size(45, 270);
            trackBar2.TabIndex = 13;
            trackBar2.Value = 30;
            trackBar2.ValueChanged += trackBar2_ValueChanged;
            trackBar2.MouseDown += trackBar2_MouseDown;
            // 
            // panel5
            // 
            panel5.BackColor = Color.LightGray;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(BGButton1);
            panel5.Controls.Add(saveButton);
            panel5.Controls.Add(timebutton);
            panel5.Controls.Add(colorbutton);
            panel5.Controls.Add(sizebutton);
            panel5.Location = new Point(0, 5);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 147);
            panel5.TabIndex = 6;
            // 
            // BGButton1
            // 
            BGButton1.FlatAppearance.BorderSize = 0;
            BGButton1.FlatStyle = FlatStyle.Flat;
            BGButton1.Font = new Font("Lucida Sans", 7F);
            BGButton1.IconChar = FontAwesome.Sharp.IconChar.DesktopAlt;
            BGButton1.IconColor = Color.Black;
            BGButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BGButton1.IconSize = 25;
            BGButton1.ImageAlign = ContentAlignment.MiddleLeft;
            BGButton1.Location = new Point(0, 59);
            BGButton1.Name = "BGButton1";
            BGButton1.Size = new Size(112, 29);
            BGButton1.TabIndex = 12;
            BGButton1.TabStop = false;
            BGButton1.Tag = "Background";
            BGButton1.Text = "startTime";
            BGButton1.TextAlign = ContentAlignment.MiddleLeft;
            BGButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            BGButton1.UseVisualStyleBackColor = true;
            BGButton1.Click += iconButton1_Click_1;
            // 
            // saveButton
            // 
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Font = new Font("Lucida Sans", 7F);
            saveButton.IconChar = FontAwesome.Sharp.IconChar.FileEdit;
            saveButton.IconColor = Color.Black;
            saveButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            saveButton.IconSize = 25;
            saveButton.ImageAlign = ContentAlignment.MiddleLeft;
            saveButton.Location = new Point(0, 115);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(112, 28);
            saveButton.TabIndex = 11;
            saveButton.TabStop = false;
            saveButton.Tag = "Save";
            saveButton.Text = "startTime";
            saveButton.TextAlign = ContentAlignment.MiddleLeft;
            saveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += iconButton1_Click;
            // 
            // timebutton
            // 
            timebutton.FlatAppearance.BorderSize = 0;
            timebutton.FlatStyle = FlatStyle.Flat;
            timebutton.Font = new Font("Lucida Sans", 7F);
            timebutton.IconChar = FontAwesome.Sharp.IconChar.Clock;
            timebutton.IconColor = Color.Black;
            timebutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            timebutton.IconSize = 25;
            timebutton.ImageAlign = ContentAlignment.MiddleLeft;
            timebutton.Location = new Point(0, 87);
            timebutton.Name = "timebutton";
            timebutton.Size = new Size(112, 28);
            timebutton.TabIndex = 10;
            timebutton.TabStop = false;
            timebutton.Tag = "Time";
            timebutton.Text = "startTime";
            timebutton.TextAlign = ContentAlignment.MiddleLeft;
            timebutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            timebutton.UseVisualStyleBackColor = true;
            timebutton.Click += timebutton_Click;
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
            colorbutton.TabStop = false;
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
            sizebutton.Size = new Size(112, 28);
            sizebutton.TabIndex = 7;
            sizebutton.TabStop = false;
            sizebutton.Tag = "Initialize";
            sizebutton.Text = "startTime";
            sizebutton.TextAlign = ContentAlignment.MiddleLeft;
            sizebutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            sizebutton.UseVisualStyleBackColor = true;
            sizebutton.Click += sizebutton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("경기천년제목V Bold", 21.75F, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point, 129);
            label4.ImageAlign = ContentAlignment.TopRight;
            label4.Location = new Point(117, 74);
            label4.Name = "label4";
            label4.Size = new Size(119, 29);
            label4.TabIndex = 10;
            label4.Text = "saved:)";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("경기천년제목V Bold", 30F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(8, 19);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(196, 40);
            label1.TabIndex = 0;
            label1.Text = "Loading..";
            label1.MouseClick += label1_MouseClick;
            label1.MouseDown += label1_MouseDown;
            label1.MouseMove += label1_MouseMove;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("경기천년제목 Bold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(119, 21);
            label3.TabIndex = 8;
            label3.Text = "Stopwatch";
            label3.DoubleClick += label3_DoubleClick_1;
            label3.MouseClick += label3_MouseClick;
            label3.MouseDown += label3_MouseDown;
            label3.MouseMove += label3_MouseMove;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.GhostWhite;
            textBox1.Location = new Point(104, 150);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 9;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.ErrorImage = null;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(334, 270);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            pictureBox2.MouseClick += pictureBox2_MouseClick;
            pictureBox2.MouseDown += pictureBox2_MouseDown;
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
            panel3.Controls.Add(optionbutton);
            panel3.Controls.Add(trackbutton);
            panel3.Controls.Add(pauserun);
            panel3.Controls.Add(iconButton3);
            panel3.Controls.Add(btnmenu);
            panel3.Controls.Add(pictureBox1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(100, 306);
            panel3.TabIndex = 0;
            panel3.MouseClick += panel3_MouseClick;
            panel3.MouseDown += panel3_MouseDown;
            // 
            // optionbutton
            // 
            optionbutton.FlatAppearance.BorderColor = Color.LightGray;
            optionbutton.FlatAppearance.BorderSize = 0;
            optionbutton.FlatStyle = FlatStyle.Flat;
            optionbutton.Font = new Font("Lucida Sans", 7F);
            optionbutton.IconChar = FontAwesome.Sharp.IconChar.ListCheck;
            optionbutton.IconColor = Color.Black;
            optionbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            optionbutton.IconSize = 25;
            optionbutton.Location = new Point(0, 172);
            optionbutton.Name = "optionbutton";
            optionbutton.Size = new Size(110, 30);
            optionbutton.TabIndex = 8;
            optionbutton.TabStop = false;
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
            trackbutton.IconChar = FontAwesome.Sharp.IconChar.Sun;
            trackbutton.IconColor = Color.Black;
            trackbutton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            trackbutton.IconSize = 25;
            trackbutton.Location = new Point(0, 142);
            trackbutton.Name = "trackbutton";
            trackbutton.Size = new Size(110, 30);
            trackbutton.TabIndex = 7;
            trackbutton.TabStop = false;
            trackbutton.Tag = "Opacity";
            trackbutton.Text = "startTime";
            trackbutton.TextAlign = ContentAlignment.MiddleLeft;
            trackbutton.TextImageRelation = TextImageRelation.ImageBeforeText;
            trackbutton.UseVisualStyleBackColor = true;
            trackbutton.Click += trackbutton_Click;
            // 
            // pauserun
            // 
            pauserun.FlatAppearance.BorderSize = 0;
            pauserun.FlatStyle = FlatStyle.Flat;
            pauserun.Font = new Font("Lucida Sans", 7F);
            pauserun.IconChar = FontAwesome.Sharp.IconChar.Play;
            pauserun.IconColor = Color.Black;
            pauserun.IconFont = FontAwesome.Sharp.IconFont.Auto;
            pauserun.IconSize = 25;
            pauserun.Location = new Point(0, 80);
            pauserun.Name = "pauserun";
            pauserun.Size = new Size(110, 30);
            pauserun.TabIndex = 6;
            pauserun.TabStop = false;
            pauserun.Tag = "Play";
            pauserun.Text = "startTime";
            pauserun.TextAlign = ContentAlignment.MiddleLeft;
            pauserun.TextImageRelation = TextImageRelation.ImageBeforeText;
            pauserun.UseVisualStyleBackColor = true;
            pauserun.Click += pauserun_Click;
            // 
            // iconButton3
            // 
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Lucida Sans", 7F);
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.ClockRotateLeft;
            iconButton3.IconColor = Color.Black;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 25;
            iconButton3.Location = new Point(0, 112);
            iconButton3.Name = "iconButton3";
            iconButton3.Size = new Size(110, 30);
            iconButton3.TabIndex = 4;
            iconButton3.TabStop = false;
            iconButton3.Tag = "Reset";
            iconButton3.Text = "startTime";
            iconButton3.TextAlign = ContentAlignment.MiddleLeft;
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = true;
            iconButton3.Click += iconButton3_Click;
            // 
            // btnmenu
            // 
            btnmenu.FlatAppearance.BorderSize = 0;
            btnmenu.FlatStyle = FlatStyle.Flat;
            btnmenu.Font = new Font("경기천년제목 Light", 9.75F);
            btnmenu.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            btnmenu.IconColor = Color.Black;
            btnmenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnmenu.IconSize = 25;
            btnmenu.Location = new Point(0, 40);
            btnmenu.Name = "btnmenu";
            btnmenu.Size = new Size(100, 22);
            btnmenu.TabIndex = 1;
            btnmenu.Tag = "Home";
            btnmenu.UseVisualStyleBackColor = true;
            btnmenu.MouseClick += btnmenu_MouseClick;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = Properties.Resources.sigaeclock;
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
            panel1.Size = new Size(98, 306);
            panel1.TabIndex = 0;
            // 
            // toolTip1
            // 
            toolTip1.ForeColor = Color.FromArgb(192, 0, 0);
            toolTip1.IsBalloon = true;
            // 
            // timer3
            // 
            timer3.Tick += timer3_Tick;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Photoclock";
            notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.BackColor = Color.Lavender;
            contextMenuStrip1.ImeMode = ImeMode.NoControl;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { exitPhotoclockToolStripMenuItem, clickThroughToolStripMenuItem, stopwatchToolStripMenuItem, clockToolStripMenuItem, titlebarhiddenToolStripMenuItem, stopwatchToolStripMenuItem1, clockToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.ShowCheckMargin = true;
            contextMenuStrip1.ShowImageMargin = false;
            contextMenuStrip1.Size = new Size(195, 198);
            // 
            // clickThroughToolStripMenuItem
            // 
            clickThroughToolStripMenuItem.Font = new Font("경기천년제목V Bold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            clickThroughToolStripMenuItem.Name = "clickThroughToolStripMenuItem";
            clickThroughToolStripMenuItem.Size = new Size(194, 24);
            clickThroughToolStripMenuItem.Text = "Click_Through";
            clickThroughToolStripMenuItem.Click += clickThroughToolStripMenuItem_Click;
            // 
            // stopwatchToolStripMenuItem
            // 
            stopwatchToolStripMenuItem.BackColor = Color.MintCream;
            stopwatchToolStripMenuItem.CheckOnClick = true;
            stopwatchToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            stopwatchToolStripMenuItem.Font = new Font("경기천년제목 Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            stopwatchToolStripMenuItem.ImageTransparentColor = Color.FromArgb(255, 192, 192);
            stopwatchToolStripMenuItem.Margin = new Padding(1);
            stopwatchToolStripMenuItem.Name = "stopwatchToolStripMenuItem";
            stopwatchToolStripMenuItem.Padding = new Padding(1);
            stopwatchToolStripMenuItem.Size = new Size(196, 24);
            stopwatchToolStripMenuItem.Text = "Stopwatch";
            stopwatchToolStripMenuItem.Click += stopwatchToolStripMenuItem_Click;
            // 
            // clockToolStripMenuItem
            // 
            clockToolStripMenuItem.BackColor = Color.MintCream;
            clockToolStripMenuItem.CheckOnClick = true;
            clockToolStripMenuItem.Font = new Font("경기천년제목 Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            clockToolStripMenuItem.Margin = new Padding(1);
            clockToolStripMenuItem.Name = "clockToolStripMenuItem";
            clockToolStripMenuItem.Padding = new Padding(1);
            clockToolStripMenuItem.Size = new Size(196, 24);
            clockToolStripMenuItem.Text = "Clock";
            clockToolStripMenuItem.Click += clockToolStripMenuItem_Click;
            // 
            // titlebarhiddenToolStripMenuItem
            // 
            titlebarhiddenToolStripMenuItem.Font = new Font("경기천년제목V Bold", 11.25F, FontStyle.Bold);
            titlebarhiddenToolStripMenuItem.Name = "titlebarhiddenToolStripMenuItem";
            titlebarhiddenToolStripMenuItem.Size = new Size(194, 24);
            titlebarhiddenToolStripMenuItem.Text = "Titlebar_hidden";
            titlebarhiddenToolStripMenuItem.Click += titlebarhiddenToolStripMenuItem_Click;
            // 
            // stopwatchToolStripMenuItem1
            // 
            stopwatchToolStripMenuItem1.BackColor = Color.MintCream;
            stopwatchToolStripMenuItem1.Font = new Font("경기천년제목 Medium", 14.25F, FontStyle.Bold);
            stopwatchToolStripMenuItem1.Name = "stopwatchToolStripMenuItem1";
            stopwatchToolStripMenuItem1.Size = new Size(194, 24);
            stopwatchToolStripMenuItem1.Text = "Stopwatch";
            stopwatchToolStripMenuItem1.Click += stopwatchToolStripMenuItem1_Click;
            // 
            // clockToolStripMenuItem1
            // 
            clockToolStripMenuItem1.BackColor = Color.MintCream;
            clockToolStripMenuItem1.Font = new Font("경기천년제목 Medium", 14.25F, FontStyle.Bold);
            clockToolStripMenuItem1.Name = "clockToolStripMenuItem1";
            clockToolStripMenuItem1.Size = new Size(194, 24);
            clockToolStripMenuItem1.Text = "Clock";
            clockToolStripMenuItem1.Click += clockToolStripMenuItem1_Click;
            // 
            // slidingTimer
            // 
            slidingTimer.Tick += slidingTimer_Tick;
            // 
            // mousetimer
            // 
            mousetimer.Tick += mousetimer_Tick;
            // 
            // exitPhotoclockToolStripMenuItem
            // 
            exitPhotoclockToolStripMenuItem.BackColor = Color.MistyRose;
            exitPhotoclockToolStripMenuItem.Font = new Font("경기천년제목V Bold", 11.25F, FontStyle.Bold);
            exitPhotoclockToolStripMenuItem.Name = "exitPhotoclockToolStripMenuItem";
            exitPhotoclockToolStripMenuItem.Size = new Size(194, 24);
            exitPhotoclockToolStripMenuItem.Text = "Exit Photoclock";
            exitPhotoclockToolStripMenuItem.Click += exitPhotoclockToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Gray;
            ClientSize = new Size(434, 306);
            ControlBox = false;
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            MinimumSize = new Size(100, 100);
            Name = "Form1";
            ShowInTaskbar = false;
            Text = " ";
            TopMost = true;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            Resize += Form1_Resize;
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Panel panel4;
        private FontAwesome.Sharp.IconButton exitbotton;
        private FontAwesome.Sharp.IconButton iconButton9;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Panel panel3;
        private FontAwesome.Sharp.IconButton trackbutton;
        private FontAwesome.Sharp.IconButton pauserun;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton btnmenu;
        private PictureBox pictureBox1;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton optionbutton;
        private Panel panel5;
        private FontAwesome.Sharp.IconButton colorbutton;
        private FontAwesome.Sharp.IconButton sizebutton;
        private FontAwesome.Sharp.IconButton timebutton;
       
        private Label label3;
        private TextBox textBox1;
        private FontAwesome.Sharp.IconButton saveButton;
        private ToolTip toolTip1;
        private ColorDialog color;
        private FontAwesome.Sharp.IconButton BGButton1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private Label label4;
        private PictureBox pictureBox2;
        private FontDialog fontDialog1;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem stopwatchToolStripMenuItem;
        private ToolStripMenuItem clockToolStripMenuItem;
        private ToolStripMenuItem clickThroughToolStripMenuItem;
        private System.Windows.Forms.Timer slidingTimer;
        private ToolStripMenuItem titlebarhiddenToolStripMenuItem;
        private ToolStripMenuItem stopwatchToolStripMenuItem1;
        private ToolStripMenuItem clockToolStripMenuItem1;
        private System.Windows.Forms.Timer mousetimer;
        private System.Windows.Forms.Timer slidingTimer2;
        private System.Windows.Forms.Timer timer4;
        private TrackBar trackBar2;
        private Panel panel6;
        private FontAwesome.Sharp.IconButton secondDash;
        private FontAwesome.Sharp.IconButton secondExit;
        private ToolStripMenuItem exitPhotoclockToolStripMenuItem;
    }
}
