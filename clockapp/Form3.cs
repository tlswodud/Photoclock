//using ExCSS;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Media3D;
using System.Data;
using clockapp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.DirectoryServices.ActiveDirectory;
using static clockapp.Form1;
using FontAwesome.Sharp;
//using Org.BouncyCastle.Bcpg;

namespace calendar
{


    public partial class Form3 : Form
    {
        private Stopwatch stopwatch;
        private Size formSize;
        private int borderSize = 5;
        [System.ComponentModel.Browsable(true)]
        public delegate void datageteventhadler3_1(int x); // Form1 의 show 송신 코드
        public datageteventhadler3_1 Datasendevent3_1;

        public delegate void datageteventhadler3_2(int x);//form2 .
                                                          // System.Windows.Forms.Label label0;
        public override bool AutoSize { get; set; }

        int cnt = 0;
        int colorcnt = 0;
        bool Flag = true;
        string BackgroundfilePath;
        public Form3()
        {

            InitializeComponent();

            //둥근 폼 디자인 방법
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            //둥근 폼 디자인 방법



            label1.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            //panel6.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;

            // textbox3.Visible = false;

            monthCalendar1.SelectionStart = DateTime.Now;

            //monthCalendar1.SelectionEnd = DateTime.Now;

            this.TopMost = true;
            // trackBar2.BackColor = Color.Transparent;
            CollapsMenu();
            this.Padding = new Padding(borderSize);
            // this.BackColor = Color.FromArgb(98, 102, 244); // border 색상
            panel5.Visible = false;

            panel6.AutoScroll = false;
            panel6.HorizontalScroll.Enabled = false;
            panel6.VerticalScroll.Visible = false;
            panel6.HorizontalScroll.Minimum = 0;
            panel6.AutoScroll = true;

            panel6.VerticalScroll.SmallChange = 0;
            panel6.VerticalScroll.LargeChange = 5;

            label2.Visible = false;
            label5.Visible = false;
            label6.Visible = false;


            panel1.Width = 37;//40
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



        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //Dllimport 는 외부 dll 함수 호출 이경우는 win 32 api 호출 한다 마우스 캡처를 해제한다? 모르겠다 써봐야알듯
        //entrypoint는 호출할 함수 지정해주는거 

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWind, int wMsg, int wParam, int Iparam);



        private void panel2_MouseDown(object sender, MouseEventArgs e) // 마우스 다운을 통해 핸들기능 추가
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }//마우스 다운 이벤트가 발생하면 호출 
        //sender 는 이벤트를 발생시킨 객체를 나타낸다
        //mouseEventargs 는 마우스 이벤트 정보 포함 마우스 버튼 클릭 위치 알수있음
        //sendmessage 는 특정 윈도우에 메세지를 보넴 0x112 는 윈도우 이동 

