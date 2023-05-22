using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimbel_2
{
     class Utils2
    {
        public static string conn2 = "Data Source = POSEIDON\\POSEIDON;Initial Catalog = backhotel;Integrated Security = true;";
        public static bool isvalid(string s)
        {
            if (s.All(char.IsNumber))
                {
                return true;

            }
            return false;
        }
        public static bool IsValidEmail1(string strIn)
        {
            //return Regex.IsMatch(s, "^.+@.+\\.[a-zA-Z]{2,3}$");
            return Regex.IsMatch(strIn, "^.+@.+\\.[a-zA-Z]{2,3}$");
        }

     
    }

   

    class CRUD2
    {
        public static void crud2(String com,String mode, [Optional]String pesan)
        {
            SqlConnection con = new SqlConnection(utils.con);
            SqlCommand cmd;
            try
            {
                con.Open();
                switch (mode) 
                {
                    case "insert":
                        cmd = new SqlCommand(com, con);
                        cmd.ExecuteNonQuery();
                        switch (pesan)
                        {
                            case "insert":
                                MessageBox.Show("Data succes inserted!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case "update":
                                MessageBox.Show("Data succes updated!!", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information );
                                break;
                        }
                        break;
                    case "delete":
                        DialogResult result = MessageBox.Show("Are you sure to deleted it???", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            cmd = new SqlCommand(com, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data succes deleted!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        break;
                      
                        
                }

            }catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }

    class command
    {
        public static DataTable getdata(string com)
        {
            SqlConnection con = new SqlConnection(utils.con);
            SqlDataAdapter da = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static void exec(string com)
        {
            SqlConnection con = new SqlConnection(utils.con);
            SqlCommand cmd = new SqlCommand(com, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }

    class sessions
    {
        public static string name { get; set; }
        public static int id { get; set; }
        public static string date { get; set; }
    }
}
