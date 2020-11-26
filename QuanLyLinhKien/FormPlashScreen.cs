using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace QuanLyLinhKien
{
    public partial class FormPlashScreen : Form
    {
        private Random r;
        public FormPlashScreen()
        {
            InitializeComponent();
            r = new Random();
            timerLoading.Start();
        }
        private void timerLoading_Tick(object sender, EventArgs e)
        {
            if (r.Next(0, 100) > 80)
            {
                timerLoading.Interval = 70;
            }
            else
            {
                timerLoading.Interval = 10;
            }
            circleLoading.Value += 1;
            if (circleLoading.Value == 100) this.Close();
        }

        private void FormPlashScreen_Load(object sender, EventArgs e)
        {
            pnTxt1.BackColor = pnTxt2.BackColor =  Color.FromArgb(120, 0, 0, 0);
        }
    }
}
