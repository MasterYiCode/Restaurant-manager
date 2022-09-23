using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class User
    {
        public string Taikhoan  { get; set; }
        public string Matkhau { get; set; }
        public string ChucVu { get; set; }


        public User(string taikhoan, string matkhau, string chucVu)
        {
            Taikhoan = taikhoan;
            Matkhau = matkhau;
            ChucVu = chucVu;
        }

        public User(User user)
        {
            this.Taikhoan = user.Taikhoan;
            this.Matkhau = user.Matkhau;
            this.ChucVu = user.ChucVu;
        }

        public override string ToString()
        {
            return Taikhoan + "#" + Matkhau + "#" + ChucVu;
        }

    }
}
