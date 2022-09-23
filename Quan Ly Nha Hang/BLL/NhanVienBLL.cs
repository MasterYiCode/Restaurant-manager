using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class NhanVienBLL : INhanVienBLL
    {
        private static readonly IDAL<NhanVien> nvDAL = new NhanVienDAL();

        #region Lấy danh sách nhân viên
        public List<NhanVien> LayDanhSachNhanVien()
        {
            return nvDAL.GetData();
        }
        #endregion

        #region Sửa thông tin nhân viên
        public void SuaNhanVien(string manhanvien, NhanVien nhanvien)
        {
            List<NhanVien> dsnv = LayDanhSachNhanVien();
            dsnv[Index(dsnv, manhanvien)] = nhanvien;
            nvDAL.Update(dsnv);
        }
        #endregion

        #region Thêm nhân viên
        public void ThemNhanVien(NhanVien nv)
        {
            nvDAL.Insert(nv);
        }
        #endregion

        #region Xóa nhân viên
        public void XoaNhanVien(string manhanvien)
        {
            List<NhanVien> dsnv = nvDAL.GetData();
            dsnv.RemoveAt(Index(dsnv, manhanvien));
            nvDAL.Update(dsnv);
        }
        #endregion

        #region Tìm và lấy ra danh sách những sinh viên có tên .......
        public List<NhanVien> SearchNhanVien(string value)
        {
            List<NhanVien> dsnv = nvDAL.GetData();
            List<NhanVien> kq = nvDAL.GetData();
            foreach (NhanVien nv in dsnv)
            {
                if (nv.Manhanvien.Contains(value) == true || nv.Hoten.ToLower().Contains(value.ToLower()) == true)
                {
                    NhanVien abc = new NhanVien(nv);
                    kq.Add(abc);
                }
            }
            return kq;
        }
        #endregion

        public int CheckMaNhanVien(string manhanvien)
        {
            List<NhanVien> dsnv = LayDanhSachNhanVien();
            if (dsnv == null) return -1;
            for(int i = 0; i < dsnv.Count; i++)
            {
                if (dsnv[i].Manhanvien.Equals(manhanvien) || dsnv[i].Hoten.Equals(manhanvien)) return i;
            }
            return -1;
        }
        #region Lấy ra index của mã nhân viên
        public int Index(List<NhanVien> dsnv, string manhanvien)
        {
            for (int index = 0; index < dsnv.Count; ++index)
                if (dsnv[index].Manhanvien.Equals(manhanvien)) return index;
            return -1;
        }
        #endregion

    }
}
