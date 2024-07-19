//using ExCSS;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Media3D;
using System.Data;
using calendar;
using FontAwesome.Sharp;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Forms;
using static clockapp.Form2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Reflection;
using System.Timers;
using System.Security.Cryptography.X509Certificates;



namespace clockapp
{
    public partial class Form1 : Form
    {
        private Stopwatch stopwatch;
        private Size formSize;
        private int borderSize = 6;
        [System.ComponentModel.Browsable(true)]
        public override bool AutoSize { get; set; }

        int cnt = 0;
        int colorcnt = 0;
        bool Flagstop = false;     // stop�� ������ save�� Ȱ��ȭ�ǵ��� ��
        bool dtplaycheck = false; // ó�� play ��ư�� ���� �ð�
        string BackgroundfilePath;
        bool vtrack = false;

        DateTime dtstop;
        DateTime dtplay;

        Form2 form2 = new Form2();
        Form3 form3 = new Form3();



        private Point startPoint;
        bool DoubleClickcheck = false;
        private Point startPoint3;

        public Form1()
        {

            InitializeComponent();

            //
            mousetimer.Start();
            mousetimer.Interval = 1;
            //
            // pictureBox2.BackColor = Color.Transparent;
            form3.Datasendevent3_1 += new Form3.datageteventhadler3_1(this.Dataget3_1);
            form2.Datasendevent += new Form2.datageteventhadler(this.Dataget);
            label1.BackColor = Color.Transparent;
            //// label3.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;

            iconButton9.BackColor = Color.Transparent;

            // textbox3.Visible = false;
            label4.Visible = false;


            form3.Show();
            form3.Visible = false;


            this.TopMost = true;

            CollapsMenu();
            this.Padding = new Padding(borderSize);
            // this.BackColor = Color.FromArgb(98, 102, 244); // border ����
            panel5.Visible = false;
            textBox1.Visible = false;



            //this.FormBorderStyle = FormBorderStyle.None;

        }


        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //Dllimport �� �ܺ� dll �Լ� ȣ�� �̰��� win 32 api ȣ�� �Ѵ� ���콺 ĸó�� �����Ѵ�? �𸣰ڴ� ����߾˵�
        //entrypoint�� ȣ���� �Լ� �������ִ°� 

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWind, int wMsg, int wParam, int Iparam);
        // �̰��� ���������� sendmessage ȣ�� �����쿡 �޼����� ������ �ִ�
        // hwind ���� ���� �������� �ڵ��� ��Ÿ���� �Ű�����
        // wMsg �� ������ ���̵� �Ű�������
        // wparam iparam �� �߰� ������ ��Ÿ���� �Ű�����


