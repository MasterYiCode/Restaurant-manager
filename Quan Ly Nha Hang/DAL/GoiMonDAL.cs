using System;
using System.Collections.Generic;
using Quan_Ly_Nha_Hang.DTO;
using System.IO;

namespace Quan_Ly_Nha_Hang.DAL
{
    public class GoiMonDAL : IDAL<GoiMon>
    {
        #region Đường dẫn file
        private readonly static string MyFile = "Data/GoiMon.txt";
        #endregion

        #region Lấy dữ liệu từ tệp
        public List<GoiMon> GetData()
        {
            List<GoiMon> dsgm = new List<GoiMon>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dsgm = null;
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
                        int index = 1;

                        List<MonAn> dsmaOrder = new List<MonAn>();

                        for(int i = 0; i < (a.Length - 2)/3; i++)
                        {
                            dsmaOrder.Add(new MonAn(a[index++], Convert.ToDouble(a[index++]), Convert.ToInt32(a[index++])));
                        }
                        dsgm.Add(new GoiMon(a[0], dsmaOrder, Convert.ToDouble(a[index])));   
                    }
                }
                myFile.Close();
                return dsgm;
            }
        }
        #endregion
        public void Insert(GoiMon Object)
        {
            using (StreamWriter fwrite = File.AppendText(MyFile))
            {
                string order = Object.Maphucvu + "#";
                foreach (MonAn ma in Object.Order)
                {
                    order += ma.Display()  + "#";
                }
                order +=Object.Tongthanhtoanhientai.ToString();
                fwrite.WriteLine(order);
                fwrite.Close();
            }
        }

        public void Update(List<GoiMon> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (GoiMon gm in list)
                {
                    string order = gm.Maphucvu + "#";
                    foreach (MonAn ma in gm.Order)
                    {
                        order += ma.Display() + "#";
                    }
                    order += gm.Tongthanhtoanhientai.ToString();
                    fwrite.WriteLine(order);
                    fwrite.Close();
                }
                fwrite.Close();
            }
        }
    }
}
