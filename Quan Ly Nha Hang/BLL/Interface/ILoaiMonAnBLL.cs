using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface ILoaiMonAnBLL
    {
        List<LoaiMonAn> LayDanhSachLoaiMonAn();
        void ThemLoaiMonAn(LoaiMonAn Object);
        void XoaLoaiMonAn(string mamon);
        void SuaLoaiMonAn(string mamon, LoaiMonAn Object);
        List<LoaiMonAn> SearchLoaiMonAn(string value);
        bool CheckMonAn(string loaimonan); // chech xem có món ăn nào có loại món là ... không
        bool CheckMaLoaiMonAn(string loaimonan);
        int Index(List<LoaiMonAn> listlma, string maloaimonan); // xem món ăn có mã món là  ... đang đứng thứ mấy
    }
}
