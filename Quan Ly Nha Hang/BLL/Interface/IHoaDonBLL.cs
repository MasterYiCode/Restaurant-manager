using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface IHoaDonBLL
    {
        List<HoaDon> LayDanhSachHoaDon();
        void ThemHoaDon(HoaDon hd);
        void XoaHoaDon(string maphucvu);
        void XoaHoaDon_Month(int month);
        void SuaHoaDon(string maphucvu, HoaDon hd);
        Dictionary<int, double> ThongKeDoanhThu();
        List<HoaDon> DanhSachHoaDon_Theo(DateTime datetime);
        int Index(List<HoaDon> listhd, string maphucvu);

    }
}