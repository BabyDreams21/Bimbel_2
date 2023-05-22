using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bimbel_2
{
    class utils
    {

        public static bool tanda = true ;
        public static string con = " Data Source = POSEIDON\\POSEIDON;Initial Catalog = bimbel_smknasional;Integrated Security = true;";


       public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, "^.+@.+\\.[a-zA-Z]{2,3}$");
        }
        public static void Email (string s)
        {
           // Match match = Re
        }
        public static bool Valid(string s)
        {
            
            if (s.All(char.IsNumber))
            {
                return true;

            }
            return false;
        }
    }

    

    class Command
    {
        public static DataTable GetData(String com)
        {
            //SqlConnection con = new SqlConnection(utils.con);

            //SqlCommand cmd = new SqlCommand(com, con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //return dt;

            SqlConnection connection = new SqlConnection(utils.con);
            SqlDataAdapter adapter = new SqlDataAdapter(com, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;

        }
        public static void Exec(String com)
        {
            SqlConnection con = new SqlConnection(utils.con);
            con.Open();
            SqlCommand cmd = new SqlCommand(com);
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
    }
    class Session
    {
        public static int ID { get; set; }
        public static int NIS { get; set; }
        public static string Nama { get; set; }
        public static String Email { get; set; }
        //ublic static int NIS { get; set; }
    }

    class CRUD
    {
        public static void crud(String com, String mode, [Optional] String pesan)
        {
            SqlConnection con = new SqlConnection(utils.con);
            SqlCommand cmd;
            try
            {
                con.Open();
                switch (mode)
                {
                    case "Insert":
                        cmd = new SqlCommand(com, con);
                        cmd.ExecuteNonQuery();
                        switch (pesan)
                        {
                            case "Insert":
                                MessageBox.Show("Data Succes Inserted !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case "Update":
                                MessageBox.Show("Data Succes Updated !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                        }
                        break;
                            case "Delete":
                        DialogResult result = MessageBox.Show("Are you sure to deleted it??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cmd = new SqlCommand(com, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data Succes Deleted !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        break;
                       
                }
            }catch(Exception ex)
            {
                MessageBox.Show(""+ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}
