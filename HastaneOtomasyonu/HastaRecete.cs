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
    public partial class HastaRecete : Form
    {
        public HastaRecete()
        {
            InitializeComponent();
        }
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        public string Tc;
        public String AdSoyad;
        private void HastaRecete_Load(object sender, EventArgs e)
        {
            LblAdSoyad.Text = AdSoyad;
            LblTc.Text = Tc;
            SqlCommand komut = new SqlCommand("select Hastane,Doktor,Brans,Recete,ReceteKodu from Recete where Tc=@p1 ", Baglan);
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable Tablo = new DataTable();
            da.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hasta hasta = new Hasta();
            hasta.Show();
            this.Hide();
        }
    }
}
