using Quan_Ly_Nha_Hang.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class GoiMonBLL
    {
        IDAL<MonAn> maDAL = new MonAnDAL();
        IDAL<GoiMon> gmDAL = new GoiMonDAL();

        public List<GoiMon> LayDanhSachGoiMon()
        {
            return gmDAL.GetData();
        }

        #region Khi khách gọi món thì thêm những gì khách gọi vào cuối tệp
        public void Insert(GoiMon Object)
        {
            gmDAL.Insert(Object);
        }
        #endregion

        public void UpdateList(List<GoiMon> list)
        {
            gmDAL.Update(list);
        }

        #region Khách muốn gọi thêm thì thêm vào và update lại danh sách vào tệp
        public void InsertMonAn(string maphucvu ,MonAn ma)
        {
            List<GoiMon> dsgm = LayDanhSachGoiMon();
            int index = Index(dsgm, maphucvu);
            if (index == -1) return;
            dsgm[index].Order.Add(ma);
            gmDAL.Update(dsgm);
        }
        #endregion

        #region Lấy ra vị trí mã phục vụ trong danh sách
        public int Index(List<GoiMon> listgm, string maphucvu)
        {
            if (listgm == null) return -1;
            for (int index = 0; index < listgm.Count; ++index)
                if (listgm[index].Maphucvu.Equals(maphucvu)) return index;
            return -1;
        }
        #endregion

        #region Chech xem mã phục vụ này có tồn tại hay không
        public bool CheckMaPhucVu(string maphucvu)
        {
            List<GoiMon> dsgm = LayDanhSachGoiMon();
             return Index(dsgm, maphucvu) == -1; // nếu như chưa tồn tại trả về true, ngược lại tồn tại rồi trả về false;
        }
        #endregion

        #region Xóa gọi món
        public void XoaGoiMon(string maphucvu)
        {
            List<GoiMon> dsgm = LayDanhSachGoiMon();
            int index = Index(dsgm, maphucvu);
            if (index == -1) return;

            dsgm.RemoveAt(Index(dsgm, maphucvu));
            gmDAL.Update(dsgm);
        }
        #endregion

    }
}
