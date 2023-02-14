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
    public partial class Dashboard_Siswa : Form
    {
        public Dashboard_Siswa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AngsuranSiswa().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Jadwal_Siswa().Show();
        }
    }
}
