using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class KhachHang
    {
        
        public string Makhachhang { get; set; }
        public string Hoten { get; set; }
        public string Diachi { get; set; }
        public string Gmail { get; set; }
        public string Sodienthoai { get; set; }

        public KhachHang(KhachHang kh)
        {
            this.Makhachhang = string.Copy(kh.Makhachhang);
            this.Hoten = string.Copy(kh.Hoten);
            this.Diachi = string.Copy(kh.Diachi);
            this.Gmail = string.Copy(kh.Gmail);
            this.Sodienthoai = string.Copy(kh.Sodienthoai);
        }

        public KhachHang(string makhachhang, string hoten, string diachi, string gmail, string sodienthoai)
        {
            Makhachhang = makhachhang;
            Hoten = hoten;
            Diachi = diachi;
            Gmail = gmail;
            Sodienthoai = sodienthoai;
        }

        public override string ToString()
        {
            return Makhachhang + "#" + Hoten + "#" + Diachi + "#" + Gmail + "#" + Sodienthoai;
        }

    }
}
