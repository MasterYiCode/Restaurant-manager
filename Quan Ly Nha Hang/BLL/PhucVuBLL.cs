using Quan_Ly_Nha_Hang.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class PhucVuBLL : IPhucVuBLL
    {
        IDAL<PhucVu> pvDAL = new PhucVuDAL();
        IBanBLL bBLL = new BanBLL();
        IHoaDonBLL hdBLL = new HoaDonBLL();
        public List<PhucVu> LayDanhSachPhucVu()
        {
            return pvDAL.GetData();
        }

        public void ThemPhucVu(PhucVu pv)
        {
            pvDAL.Insert(pv);
        }

        public void UpdateList(List<PhucVu> list)
        {
            pvDAL.Update(list);
        }
        public void SuaPhucVu(string maphucvu, PhucVu Object)
        {
            List<PhucVu> dspv = LayDanhSachPhucVu();
            dspv[Index(dspv, maphucvu)] = Object;
            pvDAL.Update(dspv);
        }

        public void XoaPhucVu(string maphucvu)
        {
            List<PhucVu> dspv = LayDanhSachPhucVu();
            int index = Index(dspv, maphucvu);
            if (index == -1) return;
            dspv.RemoveAt(index);
            pvDAL.Update(dspv);
        }
        public bool CheckBan_Ranh(string maban)
        {
            List<Ban> dsb = bBLL.LayDanhSachBan();
            if (dsb == null) return false;
            foreach(Ban b in dsb)
            {
                if (b.Trangthai.ToUpper().Equals("RANH")) return true;
            }
            return false;
            
        }

        public bool CheckMaPhucVu(string maphucvu)
        {
            List<PhucVu> dspv = LayDanhSachPhucVu();
            List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
            if (dspv == null || dshd == null) return false;
            foreach(PhucVu pv in dspv)
            {
                if (pv.Maphucvu.Equals(maphucvu)) return true;
            }

            foreach(HoaDon hd in dshd)
            {
                if (hd.Maphucvu.Equals(maphucvu)) return true;
            }
            return false;
        }
   
        public int Index(List<PhucVu> listpv, string maphucvu)
        {
            if (listpv == null) return -1;
            for (int index = 0; index < listpv.Count; ++index)
                if (listpv[index].Maphucvu.ToUpper().Equals(maphucvu.ToUpper())) return index;
            return -1;
        }
    }
}
