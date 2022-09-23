using System;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class NhanVien
    {
        public string Manhanvien { get; set; }
        public string Hoten { get; set; }
        public DateTime Ngaysinh { get; set; }
        public string Gioitinh { get; set; }
        public string Sodienthoai { get; set; }

        public NhanVien(string manhanvien, string hoten, DateTime ngaysinh, string gioitinh, string sodienthoai)
        {
            Manhanvien = manhanvien;
            Hoten = hoten;
            Ngaysinh = ngaysinh;
            Gioitinh = gioitinh;
            Sodienthoai = sodienthoai;
        }

        public NhanVien(NhanVien nv)
        {
            this.Manhanvien = string.Copy(nv.Manhanvien);
            this.Hoten = string.Copy(nv.Hoten);
            this.Ngaysinh = nv.Ngaysinh;
            this.Gioitinh = string.Copy(nv.Gioitinh);
            this.Sodienthoai = string.Copy(nv.Sodienthoai);
        }
        public override string ToString()
        {
            return Manhanvien + "#" + Hoten + "#" + Ngaysinh.ToString("dd/MM/yyyy") + "#" + Gioitinh + "#" + Sodienthoai;
        }
    }
}
