using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            dataGridView1.Columns[0].Visible= false;
            dataGridView1.Columns[1].Visible= false;
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
            comboBox1.DisplayMember = "nama";
            comboBox1.ValueMember = "idpaket";
            comboBox1.DataSource = Command.GetData(com);
        }

        void loaddetail()
        {
            SqlCommand command = new SqlCommand("select * from Paket where idpaket = " + comboBox1.SelectedValue, con);
            con.Open();
            rd = command.ExecuteReader();
            rd.Read();
            textBox7.Text = rd.GetInt32(2).ToString();
            //textBox2.Text = (Convert.ToInt32(textBox1.Text) * numericUpDown1.Value).ToString();
            con.Close();

        }

        void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
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
                string com = "insert into Jenis_Angsuran values ('"+comboBox1.SelectedValue+"','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                string mode = "Insert";
                string pesan = "Insert";
                CRUD.crud(com,mode, pesan);
                loadgrid();

                enable();
                clear();
            }
            else if (cond == 2 && valup())
            {
                string com = "update Jenis_Angsuran set nama ='"+textBox1.Text+"',keterangan = '"+textBox2.Text+"',dp = '"+textBox3.Text+ "',besar_cicilan ='"+textBox5.Text+"',banyaknya_cicilan = '"+textBox6.Text+"'";
                string mode = "Insert";
                string pesan = "Update";
                CRUD.crud(com, mode, pesan);
                loadgrid();

                enable();
                clear();
            }
            

            
        }

       

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // loadharga();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaddetail();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;

            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.SelectedValue = Convert.ToUInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
