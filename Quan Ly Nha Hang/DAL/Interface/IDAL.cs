using System.Collections.Generic;

namespace Quan_Ly_Nha_Hang.DAL
{
    public interface IDAL<T>
    {
        List<T> GetData(); // lấy dữ liệu từ tệp ra
        void Insert(T Object); // Thêm dữ liệu vào cuối tệp
        void Update(List<T> list); // Xoá dữ liệu tệp cũ và update đanh sách nhân viên mới vào tệp

    }
}
