using System;
using System.Collections.Generic;
using Quan_Ly_Nha_Hang.DTO;
using System.Text.RegularExpressions;
using System.IO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class NhanVienDAL : IDAL<NhanVien>
    {
        #region Đường dẫn file, chỉ được đọc
        private readonly static string MyFile = "./Data/NhanVien.txt";
        #endregion

        #region Lấy dữ liệu từ tệp lưu vào danh sách
        public List<NhanVien> GetData()
        {
            List<NhanVien> dsnv = new List<NhanVien>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dsnv = null;
            }
            using (StreamReader myFile = File.OpenText(MyFile))
            {
                Regex congthuc = new Regex("^[0-9]{6}#([A-Z][a-z ]*)*#(0?[1-9]|[12][0-9]|3[01])[/-](0?[1-9]|1[012])[/-][0-9]{4}#(Nam|Nu|Khac)#((09|03|07|08)[0-9]{8})$");
                string temp;
                while ((temp = myFile.ReadLine()) != null)
                {
                    if (congthuc.IsMatch(temp.Trim()))// tương đương để temp.Trim() != ""
                    {
                        string[] a = temp.Split('#');
                        DateTime DT = DateTime.ParseExact(a[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        dsnv.Add(new NhanVien(a[0], a[1], DT, a[3], a[4]));
                    }
                }
                myFile.Close();
                return dsnv;
            }
        }
        #endregion

        #region Chèn một bản ghi vào cuối tệp
        public void Insert(NhanVien Object)
        {
            StreamWriter fwrite = File.AppendText(MyFile);
            fwrite.WriteLine(Object.ToString());
            fwrite.Close();
        }
        #endregion

        #region Cập nhập lại danh sách tệp
        public void Update(List<NhanVien> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (NhanVien nv in list)
                {
                    fwrite.WriteLine(nv.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
