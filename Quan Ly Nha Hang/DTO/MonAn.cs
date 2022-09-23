using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class MonAn
    {
        public string Mamon { get; set; }
        public string Tenmon { get; set; }
        public double Dongia { get; set; }
        public int Soluong { get; set; }
        public string Loaimon { get; set; }

        public MonAn(string mamon, string tenmon, double dongia, int soluong, string loaimon)
        {
            this.Mamon = mamon;
            this.Tenmon = tenmon;
            this.Dongia = dongia;
            this.Soluong = soluong;
            this.Loaimon = loaimon;
        }
        public MonAn(MonAn ma)
        {
            this.Mamon = string.Copy(ma.Mamon);
            this.Tenmon = string.Copy(ma.Tenmon);
            this.Dongia = ma.Dongia;
            this.Soluong = ma.Soluong;
            this.Loaimon = string.Copy(ma.Loaimon);
        }

        public MonAn(string tenmon, double dongia, int soluong)
        {
            this.Mamon = "";
            this.Tenmon = tenmon;
            this.Dongia = dongia;
            this.Soluong = soluong;
            this.Loaimon = "";
        }
        public override string ToString()
        {
            return Mamon + "#" + Tenmon + "#" + Dongia + "#" + Soluong + "#" + Loaimon; 
        }
        
        public string Display()
        {
            return Tenmon + "#" + Dongia + "#" + Soluong;
        }
    }
}
