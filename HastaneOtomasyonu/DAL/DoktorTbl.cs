using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Dal
{
    internal abstract class DoktorTbl
    {
        //static değişken kullanımı
        private static int kimlik;
        private string adsoyad;

        
        //abstract kullanımı
        public abstract void listele();

        //Yapıcı Metot kullanımı
        public DoktorTbl(int id)
        {
            kimlik = id;
        }

        //abstract class kullanımı
        public abstract double maas(int katsayi);


        //katsayi için kapsülleme işlemi get/set
        private int katsayi;
        public void setkatsayi(int deger)
        {
            katsayi = deger;
        }
        public int getkatsayi()
        {
            return katsayi;
        }


        //static metot kullanımı
        //kapsülleme kimlik için set/get işlemi 
        public static void setkimlik(int tc)
        {
            kimlik = tc;
        }
        public static int getkimlik()
        {
            return kimlik;
        }
    }
}
