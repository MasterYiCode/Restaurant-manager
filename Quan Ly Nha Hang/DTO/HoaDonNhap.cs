using System;


namespace Quan_Ly_Nha_Hang.DTO
{
    public class HoaDonNhap
    {

        public string Manhaphang{ get; set; }
        public string Manhanvien { get; set; }
        public string Tennhanvien { get; set; } // Thu ngân
        public string Nhacungcap { get; set; }
        public string Tenmon { get; set; }
        public int Soluong { get; set; }
        public double Tongtien { get; set; }
        public DateTime Thoigiannhap { get; set; }
        public string Trangthai { get; set; } // Đã thanh toán hay chưa

        public HoaDonNhap(string manhaphang, string manhanvien, string tennhanvien, string nhacungcap, string tenmon, int soluong, double tongtien, DateTime thoigiannhap, string trangthai)
        {
            Manhaphang = manhaphang;
            Manhanvien = manhanvien;
            Tennhanvien = tennhanvien;
            Nhacungcap = nhacungcap;
            Tenmon = tenmon;
            Soluong = soluong;
            Tongtien = tongtien;
            Thoigiannhap = thoigiannhap;
            Trangthai = trangthai;
        }

    }
}
