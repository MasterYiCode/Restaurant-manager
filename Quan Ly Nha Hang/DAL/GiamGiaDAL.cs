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
    public class GiamGiaDAL : IDAL<GiamGia>
    {
        #region Đường dẫn file
        private readonly static string MyFile = "Data/GiamGia.txt";
        #endregion

        #region Lấy dữ liệu từ tệp
        public List<GiamGia> GetData()
        {
            List<GiamGia> dsgg = new List<GiamGia>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dsgg = null;
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
                        DateTime DayStart = DateTime.ParseExact(a[1], "dd/MM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime DayEnd = DateTime.ParseExact(a[2], "dd/MM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                        dsgg.Add(new GiamGia(a[0], DayStart, DayEnd, a[3], Convert.ToDouble(a[4])));
                    }
                }
                myFile.Close();
                return dsgg;
            }
        }
        #endregion

        #region Thêm dữ liệu vào cuối tệp
        public void Insert(GiamGia Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                fwrite.WriteLine(Object.ToString());
                fwrite.Close();
            }
        }
        #endregion

        #region Update lại danh sách
        public void Update(List<GiamGia> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (GiamGia gg in list)
                {
                    fwrite.WriteLine(gg.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
