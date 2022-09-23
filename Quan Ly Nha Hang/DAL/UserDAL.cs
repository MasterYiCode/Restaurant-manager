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
    public class UserDAL : IDAL<User>
    {
        #region Đường dẫn file, chỉ được đọc
        private readonly static string MyFile = "Data/User.txt";
        #endregion

        #region Lấy dữ liệu từ tệp lưu vào danh sách
        public List<User> GetData()
        {
            List<User> dsuser = new List<User>();
            if (!File.Exists(MyFile))
            {
                File.CreateText(MyFile);
                return dsuser = null;
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
                        dsuser.Add(new User(a[0], a[1], a[2]));
                    }
                }
                myFile.Close();
                return dsuser;
            }
        }
        #endregion

        #region Chèn một bản ghi vào cuối tệp
        public void Insert(User Object)
        {
            StreamWriter fwrite = File.AppendText(MyFile);
            fwrite.WriteLine(Object.ToString());
            fwrite.Close();
        }
        #endregion

        #region Cập nhập lại danh sách tệp
        public void Update(List<User> list)
        {
            using (StreamWriter fwrite = File.CreateText(MyFile))
            {
                foreach (User us in list)
                {
                    fwrite.WriteLine(us.ToString());
                }
                fwrite.Close();
            }
        }
        #endregion
    }
}
