using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DAL;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class MonAnBLL : IMonAnBLL
    {
        IDAL<MonAn> maDAL = new MonAnDAL();
        IDAL<LoaiMonAn> lmaDAL = new LoaiMonAnDAL();
        #region Lấy danh sách món ăn
        public List<MonAn> LayDanhSachMonAn()
        {
            return maDAL.GetData();
        }
        #endregion

        #region Tìm kiếm và lấy ra được danh sách có mã.....
        public List<MonAn> SearchMonAn(string value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Sửa món ăn
        public void SuaMonAn(string mamon, MonAn Object)
        {
            List<MonAn> dsma = LayDanhSachMonAn();
            dsma[Index(dsma ,mamon)] = Object;
            maDAL.Update(dsma);
        }
        #endregion

        #region Thêm món ăn
        public void ThemMonAn(MonAn Object)
        {
            maDAL.Insert(Object);
        }
        #endregion

        #region Xóa món ăn
        public void XoaMonAn(string mamon)
        {
            List<MonAn> dsma = maDAL.GetData();
            dsma.RemoveAt(Index(dsma ,mamon));
            maDAL.Update(dsma);
        }
        #endregion

        #region Chếch xem loại món của món này đã tồn tại hay chưa
        public bool CheckLoaiMonAn(string loaimon)
        {
            List<LoaiMonAn> listlma = lmaDAL.GetData();
            foreach (LoaiMonAn lma in listlma)
            {
                if (loaimon.ToUpper().Equals(lma.Tenloai.ToUpper())) return true;
            }
            return false;
        }
        #endregion

        #region Lấy ra vị trí có mã món ăn

        public int Index(List<MonAn> listma, string mamonan)
        {
            for (int index = 0; index < listma.Count; ++index)
                if (listma[index].Mamon.ToUpper().Equals(mamonan.ToUpper())) return index;
            return -1;
        }
        public int Index_(List<MonAn> listma, string tenmon)
        {
            if (listma.Count == 0) return -1;
            for (int i= 0; i < listma.Count; ++i)
                if (listma[i].Tenmon.ToUpper().Equals(tenmon.ToUpper())) return i;
            return -1;
        }
        #endregion

        #region Chếch xem mã món ăn đã tồn tại hay chưa

        public bool CheckMaMonAn(string mamonan)
        {
            List<MonAn> listma = maDAL.GetData();
            foreach(MonAn ma in listma)
            {
                if (ma.Mamon.Equals(mamonan)) return true;
            }
            return false;
        }
        #endregion


        public void Update(List<MonAn> list)
        {
            maDAL.Update(list);
        }
        
        public double CheckMonAn(string tenmon)  // Chếch xem tên món này có tồn tại hay không nếu tồn tại trả về true
        {
            List<MonAn> listma = LayDanhSachMonAn();
            if (listma == null) return -1;
            foreach (MonAn ma in listma)
            {
                if (ma.Tenmon.Equals(tenmon)) return ma.Dongia;
            }
            return -1;
        }

        public bool CheckSoLuong(string tenmon,int soluong) // Chếch xem số lượng món ăn này theo yêu cầu khách thì có đủ hay không?
        {
            List<MonAn> listma = LayDanhSachMonAn();
            if (listma == null) return false;
            foreach (MonAn ma in listma)
            {
                if (ma.Tenmon.ToUpper().Equals(tenmon.ToUpper()))
                {
                    if (ma.Soluong >= soluong) return true;
                    return false;
                }
                
            }
            return false;
        }

        public bool UpdateSoLuong(string tenmon, int soluong, string trangthai) // Nếu trạng thái là nhập thì + thêm số lượng, Nếu trạng thái là xuất
            // thì - đi còn gì nữa
        {
            List<MonAn> listma = LayDanhSachMonAn();
            if (listma == null) return false;
            for(int i = 0; i < listma.Count; i++)
            {
                if (listma[i].Tenmon.ToUpper().Equals(tenmon.ToUpper()))
                {
                    if(trangthai.ToUpper().Equals("NHAP"))
                    {
                        listma[i].Soluong = listma[i].Soluong + soluong;
                    }
                    if(trangthai.ToUpper().Equals("XUAT"))
                    {
                        listma[i].Soluong = listma[i].Soluong - soluong;

                    }
                    maDAL.Update(listma);
                    return true;
                }
            }
            return false;
        }
        /*
        Một số lưu ý khi sửa món ăn là: sửa cái gì thì sửa nếu sửa loại món thì phải sửa sang một loại món đã tồn tại
        hoặc thông báo : Loại món này chưa tồn tại, mời bạn vào chức năng loại món để thêm món này. hoặc hủy quay lại
        */
    }
}
