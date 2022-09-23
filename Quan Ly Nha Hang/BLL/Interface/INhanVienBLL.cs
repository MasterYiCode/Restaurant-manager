using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public interface INhanVienBLL
    {
        List<NhanVien> LayDanhSachNhanVien();
        void ThemNhanVien(NhanVien nv);
        void XoaNhanVien(string manhanvien);
        void SuaNhanVien(string manhanvien, NhanVien nhanvien);
        int CheckMaNhanVien(string manhanvien);
        List<NhanVien> SearchNhanVien(string value);
        int Index(List<NhanVien> dsnv,string manhanvien);
        
    }
}
