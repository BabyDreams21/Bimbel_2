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
    public partial class MasterAdmin : Form
    {
        int id;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;
        public MasterAdmin()
        {
            InitializeComponent();
            loadadmin();
            disable();

          //  lbladmin.Text = Session.Email.ToString();
        }

        bool val()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox1.Text.Length < 3)
            {
                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }else if (textBox3.TextLength < 8)
            {
                MessageBox.Show("Password must be 8 character!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                cmd = new SqlCommand("Select * from Admin where email = '"+textBox2.Text+"'",con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if(rd.HasRows)
                {
                    MessageBox.Show("Email already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmd = new SqlCommand("Select * from Admin where email = '" + textBox2.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    MessageBox.Show("Email already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return false;
                }
                con.Close();
            }
            return true;
        }

        void loadadmin()
        {
            string com = "Select * from vw_admin";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
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
                textBox3.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please Select an Admin!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string com = "Delete  admin where id_admin=" + id;
            string mode = "Delete";
            CRUD.crud(com, mode);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cond == 1 && val())
            {
                string com = "Insert into Admin values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                string mode = "Insert";
                string pesan = "Insert";
                CRUD.crud(com,mode,pesan);
                clear();
                loadadmin();
                disable();

            }
            else if (cond == 2 && valup())
            {

                textBox3.Enabled = true;

                string com = "Update admin set nama = '"+textBox1.Text+"',email = '"+textBox2.Text+"'where id_admin = '"+id+"'";
                string mode = "Insert";
                string pesan = "Insert";
                CRUD.crud(com, mode, pesan);
                clear();
                loadadmin();
                disable();

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected= true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
           // textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox3.UseSystemPasswordChar = true;
            }
            else
            {
                textBox3.UseSystemPasswordChar= false;
            }
        }
    }
}
