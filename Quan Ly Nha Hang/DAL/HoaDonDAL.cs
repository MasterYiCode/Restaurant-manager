using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;
using System.IO;
using System.Text.RegularExpressions;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class HoaDonDAL:IDAL<HoaDon>
    {
        #region Đường dẫn file, chỉ được đọc
        private readonly static string MyFile = "Data/HoaDon.txt";
        #endregion

        #region Lấy dữ liệu từ tệp lưu vào danh sách
        public List<HoaDon> GetData()
        {
            List<HoaDon> dshd = new List<HoaDon>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dshd = null;
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
                        DateTime TimeRa = DateTime.ParseExact(a[7], "dd/MM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                        dshd.Add(new HoaDon(a[0], a[1], a[2], a[3], a[4], Convert.ToDouble(a[5]), TimeVao, TimeRa, a[8]));
                    }
                }
                myFile.Close();
                return dshd;
            }
        }
        #endregion

        #region Chèn một bản ghi vào cuối tệp
        public void Insert(HoaDon Object)
        {
            StreamWriter fwrite = File.AppendText(MyFile);
            fwrite.WriteLine(Object.ToString());
            fwrite.Close();
        }
        #endregion

        #region Cập nhập lại danh sách tệp
        public void Update(List<HoaDon> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (HoaDon hd in list)
                {
                    fwrite.WriteLine(hd.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
