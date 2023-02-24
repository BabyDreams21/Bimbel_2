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
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rd;

        //private static string[] pelajaran = { "Ipa", "Ips", "Mtk","Inggris","Indonesia","Pkn" };
        //private static string[] Sesi1 = { "Senin", "Rabu", "Jumat" };
        //private static string[] Sesi2 = { "Selasa", "Kamis", "Sabtu" };
        //private static string[] jadwalSesi1 = new string[Sesi1.Length];
        //private static string[] jadwalSesi2 = new string[Sesi2.Length];
        //private static int lPelajaran = pelajaran.Length;
        //private static Dictionary<string,object> jadwal = new Dictionary<string,object>();
        //private static Dictionary<string,string> minggu = new Dictionary<string,string>();



        //void buatJadwal(string[] Sesi, string[] jadwal)
        //{
        //    for (int i = 0; i < Sesi.Length; i++)
        //    {
        //        Random rdn = new Random();
        //        int ang = rdn.Next(0, lPelajaran);
        //        string pelajaranAcak = pelajaran[ang];
        //        while (jadwal.Contains(pelajaranAcak))
        //        {
        //            rdn = new Random();
        //            ang = rdn.Next(0, lPelajaran);
        //            pelajaranAcak = pelajaran[ang];
        //        }
        //        jadwal[i] = pelajaranAcak;
        //        MessageBox.Show(pelajaranAcak);
        //    }
        //}

      

        public JadwalPelajaran()
        {
            InitializeComponent();
           // jadwal.Add("minggu1", minggu );
           //// jadwal["minggu1"].Add("senin", "ipa");
           // buatJadwal(Sesi1, jadwalSesi1);
            //buatJadwal(Sesi2, jadwalSesi2);
          //  MessageBox.Show(buatJadwal(Sesi1, jadwalSesi1));
        }

        void generate()
        {

        }

        void loadgrid()
        {
            int id  ;
            string[] sesi = { "Sesi 1", "Sesi 2" };
            string com = "SELECT * FROM view_jadwal WHERE id ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
        void loadadmin()
        {
            string com = "Select * from view_jadwal";
            dataGridView1.DataSource = Command.GetData(com);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            loadadmin();
        }
    }
}
