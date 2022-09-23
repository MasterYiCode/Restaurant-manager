using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;
using Quan_Ly_Nha_Hang.DAL;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class KhachHangBLL : IKhachHangBLL
    {
        private static readonly IDAL<KhachHang> khDAL = new KhachHangDAL();

        #region Lấy danh sách nhân viên
        public List<KhachHang> LayDanhSachKhachHang()
        {
            return khDAL.GetData();
        }
        #endregion

        #region Sửa thông tin nhân viên
        public void SuaKhachHang(string makhachhang, KhachHang khachhang)
        {
            List<KhachHang> dskh = LayDanhSachKhachHang();
            dskh[Index(dskh, makhachhang)] = khachhang;
            khDAL.Update(dskh);
        }
        #endregion

        #region Thêm nhân viên
        public void ThemKhachHang(KhachHang kh)
        {
            khDAL.Insert(kh);
        }
        #endregion

        #region Xóa nhân viên
        public void XoaKhachHang(string makhachhang)
        {
            List<KhachHang> dskh = khDAL.GetData();
            dskh.RemoveAt(Index(dskh, makhachhang));
            khDAL.Update(dskh);
        }
        #endregion

        #region Tìm và lấy ra danh sách những sinh viên có tên .......
        public List<KhachHang> Search(string value)
        {
            List<KhachHang> dskh = khDAL.GetData();
            List<KhachHang> kq = new List<KhachHang>();
            foreach (KhachHang kh in dskh)
            {
                if (kh.Makhachhang.Contains(value) == true || kh.Hoten.ToLower().Contains(value.ToLower()) == true)
                {
                    kq.Add(kh);
                }
            }
            return kq;
        }
        #endregion

        #region Lấy ra index của mã nhân viên
        public int Index(List<KhachHang> dskh, string makhachhang)
        {
            for (int index = 0; index < dskh.Count; ++index)
                if (dskh[index].Makhachhang.Equals(makhachhang)) return index;
            return -1;
        }


        #endregion

        #region Sinh mã khách hàng tự động nè
        public void SinhMaKhachHang(out string makhachhang)
        {
            List<KhachHang> dskh = khDAL.GetData();
            if (dskh == null) makhachhang = "000001";
            else
            {
                makhachhang = (Convert.ToInt32(dskh[dskh.Count - 1].Makhachhang) + 1).ToString();
                makhachhang = new string('0', 6 - makhachhang.Length) + makhachhang.Trim();
            }

        }
        #endregion

        public string CheckPhone(string phone)
        {
            List<KhachHang> dskh = LayDanhSachKhachHang();
            if (dskh == null)
                return "";
            foreach(var kh in dskh)
            {
                if (kh.Sodienthoai.Equals(phone)) return kh.Hoten;
            }
            return "";
        }
    }
}
