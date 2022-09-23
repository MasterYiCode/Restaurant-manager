using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;
using Quan_Ly_Nha_Hang.DAL;
using System.Text.RegularExpressions;
using System.IO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class KhachHangDAL : IDAL<KhachHang>
    {
        #region Đường dẫn file
        private readonly static string MyFile = "Data/KhachHang.txt";
        #endregion

        #region Lấy dữ liệu từ tệp
        public List<KhachHang> GetData()
        {
            List<KhachHang> dskh = new List<KhachHang>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dskh = null;
            }
            using (StreamReader myFile = File.OpenText(MyFile))
            {
                string temp;
                while ((temp = myFile.ReadLine()) != null)
                {
                    temp = temp.Trim();
;                   if (temp != "")
                    {
                        string[] a = temp.Split('#');
                        dskh.Add(new KhachHang(a[0], a[1], a[2], a[3], a[4]));
                    }
                }
                myFile.Close();
                return dskh;
            }
        }
        #endregion

        #region Chèn một bản ghi nhân viên vào cuối tệp
        public void Insert(KhachHang Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                fwrite.WriteLine(Object.ToString());
                fwrite.Close();
            }
        }
        #endregion

        #region Cập nhập lại danh sách khách hàng vào tệp
        public void Update(List<KhachHang> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (KhachHang kh in list)
                {
                    fwrite.WriteLine(kh.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}

