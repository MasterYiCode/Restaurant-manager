using System;
using System.Collections.Generic;
using Quan_Ly_Nha_Hang.DTO;
using System.IO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class PhucVuDAL : IDAL<PhucVu>
    {
        #region Đường dẫn file
        private readonly static string MyFile = "Data/PhucVu.txt";
        #endregion

        #region Lấy dữ liệu từ tệp
        public List<PhucVu> GetData()
        {
            List<PhucVu> dspv = new List<PhucVu>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dspv = null;
            }
            using (StreamReader myFile = File.OpenText(MyFile))
            {
                string temp;
                while ((temp = myFile.ReadLine()) != null)
                {
                    temp = temp.Trim();
                    if (temp != "")
                    {
                        string[] a = temp.Split('#');
                        DateTime TimeVao = DateTime.ParseExact(a[6], "dd/MM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                        dspv.Add(new PhucVu(a[0], a[1], a[2], a[3], a[4],a[5], TimeVao));
                    }
                }
                myFile.Close();
                return dspv;
            }
        }
        #endregion

        #region Thêm dữ liệu vào cuối tệp
        public void Insert(PhucVu Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                fwrite.WriteLine(Object.ToString());
                fwrite.Close();
            }
        }
        #endregion

        #region Update lại danh sách
        public void Update(List<PhucVu> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (PhucVu pv in list)
                {
                    fwrite.WriteLine(pv.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
