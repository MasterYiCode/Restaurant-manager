using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface IBanBLL
    {
        List<Ban> LayDanhSachBan();
        void ThemBan(Ban nv);
        void XoaBan(string maban);
        void SuaBan(string maban, Ban ban);// sưa bàn
        List<Ban> SearchBan(string value); // Tìm kiếm bàn
        List<Ban> DanhSachBanRanh(); // Danh sách bàn rảnh
        List<Ban> DanhSachBanCoKhach(); // Danh sách bàn trống
        void SuaTrangThai(string maban, string trangthaimoi);
        string MaBanMoi();// lấy mã bàn tự động
        bool CheckBanVipOrThuong(string maban); // Chếch xem bàn này vip hay thường

        int Index(List<Ban> dsb, string maban);  // Lấy ra index của mã bàn này trong danh sach bàn

    }
}
