using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Threading;
using RestSharp;
using System.Text.RegularExpressions;
namespace COVID_INFO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            threading();
        }

        private void threading()
        {
            Thread orangganteng = new Thread(gasken);
            orangganteng.Start();
        }


        private void gasken()
        {
            TextBox.CheckForIllegalCrossThreadCalls = false;

            string[] information = { "COVID-19 is the infectious disease caused by the most recently",
                                     "discovered coronavirus. This new virus and disease were unknown",
                                     "before the outbreak began in Wuhan, China, in December 2019."};

   
            string url = "https://data.covid19.go.id/public/api/prov.json";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            Root json = JsonConvert.DeserializeObject<Root>(response.Content);

            foreach (var data in json.list_data)
            {
                //label1.Text = string.Format("LAST UPDATE     : {0}", json.last_date);
                textBox1.Text = string.Format("{0}", json.last_date);
                textBox2.Text = string.Format("{0}", data.jumlah_kasus);
                textBox3.Text = string.Format("{0}", data.jumlah_sembuh);
                textBox4.Text = string.Format("{0}", data.jumlah_meninggal);
                Thread.Sleep(100);

            }


            string regex = "<div>Positif<br><strong>(.*?)</strong></div>";
            var client2 = new WebClient();
            var requests = client2.DownloadString("https://www.covid19.go.id/");

            Regex rx = new Regex(regex);
            foreach (Match match in rx.Matches(requests))
            {
                string grab = (match.Groups[1].Value.ToString());
                textBox2.Text = grab;
               
            }

            string regex2 = "<div>Sembuh<br><strong>(.*?)</strong></div>";
            Regex rx2 = new Regex(regex2);
            foreach(Match match2 in rx2.Matches(requests))
            {
                string grab = (match2.Groups[1].Value.ToString());
                textBox3.Text = grab;
            }

            string regex3 = "<div>Meninggal<br><strong>(.*?)</strong></div>";
            Regex rx3 = new Regex(regex3);
            foreach (Match match2 in rx3.Matches(requests))
            {
                string grab = (match2.Groups[1].Value.ToString());
                textBox4.Text = grab;
            }



        }

        public class Jeniskelamin
        {
            public string key { get; set; }
            public int doc_count { get; set; }

        }

        public class Usia
        {
            public double value { get; set; }

        }

        public class KelompokUmur
        {
            public string key { get; set; }
            public int doc_count { get; set; }
            public Usia usia { get; set; }


        }


        public class lokasi
        {
            public double lon { get; set; }
            public double lat { get; set; }

        }

        public class Penambahan
        {
            public int positif { get; set; }
            public int sembuh { get; set; }
            public int meninggal { get; set; }

        }

        public class listData
        {
            public string key { get; set; }
            public double doc_count { get; set; }
            public int jumlah_kasus { get; set; }
            public int jumlah_sembuh { get; set; }
            public int jumlah_meninggal { get; set; }
            public int jumlah_dirawat { get; set; }
            public List<Jeniskelamin> Jenis_kelamin { get; set; }
            public List<KelompokUmur> Kelompok_umur { get; set; }
            public lokasi Lokasi { get; set; }
            public Penambahan penambahan { get; set; }

        }

        public class Root
        {
            public string last_date { get; set; }
            public double current_data { get; set; }
            public double missing_data { get; set; }
            public int tanpa_provinsi { get; set; }
            public List<listData> list_data { get; set; }
        }









        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
      );
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < 100; i++)
            {
                backgroundWorker1.ReportProgress(i);
                System.Threading.Thread.Sleep(100);
            }

        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private Form activeForm = null;

        private void formkecil(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2ShadowPanel1.Controls.Add(childForm);
            guna2ShadowPanel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            formkecil(new dataprovinsi());

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            formkecil(new Formawal());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            formkecil(new About());
        }
    }
}
