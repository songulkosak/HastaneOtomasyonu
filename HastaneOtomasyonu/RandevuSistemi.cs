using HastaneOtomasyonu.Arayüzler;
using HastaneOtomasyonu.DAL;
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
    public partial class RandevuSistemi : Form,Interface2,Interface3
    {
        SqlConnection Baglan = new SqlConnection("Data Source=DESKTOP-ER0GUQT\\SQLEXPRESS;Initial Catalog=HastaneOtomasyon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable table=new DataTable();

        public RandevuSistemi()
        {
            InitializeComponent();
        }
        
        public void listele()
        {
            da=new SqlDataAdapter("select * from Randevu",Baglan);
            DataTable tbl=new DataTable();
            Baglan.Open();
            da.Fill(tbl);
            dataGridView1.DataSource = tbl;
            Baglan.Close();

        }
        public void getKlinik()
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from Klinikler", Baglan);
            da.Fill(tablo);
            CmbBrans.DisplayMember = "KlinikAdi";
            CmbBrans.ValueMember = "klinikId";
            CmbBrans.DataSource = tablo;
        }
        public void getdoktor()
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from Doktor", Baglan);
            da.Fill(tablo);
            CmbDoktor.DisplayMember = "AdSoyad";
            CmbDoktor.ValueMember = "DoktorId";
            CmbDoktor.DataSource = tablo;
        }
        public void satir_boya() //datagridview tablosundaki çift satırların rengini bisqui yapar.
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                }
            }
        }
        public void datagriduzenle()
        {
            dateTimePicker1.CustomFormat = "dd.MM.yyyy"; //tarih formatı ayarlama
            dateTimePicker1.Format = DateTimePickerFormat.Custom; //belilediğim format olsun 

            //Sütun genişliklerini ayarlama kodu
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[7].Width = 60;

            //Datagridview tablosundaki başlıkları değiştirme işlemi
            dataGridView1.Columns[0].HeaderText = "Rnd.No";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "Tc Kimlik";
            dataGridView1.Columns[4].HeaderText = "Klinik";
            dataGridView1.Columns[5].HeaderText = "Doktor";
            dataGridView1.Columns[6].HeaderText = "Tarih";
            dataGridView1.Columns[7].HeaderText = "Saat";

            //sayısal sahaların hücre stillerini hizalama sağ,sol,orta vb.
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void RandevuSistemi_Load(object sender, EventArgs e)
        {
            listele();
            getKlinik();
            getdoktor();
            datagriduzenle();
            satir_boya();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridViewCellStyle ColumnRenk;
            dataGridView1.EnableHeadersVisualStyles = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                ColumnRenk = dataGridView1.Columns[i].HeaderCell.Style;

                ColumnRenk.BackColor = Color.Navy;
                ColumnRenk.ForeColor = Color.White;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox_rndno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox_ad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox_soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox_tc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            CmbBrans.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            CmbDoktor.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox_saat.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        public void ekle()
        {
            cmd = new SqlCommand("insert into Randevu values (@ad,@soyad,@tc,@brans,@doktor,@tarih,@saat)", Baglan);
            cmd.Parameters.AddWithValue("@ad", textBox_ad.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox_soyad.Text);
            cmd.Parameters.AddWithValue("@tc", textBox_tc.Text);
            cmd.Parameters.AddWithValue("@brans", CmbBrans.Text);
            cmd.Parameters.AddWithValue("@doktor", CmbDoktor.Text);
            cmd.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@saat", textBox_saat.Text);
            Baglan.Open();
            cmd.ExecuteNonQuery();
            Baglan.Close();
        }
        private void button_ekle_Click(object sender, EventArgs e)
        {
            ekle();
            listele();
        }

        public void sil()
        {
            DialogResult cevap = MessageBox.Show("Bu kaydı silmek istediğinize emin misiniz?", "Kayıt Silme", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (cevap == DialogResult.OK)
            {
                cmd = new SqlCommand("delete from Randevu where RandevuId=@rndid", Baglan);
                cmd.Parameters.AddWithValue("@rndid", Convert.ToInt32(textBox_rndno.Text));
                Baglan.Open();
                cmd.ExecuteNonQuery();
                Baglan.Close();
                listele();
            }
        }
        private void button_sil_Click(object sender, EventArgs e)
        {
            sil();
        }

        public void guncelle()
        {
            cmd = new SqlCommand("update Randevu set Ad=@ad,Soyad=@soyad,Tc=@tc,KlinikId=@brans,DoktorId=@doktor,Tarih=@tarih,Saat=@saat where RandevuId=@rndno", Baglan);
            cmd.Parameters.AddWithValue("@ad", textBox_ad.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox_soyad.Text);
            cmd.Parameters.AddWithValue("@tc", textBox_tc.Text);
            cmd.Parameters.AddWithValue("@brans", CmbBrans.Text);
            cmd.Parameters.AddWithValue("@doktor", CmbDoktor.Text);
            cmd.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@saat", textBox_saat.Text);
            cmd.Parameters.AddWithValue("@rndno", Convert.ToInt32(textBox_rndno.Text));
            Baglan.Open();
            cmd.ExecuteNonQuery();
            Baglan.Close();
        }
        private void button_guncelle_Click(object sender, EventArgs e)
        {
            guncelle();
            listele();
        }

        private void btnanasayfa_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa=new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void pictureBox_geri_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void button_ara_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView();
            dv= table.DefaultView;
            dv.RowFilter = "Tc Like '" + textBox_ara.Text + "%'";
            dataGridView1.DataSource = dv;

        }
    }
}
