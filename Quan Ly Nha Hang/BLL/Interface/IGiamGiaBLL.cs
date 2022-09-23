using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;


namespace Quan_Ly_Nha_Hang.BLL
{
    public interface IGiamGiaBLL
    {
        List<GiamGia> LayDanhSachGiamGia();
        void ThemGiamGia(GiamGia giamgia);
        void XoaGiamGia(string magiamgia);
        void SuaGiamGia(string magiamgia, GiamGia giamgia);
        int Index(List<GiamGia> dsnv, string magiamgia);

        bool CheckMaGiamGia(string magiamgia);
    }
}
