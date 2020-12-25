using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise2_051_MuhammadRifqiFirmansyah
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TampilData();
        }

        public class Mahasiswa
        {
            private string _nama, _nim, _prodi, _angkatan;
            public string nama
            {
                get { return _nama; }
                set { _nama = value; }
            }

            public string nim
            {
                get { return _nim; }
                set { _nim = value; }
            }

            public string prodi
            {
                get { return _prodi; }
                set { _prodi = value; }
            }

            public string angkatan
            {
                get { return _angkatan; }
                set { _angkatan = value; }
            }
        }


        public void TampilData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dataGridView1.DataSource = data;

        }
        public void SearchData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            string nim = textBoxNIM.Text;
            if (nim == null || nim == "")
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                var item = data.Where(x => x.nim == textBoxNIM.Text).ToList();

                dataGridView1.DataSource = item;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string baseurl = "http://localhost:1907/";
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Mahasiswa mhs = new Mahasiswa();
            mhs.nama = textBoxNama.Text;
            mhs.nim = textBoxNIM.Text;
            mhs.prodi = textBoxProdi.Text;
            mhs.angkatan = textBoxAngkatan.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var postdata = new WebClient();
            postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = postdata.UploadString(baseurl + "Mahasiswa", data);
            Console.WriteLine(response);
            TampilData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            if (MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string nim = textBoxNIM.Text;
                var item = data.Where(x => x.nim == textBoxNIM.Text).FirstOrDefault();
                if (item != null)
                {
                    data.Remove(item);
                    // Save everything
                    string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                    var postdata = new WebClient();
                    postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string response = postdata.UploadString(baseurl + "DeleteMahasiswa", output);
                    Console.WriteLine(response);
                    TampilData();
                }

            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            string nim = textBoxNIM.Text;
            var item = data.Where(x => x.nim == textBoxNIM.Text).FirstOrDefault();
            if (item != null)
            {
                // update logger with your textboxes data
                item.nama = textBoxNama.Text;
                item.nim = textBoxNIM.Text;
                item.prodi = textBoxProdi.Text;
                item.angkatan = textBoxAngkatan.Text;

                // Save everything
                string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                var postdata = new WebClient();
                postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = postdata.UploadString(baseurl + "UpdateMahasiswa", output);
                Console.WriteLine(response);
                TampilData();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxNama.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBoxNIM.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            textBoxProdi.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            textBoxAngkatan.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
        }

        private void buttonJumlah_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            int length = data.Count();
            labelTotal.Text = Convert.ToString(length);
        }
    }
}
