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
    public partial class KategoriAngsuran : Form
    {
        int id;
        int cond = 0;
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd;
        SqlDataReader rd;

        public KategoriAngsuran()
        {
            InitializeComponent();
            loadgrid();
            loadcombo();
            disable();
        }

        bool val()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox3.Text.Length < 1 ||  textBox5.Text.Length < 1 || textBox6.Text.Length < 1)
            {
                MessageBox.Show("All field must be filled!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                cmd = new SqlCommand("Select * from Jenis_Angsuran where Nama = '" + textBox1.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    MessageBox.Show("Nama already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmd = new SqlCommand("Select * from Jenis_Angsuran where Nama = '" + textBox1.Text + "'", con);
                con.Open();
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    if (Convert.ToInt32(rd["Nama"]) != id)
                    {
                        MessageBox.Show("Nama already used!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string com = "Select * from vw_kategoriangsuran";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void loadcombo()
        {
            string com = "Select * from Paket";
            comboBox1.DataSource= Command.GetData(com);
            comboBox1.ValueMember = "idpaket";
            comboBox1.DisplayMember = "nama";
            
        }

        void loadharga()
        {
            con.Open();
            int harga;
            cmd = new SqlCommand("select * from Paket", con);
            rd= cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                for(int i = 0; i < rd.; i++)
                {
                    harga = Convert.ToInt32(rd[2]);
                    textBox7.Text = harga.ToString();
                }
                
            }
           
            con.Close();
        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.Text = "";
           
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
            string com = " select * from vw_kategoriangsuran ";
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
                MessageBox.Show ("Please select a Angsuran!!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string com = "Delete Jenis_Angsuran where idjenisangsuran =" + id;
            string m = "Delete";
            CRUD.crud(com, m);
            disable();
            clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cond == 1 && val())
            {
                string com = "insert into Jenis_Angsuran values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                string mode = "Insert";
                string pesan = "Insert";
                CRUD.crud(com,mode, pesan);
                enable();
                clear();
            }
            else if (cond == 2 && val())
            {
                string com = "update Jenis_Angsuran set nama ='"+textBox1.Text+"',keterrangan = '"+textBox2.Text+"',dp = '"+textBox3.Text+ "',besar_cicilan ='"+textBox5.Text+"',banyaknya_cicilan = '"+textBox6.Text+"'";
                string mode = "Insert";
                string pesan = "Update";
                CRUD.crud(com, mode, pesan);
                enable();
                clear();
            }
            

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadharga();
        }
    }
}
