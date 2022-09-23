using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class Ban
    {
        public string Maban { get; set; }
        public string Loaiban { get; set; }
        public string Trangthai { get; set; }
        public string Ghichu { get; set; }

        public Ban(string maban, string loaiban, string trangthai, string ghichu)
        {
            Maban = maban;
            Loaiban = loaiban;
            Trangthai = trangthai;
            Ghichu = ghichu;
        }
        public Ban(Ban b)
        {
            this.Maban = b.Maban;
            this.Loaiban = b.Loaiban;
            this.Trangthai = b.Trangthai;
            this.Ghichu = b.Ghichu;
        }

        public override string ToString()
        {
            return Maban + "#" + Loaiban + "#" + Trangthai + "#" + Ghichu;
        }
    }
}
