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
    public partial class PilihPaket : Form
    {
        public PilihPaket()
        {
            InitializeComponent();
            loadgrid();
        }

        public string paket;
        public string harga;
        public int idpaket;

        void loadgrid()
        {
            string com = "select * from Paket";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string com = "Select * from Paket where nama like '%" + textBox4.Text + "%' or harga like '%" + textBox4.Text + "%'";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            paket = row.Cells["nama"].Value.ToString();
            harga = row.Cells["harga"].Value.ToString();
            idpaket = Convert.ToInt32(row.Cells["idpaket"].Value.ToString());

            this.Close();


        }

        public String nama_paket
        {
            get
            {
                return paket;
            }
        }

        public String harga_paket
        {
            get
            {
                return harga;
            }
        }

        public int getidpaket
        {
            get
            {
                return idpaket;
            }
        }
    }
}