        private void panel2_MouseDown(object sender, MouseEventArgs e) // ���콺 �ٿ��� ���� �ڵ��� �߰�
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }//���콺 �ٿ� �̺�Ʈ�� �߻��ϸ� ȣ�� 
         //sender �� �̺�Ʈ�� �߻���Ų ��ü�� ��Ÿ����
         //mouseEventargs �� ���콺 �̺�Ʈ ���� ���� ���콺 ��ư Ŭ�� ��ġ �˼�����
         //sendmessage �� Ư�� �����쿡 �޼����� ���� 0x112 �� ������ �̵� 
        const int WM_NCHITTEST = 0x0084;

        const int HTCLIENT = 1;

        const int HTCAPTION = 2;

        protected override void WndProc(ref Message m)
        {

            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }

                    }
                    return;
            }


            base.WndProc(ref m);
        }
        protected override CreateParams CreateParams/*// ������ ���� �ձ� ������ ������ ���� �̻��Ѱ� ��*/
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.Style |= 0x20000;// 0x40000
                }
                return cp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stopwatch = new Stopwatch();



            //�ձ� �� ������ ���
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            //�ձ� �� ������ ���

            secondDash.MouseUp += secondDash_MouseClick;
            iconButton9.MouseUp += iconButton9_MouseClick; //���콺 Ŭ�� �ޱ� 


            this.SetStyle(ControlStyles.ResizeRedraw, true);


            this.Width = Properties.Settings.Default.FormWidth;
            this.Height = Properties.Settings.Default.FormHeight;
            BackgroundfilePath = Properties.Settings.Default.backgroundimg;

            this.BackColor = Properties.Settings.Default.bordercolor;
            this.panel3.BackColor = Properties.Settings.Default.panelcolor;
            this.panel5.BackColor = Properties.Settings.Default.panelcolor;

            //��ư ������ �̻��� border ����� ��//
            foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
            {
                bordermenu.FlatAppearance.BorderColor = Properties.Settings.Default.panelcolor;
            }
            foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
            {
                bordermenu2.FlatAppearance.BorderColor = Properties.Settings.Default.panelcolor;
            }
            //��ư ������ �̻��� border ����� ��// ���� bordersize �� 0 ������ border�� ������ 


            label1.ForeColor = Properties.Settings.Default.label1color;
            label3.ForeColor = Properties.Settings.Default.label3color;
            this.Location = Properties.Settings.Default.form1location;

            label1.Font = Properties.Settings.Default.form1label1font;
            label3.Font = Properties.Settings.Default.form1label3font;
            label3.Text = Properties.Settings.Default.lable3_1text;

            this.Opacity = Properties.Settings.Default.opa1;

            label1.Location = Properties.Settings.Default.label1location;
            label3.Location = Properties.Settings.Default.label3location;

            trackBar2.BackColor = Properties.Settings.Default.track1;
            trackBar2.Visible = Properties.Settings.Default.track1visable;

            secondDash.BackColor = Properties.Settings.Default.Dashbuttoncolor;
            iconButton9.BackColor = Properties.Settings.Default.Dashbuttoncolor;



            if (Properties.Settings.Default.contextstop == true) 
            {
                stopwatchToolStripMenuItem1.Checked = true;
                slidingTimer.Start();
            }

            if (Properties.Settings.Default.contextclock == true)
            {
                clockToolStripMenuItem1.Checked = true;

                Clocktoolcheck.CT = 1;
            }
            else
            {
                Clocktoolcheck.CT = 2;
            }

            if (Properties.Settings.Default.statethroghclock == true)
            {
                ClickThrough2();
                clickthr2 = 0;
                clockToolStripMenuItem.Checked = true;
            }

            if (Properties.Settings.Default.statethroghstop == true)
            {
                ClickThrough();
                clickthr = 0;
                stopwatchToolStripMenuItem.Checked = true;
            }

            //trayicon �ձ۰� ������
            contextMenuStrip1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, contextMenuStrip1.Width, contextMenuStrip1.Height, 20, 20));


            if (System.IO.File.Exists(BackgroundfilePath) && BackgroundfilePath != "")
            {
                pictureBox2.Visible = true;
                label3.Parent = pictureBox2;
                label1.Parent = pictureBox2;
                pictureBox2.Image = Bitmap.FromFile(BackgroundfilePath);
            }

            if (Properties.Settings.Default.btnshow == true)
            {
                // CollapsMenu();
                panel1.Width = 37;
                panel3.Width = 37;
                pictureBox1.Visible = false;
                btnmenu.Dock = DockStyle.Top;
                foreach (Button menubtn in this.panel3.Controls.OfType<Button>())
                {
                    menubtn.Text = "";
                    menubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menubtn.Padding = new Padding(0);

                }
                pauserun.Location = new Point(0, 45);
                iconButton3.Location = new Point(0, 77);
                trackbutton.Location = new Point(0, 107);
                optionbutton.Location = new Point(0, 137);
            }
            else
            {
                panel1.Width = 105;
                panel3.Width = 105;
                pictureBox1.Visible = true;
                btnmenu.Dock = DockStyle.None;
                foreach (Button menubtn in this.panel3.Controls.OfType<Button>())
                {
                    menubtn.Text = "  " + menubtn.Tag.ToString();
                    menubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menubtn.Padding = new Padding(0, 0, 0, 0);
                }
            }
            form2.Show();
            form2.Visible = true; // form1 ������ ������ ���� form 2 ���� �۾�ǥ���ٿ� �ȶߴ� ����

        }
        //������ API DWM  �ձ۰� ������ �ϴ� API 
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33 // â�𼭸� ���� �Ӽ�
        }
       
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,    // �ý��۱⺻
            DWMWCP_DONOTROUND = 1, // â�𼭸� �ձ۰��Ұ���
            DWMWCP_ROUND = 2,      // �ձ۰�
            DWMWCP_ROUNDSMALL = 3  
        }
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);
        //������ API DWM  �ձ۰� ������ �ϴ� API 

        // contextmenu �ձ۰� ������
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        // �ձ۰� ������ 


        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();

        }
        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 11, 11, 0); //�ִ�ȭ����//
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize) ;
                    this.Padding = new Padding(borderSize);
                    break;
            }
        }
        private void exitbotton_MouseClick(object sender, MouseEventArgs e)
        {
            

            this.Hide();

            int count = 0;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Visible == false)
                    count++;
            }
            
            if (count == 3)  
            {
                Application.Exit(); // 3�� ���� �ݱ�� �������� 
            }

        }
        private void secondExit_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();

            int count = 0;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Visible == false)
                    count++;
            }
           
            if (count == 3)
            {
                Application.Exit();
            }
        }




        private void maxwindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;

            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnmenu_MouseClick(object sender, MouseEventArgs e)
        {
            CollapsMenu();

            if (panel1.Width == 37 && cnt == 0)
            {
                pauserun.Location = new Point(0, 45);
                iconButton3.Location = new Point(0, 77);
                trackbutton.Location = new Point(0, 107);
                optionbutton.Location = new Point(0, 137);
            }
            else if (panel1.Width == 37 && cnt == 1) // ��� ���� 
            {
                pauserun.Location = new Point(0, 60);
                iconButton3.Location = new Point(0, 92);
                trackbutton.Location = new Point(0, 122);
                optionbutton.Location = new Point(0, 152);
            }
            else
            {
                pauserun.Location = new Point(0, 80);
                iconButton3.Location = new Point(0, 112);
                trackbutton.Location = new Point(0, 142);
                optionbutton.Location = new Point(0, 172);
            }

            if (panel5.Visible == true)
            {
                optionMenucollapso();
            }

        }
        private void CollapsMenu()
        {
            if (this.panel1.Width > 104)
            {
                panel1.Width = 37;
                panel3.Width = 37;
                pictureBox1.Visible = false;
                btnmenu.Dock = DockStyle.Top;
                foreach (Button menubtn in this.panel3.Controls.OfType<Button>())
                {
                    menubtn.Text = "";
                    menubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menubtn.Padding = new Padding(0);

                }
            }

            else
            {
                panel1.Width = 105;
                panel3.Width = 105;
                pictureBox1.Visible = true;
                btnmenu.Dock = DockStyle.None;
                foreach (Button menubtn in this.panel3.Controls.OfType<Button>())
                {
                    menubtn.Text = "  " + menubtn.Tag.ToString();
                    menubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menubtn.Padding = new Padding(0, 0, 0, 0);

                }
            }

        }


        private void pauserun_Click(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                pauserun.IconChar = FontAwesome.Sharp.IconChar.Play;
                pauserun.Tag = "Play";
                pauserun.Text = "  " + pauserun.Tag.ToString();
                stopwatch.Stop();
                Flagstop = true; 
                dtstop = DateTime.Now;

            }
            else
            {
                pauserun.IconChar = FontAwesome.Sharp.IconChar.Pause;
                stopwatch.Start();
                Flagstop = false;
                pauserun.Tag = "Pause";
                pauserun.Text = "  " + pauserun.Tag.ToString();
                if (dtplaycheck == false)
                {
                    dtplay = DateTime.Now;
                    dtplaycheck = true;
                }
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            dtplaycheck = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = string.Format("{0:hh\\:mm\\:ss}", stopwatch.Elapsed);

        }

        private void trackbutton_Click(object sender, EventArgs e)
        {
            if (trackBar2.Visible == true)
            {
                trackBar2.Visible = false;
            }
            else
            {
                //trackbutton.IconColor = Color.Snow;
                trackBar2.Visible = true;
            }
            Properties.Settings.Default.track1visable = trackBar2.Visible; // Ʈ���ٴ� ���� ����Ǹ� visble�� false�� ��ȯ�Ǿ�  ������ false�� ������..
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = trackBar2.Value * 0.01;
        }



        private void optionbutton_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            else
            {
                panel5.Visible = true;
            }
            optionMenucollapso();

        }

        private void optionMenucollapso()
        {
            if (this.panel1.Width == 37)//40
            {
                panel5.Width = 37;
                foreach (Button menubtn in this.panel5.Controls.OfType<Button>())
                {
                    menubtn.Text = "";
                    menubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menubtn.Padding = new Padding(0);

                }
            }
            else
            {
                panel5.Width = 105;
                foreach (Button menubtn in this.panel5.Controls.OfType<Button>())
                {
                    menubtn.Text = "  " + menubtn.Tag.ToString();
                    menubtn.ImageAlign = ContentAlignment.MiddleLeft;
                    menubtn.Padding = new Padding(0, 0, 0, 0);
                }
            }
        }


        private void sizebutton_Click(object sender, EventArgs e) // -> �ʱ�ȭ��ư���� ����
        {
            label1.Font = new Font("���õ������V Bold", 30, FontStyle.Bold);
            label3.Font = new Font("���õ������ Bold", 16, FontStyle.Bold);
            textBox1.Font = new Font(textBox1.Font.FontFamily, 16);

            iconButton9.BackColor = Color.Transparent;
            secondDash.BackColor = Color.Transparent;


            label3.Location = new Point(12, 73);
            label1.Location = new Point(8, 19);//15
            label1.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label3.Text = "Stopwatch";

            panel3.BackColor = Color.LightGray;
            panel5.BackColor = Color.LightGray;
            this.BackColor = Color.Gray;
            trackBar2.BackColor = Color.LightGray;

            pictureBox2.Image = null;

        }
        private void colorbutton_Click(object sender, EventArgs e)
        {
            colorcnt++;

            if (colorcnt == 3)
            {
                colorcnt = 0;
            }

            if (colorcnt == 2)
            {
                panel3.BackColor = Color.Linen;
                panel5.BackColor = Color.Linen;
                this.BackColor = Color.LightPink;
                trackBar2.BackColor = Color.LightPink;

                foreach (Button bormenu1 in this.panel5.Controls.OfType<Button>())
                {
                    bormenu1.FlatAppearance.BorderColor = Color.Linen;
                }
                foreach (Button bormenu1_2 in this.panel3.Controls.OfType<Button>())
                {
                    bormenu1_2.FlatAppearance.BorderColor = Color.Linen;
                }
            }
            else if (colorcnt == 1)
            {
                panel3.BackColor = Color.LightSkyBlue;
                panel5.BackColor = Color.LightSkyBlue;
                this.BackColor = Color.DeepSkyBlue;
                trackBar2.BackColor = Color.PaleTurquoise;

                foreach (Button bormenu1 in this.panel5.Controls.OfType<Button>())
                {
                    bormenu1.FlatAppearance.BorderColor = Color.LightSkyBlue;
                }
                foreach (Button bormenu1_2 in this.panel3.Controls.OfType<Button>())
                {
                    bormenu1_2.FlatAppearance.BorderColor = Color.LightSkyBlue;
                }
            }
            else if (colorcnt == 0)
            {
                panel3.BackColor = Color.LightGray;
                panel5.BackColor = Color.LightGray;
                this.BackColor = Color.Gray;

                trackBar2.BackColor = Color.LightGray;

                foreach (Button bormenu1 in this.panel5.Controls.OfType<Button>())
                {
                    bormenu1.FlatAppearance.BorderColor = Color.LightGray;
                }
                foreach (Button bormenu1_2 in this.panel3.Controls.OfType<Button>())
                {
                    bormenu1_2.FlatAppearance.BorderColor = Color.LightGray;
                }
            }

        }
        private void timebutton_Click(object sender, EventArgs e)
        {
            //  Application.Run(form2);
            //  form2.Datasendevent += new Form2.datageteventhadler(this.Dataget);
            form2.Show();

        }
        private void Dataget(int x)
        {
            this.Visible = true;
        }

        private void label3_DoubleClick_1(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label3.Visible = false;
            textBox1.Location = label3.Location;
            textBox1.Size = label3.Size;
            textBox1.Text = label3.Text;
            textBox1.Font = label3.Font;
            DoubleClickcheck = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                DoubleClickcheck = false;
                label3.Text = textBox1.Text;
                textBox1.Visible = false;
                label3.Visible = true;
                int i = 0, j = 0;
                int poxx = 0;
                while (label3.Text.Length > j) // ��ĭ�� �Է�������� �̻������°� �����ϴ°�
                {
                    if (label3.Text[i] == ' ') { i++; }

                    else
                    {
                        poxx++;
                        break;
                    }
                    j++;
                }

                if (label3.Text.Length == 0 || poxx == 0) // poxx ��ĭ�� ������쿡 �ٲ��ذ�
                {
                    label3.Text = "                   ";
                }
            }

            return;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (Flagstop == true)
            {
                var dtyyMM = dtplay.ToString("yy/MM");

                var savePath = "C:\\clocktxtfolder";

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath); // ���� �����ϰ� 
                }
                string settingfile = $"clockapptest_{dtyyMM}.txt";

                settingfile = System.IO.Path.Combine(savePath, settingfile); // �ϳ��� ��η� �����  

                StreamWriter File = new StreamWriter(settingfile, true);


                File.Write(dtplay.ToString("yy/MM/dd"));
                File.Write("\n");
                File.Write(label3.Text);
                File.Write("\n");
                File.Write(dtplay.ToString("tt hh:mm"));
                File.Write("    ");
                File.Write(dtstop.ToString("tt hh:mm"));
                File.Write("    ");
                File.Write(label1.Text);
                File.Write("\n");
                File.Close();
                if (trackBar2.Visible == true)
                {
                    vtrack = true;
                }
                trackBar2.Visible = false;
                label4.Visible = true;
                label4.Location = trackBar2.Location;
                label4.BackColor = Color.WhiteSmoke;
                timer3.Start();
                timer3.Interval = 2400;

            }
            else
            {
                ToolTip toolTip = new ToolTip();

                toolTip.AutoPopDelay = 5000;
                toolTip.InitialDelay = 300;

                toolTip.SetToolTip(saveButton, "Plz press stop button");
            }
        }

        //Dashboard  ����
        private void iconButton9_Click(object sender, EventArgs e)
        {
            form3.Show();


        }
        private void secondDash_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color.ShowDialog();
                if (result == DialogResult.OK)
                {

                    iconButton9.BackColor = color.Color;
                    secondDash.BackColor = color.Color;
                }
            }
            else
            {
                form3.Show();
            }
        }
        //Dashboard  ����

        private void Dataget3_1(int x)
        {
            this.Visible = true;
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color.ShowDialog();
                DialogResult result2 = this.fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label1.ForeColor = color.Color;
                }

                if (result2 == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label1.Font = fontDialog1.Font;
                }

            }
        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color.ShowDialog();
                DialogResult result2 = this.fontDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label3.ForeColor = color.Color;
                }
                if (result2 == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label3.Font = fontDialog1.Font;
                }
            }
        }
        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }

            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color.ShowDialog();
                if (result == DialogResult.OK)
                {

                    trackBar2.BackColor = color.Color;

                }
            }
        }
        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }


            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    panel3.BackColor = color.Color;
                    panel5.BackColor = color.Color;

                    foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
                    {
                        bordermenu.FlatAppearance.BorderColor = color.Color;
                    }
                    foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
                    {
                        bordermenu2.FlatAppearance.BorderColor = color.Color;
                    }

                }
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }

            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = color.ShowDialog();


                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    this.BackColor = color.Color;
                }

            }



        }

        private void panel4_DragDrop(object sender, DragEventArgs e)
        {
            string[] filess = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (filess.Length > 0 && File.Exists(filess[0]))
            {
                BackgroundfilePath = filess[0];

                pictureBox2.Visible = true;

                label3.Parent = pictureBox2;
                label1.Parent = pictureBox2;

                pictureBox2.Image = Bitmap.FromFile(BackgroundfilePath);

            }
        }


        private void panel4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            if (this.TopMost == true)
            {
                this.TopMost = false;
            }
            else
            {
                this.TopMost = true;

            }



        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label4.Visible = false;
            if (vtrack == true)
            {
                trackBar2.Visible = true;
                vtrack = false;
            }
            timer3.Stop();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }


        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            notifyIcon1.Dispose();
            Properties.Settings.Default.FormHeight = this.Height;
            Properties.Settings.Default.FormWidth = this.Width;

            if (pictureBox2.Image == null)
            {
                BackgroundfilePath = "";
            }
            Properties.Settings.Default.backgroundimg = BackgroundfilePath;

            Properties.Settings.Default.bordercolor = this.BackColor;
            Properties.Settings.Default.panelcolor = panel3.BackColor;
            Properties.Settings.Default.label1color = label1.ForeColor;
            Properties.Settings.Default.label3color = label3.ForeColor;
            Properties.Settings.Default.form1location = this.Location;

            Properties.Settings.Default.form1label1font = label1.Font;
            Properties.Settings.Default.form1label3font = label3.Font;

            Properties.Settings.Default.opa1 = this.Opacity;

            Properties.Settings.Default.label1location = label1.Location;
            Properties.Settings.Default.label3location = label3.Location;

            Properties.Settings.Default.track1 = trackBar2.BackColor;

            Properties.Settings.Default.contextclock = clockToolStripMenuItem1.Checked;
            Properties.Settings.Default.contextstop = stopwatchToolStripMenuItem1.Checked;

            Properties.Settings.Default.statethroghstop = stopwatchToolStripMenuItem.Checked;
            Properties.Settings.Default.statethroghclock = clockToolStripMenuItem.Checked;

            Properties.Settings.Default.Dashbuttoncolor = secondDash.BackColor;

            Properties.Settings.Default.lable3_1text = label3.Text;

            if (panel1.Width == 37)
            {
                Properties.Settings.Default.btnshow = true;
            }
            else
            {
                Properties.Settings.Default.btnshow = false;
            }



            Properties.Settings.Default.Save();

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {


            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
            }

        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {

                label1.Left += e.X - startPoint.X;
                label1.Top += e.Y - startPoint.Y;

            }

        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            if (DoubleClickcheck == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    startPoint3 = e.Location;
                }
            }
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                label3.Left += e.X - startPoint3.X;
                label3.Top += e.Y - startPoint3.Y;
            }
        }
        //���� ���� �ϰ� �ؼ� Ŭ���� ����ǵ��� ������� 
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        int clickthr = 1;
        void ClickThrough()
        {
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);// ���� â ��Ÿ�� �������� 
            wl = wl | 0x20;                                  //WS_EX_TRANSPARENT �÷���
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
        }

        void ClickThroughfalse()
        {
            int wl = GetWindowLong(this.Handle, GWL.ExStyle); 
            wl = wl & ~(0x20); // ��� ������ ������Ű�� & �������� ��Ʈ ����  
            SetWindowLong(this.Handle, GWL.ExStyle, wl);

        }



        private void stopwatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                stopwatchToolStripMenuItem.Checked = false;
                return;
            }
            if (clickthr == 0)
            {
                ClickThroughfalse();
                clickthr = 1;
            }
            else
            {
                ClickThrough();
                clickthr = 0;
            }
            contextMenuStrip1.Visible = true;
        }
        void ClickThrough2()
        {
            int wl2 = GetWindowLong(form2.Handle, GWL.ExStyle);

            wl2 = wl2 | 0x20;
            SetWindowLong(form2.Handle, GWL.ExStyle, wl2);
        }

        public class clickthrogh_state // clock �� clickthrogh check ����
        {
            public static bool ClickTS { get; set; } = false;
        }


        void ClickThroughfalse2()
        {
            int wl2 = GetWindowLong(form2.Handle, GWL.ExStyle);
            // ��� ������ ������Ű�� & �������� ��Ʈ ���� 
            wl2 = wl2 & ~(0x20);
            SetWindowLong(form2.Handle, GWL.ExStyle, wl2);

        }

        int clickthr2 = 1;
        private void clockToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (form2.Visible == false)
            {
                clockToolStripMenuItem.Checked = false; // false �� 
                return;
            }
            if (clickthr2 == 0)
            {
                ClickThroughfalse2();
                clickthr2 = 1;
                clickthrogh_state.ClickTS = false;
            }
            else
            {
                ClickThrough2();
                clickthr2 = 0;
                clickthrogh_state.ClickTS = true;// clock�� Ŭ�� ���� ����� �۵� �ȵȴٸ�

            }
            contextMenuStrip1.Visible = true;

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //�����̵� �޴��� ���̴�/������ �ӵ� ����
        const int STEP_SLIDING = 10;
        int _posSliding = 34;


        private void slidingTimer_Tick(object sender, EventArgs e)
        {
            //�����̵� �޴��� ���̴�/������ �ӵ� ����

            if (stopwatchToolStripMenuItem1.Checked == true)//
            {
                //�����̵� �޴��� ����� ����
                _posSliding -= STEP_SLIDING;
                if (_posSliding <= 0)
                {
                    slidingTimer.Stop();
                }

            }
            else
            {
                //�����̵� �޴��� ���̴� ����
                _posSliding += STEP_SLIDING;
                if (_posSliding >= 34)
                {
                    slidingTimer.Stop();
                }

            }
            panel2.Height = _posSliding;
        }

        private void stopwatchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (stopwatchToolStripMenuItem1.Checked == false)//
            {
                stopwatchToolStripMenuItem1.Checked = true;//

            }
            else
            {
                stopwatchToolStripMenuItem1.Checked = false;//

            }
            slidingTimer.Start();

            contextMenuStrip1.Visible = true;
        }

        public class Clocktoolcheck
        {
            public static int CT { get; set; } = 0; // �ȴ������� 
        }

        private void clickThroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = true;
        }

        private void titlebarhiddenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Visible = true;
        }

        public void clockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (clockToolStripMenuItem1.Checked == false)//
            {
                clockToolStripMenuItem1.Checked = true;//

                Clocktoolcheck.CT = 1; // true ������������ ������

            }
            else
            {
                clockToolStripMenuItem1.Checked = false;//

                Clocktoolcheck.CT = 0;//false ����
            }
            contextMenuStrip1.Visible = true;
        }

        //form1 panel6;
        private void mousetimer_Tick(object sender, EventArgs e) //���콺�� ���ȿ� �ֳ� �ۿ� �ֳ��� ���� ����� ����
        {
            int X_mouse = Cursor.Position.X;
            int Y_mouse = Cursor.Position.Y;

            Point formZeroPosiontoScreen = panel2.PointToScreen(Point.Empty);

            int X_form1_mouseLocation = formZeroPosiontoScreen.X;
            int Y_form1_mouseLocation = formZeroPosiontoScreen.Y;

            if (stopwatchToolStripMenuItem1.Checked == true)
            {
                if (X_mouse <= X_form1_mouseLocation + this.Width && Y_mouse <= Y_form1_mouseLocation + this.Height &&
                    X_form1_mouseLocation <= X_mouse && Y_form1_mouseLocation <= Y_mouse)
                {
                    //panel2.Height = 34;
                    if (panel5.Visible == false && stopwatchToolStripMenuItem.Checked == false)//
                    {
                        panel6.Visible = true;
                    }
                }
                else
                {
                    panel6.Visible = false;
                    //panel2.Height = 0;
                }
            }

        }
        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }
        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }

            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = color.ShowDialog();


                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    this.BackColor = color.Color;
                }

            }
        }

        private void iconButton9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = color.ShowDialog();


                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    secondDash.BackColor = color.Color;
                    iconButton9.BackColor = color.Color;
                }

            }
        }

        
    }

}
