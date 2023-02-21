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
    public partial class RegistrasiAnggotaBimbel : Form
    {
        int id;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;

        public RegistrasiAnggotaBimbel()
        {
            InitializeComponent();
            disable();
            loadgrid();
            loadangsuran();
        }

        bool val()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox3.Text.Length < 1 || textBox5.Text.Length < 1 || textBox6.Text.Length < 1)
            {
                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                cmd = new SqlCommand("Select * from Pendaftaran where idpendaftaran = '" +id+ "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    MessageBox.Show("Name already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                con.Close();

            }
            return true;
        }

        bool valup()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox1.Text.Length < 3)
            {
                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                cmd = new SqlCommand("Select * from Pendaftaran where idpendaftaran = '" +id+"'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    if (Convert.ToInt32(rd["idpendaftaran"]) != id)
                    {
                        MessageBox.Show("Name already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        return false;
                    }

                }
                con.Close();
            }
            return true;
        }

        void loadgrid()
        {
            string com = "Select * from vw_anggotabimbel";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void loadangsuran()
        {
            string com = "Select * from Jenis_Angsuran";
            comboBox2.DisplayMember = "nama";
            comboBox2.ValueMember = "idmatapelajaran";
            comboBox2.DataSource = Command.GetData(com);

        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
             comboBox1.Text = "";
             comboBox2.Text = "";
             comboBox3.Text = "";

        }

        void enable()
        {
            groupBox1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
        }

        void disable()
        {
            groupBox1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
        }

        void loadharga()
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            PilihSiswa plh = new PilihSiswa();
            plh.ShowDialog();
           // textBox1.Text = PilihSiswa.ambilidsiswa();
            textBox1.Text = plh.namasiswa;
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            PilihPaket pkt = new PilihPaket();
            pkt.ShowDialog();
            textBox5.Text = pkt.nama_paket;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PilihKelas kls = new PilihKelas();
            kls.ShowDialog();
            textBox6.Text = kls.namakelas;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string com = "Select * from vw_anggotabimbel where [Nama siswa] like '%" + textBox4.Text+"%'";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cond = 1;
            enable();

        }
    }
}
