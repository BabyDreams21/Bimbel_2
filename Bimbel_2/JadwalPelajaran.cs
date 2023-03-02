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
    public partial class JadwalPelajaran : Form
    {
        SqlConnection con = new SqlConnection(utils.con);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rd;

        int idguru, idsiswa, idmapel;
        string namaguru, namasiswa, namamapel;

        private string[] guru1;







        private static string[] pelajaran1 = { "Ipa", "Ips", "Mtk", "Inggris", "Indonesia", "Pkn" };
        private static string[] Sesi1 = { "Senin", "Rabu", "Jumat" };
        private static string[] Sesi2 = { "Selasa", "Kamis", "Sabtu" };
        private static string[] jadwalSesi1 = new string[Sesi1.Length];
        private static string[] jadwalSesi2 = new string[Sesi2.Length];
        private static int lPelajaran = pelajaran1.Length;
        private static Dictionary<string, object> jadwal = new Dictionary<string, object>();
        private static Dictionary<string, string> minggu = new Dictionary<string, string>();

        public JadwalPelajaran()
        {
            InitializeComponent();
            guru();
          
            // jadwal.Add("minggu1", minggu );
            //// jadwal["minggu1"].Add("senin", "ipa");
            // buatJadwal(Sesi1, jadwalSesi1);
            //buatJadwal(Sesi2, jadwalSesi2);
            //  MessageBox.Show(buatJadwal(Sesi1, jadwalSesi1));
        }

        void buatjadwal(string[] sesi, string[] jadwal)
        {
            for (int i = 0; i < sesi.Length; i++)
            {
                Random rdn = new Random();
                int ang = rdn.Next(0, lPelajaran);
                string pelajaranacak = pelajaran1[ang];
                while (jadwal.Contains(pelajaranacak))
                {
                    rdn = new Random();
                    ang = rdn.Next(0, lPelajaran);
                    pelajaranacak = pelajaran1[ang];
                }
                jadwal[i] = pelajaranacak;
                MessageBox.Show(pelajaranacak);
            }
        }



    

        void pelajaran()
        {
            // string com = "select * from Mata_Pelajaran";
            cmd = new SqlCommand("select * from Mata_Pelajaran where = 'belum'", con);
            con.Open();
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                idmapel = Convert.ToInt32(rd[0]);
                namamapel = rd[0].ToString();
            }
            con.Close();
        }

        void guru()
        {
            string[]guru1;
            cmd = new SqlCommand("select nama from Guru", con);
            con.Open();
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                idguru = Convert.ToInt32(rd[0]);
                
            }
            con.Close();
        }

        void siswa()
        {
            cmd = new SqlCommand("select * from Siswa", con);
            con.Open();
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                idsiswa = Convert.ToInt32(rd[0]);
                namasiswa = rd[1].ToString();
            }
            con.Close();
        }

        void generate()
        {
            cmd = new SqlCommand("select * from Mata_Pelajaran where keterangan !=1  LIMIT 1 ");
           
        }

        void loadgrid()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            int id  ;
            string[] sesi = { "Sesi 1", "Sesi 2" };
            string com = "SELECT * FROM view_jadwal WHERE id ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                application.Application.Workbooks.Add(Type.Missing);
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    application.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        application.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }

                application.Columns.AutoFit();
                application.Visible = true;
            }
        }
        void loadadmin()
        {
            string com = "Select * from view_jadwal";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void sesi1()
        {
            string com = " select * from ";
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            loadadmin();
        }
    }
}
