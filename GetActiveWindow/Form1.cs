using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GetActiveWindow
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);

        public Form1()
        {
            InitializeComponent();

            this.TopMost = true;

            Timer tmr = new Timer();
            tmr.Interval = 1000;
            tmr.Tick += Tmr_Tick;
            tmr.Start();
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            //get title of active window
            string title = ActiveWindowTitle();
            //check if it is null and add it to list if correct
            if (title != "")
            {
                lbActiveWindows.Items.Add( DateTime.Now.ToString("hh:mm:ss") + " - " + title);
            }
        }


        private string ActiveWindowTitle()
        {
            //Create the variable
            const int nChar = 256;
            StringBuilder ss = new StringBuilder(nChar);
            
            //Run GetForeGroundWindows and get active window informations
            //assign them into handle pointer variable
            IntPtr handle = IntPtr.Zero;
            handle = GetForegroundWindow();

            if (GetWindowText(handle, ss, nChar) > 0) return ss.ToString();
            else return "";
        }

    }
}
