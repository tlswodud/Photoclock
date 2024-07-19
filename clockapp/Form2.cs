//using ExCSS;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System.Windows.Controls.Ribbon;
using System.Windows.Documents;
using System.Windows.Forms;
using calendar;
using clockapp;
using FontAwesome.Sharp;
using static clockapp.Form1;




namespace clockapp
{

    public partial class Form2 : Form
    {
        private Stopwatch stopwatch;
        private Size formSize;
        private int borderSize = 6;
        [System.ComponentModel.Browsable(true)]
        public override bool AutoSize { get; set; }
        Form3 form3 = new Form3();

        public delegate void datageteventhadler(int x);
        public datageteventhadler Datasendevent;

       


        int cnt = 0;
        int colorcnt = 0;
        string BackgroundfilePath;
        bool DoubleClickcheck = false;
        private Point startPoint;
        private Point startPoint3;

        public Form2()
        {

            InitializeComponent();

            iconButton3.MouseUp +=  iconButton3_MouseClick;
            iconButton9.MouseUp += iconButton9_MouseClick;

            //둥근 폼 디자인 방법
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            //둥근 폼 디자인 방법

            label1.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            this.TopMost = true;
            // trackBar2.BackColor = Color.Transparent;
            textBox1.Visible = false;
            CollapsMenu();
            this.Padding = new Padding(borderSize);
            // this.BackColor = Color.FromArgb(98, 102, 244); // border 색상
            panel5.Visible = false;

            checkclockTimer.Start();
            mousetimer_2.Start();
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //Dllimport 는 외부 dll 함수 호출 이경우는 win 32 api 호출 한다 마우스 캡처를 해제한다? 모르겠다 써봐야알듯
        //entrypoint는 호출할 함수 지정해주는거 

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWind, int wMsg, int wParam, int Iparam);
        // 이경우는 마찬가지로 sendmessage 호출 윈도우에 메세지를 보낼수 있다
        // hwind 같은 경우는 윈도우의 핸들을 나타내는 매개변수
        // wMsg 는 전송할 아이디 매개변수고
        // wparam iparam 은 추가 데이터 나타내는 매개변수
        const int WM_NCHITTEST = 0x0084;

        const int HTCLIENT = 1;

        const int HTCAPTION = 2;


