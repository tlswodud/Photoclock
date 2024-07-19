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
        bool Flagstop = false;     // stop을 눌러야 save가 활성화되도록 함
        bool dtplaycheck = false; // 처음 play 버튼을 누른 시간
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
            // this.BackColor = Color.FromArgb(98, 102, 244); // border 색상
            panel5.Visible = false;
            textBox1.Visible = false;



            //this.FormBorderStyle = FormBorderStyle.None;

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


        private void panel2_MouseDown(object sender, MouseEventArgs e) // 마우스 다운을 통해 핸들기능 추가
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }//마우스 다운 이벤트가 발생하면 호출 
         //sender 는 이벤트를 발생시킨 객체를 나타낸다
         //mouseEventargs 는 마우스 이벤트 정보 포함 마우스 버튼 클릭 위치 알수있음
         //sendmessage 는 특정 윈도우에 메세지를 보넴 0x112 는 윈도우 이동 
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
        protected override CreateParams CreateParams/*// 사이즈 조절 둥근 디자인 이지만 위에 이상한게 뜸*/
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



            //둥근 폼 디자인 방법
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            //둥근 폼 디자인 방법

            secondDash.MouseUp += secondDash_MouseClick;
            iconButton9.MouseUp += iconButton9_MouseClick; //마우스 클릭 받기 


            this.SetStyle(ControlStyles.ResizeRedraw, true);


            this.Width = Properties.Settings.Default.FormWidth;
            this.Height = Properties.Settings.Default.FormHeight;
            BackgroundfilePath = Properties.Settings.Default.backgroundimg;

            this.BackColor = Properties.Settings.Default.bordercolor;
            this.panel3.BackColor = Properties.Settings.Default.panelcolor;
            this.panel5.BackColor = Properties.Settings.Default.panelcolor;

            //버튼 누를시 이상한 border 지우는 것//
            foreach (Button bordermenu in this.panel3.Controls.OfType<Button>())
            {
                bordermenu.FlatAppearance.BorderColor = Properties.Settings.Default.panelcolor;
            }
            foreach (Button bordermenu2 in this.panel5.Controls.OfType<Button>())
            {
                bordermenu2.FlatAppearance.BorderColor = Properties.Settings.Default.panelcolor;
            }
            //버튼 누를시 이상한 border 지우는 것// 원인 bordersize 가 0 이지만 border가 생성됨 


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

            //trayicon 둥글게 디자인
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
            form2.Visible = true; // form1 생성이 끝나고 난후 form 2 생성 작업표시줄에 안뜨는 오류

        }
        //윈도우 API DWM  둥글게 디자인 하는 API 
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33 // 창모서리 설정 속성
        }
       
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,    // 시스템기본
            DWMWCP_DONOTROUND = 1, // 창모서리 둥글게할거임
            DWMWCP_ROUND = 2,      // 둥글게
            DWMWCP_ROUNDSMALL = 3  
        }
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);
        //윈도우 API DWM  둥글게 디자인 하는 API 

        // contextmenu 둥글게 디자인
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        // 둥글게 디자인 


        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();

        }
        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 11, 11, 0); //최대화제거//
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
                Application.Exit(); // 3개 폼이 닫기면 완전종료 
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
            else if (panel1.Width == 37 && cnt == 1) // 폐기 ㅎㅎ 
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
            Properties.Settings.Default.track1visable = trackBar2.Visible; // 트렉바는 폼이 종료되면 visble이 false로 전환되어  무조건 false로 나오넹..
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


        private void sizebutton_Click(object sender, EventArgs e) // -> 초기화버튼으로 변경
        {
            label1.Font = new Font("경기천년제목V Bold", 30, FontStyle.Bold);
            label3.Font = new Font("경기천년제목 Bold", 16, FontStyle.Bold);
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (Flagstop == true)
            {
                var dtyyMM = dtplay.ToString("yy/MM");

                var savePath = "C:\\clocktxtfolder";

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath); // 파일 생성하고 
                }
                string settingfile = $"clockapptest_{dtyyMM}.txt";

                settingfile = System.IO.Path.Combine(savePath, settingfile); // 하나의 경로로 만들고  

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

        //Dashboard  생성
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
        //Dashboard  생성

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
        //폼을 투명 하게 해서 클릭이 통과되도록 만들거임 
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        int clickthr = 1;
        void ClickThrough()
        {
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);// 현재 창 스타일 가져오기 
            wl = wl | 0x20;                                  //WS_EX_TRANSPARENT 플래그
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
        }

        void ClickThroughfalse()
        {
            int wl = GetWindowLong(this.Handle, GWL.ExStyle); 
            wl = wl & ~(0x20); // 통과 값에서 반전시키고 & 연산으로 비트 제거  
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

        public class clickthrogh_state // clock 의 clickthrogh check 여부
        {
            public static bool ClickTS { get; set; } = false;
        }


        void ClickThroughfalse2()
        {
            int wl2 = GetWindowLong(form2.Handle, GWL.ExStyle);
            // 통과 값에서 반전시키고 & 연산으로 비트 제거 
            wl2 = wl2 & ~(0x20);
            SetWindowLong(form2.Handle, GWL.ExStyle, wl2);

        }

        int clickthr2 = 1;
        private void clockToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (form2.Visible == false)
            {
                clockToolStripMenuItem.Checked = false; // false 가 
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
                clickthrogh_state.ClickTS = true;// clock의 클릭 스루 기능이 작동 안된다면

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

        //슬라이딩 메뉴가 보이는/접히는 속도 조절
        const int STEP_SLIDING = 10;
        int _posSliding = 34;


        private void slidingTimer_Tick(object sender, EventArgs e)
        {
            //슬라이딩 메뉴가 보이는/접히는 속도 조절

            if (stopwatchToolStripMenuItem1.Checked == true)//
            {
                //슬라이딩 메뉴를 숨기는 동작
                _posSliding -= STEP_SLIDING;
                if (_posSliding <= 0)
                {
                    slidingTimer.Stop();
                }

            }
            else
            {
                //슬라이딩 메뉴를 보이는 동작
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
            public static int CT { get; set; } = 0; // 안누른상태 
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

                Clocktoolcheck.CT = 1; // true 히든이켜져서 가려짐

            }
            else
            {
                clockToolStripMenuItem1.Checked = false;//

                Clocktoolcheck.CT = 0;//false 보임
            }
            contextMenuStrip1.Visible = true;
        }

        //form1 panel6;
        private void mousetimer_Tick(object sender, EventArgs e) //마우스가 폼안에 있나 밖에 있나에 따라 페널을 없엠
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
