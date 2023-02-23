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
    public partial class TambahAngsuran : Form
    {
        public TambahAngsuran()
        {
            InitializeComponent();
            loadgrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PilihSiswa siswa = new PilihSiswa();
            siswa.ShowDialog();
            textBox1.Text = siswa.namasiswa;
        }

        void loadgrid()
        {
            string com = "Select * from Histori_Angsuran";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}