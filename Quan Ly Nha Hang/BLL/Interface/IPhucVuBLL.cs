using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;
using Quan_Ly_Nha_Hang.DAL;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface IPhucVuBLL
    {
        List<PhucVu> LayDanhSachPhucVu();
        void ThemPhucVu(PhucVu pv);
        void XoaPhucVu(string maphucvu);
        void UpdateList(List<PhucVu> list);
        void SuaPhucVu(string maphucvu, PhucVu Object);
        bool CheckBan_Ranh(string maban); // Chếch xem bàn có rảnh hay không. Rảnh => true, ngược lại false
        bool CheckMaPhucVu(string maphucvu);
        int Index(List<PhucVu> dspv ,string maphucvu);
    }
}
