using Org.BouncyCastle.Crypto.Agreement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimbel_2
{
    public partial class TambahAngsuran : Form
    {
        int lunas;
        SqlDataReader rd;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(utils.con);
        int id = 0;

        
        int idsiswa, idpendaftaran;
        int sisacil,besarcil,sisaba;
        string tanggal;
        public TambahAngsuran()
        {
            InitializeComponent();
            loadgrid();
            dataGridView1.Columns[0].Visible= false;
            dataGridView1.Columns[1].Visible= false;
            dataGridView1.Columns[8].Visible= false;
            
        }

        void clear()
        {
            textBox1.Clear();
            txtbesarcil.Clear();
            txtcicilanke.Clear();
            txtbanyak.Clear();
        }

        //int loadsisacicilan()
        //{
        //    string com = "Select * from Histori_Angsuran";
        //    Command.GetData(com);
        //    besarcil = Convert.ToInt32(txtbesarcil.Text);
        //    sisaba = Convert.ToInt32(txtbanyak.Text);
        //    sisacil = sisaba - besarcil; 
        //}


        int lunasa()
        {
            //cmd = new SqlCommand("Select * from Histori_Angsuran",con);
            //con.Open();
            //rd =  cmd.ExecuteReader();
            //rd.Read();
            //sisacil = rd.GetInt32(2);
           
            if (sisacil == 0)
            {
                 lunas = 1;
            }
            else
            {
                 lunas = 0;
            }
            return lunas;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            idpendaftaran = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            idsiswa = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[9].Value);
            sisacil = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);

            textBox1.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            txtbanyak.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            sisaba = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value);
            txtbesarcil.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            sisacil = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);
            txtcicilanke.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            utils.tanda = true;
            PilihSiswa siswa = new PilihSiswa();
            siswa.ShowDialog();
            textBox1.Text = siswa.namasiswa;
            idsiswa = siswa.idsiswa;
            idpendaftaran = siswa.idpendaftaran;
        }

        void loadgrid()
        {
            string com = "Select * from vw_cicil";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id== 0)
            {
                besarcil = Convert.ToInt32(txtbesarcil.Text);
                sisaba = Convert.ToInt32(txtbanyak.Text);
                sisacil = sisaba - besarcil;

                string com = "Insert into Histori_Angsuran values('" + idpendaftaran + "','" + txtcicilanke.Text + "','" + txtbesarcil.Text + "','" + sisacil + "','" + txtbanyak.Text + "','"+lunasa()+"',GETDATE()) ";

                string m = "Insert";
                string p = "Insert";
                CRUD.crud(com, m, p);
                loadgrid();
                clear();
            }
            else
            {
                besarcil = Convert.ToInt32(txtbesarcil.Text);
                sisaba = Convert.ToInt32(txtbanyak.Text);
                sisacil = sisaba - besarcil;
                sisaba = sisacil;
                string com = "update histori_angsuran set id_pendaftaran = '" + idpendaftaran + "',cicilan_ke = '" + txtcicilanke.Text + "',besar_cicilan = '"+besarcil+"',sisa_besar_cicilan ='"+sisacil+"',sisa_banyaknya_cicilan = '" + sisaba + "',sudah_lunas='"+lunasa()+"',tgl_bayar = getdate() where idhistoriangsuran = " + id;
                string mode = "Insert";
                string p = "Update";
                CRUD.crud(com, mode, p);
                loadgrid();
                clear();
            }






        }
    }
}