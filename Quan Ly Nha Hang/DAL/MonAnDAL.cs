using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;
using System.IO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class MonAnDAL : IDAL<MonAn>
    {
        #region Đường dẫn file, chỉ được đọc
        private readonly static string MyFile = "Data/MonAn.txt";
        #endregion

        #region Lấy dữ liệu từ tệp lưu vào danh sách
        public List<MonAn> GetData()
        {
            List<MonAn> dsma = new List<MonAn>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dsma = null;
            }
            using (StreamReader myFile = File.OpenText(MyFile))
            {
                string temp;
                while ((temp = myFile.ReadLine()) != null)
                {
                    if (temp.Trim() != "") // String.IsNullOrEmpty = kiểm tra xem một chuỗi là null hay một chuỗi trống
                    {
                        string[] a = temp.Split('#');
                        dsma.Add(new MonAn(a[0], a[1], Convert.ToDouble(a[2]), Convert.ToInt32(a[3]), a[4]));
                    }
                }
                myFile.Close();
                return dsma;
            }
        }
        #endregion

        #region Chèn một bản ghi vào cuối tệp
        public void Insert(MonAn Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                fwrite.WriteLine(Object.ToString());
                fwrite.Close();
            }
        }
        #endregion

        #region Cập nhập lại danh sách tệp
        public void Update(List<MonAn> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (MonAn ma in list)
                {
                    fwrite.WriteLine(ma.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