        protected override void WndProc(ref Message m)
        {


            const int RESIZE_HANDLE_SIZE = 50;

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

      
        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }
        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 11, 11, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize) ;
                    this.Padding = new Padding(borderSize);
                    break;

            }
        }
        private void exitbotton_MouseClick(object sender, MouseEventArgs e)
        {
            //Application.Exit();

            this.Hide();

            int count = 0;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Visible == false)
                    count++;
            }
            //label1.Text = count.ToString(); // Active Forms value :)
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

            if (panel5.Visible == true)
            {
                optionMenucollapso();
            }

        }
        private void CollapsMenu()
        {
            if (this.panel1.Width > 104)
            {
                panel1.Width = 37;//40
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



        private void trackbutton_Click(object sender, EventArgs e)
        {
            if (trackBar2.Visible == true)
            {

                trackBar2.Visible = false;
            }
            else
            {
                trackBar2.Visible = true;
            }
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
            if (this.panel1.Width == 37)
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


        private void sizebutton_Click(object sender, EventArgs e)
        {
            // 

            if (monthCalendar1.Visible == true)
            {
                monthCalendar1.Visible = false;
                panel6.Location = new Point(19, 30);
                panel6.Size = new Size(334, 400);
            }
            else
            {
                monthCalendar1.Visible = true;
              
                panel6.Location = new Point(19, 203);
                panel6.Size = new Size(334, 220);
            }

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
                    bormenu1.FlatAppearance.BorderColor = Color.LightSkyBlue;
                }
                foreach (Button bormenu1_2 in this.panel3.Controls.OfType<Button>())
                {
                    bormenu1_2.FlatAppearance.BorderColor = Color.LightSkyBlue;
                }
            }

        }
        private void Dataget(int x)
        {
            this.Visible = true;
        }


        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            label1.Text = monthCalendar1.SelectionStart.ToString("yy/MM/dd");
            Flag = false;
            readfile();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            if (Flag == true)
            {
                //label1.Text = DateTime.Now.ToString("yy/MM/dd");
                label1.Text = monthCalendar1.SelectionStart.ToString("yy/MM/dd");

            }
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            int x = 1;
            Datasendevent3_1(x);
        }

        private void readfile()
        {
            string dtyyMM = monthCalendar1.SelectionStart.ToString("yy_MM");
            string path = $"C:\\clocktxtfolder\\clockapptest_{dtyyMM}.txt";

            if (System.IO.File.Exists(path) == false)
            {
                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;

                return;
            }
            label2.Visible = true;
            label5.Visible = true;
            label6.Visible = true;


            StreamReader sr2 = new StreamReader(File.Open(path, FileMode.Open, FileAccess.ReadWrite));
            label4.Text = "Record your daily life!";

            string line = "";
            string linecheck;
            int o = 0; int c = 1;
            int checcnt = 0;
            //label4.Text = sr2.ReadLine();
            while (true)
            {
                linecheck = sr2.ReadLine();

                if (linecheck == label1.Text)
                {
                    checcnt++;
                    o = 1;
                    c++;
                    continue;
                }
                if (linecheck == null)
                {
                    break;
                }
                if (o == 1 && c == 2)
                {
                    line += linecheck;
                    line += "\n";
                    line += "\n";
                    line += "   ";
                    c++;
                    continue;
                }
                if (o == 1 && c == 3)
                {

                    line += linecheck;
                    line += "\n";
                    line += "\n";
                    c = 1;
                    o = 0;
                    continue;
                }
            }
            if (checcnt == 0)
            {
                label4.Text = label4.Text;
                label2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }
            else
            {
                label4.Text = line;
            }
            sr2.Close();
        }
        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color3.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label4.ForeColor = color3.Color;
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
                DialogResult result = this.color3.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.

                    this.BackColor = color3.Color;
                    iconButton9.BackColor = color3.Color;
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

                DialogResult result = this.color3.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    panel3.BackColor = color3.Color;
                    panel5.BackColor = color3.Color;

                    foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
                    {
                        bordermenu.FlatAppearance.BorderColor = color3.Color;
                    }
                    foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
                    {
                        bordermenu2.FlatAppearance.BorderColor = color3.Color;
                    }
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

                // label3.Parent = pictureBox2;
                //  label1.Parent = pictureBox2;

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

        private void backgroundbutton_Click(object sender, EventArgs e)
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

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
            else
            {
                panel6.Visible = true;
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

                DialogResult result = this.color3.ShowDialog();
                if (result == DialogResult.OK)
                {

                    trackBar2.BackColor = color3.Color;

                }

            }
        }
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color3.ShowDialog();
                DialogResult result2 = fontDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label1.ForeColor = color3.Color;
                }

                if (result2 == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label1.Font = fontDialog1.Font;
                }

            }
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

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                BackgroundfilePath = "";
            }
            clockapp.Properties.Settings.Default.backgroundimg3 = BackgroundfilePath;
            clockapp.Properties.Settings.Default.bordercolor3 = this.BackColor;
            clockapp.Properties.Settings.Default.panelcolor3 = panel3.BackColor;
            clockapp.Properties.Settings.Default.label1color3 = label1.ForeColor;
            clockapp.Properties.Settings.Default.label4color3 = label4.ForeColor;
            clockapp.Properties.Settings.Default.form3location = this.Location;
            clockapp.Properties.Settings.Default.form3label1font = label1.Font;
            clockapp.Properties.Settings.Default.Dashbuttoncolor3 = iconButton9.BackColor;


            clockapp.Properties.Settings.Default.track3 = trackBar2.BackColor;

            clockapp.Properties.Settings.Default.Save();

        }

        private void Form3_Load(object sender, EventArgs e)
        {

            BackgroundfilePath = clockapp.Properties.Settings.Default.backgroundimg3;

            iconButton9.MouseUp += iconButton9_MouseClick;

            this.BackColor = clockapp.Properties.Settings.Default.bordercolor3;
            this.panel3.BackColor = clockapp.Properties.Settings.Default.panelcolor3;
            this.panel5.BackColor = clockapp.Properties.Settings.Default.panelcolor3;
            label1.ForeColor = clockapp.Properties.Settings.Default.label1color3;
            label4.ForeColor = clockapp.Properties.Settings.Default.label4color3;
            this.Location = clockapp.Properties.Settings.Default.form3location;
            label1.Font = clockapp.Properties.Settings.Default.form3label1font;
            iconButton9.BackColor = clockapp.Properties.Settings.Default.Dashbuttoncolor3;
            trackBar2.BackColor = clockapp.Properties.Settings.Default.track3;


            foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
            {
                bordermenu.FlatAppearance.BorderColor = clockapp.Properties.Settings.Default.panelcolor3;
            }
            foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
            {
                bordermenu2.FlatAppearance.BorderColor = clockapp.Properties.Settings.Default.panelcolor3;
            }

            if (System.IO.File.Exists(BackgroundfilePath) && BackgroundfilePath != "")
            {
                pictureBox2.Visible = true;


                pictureBox2.Image = Bitmap.FromFile(BackgroundfilePath);
            }




        }

        private void initial_button_Click(object sender, EventArgs e)
        {
            label1.Font = new Font("경기천년제목V Bold", 24, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            trackBar2.BackColor = Color.LightGray;
            pictureBox2.Image = null;
            panel4.BackgroundImage = null;
            label4.ForeColor = Color.Black;
            foreach (Button bormenu1 in this.panel5.Controls.OfType<Button>())
            {
                bormenu1.FlatAppearance.BorderColor = Color.LightGray;
            }
            foreach (Button bormenu1_2 in this.panel3.Controls.OfType<Button>())
            {
                bormenu1_2.FlatAppearance.BorderColor = Color.LightGray;
            }
            iconButton9.BackColor = Color.Transparent;
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {

        }

        private void iconButton9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color3.ShowDialog();
                if (result == DialogResult.OK)
                {
                    iconButton9.BackColor = color3.Color;
                }
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}




