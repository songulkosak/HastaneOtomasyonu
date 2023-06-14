using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.DAL
{
    internal class Hasta
    {
        public int HastaId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Cinsiyet { get; set; }
        public string Tc { get; set; }
        public string Telefon { get; set; }
        public string DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }

    }
}
