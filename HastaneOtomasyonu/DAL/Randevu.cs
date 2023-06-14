using HastaneOtomasyonu.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.DAL
{
    internal class Randevu:DoktorTbl
    {
        public Randevu(int id) : base(id)
        {
        }

        //override işlemi
        public override void listele()
        {
            throw new NotImplementedException();
        }

        public override double maas(int katsayi)
        {
            throw new NotImplementedException();
        }
    }
}
