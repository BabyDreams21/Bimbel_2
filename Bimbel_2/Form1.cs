using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bimbel_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        async Task getAsync()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44309/api/GetAdmins");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            MyData[] myData = JsonConvert.DeserializeObject<MyData[]>(responseContent);
            dataGridView1.DataSource = myData;

        }



        private async void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                HttpClient htpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://ad1e-110-137-101-136.ap.ngrok.io/api/GetAdmins");
                HttpResponseMessage response = await client.SendAsync(request);
                string responseContent = await response.Content.ReadAsStringAsync();
                MyData[] myData = JsonConvert.DeserializeObject<MyData[]>(responseContent);
                dataGridView1.DataSource = myData;

            }
        }

        public class MyData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
