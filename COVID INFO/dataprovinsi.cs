using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace COVID_INFO
{
    public partial class dataprovinsi : Form
    {
        public dataprovinsi()
        {
            InitializeComponent();
            threading();
        }

        private void threading()
        {
            Thread ganteng = new Thread(grab);
            ganteng.Start();
        }

        private void threading2()
        {
            Thread ganteng = new Thread(grab2);
            ganteng.Start();
        }

        private void grab2()
        {
            TextBox.CheckForIllegalCrossThreadCalls = false;

            string url = "https://data.covid19.go.id/public/api/prov.json";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            Root json = JsonConvert.DeserializeObject<Root>(response.Content);

            foreach (var data in json.list_data)
            {
                //label1.Text = string.Format("LAST UPDATE     : {0}", json.last_date);

                //Thread.Sleep(100);

                /*
               
                */
                /*
                ListViewItem myitem = new ListViewItem(data.key);
                myitem.SubItems.Add(data.jumlah_meninggal.ToString());
                myitem.SubItems.Add(data.jumlah_kasus.ToString());
                listView1.Items.Add(myitem);
                */
                foreach (var jenis in data.Jenis_kelamin)
                {
                    textBox4.Text = jenis.doc_count.ToString();
                }
                textBox1.Text = (data.jumlah_meninggal.ToString());
                textBox2.Text = (data.jumlah_kasus.ToString());
                textBox3.Text = (data.jumlah_sembuh.ToString());
                foreach(var jenis in data.Jenis_kelamin)
                {
                    textBox5.Text = jenis.doc_count.ToString();
                    if(jenis.key.Contains("LAKI-LAKI")){
                        break;
                    }


                }
                if (data.key.Contains(guna2ComboBox1.Text))
                {
                    break;
                }

               


            }
        }
        private void grab()
        {
            TextBox.CheckForIllegalCrossThreadCalls = false;

            string url = "https://data.covid19.go.id/public/api/prov.json";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            Root json = JsonConvert.DeserializeObject<Root>(response.Content);

            foreach (var data in json.list_data)
            {
                //label1.Text = string.Format("LAST UPDATE     : {0}", json.last_date);

                //Thread.Sleep(100);

                /*
               
                */
                guna2ComboBox1.Items.Add(data.key);




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
            public string angka { get; set; }
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            threading2();
        }
    }
}
