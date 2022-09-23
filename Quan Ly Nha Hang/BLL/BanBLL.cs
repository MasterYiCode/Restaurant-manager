using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class BanBLL : IBanBLL
    {
        private static readonly IDAL<Ban> bDAL = new BanDAL();

        #region Lấy danh sách bàn
        public List<Ban> LayDanhSachBan()
        {
            return bDAL.GetData();
        }
        #endregion

        #region Sửa thông tin bàn
        public void SuaBan(string maban, Ban ban)
        {
            List<Ban> dsb = LayDanhSachBan();
            dsb[Index(dsb, maban)] = ban;
            bDAL.Update(dsb);
        }
        #endregion

        #region Thêm bàn
        public void ThemBan(Ban b)
        {
            bDAL.Insert(b);
        }
        #endregion

        #region Xóa bàn
        public void XoaBan(string manhanvien)
        {
            List<Ban> dsb = bDAL.GetData();
            dsb.RemoveAt(Index(dsb, manhanvien));
            bDAL.Update(dsb);
        }
        #endregion

        #region Tìm và lấy ra danh sách những bàn có tên .......
        public List<Ban> SearchBan(string value)
        {
            List<Ban> dsb = bDAL.GetData();
            List<Ban> kq = new List<Ban>();
            foreach (Ban b in dsb)
            {
                if (b.Maban.Contains(value) == true || b.Loaiban.ToLower().Contains(value.ToLower()) == true)
                {
                    kq.Add(new Ban(b));
                }
            }
            return kq;
        }
        #endregion

        #region Lấy ra index của mã bàn
        public int Index(List<Ban> dsb, string maban)
        {
            for (int index = 0; index < dsb.Count; ++index)
                if (dsb[index].Maban.Equals(maban)) return index;
            return -1;
        }
        #endregion

        #region Lấy ra mã bàn tự động
        public string MaBanMoi()
        {
            List<Ban> dsb = LayDanhSachBan();
            if (dsb.Count == 0) return "1";
            return (Convert.ToInt32(dsb[dsb.Count - 1].Maban.Trim()) + 1).ToString();
        }
        #endregion

        #region Lấy ra danh sách bàn rảnh và bận
        public List<Ban> DanhSachBanRanh()
        {
            List<Ban> dsb = LayDanhSachBan();
            List<Ban> dsb_ranh = new List<Ban>();
            foreach (Ban b in dsb)
            {
                if (b.Trangthai.ToUpper().Equals("RANH"))
                {
                    dsb_ranh.Add(b);
                }
            }
            return dsb_ranh;
        }

        public List<Ban> DanhSachBanCoKhach()
        {
            List<Ban> dsb = LayDanhSachBan();
            List<Ban> dsb_cokhach = new List<Ban>();
            foreach (Ban b in dsb)
            {
                if (b.Trangthai.ToUpper().Equals("BAN"))
                {
                    dsb_cokhach.Add(b);
                }
            }
            return dsb_cokhach;
        }
        #endregion
        public void SuaTrangThai(string maban, string trangthaimoi)
        {
            List<Ban> dsb = LayDanhSachBan();
            dsb[Index(dsb, maban)].Trangthai = trangthaimoi;
            bDAL.Update(dsb);
        }

        public bool CheckBanVipOrThuong(string maban)// nếu là bàn vip => true; bàn thường => false;
        {
            List<Ban> dsb = LayDanhSachBan();
            if(dsb[Index(dsb, maban)].Loaiban.ToUpper().Equals("VIP"))
            {
                return true;
            }
            return false;
        }

    }
}