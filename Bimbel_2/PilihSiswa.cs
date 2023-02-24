using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimbel_2
{
    public partial class PilihSiswa : Form
    {
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        public static string ambilsiswa;

        public string siswa;
        public int idsiswa,idpendaftaran;

        
        public PilihSiswa()
        {
            InitializeComponent();
            loadgrid();
        }

        void loadgrid()
        {
            string com = "select * from vw_siswa";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string com = "Select * from vw_siswa where nama like '%" + textBox4.Text + "%' or NIS like '%" + textBox4.Text + "%'";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                idpendaftaran = Convert.ToInt32(row.Cells["idpendaftaran"].Value.ToString());
                siswa = row.Cells["nama"].Value.ToString();
                idsiswa = Convert.ToInt32(row.Cells["id_siswa"].Value.ToString());
                this.Close();
            }
            catch(Exception x) {
                MessageBox.Show(x.Message);
            }
        }

       public int getidpen
        {
            get
            {
                return idpendaftaran;
            }
        }

        public String namasiswa
        {
            get { return siswa; }
        }

        public int ambilidsiswa
        {
            get
            {
                return idsiswa;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
