using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class HoaDonBLL : IHoaDonBLL
    {
        IDAL<HoaDon> hdDAL = new HoaDonDAL();
        public List<HoaDon> LayDanhSachHoaDon()
        {
            return hdDAL.GetData();
        }

        public void SuaHoaDon(string maphucvu, HoaDon Object)
        {
            List<HoaDon> dshd = LayDanhSachHoaDon();
            dshd[Index(dshd, maphucvu)] = Object;
            hdDAL.Update(dshd);
        }

        public void ThemHoaDon(HoaDon hd)
        {
            hdDAL.Insert(hd);
        }

        public Dictionary<int, double> ThongKeDoanhThu()
        {
            List<HoaDon> dshd = new List<HoaDon>();
            Dictionary<int, double> doanhthu = new Dictionary<int, double>();
            foreach (HoaDon hd in dshd)
            {
                if (doanhthu.ContainsKey(hd.Thoigianthanhtoan.Month))
                {
                    doanhthu[hd.Thoigianthanhtoan.Month] += hd.Tongthanhtoan;
                }
                else
                {
                    doanhthu.Add(hd.Thoigianthanhtoan.Month, hd.Tongthanhtoan);
                }
            }
            return doanhthu;
        }

        public void XoaHoaDon(string maphucvu)
        {
            List<HoaDon> dshd = LayDanhSachHoaDon();
            dshd.RemoveAt(Index(dshd, maphucvu));
            hdDAL.Update(dshd);
        }
        public void XoaHoaDon_Month(int month)
        {
            List<HoaDon> dshd = LayDanhSachHoaDon();
            for(int i = 0; i < dshd.Count; i++)
            {
                if(dshd[i].Thoigianthanhtoan.Month == month)
                {
                    dshd.RemoveAt(i);
                }
            }
            hdDAL.Update(dshd);
        }
        public int Index(List<HoaDon> listhd, string maphucvu)
        {
            for (int index = 0; index < listhd.Count; ++index)
                if (listhd[index].Maphucvu.ToUpper().Equals(maphucvu.ToUpper())) return index;
            return -1;
        }

        public List<HoaDon> DanhSachHoaDon_Theo(DateTime datetime)
        {
            List<HoaDon> dshd = LayDanhSachHoaDon();
            List<HoaDon> temp = new List<HoaDon>();
            if (dshd.Count == 0) return dshd = null;
            foreach (HoaDon hd in dshd)
            {
                if (hd.Thoigianthanhtoan.ToString("MM/yyyy").Equals(datetime.ToString("MM/yyyy")) || hd.Thoigianthanhtoan.ToString("dd/MM/yyyy").Equals(datetime.ToString("dd/MM/yyyy")))
                {
                    temp.Add(new HoaDon(hd));
                }
            }
            return dshd;

        }

        
    }
}
