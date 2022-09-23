using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface IKhachHangBLL
    {
        List<KhachHang> LayDanhSachKhachHang();
        void ThemKhachHang(KhachHang kh);
        void XoaKhachHang(string makhachhang);
        void SuaKhachHang(string makhachhang, KhachHang kh);
        string CheckPhone(string phone);
        List<KhachHang> Search(string value);

    }
}
