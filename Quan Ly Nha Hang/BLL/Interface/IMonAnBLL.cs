using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface IMonAnBLL
    {
        List<MonAn> LayDanhSachMonAn();
        void ThemMonAn(MonAn Object);
        void Update(List<MonAn> list);
        void XoaMonAn(string mamon);
        void SuaMonAn(string mamon, MonAn Object);
        List<MonAn> SearchMonAn(string value);
        bool CheckLoaiMonAn(string loaimon);
        bool CheckMaMonAn(string mamonan);
        double CheckMonAn(string tenmon);
        bool CheckSoLuong(string tenmon,int soluong);
        bool UpdateSoLuong(string tenmon, int soluong, string trangthai);
        int Index(List<MonAn> listma, string mamonan);
        int Index_(List<MonAn> listma, string tenmon);
    }
}
