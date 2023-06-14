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
    public partial class Hasta : Form
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;

        public string Tc;
        public string Ad_Soyad;
        public Hasta()
        {
            InitializeComponent();
        }
        void Randevulistele()
        {
            LblAdSoyad.Text = Ad_Soyad;
            LblTc.Text = Tc;
            SqlCommand komut = new SqlCommand("select RandevuId,KlinikId,DoktorId,Tarih,Saat from Randevu where Ad=@p1 or Tc=@p2", Baglan);
            komut.Parameters.AddWithValue("@p1", LblAdSoyad.Text);
            komut.Parameters.AddWithValue("@p2", LblTc.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }
        void AdSoyad()
        {
            //cmd = new SqlCommand("select Ad,Soyad from Hasta where Tc=@p1 ", Baglan);
            //cmd.Parameters.AddWithValue("@p1", LblTc.Text);
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    LblAdSoyad.Text = dr[0] + " " + dr[1].ToString();
            //}

        }

        void Randevuİptali()
        {
            SqlCommand komut0 = new SqlCommand("DELETE From Randevu where RandevuId=@p1", Baglan);
            komut0.Parameters.AddWithValue("@p1", LbkRandevuId.Text);
            try
            {
                komut0.ExecuteNonQuery();
                MessageBox.Show("Randevu Kaydınız Silindi", "UYAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Randevu silinirken bir hata oluştu. Tekrar deneyiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RandevuIptaliGüncelle()
        {
            SqlCommand komut1 = new SqlCommand("UPDATE Randevu set DURUM=@p1 where RandevuId=@p2", Baglan);
            komut1.Parameters.AddWithValue("@p1", "True");
            komut1.Parameters.AddWithValue("@p2", LbkRandevuId.Text);
            try
            {
                komut1.ExecuteNonQuery();
            }
            catch (Exception)
            {

                MessageBox.Show("Randevu silinirken bir hata oluştu. Tekrar deneyiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hasta_Load(object sender, EventArgs e)
        {
            LblAdSoyad.Text = Ad_Soyad;
            LblTc.Text = Tc;
            Randevulistele();
            AdSoyad();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int tut = dataGridView1.SelectedCells[0].RowIndex;
            LbkRandevuId.Text = dataGridView1.Rows[tut].Cells[0].Value.ToString();
            BtnRandevuIptali.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RandevuSistemi Frm = new RandevuSistemi();
            Frm.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RandevuIptaliGüncelle();
            Randevuİptali();
            Randevulistele();
            BtnRandevuIptali.Visible = false;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HastaRecete frm = new HastaRecete();
            frm.Tc = LblTc.Text;
            frm.AdSoyad = LblAdSoyad.Text;
            frm.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HastaGuncelle frm = new HastaGuncelle();
            frm.Tc = Tc;
            frm.Show();
            this.Hide();
        }
    }
}
