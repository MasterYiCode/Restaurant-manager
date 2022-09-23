using Quan_Ly_Nha_Hang.DTO;
using Quan_Ly_Nha_Hang.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.BLL
{
    public class LoaiMonAnBLL : ILoaiMonAnBLL
    {
        IDAL<LoaiMonAn> lmaDAL = new LoaiMonAnDAL();
        IDAL<MonAn> maDAL = new MonAnDAL();

        #region  Khi muốn xóa loại món ăn phải xem có món ăn nào có loại món ăn muốn xóa không, nếu có thì phải xóa hết món ăn có loại món ăn này thì mới xóa được loại món ăn này
        public bool CheckMonAn(string loaimonan) // Khi muốn xóa loại món ăn phải xem có món ăn nào có loại món ăn muốn xóa không, nếu có thì phải xóa hết món 
            //ăn có loại món ăn này thì mới xóa được loại món ăn này
        {
            List<MonAn> dsma = maDAL.GetData();
            foreach(MonAn ma in dsma)
            {
                if (ma.Loaimon.ToUpper().Equals(loaimonan.ToUpper()))
                {
                    return true;
                }
            }
            return false; // Không có món nào mang tên loại món
        }
        #endregion

        #region Check loại món ăn này tồn tại hay chưa
        public bool CheckMaLoaiMonAn(string maloai)
        {
            List<LoaiMonAn> dslma = lmaDAL.GetData();
            foreach (LoaiMonAn lma in dslma)
            {
                if (lma.Maloai.Equals(maloai))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Lấy ra vị trí của mã loại món ăn trong danh sách
        public int Index(List<LoaiMonAn> listlma, string maloai)
        {
            for (int index = 0; index < listlma.Count; ++index)
                if (listlma[index].Maloai.ToUpper().Equals(maloai.ToUpper())) return index;
            return -1;
        }
        #endregion

        #region Lấy ra danh sách loại món ăn
        public List<LoaiMonAn> LayDanhSachLoaiMonAn()
        {
            return lmaDAL.GetData();
        }
        #endregion

        #region Lấy ra danh sách có mã hay tên trùng với .....
        public List<LoaiMonAn> SearchLoaiMonAn(string value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Sửa loại món ăn ăn
        public void SuaLoaiMonAn(string mamon, LoaiMonAn Object)
        {
            List<LoaiMonAn> dslma = LayDanhSachLoaiMonAn();
            dslma[Index(dslma, mamon)] = Object;
            lmaDAL.Update(dslma);
        }
        #endregion

        #region Thêm loại món ăn vào cuối tệp
        public void ThemLoaiMonAn(LoaiMonAn Object)
        {
            lmaDAL.Insert(Object);
        }
        #endregion

        #region Xóa đi loại món ăn nào đó
        public void XoaLoaiMonAn(string mamon)
        {
            List<LoaiMonAn> dslma = lmaDAL.GetData(); // =  LayDanhSachMonAn();
            dslma.RemoveAt(Index(dslma, mamon));
            lmaDAL.Update(dslma);
        }
        #endregion



        /*
         Một số lưu ý ở loại món ăn
        - Nếu sửa loại món ăn, sửa mã không sao, nếu sửa tên thì phải kiểm tra xem có món ăn nào có loại món này không, nếu có thì khi sửa loại món này thì 
        loại món của những món ăn kia cũng sẽ phải bị thay đổi hết sang loại món mới.

         */
    }
}
