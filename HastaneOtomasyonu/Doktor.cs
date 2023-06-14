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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaneOtomasyonu
{
    public partial class Doktor : Form
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        public Doktor()
        {
            InitializeComponent();
        }

        //özel(private)  tanımlı değişkenlerin kullanımı 
        public string Tc;
        public string Ad_Soyad;
        public string hastane;
        public string Tarih;

        public Doktor(string Tc,string Ad_Soyad,string hastane,string tarih)
        { //Kullanılan değişkenlere ilk değer ataması
            this.Tc=Tc;
            this.Ad_Soyad=Ad_Soyad;
            this.hastane=hastane;
            this.Tarih=tarih;
        }
        
        
        void Hastalistesi()
        {
          
                da = new SqlDataAdapter("select * from Randevu", Baglan);
                DataTable tbl = new DataTable();
                Baglan.Open();
                da.Fill(tbl);
                dataGridView1.DataSource = tbl;
                Baglan.Close();    

        }

        void DoktorBilgi()
        {
            cmd = new SqlCommand("select *from Doktor where Tc=@tc",Baglan);
            cmd.Parameters.AddWithValue("@tc", Tc);
            //SqlCommand DoktoBilgi = new SqlCommand("select * From Doktor where Tc=@p1", Baglan);
            //DoktoBilgi.Parameters.AddWithValue("@p1", Tc);
            //Baglan.Open();
            //SqlDataReader dr = DoktoBilgi.ExecuteReader();
            //while (dr.Read())
            //{
            //    LblAdSoyad.Text = dr[1] + " " + dr[2].ToString();
            //    label2.Text = dr[7].ToString();
            //    label4.Text = dr[6].ToString();
            //}
            //Baglan.Close();
        }

        private void Doktor_Load(object sender, EventArgs e)
        {
            label2.Text = hastane;
            LblTc.Text = Tc;
            LblTarih.Text = dateTimePicker1.Text;
            DoktorBilgi();
            Hastalistesi();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (label5.Text == "RANDEVU LİSTESİ")
            {
                int HastaBilgiAktar = dataGridView1.SelectedCells[0].RowIndex;
                DoktorRecete Frm = new DoktorRecete();
                Frm.HastaAdSoyad= dataGridView1.Rows[HastaBilgiAktar].Cells[0].Value.ToString();
                Frm.HastaTc = dataGridView1.Rows[HastaBilgiAktar].Cells[1].Value.ToString();
                Frm.RandevuTarih = dataGridView1.Rows[HastaBilgiAktar].Cells[2].Value.ToString();
                Frm.RandevuSaat = dataGridView1.Rows[HastaBilgiAktar].Cells[3].Value.ToString();
                Frm.DoktorAd = LblAdSoyad.Text;
                Frm.Hastane= label2.Text;
                Frm.Brans = label4.Text;
                Frm.DoktorTc = LblTc.Text;
                Frm.Show();
                this.Hide();
            }

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hastalistesi();
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            LblTarih.Text = dateTimePicker1.Text;
            Hastalistesi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa ana= new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DoktorGiris giris =new DoktorGiris();
            giris.Show();
            this.Hide();
        }
    }
}
