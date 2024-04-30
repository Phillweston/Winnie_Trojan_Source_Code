using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace 仙女病毒
{
    public partial class OVO : Form
    {
        public OVO()
        {
            InitializeComponent();
        }

        private void OVO_Load(object sender, EventArgs e)
        {
            int X = Screen.PrimaryScreen.Bounds.Width;
            int Y = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point((X - Size.Width) / 2, (Y - Size.Height) / 2);


        }
        public int Closeint = 0;
        private int WM_SYSCOMMAND = 0x112;
        private long SC_CLOSE = 0xF060;
        protected override void WndProc(ref Message m) //阻止窗口关闭
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt64() == SC_CLOSE)
                {
                    MessageBox.Show("大胆刁民，敢谋害朕", "大胆刁民，敢谋害朕");
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
