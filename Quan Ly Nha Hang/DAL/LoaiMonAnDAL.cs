using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class LoaiMonAnDAL : IDAL<LoaiMonAn>
    {
        #region Đường dẫn file, chỉ được đọc
        private readonly static string MyFile = "Data/LoaiMonAn.txt";
        #endregion

        #region Lấy các loại món ăn từ tệp
        public List<LoaiMonAn> GetData()
        {
            List<LoaiMonAn> dslma = new List<LoaiMonAn>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dslma = null;
            }
            using (StreamReader myFile = File.OpenText(MyFile))
            {
                string temp;
                while ((temp = myFile.ReadLine()) != null)
                {
                    if (temp != "")
                    {
                        string[] a = temp.Split('#');
                        dslma.Add(new LoaiMonAn(a[0], a[1]));
                    }
                }
                myFile.Close();
                return dslma;
            }
        }
        #endregion

        #region Thêm loại món ăn vào cuối tệp
        public void Insert(LoaiMonAn Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                fwrite.WriteLine(Object.ToString());
                fwrite.Close();
            }
        }
        #endregion

        #region Cập nhập lại danh sách loại món ăn
        public void Update(List<LoaiMonAn> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (LoaiMonAn nv in list)
                {
                    fwrite.WriteLine(nv.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
