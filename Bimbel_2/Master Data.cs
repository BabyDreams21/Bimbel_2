using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimbel_2
{
    public partial class Master_Data : Form
    {
        public Master_Data()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MasterAdmin().Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MasterGuru().Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new MasterSiswa().Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new MasterKelas().Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            new MasterPaketBimbel().Show();

        }
    }
}
