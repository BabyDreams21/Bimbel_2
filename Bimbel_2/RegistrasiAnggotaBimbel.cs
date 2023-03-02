
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bimbel_2
{
    public partial class RegistrasiAnggotaBimbel : Form
    {
        //MySqlConnection conN = new MySqlConnection("Data Source = ;Initial Catalog = db;Integrated Security = true; ");
        public int harga;
        int id,idsiswa,idkelas,idpaket;
        int temp;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;

        public RegistrasiAnggotaBimbel()
        {
            InitializeComponent();
            disable();
           // loadgrid();
            loadangsuran();
          //  hidecell();
            loadsesi();
           // loadharga();
        }

        void hidecell()
        {
            dataGridView1.Columns[0].Visible= false;
            dataGridView1.Columns[1].Visible= false;
            dataGridView1.Columns[2].Visible= false;
            dataGridView1.Columns[3].Visible= false;
            dataGridView1.Columns[4].Visible= false;
        }

        bool numeric()
        {
          
                try
                {

                    int temp = Convert.ToInt32(textBox3.Text);
                    return true;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please provide number only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
            


        }

        bool val()
        {

            if (!utils.Valid(textBox3.Text))
            {
                MessageBox.Show("Ok");
            }
            if (textBox1.Text.Length < 1 || /*textBox2.Text.Length < 1 ||*/ textBox3.Text.Length < 1 || textBox5.Text.Length < 1 || textBox6.Text.Length < 1)
            {
                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                cmd = new SqlCommand("Select * from Pendaftaran where id_siswa = '" +idsiswa+ "'", con);
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
            comboBox2.ValueMember = "idjenisangsuran";
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

        void loadsesi()
        {
            string com = "SELECT * FROM Sesi ";
            comboBox3.ValueMember = "id";
            comboBox3.DisplayMember = "nama";
            comboBox3.DataSource = Command.GetData(com);
        }

        int loadharga()
        {
            int diskon = 0;
            diskon = Convert.ToInt32(textBox3.Text);
            int afterdis = harga - (harga * diskon / 100);
            textBox2.Text = afterdis.ToString();
            return afterdis;

            // textBox2.Text = pkt.harga_paket;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            utils.tanda = false;
            PilihSiswa plh = new PilihSiswa();
            plh.ShowDialog();
           // textBox1.Text = PilihSiswa.ambilidsiswa();
            textBox1.Text = plh.namasiswa;
            idsiswa = plh.idsiswa;
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            PilihPaket pkt = new PilihPaket();
            pkt.ShowDialog();
            textBox5.Text = pkt.nama_paket;
            harga = Convert.ToInt32(pkt.harga_paket);
            idpaket = pkt.getidpaket;
           // loadharga();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PilihKelas kls = new PilihKelas();
            kls.ShowDialog();
            textBox6.Text = kls.namakelas;
            idkelas = kls.getidkelas;
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //if (numeric())
            //{
            //    loadharga();
            //}
            if (!utils.Valid(textBox3.Text))
            {
                MessageBox.Show("Please provide number only!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                loadharga();
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected= true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            idsiswa = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            idkelas = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            idpaket = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value);
            comboBox2.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);
            comboBox3.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[14].Value);

            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[9].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            comboBox3.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[12].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[13].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cond = 2;
            enable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string com = "Delete  Pendaftaran where idpendaftaran=" + id;
            string mode = "Delete";
            CRUD.crud(com, mode);
            loadgrid();
            clear();
            enable();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cond == 1 && val() && numeric() )
            {
                cmd = new SqlCommand("Insert into Pendaftaran(id_siswa,id_paket,id_kelas,id_jenis_angsuran,id_sesi,tgl_registrasi,tipe,diskon,total_bayar)values(@siswa,@paket,@kelas,@jenis,@sesi,@regis,@tipe,@diskon,@total)", con);
                cmd.Parameters.AddWithValue("@siswa", idsiswa);
                cmd.Parameters.AddWithValue("@paket", idpaket);
                cmd.Parameters.AddWithValue("@kelas", idkelas);
                cmd.Parameters.AddWithValue("@jenis", comboBox2.SelectedValue);
                cmd.Parameters.AddWithValue("@sesi", comboBox3.SelectedValue);
                cmd.Parameters.AddWithValue("@regis", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@tipe", comboBox1.Text);
                cmd.Parameters.AddWithValue("@diskon", textBox3.Text);
                cmd.Parameters.AddWithValue("@total", textBox2.Text);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Succes");
                    clear();
                    loadgrid();
                    disable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }

                //string com = "INSERT INTO Pendaftaran values ('" + idsiswa + "','" + idpaket + "','" + idkelas + "','" + comboBox2.SelectedValue + "','"+ dateTimePicker1.Value + "','" + comboBox1.Text + "','" + comboBox3.Text + "','" + textBox3.Text + "','" + textBox2.Text + "')";

                //string mode = "Insert";
                //string pesan = "Insert";


                //CRUD.crud(com, mode, pesan);
                //cmd = new SqlCommand("INSERT INTO Pendaftaran values ('" + idsiswa + "','" + idpaket + "','" + idkelas + "','" + comboBox2.SelectedValue + "','" + dateTimePicker1.Value + "','" + comboBox1.Text + "','" + comboBox3.Text + "','" + textBox3.Text + "','" + textBox2.Text + "')", con);
                //try
                //{
                //    con.Open();
                //    cmd.ExecuteNonQuery();
                //    clear();
                //    loadgrid();
                //    disable();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(""+ex.Message);
                //}
                //finally { con.Close(); }

                //clear();
                //loadgrid();
                //disable();

            }
            else if ( cond == 2 && valup()&& numeric())
            {
              cmd  = new SqlCommand ("Update Pendaftaran set id_siswa ='"+idsiswa+"',id_paket ='"+idpaket+"',id_kelas ='"+idkelas+"',id_jenis_angsuran ='"+comboBox2.SelectedValue+"',tgl_registrasi =@date,tipe_bayar ='"+comboBox1.Text+ "',pilihan_hari ='" + comboBox3.SelectedValue+"',diskon ='"+textBox3.Text+"',total_bayar ='"+textBox2.Text+"'where idpendaftaran = '"+id+"' ",con);;
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Succes");
                }catch(Exception ex) { MessageBox.Show(ex.ToString()); 
                }finally { con.Close(); }   
               


                //CRUD.crud(com, mode, pesan);
                clear();
                loadgrid();
                disable();

            }
        }
    }
}
