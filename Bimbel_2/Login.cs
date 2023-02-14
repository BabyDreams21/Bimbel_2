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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;
        public Login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false )
            {
                textBox2.UseSystemPasswordChar= true;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {

                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmd = new SqlCommand("select * from Admin where email = '" + textBox1.Text + "'and password = '" + textBox2.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();

                if (rd.HasRows == false)
                {
                    con.Close();
                    con.Open();
                    cmd = new SqlCommand("select * from Siswa where nis = '" + textBox1.Text + "'", con);

                    rd = cmd.ExecuteReader();
                    rd.Read();
                    if (rd.HasRows)
                    {
                        Session.ID = Convert.ToInt32(rd[0]);
                        Session.NIS = Convert.ToInt32(rd[1]);
                        Session.Nama = (rd[2]).ToString();

                        new Dashboard_Siswa().Show();
                        this.Hide();
                        con.Close();

                    }
                    else
                    {
                        MessageBox.Show("Check your username or password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (rd.HasRows == true) 
                {

                    Session.ID = Convert.ToInt32(rd[0]);
                    Session.Nama = (rd[1]).ToString();
                    Session.Email = (rd[2]).ToString();
                    new Dashboard().Show();
                    this.Hide();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Check your username or password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }
    }
}
