using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using System.Linq;

namespace Bimbel_2
{
    public partial class Form1 : Form
    {
        private string captchaCode;
        public Form1()
        {
            InitializeComponent();

           // captcha1();
            //  generateCaptchaCode();
            //var code = GenerateRandomCode(6);
            //captchaCode = GenerateRandomCode(6);
            //GenerateCaptchaImage(captchaCode);


        }

        private Bitmap CaptchaToImage(string text, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            SolidBrush sb = new SolidBrush(Color.White);
            g.FillRectangle(sb, 0, 0, bmp.Width, bmp.Height);
            Font font = new Font("Tahoma", 45);
            sb = new SolidBrush(Color.Black);
            g.DrawString(text, font, sb, bmp.Width / 2 - (text.Length / 2) * font.Size, (bmp.Height / 2) - font.Size);
            int count = 0;
            Random rand = new Random();
            while (count < 1000)
            {
                sb = new SolidBrush(Color.YellowGreen);
                g.FillEllipse(sb, rand.Next(0, bmp.Width), rand.Next(0, bmp.Height), 4, 2);
                count++;
            }
            count = 0;
            while (count < 25)
            {
                g.DrawLine(new Pen(Color.Bisque), rand.Next(0, bmp.Width), rand.Next(0, bmp.Height), rand.Next(0, bmp.Width), rand.Next(0, bmp.Height));
                count++;
            }
            return bmp;
        }

        public string RandomString()
        {
            Random rnd = new Random();
            int number = rnd.Next(10000, 99999);
            return MD5(number.ToString()).ToUpperInvariant().Substring(0, 6);
        }
        public string MD5(string input)
        {
            using (System.Security.Cryptography.MD5CryptoServiceProvider cryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] hashedBytes = cryptoServiceProvider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void GenerateCaptchaImage(string code)
        {
            var bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            var font = new Font("Arial", 20);
            var brush = new SolidBrush(Color.Black);
            g.DrawString(code, font, brush, 10, 10);
            this.pictureBox1.Image = bmp;
        }

        private string generateCaptchaCode(int charCount)
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < charCount; i++)
            {
                int a = r.Next(3);
                int char1;
                switch (a)
                {
                    case 1:
                        char1 = r.Next(0, 9);
                        s = s + char1.ToString();
                        break;
                    case 2:
                        char1 = r.Next(65, 90);
                        s = s + Convert.ToChar(char1).ToString();
                        break;
                    case 3:
                        char1 = r.Next(97, 122);
                        s = s + Convert.ToChar(char1).ToString();
                        break;
                }
            }
            return s;
           label2.Text = s.ToString();
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



        private  void button1_Click(object sender, EventArgs e)
        {
            var code = this.textBox1.Text;
            if (code == captchaCode)
            {
                MessageBox.Show("CAPTCHA code is correct!");
            }
            else
            {
                MessageBox.Show("CAPTCHA code is incorrect. Please try again.");
                captchaCode = GenerateRandomCode(6);
                GenerateCaptchaImage(captchaCode);
                this.textBox1.Text = "";
            }
            //using (var client = new HttpClient())
            //{
            //    HttpClient htpClient = new HttpClient();
            //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://ad1e-110-137-101-136.ap.ngrok.io/api/GetAdmins");
            //    HttpResponseMessage response = await client.SendAsync(request);
            //    string responseContent = await response.Content.ReadAsStringAsync();
            //    MyData[] myData = JsonConvert.DeserializeObject<MyData[]>(responseContent);
            //    dataGridView1.DataSource = myData;

            //}

            //if (textBox1.Text == captcha)
            //{
            //    MessageBox.Show("You are human!");
            //}
            //else
            //{
            //    MessageBox.Show("You are a bot!");
            //}




            //if (utils.Valid(textBox1.Text))
            //{
            //    MessageBox.Show("nomor valid");
            //}
            //MessageBox.Show("nomor tidak valid");
            // pictureBox1.Image = CaptchaToImage(textBox1.Text, pictureBox1.Width, pictureBox1.Height);
            // pictureBox1.Image = CaptchaToImage(RandomString(), pictureBox1.Width, pictureBox1.Height);
        }

        public class MyData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!utils.Valid(textBox1.Text))
            {
                label1.Text = "harus berisi nomor";
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }
           
           // MessageBox.Show("nomor tidak valid");
        }


        void captcha1()
        {
            Random random = new Random();
            int num = random.Next(6, 8);
            string captcha = "";

            int total = 0;
            do
            {
                int chr = random.Next(48, 123);
                if ((chr >=48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    captcha = captcha + (char)chr;
                    total++;
                    if (total == num)
                        break;
                }
            } while (true);
            

            textBox3.Text = captcha.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            captcha1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (utils.IsValidEmail(textBox4.Text))
            {
                MessageBox.Show("benar");
            }
            else
            {
                MessageBox.Show("Salah");
            }
        }
    }
}
