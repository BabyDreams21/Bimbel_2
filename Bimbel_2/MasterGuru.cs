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
    public partial class MasterGuru : Form
    {
        int id;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;

        public MasterGuru()
        {
            InitializeComponent();
            loadguru();
            loadmapel();
            disable();
            label8.Text = Session.Nama;
        }

        bool val()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 )
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

        void loadguru()
        {
            string com = "Select * from view_guru";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void loadmapel()
        {
            string com = "Select * from Mata_Pelajaran";
            comboBox3.DataSource = Command.GetData(com);
            comboBox3.ValueMember = "idmatapelajaran";
            comboBox3.DisplayMember = "nama";
        }


        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
           // comboBox1.Text = "";
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

        private void button1_Click(object sender, EventArgs e)
        {
            cond = 1;
            enable();
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            if ( dataGridView1.CurrentRow.Selected == true)
            {
                cond = 2;
                enable();
            }
            else
            {
                MessageBox.Show("Please Select a Guru!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string com = "Delete  Guru where idguru=" + id;
            string mode = "Delete";
            CRUD.crud(com, mode);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            comboBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();



            comboBox3.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string com = "Select * from view_guru where nama like '%" + textBox4.Text + "%' or [Mata Pelajaran] like '"+textBox4.Text+"' ";
            dataGridView1.DataSource = Command.GetData(com);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cond == 1 && val())
            {
                string com = "Insert into Guru values('" + textBox1.Text + "','"+comboBox1.SelectedText+"','" + textBox2.Text + "','" + comboBox2.SelectedText + "','"+comboBox3.SelectedValue+"')";
                string mode = "Insert";
                string pesan = "Insert";
                CRUD.crud(com, mode, pesan);
                clear();
                loadguru();
                disable();

            }
            else if (cond == 2 && valup())
            {

               

                string com = "Update Guru set nama = '" + textBox1.Text + "',id_mata_pelajaran='"+comboBox3.SelectedValue+"',jenis_kelamin = '" + comboBox1.Text + "',no_telp = '"+textBox2.Text+"',lulusan = '"+comboBox2.Text+"' where idguru = '" + id + "'";
                string mode = "Insert";
                string pesan = "Update";
                CRUD.crud(com, mode, pesan);
                clear();
                loadguru();
                disable();

            }
        }
    }
}
