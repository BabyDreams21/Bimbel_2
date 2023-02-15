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
    public partial class MasterKelas : Form
    {
        int id;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;

        public MasterKelas()
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
                cmd = new SqlCommand("Select * from Guru where nama = '" + textBox1.Text + "'", con);
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
                cmd = new SqlCommand("Select * from Guru where nama = '" + textBox1.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    if (Convert.ToInt32(rd["idguru"]) != id)
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
            string com = "Select * from Kelas";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
     

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            // comboBox1.Text = "";
          
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            string com = "Select * from Kelas where nama like '%" + textBox4.Text + "%'";
            dataGridView1.DataSource = Command.GetData(com);
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
                MessageBox.Show("Please Select a Kelas!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string com = "Delete  Kelas where idkelas=" + id;
            string mode = "Delete";
            CRUD.crud(com, mode);
            clear();
            loadgrid();
            disable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cond == 1 && val())
            {
                string com = "Insert into Kelas values('" + textBox1.Text + "','" + textBox2.Text + "')";
                string mode = "Insert";
                string pesan = "Insert";
                CRUD.crud(com, mode, pesan);
                clear();
                loadgrid();
                disable();
            }
            else if (cond == 2 && valup())
            {


                string com = "Update Kelas set nama = '" + textBox1.Text + "',maksimal_siswa = '" + textBox2.Text + "'where idkelas = '" + id + "'";
                string mode = "Insert";
                string pesan = "Update";
                CRUD.crud(com, mode, pesan);
                clear();
                loadgrid();
                disable();
            }
        }
    }
}
