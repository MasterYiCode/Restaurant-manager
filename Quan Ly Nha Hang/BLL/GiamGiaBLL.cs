using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class GiamGiaBLL : IGiamGiaBLL
    {
        private static readonly IDAL<GiamGia> ggDAL = new GiamGiaDAL();
        public int Index(List<GiamGia> dsnv, string magiamgia)
        {
            List<GiamGia> dsgg = LayDanhSachGiamGia();
            if(dsgg == null)
            {
                return -1;
            }
            for(int i = 0; i < dsgg.Count; i++)
            {
                if (magiamgia.Equals(dsgg[i].MaGiamGia)){
                    return i;
                }
            }
            return -1;
        }

        public List<GiamGia> LayDanhSachGiamGia()
        {
           return ggDAL.GetData();
        }

        public void SuaGiamGia(string magiamgia, GiamGia giamgia)
        {
            List<GiamGia> dsgg = LayDanhSachGiamGia();
            dsgg[Index(dsgg, magiamgia)] = giamgia;
            ggDAL.Update(dsgg);
        }

        public void ThemGiamGia(GiamGia giamgia)
        {
            ggDAL.Insert(giamgia);
        }

        public void XoaGiamGia(string magiamgia)
        {
            List<GiamGia> dsgg = LayDanhSachGiamGia();
            dsgg.RemoveAt(Index(dsgg, magiamgia));
            ggDAL.Update(dsgg);
        }
 
        public bool CheckMaGiamGia(string magiamgia)
        {
            List<GiamGia> dsgg = LayDanhSachGiamGia();
            if(dsgg == null)
            {
                return false;
            }
            for(int i = 0; i < dsgg.Count; i++)
            {
                if (magiamgia.Equals(dsgg[i].MaGiamGia))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
