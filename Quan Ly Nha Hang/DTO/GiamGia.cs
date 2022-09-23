using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class GiamGia
    {  
        public string MaGiamGia { get; set; }
        public  DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string DoiTuong { get; set; }
        public double SaleOff { get; set; }

        public GiamGia(string maGiamGia, DateTime dayStart, DateTime dayEnd, string doiTuong, double saleOff)
        {
            MaGiamGia = maGiamGia;
            DayStart = dayStart;
            DayEnd = dayEnd;
            DoiTuong = doiTuong;
            SaleOff = saleOff;
        }

        public override string ToString()
        {
            return  MaGiamGia + "#" + DayStart.ToString("dd/MM/yyyy HH:mm:ss tt") + "#" + DayEnd.ToString("dd/MM/yyyy HH:mm:ss tt") + "#" + DoiTuong + "#" + SaleOff;
        }
    }

 
}
