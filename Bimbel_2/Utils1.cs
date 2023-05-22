
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimbel_2
{
     class Utils1
    {
        public static string con = " data source = ; Initial Catalog = ; Integrated Security = ;";

        public static bool IsValid(string s)
        {
            if (s.All(char.IsNumber))
            {
                return true;
            }
            return false;
        }
    }

    class CRUD1
    {
        public static void Crud(String com,String mode, [Optional] String pesan)
        {
            switch (mode)
            {
                case "Insert":
                    {
                        SqlConnection con = new SqlConnection(utils.con);
                        SqlCommand cmd = new SqlCommand(com, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        switch (pesan)
                        {
                            case "Insert":
                                MessageBox.Show("Data succes inserted !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case "Update":
                                MessageBox.Show("Data succes updated !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                        }
                        break;
                    }
                case "delete":
                    {
                        DialogResult dialog = MessageBox.Show("Are you sure to deleted it???", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(dialog== DialogResult.Yes)
                        {
                            SqlConnection con = new SqlConnection(utils.con);
                            SqlCommand cmd = new SqlCommand(com, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Data succes deleted!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                       
                        
                    }
            }
        }
    }

    class Command1 {
       public static DataTable GetData (string com)
        {
            SqlConnection con = new SqlConnection(utils.con);
            SqlDataAdapter da = new SqlDataAdapter(com, con);
            DataTable dataTable= new DataTable();
            da.Fill(dataTable);
            return dataTable;

        }

        public static void Exec1(String com)
        {
            SqlConnection con = new SqlConnection(utils.con);
            SqlCommand cmd = new SqlCommand(com, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }


    class Session1
    {
        public static int id { get; set; }
        public static string name { get; set; }
        public static string name1 { get; set; }
    }

}
