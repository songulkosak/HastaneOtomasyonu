using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HastaneOtomasyonu.Dal;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HastaneOtomasyonu
{
    public partial class DoktorRecete : Form
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        public DoktorRecete()
        {
            InitializeComponent();
        }
        public string DoktorAd;
        public string Brans;
        public string Hastane;
        public string DoktorTc;
        public string HastaAdSoyad;
        public string HastaTc;
        public string RandevuTarih;
        public string RandevuSaat;
        private string ReceteKodu;

        void Captcha_Oluşturma()
        {
            string[] sembol1 = { "a", "b", "c", "d", "e", "f", "g" };
            string[] sembol2 = { "+", "-", "/", "+", "&" };
            string[] sembol3 = { "A", "B", "C", "D", "E" };
            Random r = new Random();
            int s1, s2, s3, s4;
            s1 = r.Next(0, sembol1.Length);
            s2 = r.Next(0, sembol2.Length);
            s3 = r.Next(1, 10);
            s4 = r.Next(0, sembol3.Length);
            textBox1.Text = sembol1[s1].ToString() + sembol2[s2].ToString() + s3.ToString() + sembol3[s4].ToString();
        }
        private void DoktorRecete_Load(object sender, EventArgs e)
        {
            label2.Text = DoktorTc;
            ReceteKodu = Convert.ToString(textBox1.Text);
            LblHastaAd.Text = HastaAdSoyad;
            LblHastaTC.Text = HastaTc;
            Captcha_Oluşturma();

            //Kategoriler:
            CmbKatagoriler.Text = "";
            DataTable tbl= new DataTable();
            SqlDataAdapter Listele = new SqlDataAdapter("select TedaviAdi from Tedaviler", Baglan);
            Listele.Fill(tbl);
            CmbKatagoriler.ValueMember = "id";
            CmbKatagoriler.DisplayMember = "Adı";
            CmbKatagoriler.DataSource = tbl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label3.Text == "1")
            {
                SqlCommand ReceteOnay = new SqlCommand("insert into Recete (Tc,Hastane,Doktor,Brans,Recete,ReceteKodu,AdSoyad,Tarih,Saat) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9) ", Baglan);
                ReceteOnay.Parameters.AddWithValue("@p1", LblHastaTC.Text);
                ReceteOnay.Parameters.AddWithValue("@p2", Hastane);
                ReceteOnay.Parameters.AddWithValue("@p3", DoktorAd);
                ReceteOnay.Parameters.AddWithValue("@p4", Brans);
                ReceteOnay.Parameters.AddWithValue("@p5", richTextBox1.Text);
                ReceteOnay.Parameters.AddWithValue("@p6", textBox1.Text);
                ReceteOnay.Parameters.AddWithValue("@p7", LblHastaAd.Text);
                ReceteOnay.Parameters.AddWithValue("@p8", RandevuTarih);
                ReceteOnay.Parameters.AddWithValue("@p9", RandevuSaat);
                Baglan.Open();
                ReceteOnay.ExecuteNonQuery();
                MessageBox.Show("Recete Onaylandı. Recete Kodu:" + textBox1.Text + " dır.");
                label3.Text = "0";
            }
            if (label3.Text == "0")
            {
                richTextBox1.Enabled = false;
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
            Baglan.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Doktor Frm = new Doktor();
            Frm.Tc = DoktorTc;
            Frm.Show();
            this.Hide();
        }

        private void CmbKatagoriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ilaclar
            Cmbİlaclar.Items.Clear();
            SqlCommand Komut = new SqlCommand("select * from Tbl_Ilaclar where TedaviId=@p1", Baglan);
            Komut.Parameters.AddWithValue("@p1", CmbKatagoriler.SelectedIndex + 1);
            SqlDataReader dr = Komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbİlaclar.Items.Add(dr[1]);
            }
        }

        private void Cmbİlaclar_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = Cmbİlaclar.Text;
        }
    }
}
