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
    public partial class PilihKelas : Form
    {
        public string kelas;
        public PilihKelas()
        {
            InitializeComponent();
            loadgrid();
        }

        void loadgrid()
        {
            string com = "Select * from Kelas";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            kelas = row.Cells["nama"].Value.ToString();
            dataGridView1.AutoSizeColumnsMode= DataGridViewAutoSizeColumnsMode.Fill;
            this.Close();
        }

        public string namakelas
        {
            get { return kelas; }
        }
    }
}
