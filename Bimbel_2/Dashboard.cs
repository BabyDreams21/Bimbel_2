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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Master_Data().Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new DataAngsuran().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new JadwalPelajaran().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new RegistrasiAnggotaBimbel().Show() ;
        }
    }
}