        private void panel2_MouseDown(object sender, MouseEventArgs e) // 마우스 다운을 통해 핸들기능 추가
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }//마우스 다운 이벤트가 발생하면 호출 
        //sender 는 이벤트를 발생시킨 객체를 나타낸다
        //mouseEventargs 는 마우스 이벤트 정보 포함 마우스 버튼 클릭 위치 알수있음
        //sendmessage 는 특정 윈도우에 메세지를 보넴 0x112 는 윈도우 이동 

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

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
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
  int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
  int nWidthEllipse, int nHeightEllipse);

        private void Form1_Load(object sender, EventArgs e)
        {
            stopwatch = new Stopwatch();

            //둥근 폼 디자인 방법  Load
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            //둥근 폼 디자인 방법 

            //setting 값 불러오기
            this.Width = Properties.Settings.Default.FormWidth2;
            this.Height = Properties.Settings.Default.FormHeight2;
            BackgroundfilePath = Properties.Settings.Default.backgroundimg2;

            this.BackColor = Properties.Settings.Default.bordercolor2;
            this.panel3.BackColor = Properties.Settings.Default.panelcolor2;
            this.panel5.BackColor = Properties.Settings.Default.panelcolor2;

            foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
            {
                bordermenu.FlatAppearance.BorderColor = Properties.Settings.Default.panelcolor2;

            }
            foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
            {
                bordermenu2.FlatAppearance.BorderColor = Properties.Settings.Default.panelcolor2;

            }
            


            label1.ForeColor = Properties.Settings.Default.label1color2;
            label3.ForeColor = Properties.Settings.Default.label3color2;
            this.Location = Properties.Settings.Default.form2location;
            label3.Text = Properties.Settings.Default.label3_2text;

            label1.Font = Properties.Settings.Default.form2label1font;
            label3.Font = Properties.Settings.Default.form2label3font;
            label1.Location = Properties.Settings.Default.label1location2;
            label3.Location = Properties.Settings.Default.label3location2;

            trackBar2.BackColor = Properties.Settings.Default.track2;
            trackBar2.Visible = Properties.Settings.Default.track2visable;

            this.Opacity = Properties.Settings.Default.opa2;

            iconButton9.BackColor = Properties.Settings.Default.DashButtoncolor2;
            iconButton3.BackColor = Properties.Settings.Default.DashButtoncolor2;

            if (Properties.Settings.Default.contextclock == true)//
            {
                SlidingTimer.Start();
            }


            if (System.IO.File.Exists(BackgroundfilePath) && BackgroundfilePath != "")
            {
                pictureBox2.Visible = true;


                label3.Parent = pictureBox2;
                label1.Parent = pictureBox2;

                pictureBox2.Image = Bitmap.FromFile(BackgroundfilePath);
            }
            if (Properties.Settings.Default.btnshow2 == true)
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
                timebutton.Location = new Point(0, 45);
                trackbutton.Location = new Point(0, 77);
                optionbutton.Location = new Point(0, 107);

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
            //setting 값 불러오기// 
        }

        private void Form2_Resize(object sender, EventArgs e)
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
            //  Application.Exit();
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

        private void secondExit2_MouseClick(object sender, MouseEventArgs e)
        {
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
                timebutton.Location = new Point(0, 80);
                trackbutton.Location = new Point(0, 112);
                optionbutton.Location = new Point(0, 144);

            }
            if (panel3.Width == 37)
            {
                timebutton.Location = new Point(0, 45);
                trackbutton.Location = new Point(0, 77);
                optionbutton.Location = new Point(0, 107);
            }
            else
            {
                timebutton.Location = new Point(0, 80);
                trackbutton.Location = new Point(0, 112);
                optionbutton.Location = new Point(0, 144);
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
                    menubtn.Padding = new Padding(0, 0, 0, 0);

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



        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString("hh:mm:ss");

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
            Properties.Settings.Default.track2visable = trackBar2.Visible;
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


        private void iconButton4_Click_1(object sender, EventArgs e)
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

        private void sizebutton_Click(object sender, EventArgs e)
        {
            iconButton9.BackColor = Color.Transparent;
            iconButton3.BackColor = Color.Transparent;

            label1.Font = new Font("경기천년제목V Bold", 30, FontStyle.Bold);
            label3.Font = new Font("경기천년제목 Bold", 16, FontStyle.Bold);
            textBox1.Font = new Font(textBox1.Font.FontFamily, 16);

            label3.Location = new Point(12, 73);
            label1.Location = new Point(8, 19);

            trackBar2.BackColor = Color.LightGray;
            label1.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;

            panel3.BackColor = Color.LightGray;
            panel5.BackColor = Color.LightGray;
            this.BackColor = Color.Gray;
            panel2.BackgroundImage = null;
            pictureBox2.Image = null;
            label3.Text = "Clock";



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
                foreach (Button bormenu1_1 in this.panel3.Controls.OfType<Button>())
                {
                    bormenu1_1.FlatAppearance.BorderColor = Color.Linen;
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
                foreach(Button bormenu1 in this.panel5.Controls.OfType<Button>())
                {
                    bormenu1.FlatAppearance.BorderColor = Color.LightGray;
                }
                foreach (Button bormenu1_3 in this.panel5.Controls.OfType<Button>())
                {
                    bormenu1_3.FlatAppearance.BorderColor = Color.LightGray;
                }

            }

        }

        private void timebutton_Click(object sender, EventArgs e)
        {

            int x = 1;
            Datasendevent(x);

        }

        private void label3_MouseDoubleClick(object sender, MouseEventArgs e)
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
                while (label3.Text.Length > j) // 빈칸만 입력했을경우 이상해지는걸 방지하는것
                {
                    if (label3.Text[i] == ' ') { i++; }

                    else
                    {
                        poxx++;
                        break;
                    }
                    j++;
                }

                if (label3.Text.Length == 0 || poxx == 0) // poxx 빈칸만 있을경우에 바꿔준것
                {
                    label3.Text = "                   ";
                }
            }

            return;

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color2.ShowDialog();
                DialogResult result2 = fontDialog1.ShowDialog();


                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label1.ForeColor = color2.Color;
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

                DialogResult result = this.color2.ShowDialog();
                DialogResult result2 = fontDialog1.ShowDialog();


                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label3.ForeColor = color2.Color;
                }
                if (result2 == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    label3.Font = fontDialog1.Font;
                }
            }
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
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
                DialogResult result = this.color2.ShowDialog();



                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    panel3.BackColor = color2.Color;
                    panel5.BackColor = color2.Color;
                    foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
                    {
                        bordermenu.FlatAppearance.BorderColor = color2.Color;

                    }
                    foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
                    {
                        bordermenu2.FlatAppearance.BorderColor = color2.Color;

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

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }


            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color2.ShowDialog();



                if (result == DialogResult.OK)
                {
                    this.BackColor = color2.Color;


                }
            }
        }

        private void panel6_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }


            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color2.ShowDialog();



                if (result == DialogResult.OK)
                {
                    this.BackColor = color2.Color;
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

                DialogResult result = this.color2.ShowDialog();
                if (result == DialogResult.OK)
                {

                    trackBar2.BackColor = color2.Color;

                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.FormHeight2 = this.Height;
            Properties.Settings.Default.FormWidth2 = this.Width;

            if (pictureBox2.Image == null)
            {
                BackgroundfilePath = "";
            }
            Properties.Settings.Default.backgroundimg2 = BackgroundfilePath;
            Properties.Settings.Default.bordercolor2 = this.BackColor;
            Properties.Settings.Default.panelcolor2 = panel3.BackColor;
            Properties.Settings.Default.label1color2 = label1.ForeColor;
            Properties.Settings.Default.label3color2 = label3.ForeColor;
            Properties.Settings.Default.form2location = this.Location;

            Properties.Settings.Default.form2label1font = label1.Font;
            Properties.Settings.Default.form2label3font = label3.Font;
            Properties.Settings.Default.label1location2 = label1.Location;
            Properties.Settings.Default.label3location2 = label3.Location;

            Properties.Settings.Default.track2 = trackBar2.BackColor;
            Properties.Settings.Default.opa2 = this.Opacity;

            Properties.Settings.Default.DashButtoncolor2 = iconButton9.BackColor;

            Properties.Settings.Default.label3_2text = label3.Text;

            if (panel1.Width == 37)
            {
                Properties.Settings.Default.btnshow2 = true;
            }
            else
            {
                Properties.Settings.Default.btnshow2 = false;
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

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                label1.Left += e.X - startPoint.X;
                label1.Top += e.Y - startPoint.Y;
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


        public enum GWL
        {
            ExStyle = -20
        }

        public enum WS_EX
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }

        //투명화아래 폼 인식 코드// 

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        public void ClickThrough()
        {
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
        }

     
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }
        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

     

        const int STEP_SLIDING2 = 10;
        int _posSliding2 = 34;


        private void SlidingTimer_Tick(object sender, EventArgs e)
        {
            if (Clocktoolcheck.CT == 1)
            {
                _posSliding2 -= STEP_SLIDING2;
                if (_posSliding2 <= 0)
                {
                    SlidingTimer.Stop();
                }
            }
            else
            { 
                _posSliding2 += STEP_SLIDING2;
                if (_posSliding2 >= 34)
                {
                    SlidingTimer.Stop();
                }

            }
            panel2.Height = _posSliding2;
        }

        int checkclockonoff = 0; // contextmenue 안눌렀을때 
        private void checkclockTimer_Tick(object sender, EventArgs e)
        {

            if (checkclockonoff == 0 && Clocktoolcheck.CT == 1)// 안누른 상태에서 true로 변함 이벤트 추가 
            {
                checkclockonoff = 1; // 지금 false로 체크된 상태
                SlidingTimer.Start();
            }
            else if (checkclockonoff == 1 && Clocktoolcheck.CT == 0) // true에서 false로 바뀜을 감지
            {
                checkclockonoff = 0;// 안누른 상태로 초기화
                SlidingTimer.Start();
            }

        }

        private void mousetimer_2_Tick(object sender, EventArgs e)
        {
            int X_mouse = Cursor.Position.X;
            int Y_mouse = Cursor.Position.Y;

            Point formZeroPosiontoScreen = panel2.PointToScreen(Point.Empty);

            int X_form1_mouseLocation = formZeroPosiontoScreen.X;
            int Y_form1_mouseLocation = formZeroPosiontoScreen.Y;

            if (Clocktoolcheck.CT == 1)// 체크했을때
            {
                if (X_mouse <= X_form1_mouseLocation + this.Width && Y_mouse <= Y_form1_mouseLocation + this.Height &&
                    X_form1_mouseLocation <= X_mouse && Y_form1_mouseLocation <= Y_mouse)
                {
                    //panel2.Height = 34;
                    if (panel5.Visible == false && clickthrogh_state.ClickTS == false)//
                    {
                        panel6.Visible = true;
                    }
                }
                else
                {
                    panel6.Visible = false;
                }
                
            }
        }

  
        private void iconButton9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    iconButton3.BackColor = color2.Color;
                    iconButton9.BackColor = color2.Color;
                }
            }
        }

        private void iconButton3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                DialogResult result = this.color2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    iconButton3.BackColor = color2.Color;
                    iconButton9.BackColor = color2.Color;
                }
            }
        }
    }
}


