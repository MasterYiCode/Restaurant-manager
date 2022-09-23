using System;
using System.Collections.Generic;
using Quan_Ly_Nha_Hang.DTO;

using System.IO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class BanDAL : IDAL<Ban>
    {
        #region Đường dẫn file
        private readonly static string MyFile = "Data/Ban.txt";
        #endregion

        #region Lấy dữ liệu từ tệp
        public List<Ban> GetData()
        {
            List<Ban> dsb = new List<Ban>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dsb = null;
            }
            using (StreamReader myFile = File.OpenText(MyFile))
            {
                string temp;
                while ((temp = myFile.ReadLine()) != null)
                {
                    temp = temp.Trim();
                    ; if (temp != "")
                    {
                        string[] a = temp.Split('#');
                        dsb.Add(new Ban(a[0], a[1], a[2], a[3]));
                    }
                }
                myFile.Close();
                return dsb;
            }
        }
        #endregion

        #region Chèn một bản ghi nhân viên vào cuối tệp
        public void Insert(Ban Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                fwrite.WriteLine(Object.ToString());
                fwrite.Close();
            }
        }
        #endregion

        #region Cập nhập lại danh sách khách hàng vào tệp
        public void Update(List<Ban> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (Ban b in list)
                {
                    fwrite.WriteLine(b.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
