using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace Bimbel_2
{
    public partial class MasterSiswa : Form
    {
        int id;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;

        public MasterSiswa()
        {
            InitializeComponent();
            loadgrid();
            disable();
        }

        bool val()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1)
            {
                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                cmd = new SqlCommand("Select * from Siswa where NIS = '" + textBox1.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    MessageBox.Show("NIS already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmd = new SqlCommand("Select * from Siswa where NIS = '" + textBox1.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    if (Convert.ToInt32(rd["NIS"]) != id)
                    {
                        MessageBox.Show("NIS already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string com = "Select * from Siswa";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

       

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image = null;
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

        private void UpdateDataGridView()
        {
            string selectedItem = cbFilter.SelectedItem.ToString();

          
        }
        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string com = "Select * from Siswa where NIS like '%" + textBox4.Text + "%' or nama like '%"+textBox4.Text+"%'";
            dataGridView1.DataSource = Command.GetData(com);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string com = "Delete  Siswa where id_siswa=" + id;
            string mode = "Delete";
            CRUD.crud(com, mode);
            clear();
            loadgrid();
            disable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cond = 1;
            enable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected == true)
            {
                cond = 2;
                enable();
            }
            else
            {
                MessageBox.Show("Please Select a Siswa!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[5].Value);
            if (dataGridView1.SelectedRows[0].Cells[6].Value != System.DBNull.Value)
            {
                byte[] b = (byte[])dataGridView1.SelectedRows[0].Cells[6].Value;
                MemoryStream stream = new MemoryStream(b);
                Image img = Image.FromStream(stream);
                Bitmap bmp = (Bitmap)img;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.Image = null;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cond == 1 && val())
            {
                ImageConverter convert = new ImageConverter();
                byte[] b = (byte[])convert.ConvertTo(pictureBox1.Image,typeof(byte[]));

                cmd  = new SqlCommand("Insert into Siswa values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"+comboBox1.Text+"','"+dateTimePicker1.Value+"',@foto)",con);
                cmd.Parameters.AddWithValue("@foto", b);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Succes Inserted !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    loadgrid();
                    disable();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    con.Close();
                }    
                

            }
            else if (cond == 2 && valup())
            {


                ImageConverter convert = new ImageConverter();
                byte[] b = (byte[])convert.ConvertTo(pictureBox1.Image, typeof(byte[]));

                cmd = new SqlCommand("Update Siswa set NIS = '"+textBox1.Text+"',nama = '"+textBox2.Text+"',alamat = '"+textBox3.Text+"',jenis_kelamin = '"+comboBox1.Text+"',tanggal_lahir = '"+dateTimePicker1.Value+"',foto = @foto ",con);
                cmd.Parameters.AddWithValue("@foto", b);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Succes Updated !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    loadgrid();
                    disable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.png;*.jpg;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                Bitmap bmp = (Bitmap)img;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Nama");
            comboBox1.Items.Add("NIS");
          //  comboBox1.Items.Add("Jurusan");

            //filter();
        }
    }
}
