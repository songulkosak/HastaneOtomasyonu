﻿using HastaneOtomasyonu.Dal;
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
    public partial class DoktorGiris : Form
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        public DoktorGiris()
        {
            InitializeComponent();
        }
        void temizle()
        {
            TextBox_kullaniciAdi.Text = "";
            textBox_sifre.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)//Anasayfa butonu
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void button_girisyap_Click(object sender, EventArgs e)
        {
            SqlCommand Giris = new SqlCommand("select * from doktor where TC = @p1 and SİFRE=@p2 ", Baglan);
            Giris.Parameters.AddWithValue("@p1", TextBox_kullaniciAdi.Text);
            Giris.Parameters.AddWithValue("@p2", textBox_sifre.Text);
            Baglan.Open();
            SqlDataReader dm = Giris.ExecuteReader();
            if (dm.Read())
            {
                MessageBox.Show("Giriş Yapıldı", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Doktor doktor = new Doktor();
                doktor.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı Giris","Eror",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            Baglan.Close();
        }

        private void linkLabel_gizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel_gizle.Text == "Göster")
            {
                textBox_sifre.PasswordChar = '\0';
                linkLabel_gizle.Text = "Gizle";
            }
            else
            {
                linkLabel_gizle.Text = "Gizle";
                textBox_sifre.PasswordChar = '*';
                linkLabel_gizle.Text = "Göster";
            }
        }
    }
}
