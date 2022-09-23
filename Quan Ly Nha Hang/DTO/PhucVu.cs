using System;

namespace Quan_Ly_Nha_Hang.DTO
{
    public class PhucVu
    {
        public string Maphucvu { get; set; } // Lúc khách đặt bàn thì mã phục vụ sẽ được khởi tạo
        public string Maban { get; set; } // Bàn khách ngồi sẽ chuyển từ rảnh sang bận, nếu bàn nào đang trạng thái bận thì sẽ không thể chọn
        public string Manhanvien { get; set; } // Mã nhân viên phục vụ
        public string Tennhanvien { get; set; } // Tên nhân viên phục vụ
        public string Phone { get; set; } // Số điện thoại khách hàng
        public string Tenkhachhang { get; set; } // Tên khách hàng sẽ được chọn trong danh sách khách hàng hoặc là sẽ là khách mới(Khach Hang).
        public DateTime Thoigianvao { get; set; } // Thời gian khách vào và ngồi vào bàn

        public PhucVu(string maphucvu, string maban, string manhanvien, string tennhanvien,string phone ,string tenkhachhang, DateTime thoigianvao)
        {
            Maphucvu = maphucvu;
            Maban = maban;
            Manhanvien = manhanvien;
            Tennhanvien = tennhanvien;
            Phone = phone;
            Tenkhachhang = tenkhachhang;
            Thoigianvao = thoigianvao;
        }
        public override string ToString()
        {
            return Maphucvu + "#" + Maban + "#" + Manhanvien + "#" + Tennhanvien + "#" + Phone + "#"  + Tenkhachhang + "#" + Thoigianvao.ToString("dd/MM/yyyy HH:mm:ss tt");
        }

    }
}
