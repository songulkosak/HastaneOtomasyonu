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

namespace HastaneOtomasyonu
{
    public partial class HastaYeniKayit : Form
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        public HastaYeniKayit()
        {
            InitializeComponent();
        }
        bool durum;
        void TekrarEngenleme()//Sürekli Aynı Kaydı girmemek için bu metot oluşturuldu.
        {
            Baglan.Open();
            SqlCommand komut = new SqlCommand("select * from Hasta where TC=@tc", Baglan);
            komut.Parameters.AddWithValue("@tc", MskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            Baglan.Close();
        }
        private void button_kaydet_Click(object sender, EventArgs e)
        {
            TekrarEngenleme();
            if (durum==true)
            {
                SqlCommand cmd=new SqlCommand("insert into Hasta values(@ad,@soyad,@cinsiyet,@tc,@telefon,@dtar,@dyer,@eposta,@sifre)", Baglan);
                cmd.Parameters.AddWithValue("@ad", TxtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", TxtSoyad.Text);
                cmd.Parameters.AddWithValue("@tc", MskTc.Text);
                cmd.Parameters.AddWithValue("@telefon", MskTelefon.Text);
                cmd.Parameters.AddWithValue("@eposta", MskMail.Text);
                cmd.Parameters.AddWithValue("@dtar", MskDogumTarih.Text);
                cmd.Parameters.AddWithValue("@dyer", Cmbdyer.Text);
                cmd.Parameters.AddWithValue("@sifre", MskSifre.Text);
                if (radioButton1.Checked ==true)
                {
                    cmd.Parameters.AddWithValue("@cinsiyet",true);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@cinsiyet", false);
                }
                Baglan.Open();
                cmd.ExecuteNonQuery();
                Baglan.Close();
                MessageBox.Show("Kayıt başarıyla eklendi", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bu kayıt zaten var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button_anasayfa_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void pictureBox_geri_Click(object sender, EventArgs e)
        {
            HastaGiris giris = new HastaGiris();
            giris.Show();
            this.Hide();
        }

        private void Btn_gizlegoster_MouseClick(object sender, MouseEventArgs e)
        {
            if (Btn_gizlegoster.Text=="Göster")
            {
                MskSifre.PasswordChar = '\0';
                Btn_gizlegoster.Text = "Gizle";
            }
            else
            {
                Btn_gizlegoster.Text = "Gizle";
                MskSifre.PasswordChar = '*';
                Btn_gizlegoster.Text = "Göster";
            }
        }


    }
}
