using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void button_doktor_Click(object sender, EventArgs e)
        {
            DoktorGiris doktor = new DoktorGiris();
            doktor.Show();
            this.Hide();
        }

        private void button_hasta_Click(object sender, EventArgs e)
        {
            HastaGiris hasta = new HastaGiris();
            hasta.Show();
            this.Hide();
        }

        private void button_randevu_Click(object sender, EventArgs e)
        {
            RandevuSistemi randevuSistemi = new RandevuSistemi();
            randevuSistemi.Show();
            this.Hide();
        }

        private void button_kapat_Click(object sender, EventArgs e)
        {
            DialogResult secim;
            secim = MessageBox.Show("Programı kapatmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (secim == DialogResult.OK)
            {
                Application.Exit();
            }
        }


    }
}
