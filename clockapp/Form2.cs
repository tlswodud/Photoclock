//using ExCSS;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls.Ribbon;
using System.Windows.Documents;
using System.Windows.Forms;
using calendar;
using clockapp;
using FontAwesome.Sharp;

namespace clockapp
{

    public partial class Form2 : Form
    {
        private Stopwatch stopwatch;
        private Size formSize;
        private int borderSize = 5;
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

            label1.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            this.TopMost = true;
            // trackBar2.BackColor = Color.Transparent;
            textBox1.Visible = false;
            CollapsMenu();
            this.Padding = new Padding(borderSize);
            // this.BackColor = Color.FromArgb(98, 102, 244); // border 색상
            panel5.Visible = false;
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

        protected override void WndProc(ref Message m)
        {
            const int WM_SETREDRAW = 0x0083;
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>

            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST

                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;

                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            if (m.Msg == WM_SETREDRAW && m.WParam.ToInt32() == 1)
            {
                return;
            }
            if (m.Msg == WM_SYSCOMMAND)
            {

                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stopwatch = new Stopwatch();

            this.Width = Properties.Settings.Default.FormWidth2;
            this.Height = Properties.Settings.Default.FormHeight2;
            BackgroundfilePath = Properties.Settings.Default.backgroundimg2;

            this.BackColor = Properties.Settings.Default.bordercolor2;
            this.panel3.BackColor = Properties.Settings.Default.panelcolor2;
            this.panel5.BackColor = Properties.Settings.Default.panelcolor2;
            label1.ForeColor = Properties.Settings.Default.label1color2;
            label3.ForeColor = Properties.Settings.Default.label3color2;
            this.Location = Properties.Settings.Default.form2location;

            label1.Font = Properties.Settings.Default.form2label1font;
            label3.Font = Properties.Settings.Default.form2label3font;
            label1.Location = Properties.Settings.Default.label1location2;
            label3.Location = Properties.Settings.Default.label3location2;

            trackBar2.BackColor = Properties.Settings.Default.track2;
            trackBar2.Visible = Properties.Settings.Default.track2visable;

            this.Opacity = Properties.Settings.Default.opa2;

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


        private void iconButton4_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString("hh:mm:ss");

        }

        private void trackbutton_Click(object sender, EventArgs e)
        {
            if (trackBar2.Visible == true)
            {
                trackbutton.IconChar = FontAwesome.Sharp.IconChar.Egg;
                trackBar2.Visible = false;
            }
            else
            {
                trackbutton.IconChar = FontAwesome.Sharp.IconChar.Dove;
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
            }
            else if (colorcnt == 1)
            {
                panel3.BackColor = Color.LightSkyBlue;
                panel5.BackColor = Color.LightSkyBlue;
                this.BackColor = Color.DeepSkyBlue;
                trackBar2.BackColor = Color.PaleTurquoise;

            }
            else if (colorcnt == 0)
            {
                panel3.BackColor = Color.LightGray;
                panel5.BackColor = Color.LightGray;
                this.BackColor = Color.Gray;

                trackBar2.BackColor = Color.LightGray;

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

       
    }
}


