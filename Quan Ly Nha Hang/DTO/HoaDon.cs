using System;


namespace Quan_Ly_Nha_Hang.DTO
{
    public class HoaDon
    {
        public string Maphucvu { get; set; }
        public string Ban { get; set; }
        public string Tennhanvien { get; set; } // Thu ngân
        public string Tenkhachhang { get; set; }
        public string Giamgia { get; set; }
        public double Tongthanhtoan { get; set; }
        public DateTime Thoigianvao { get; set; }
        public DateTime Thoigianthanhtoan { get; set; }
        public string Trangthai { get; set; }

        
        public HoaDon(string maphucvu, string ban, string tennhanvien, string tenkhachhang, string giamgia, double tongthanhtoan, DateTime thoigianvao, DateTime thoigianthanhtoan, string trangthai)
        {
            Maphucvu = maphucvu;
            Ban = ban;
            Tennhanvien = tennhanvien;
            Tenkhachhang = tenkhachhang;
            Giamgia = giamgia;
            Tongthanhtoan = tongthanhtoan;
            Thoigianvao = thoigianvao;
            Thoigianthanhtoan = thoigianthanhtoan;
            Trangthai = trangthai;
        }
        
        public HoaDon(HoaDon hd)
        {
            Maphucvu = hd.Maphucvu;
            Ban = hd.Ban;
            Tennhanvien = hd.Tennhanvien;
            Tenkhachhang = hd.Tenkhachhang;
            Giamgia = hd.Giamgia;
            Tongthanhtoan = hd.Tongthanhtoan;
            Thoigianvao = hd.Thoigianvao;
            Thoigianthanhtoan = hd.Thoigianthanhtoan;
            Trangthai = hd.Trangthai;
        }
        public override string ToString()
        {
            return Maphucvu + "#" + Ban + "#" + Tennhanvien + "#" + Tenkhachhang + "#" + Giamgia + 
                "#" + Tongthanhtoan + "#" + Thoigianvao.ToString("dd/MM/yyyy HH:mm:ss tt") + "#" + Thoigianthanhtoan.ToString("dd/MM/yyyy HH:mm:ss tt") + "#" + Trangthai;
        }

        // Viết hàm đọc tiền thành chữ

    }
}
