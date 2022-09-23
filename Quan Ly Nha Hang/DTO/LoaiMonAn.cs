using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class LoaiMonAn
    {
        
        public string Maloai { get; set; }
        public string Tenloai { get; set; }
        
        public LoaiMonAn(string loaimonan, string tenloai)
        {
            Maloai = loaimonan;
            Tenloai = tenloai;
        }
        public LoaiMonAn(LoaiMonAn lma)
        {
            this.Maloai = string.Copy(lma.Maloai);
            this.Tenloai = string.Copy(lma.Tenloai);
        }
        public override string ToString()
        {
            return Maloai + "#" + Tenloai;
        }
    }
}
