using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class HastaGuncelle : Form
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        public string Tc;
        public HastaGuncelle()
        {
            InitializeComponent();
        }
        void kayitguncelle()
        {
            SqlCommand cmd = new SqlCommand("update Hasta set Ad=@ad,Soyad=@soyad,Cinsiyet=@cinsiyet,Telefon=@telefon,DogumTarihi=@dtar,DogumYeri=@dyer,Eposta=@eposta,Sifre=@sifre where Tc=@tc ",Baglan);
            cmd.Parameters.AddWithValue("@ad", TxtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@telefon", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@dtar", MskDogumTarih.Text);
            cmd.Parameters.AddWithValue("@dyer", Cmbdyer.Text);
            cmd.Parameters.AddWithValue("@eposta", MskMail.Text);
            cmd.Parameters.AddWithValue("@sifre", MskSifre.Text);
            if (radioButton1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@cinsiyet", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@cinsiyet", false);
            }
            cmd.Parameters.AddWithValue("@tc", MskTc.Text);
            Baglan.Open();
            cmd.ExecuteNonQuery();
            Baglan.Close();
            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void button_guncelle_Click(object sender, EventArgs e)
        {
            kayitguncelle();
        }

        private void linkLabel_gizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel_gizle.Text == "Göster")
            {
                MskSifre.PasswordChar = '\0';
                linkLabel_gizle.Text = "Gizle";
            }
            else
            {
                linkLabel_gizle.Text = "Gizle";
                MskSifre.PasswordChar = '*';
                linkLabel_gizle.Text = "Göster";
            }
        }

        private void pictureBox_geri_Click(object sender, EventArgs e)
        {
            Hasta hasta = new Hasta();
            hasta.Show();
            this.Hide();
        }

        private void HastaGuncelle_Load(object sender, EventArgs e)
        {
            MskTc.Text = Tc;
            kayitguncelle();
            Baglan.Open();
            Baglan.Close();

        }
    }
}
