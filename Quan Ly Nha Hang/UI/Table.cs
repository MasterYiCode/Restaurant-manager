using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.UI
{
    public class Table
    {
        #region Trường dữ liệu
        private const string KhopTrenBenTrai = "╔"; //Khớp trên cùng bên trái
        private const string KhopTrenBenPhai = "╗"; // KHớp trên cùng bên phải
        private const string KhopDuoiBenTrai = "╚";//Khớp dưới cùng bên trái
        private const string KhopDuoiBenPhai = "╝";//Khớp dưới cùng bên phải
        private const string NoiTrai = "╠";
        private const string NoiPhai = "╣";
        private const string Ngang = "═";
        private const string Doc = "║";
        private const string Up = "╦";
        private const string Ngua = "╩";
        #endregion
        public static void Khung()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(KhopTrenBenTrai); for (int i = 1; i < 155; i++) Console.Write(Ngang); Console.Write(KhopTrenBenPhai);
            for (int i = 1; i < 4; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write(Doc);
                Console.SetCursorPosition(155, i); Console.Write(Doc);
            }
            Console.Write("\n" + NoiTrai);
            for (int i = 1; i < 50; i++) Console.Write(Ngang);
            Console.Write(Up);
            for (int i = 51; i < 155; i++) Console.Write(Ngang);
            Console.Write(NoiPhai);
            for (int i = 5; i < 34; i++)
            {
                if(i == 27)
                {
                    Console.SetCursorPosition(0, i); Console.Write(NoiTrai + new string('═', 49) + NoiPhai);
                    Console.SetCursorPosition(155, i); Console.Write(Doc);
                    continue;
                }
                Console.SetCursorPosition(0, i); Console.Write(Doc);
                Console.SetCursorPosition(50, i); Console.Write(Doc);
                Console.SetCursorPosition(155, i); Console.Write(Doc);
            }

            Console.Write("\n" + NoiTrai + new string('═', 49) + Ngua + new string('═', 104) + NoiPhai);
            Console.Write("\n" + Doc + new string(' ', 154) + Doc);

            Console.Write("\n" + NoiTrai + new string('═', 154) + NoiPhai);

            Console.SetCursorPosition(0, 37); Console.Write(Doc);
            Console.SetCursorPosition(155, 37); Console.Write(Doc);
            Console.Write("\n" + KhopDuoiBenTrai); for (int i = 1; i < 155; i++) Console.Write(Ngang); Console.Write(KhopDuoiBenPhai);
            Console.ResetColor();

            Console.SetCursorPosition(70, 2);
            Console.Write("QUẢN LÝ NHÀ HÀNG");
            Console.SetCursorPosition(3, 35);
            Console.Write("THÔNG BÁO : ");
        }

        public static void KhungPhucVu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(KhopTrenBenTrai); for (int i = 1; i < 155; i++) Console.Write(Ngang); Console.Write(KhopTrenBenPhai);
            for (int i = 1; i < 4; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write(Doc);
                Console.SetCursorPosition(155, i); Console.Write(Doc);
            }
            Console.Write("\n" + NoiTrai);
            for (int i = 1; i < 85; i++) Console.Write(Ngang);
            Console.Write(Up);
            for (int i = 86; i < 155; i++) Console.Write(Ngang);
            Console.Write(NoiPhai);
            for (int i = 5; i < 36; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write(Doc);
                Console.SetCursorPosition(85, i); Console.Write(Doc);
                Console.SetCursorPosition(155, i); Console.Write(Doc);


            }
            Console.Write("\n" + NoiTrai);
            for (int i = 1; i < 85; i++) Console.Write(Ngang);
            Console.Write(Ngua);
            for (int i = 86; i < 155; i++) Console.Write(Ngang);
            Console.Write(NoiPhai);
            Console.SetCursorPosition(0, 37); Console.Write(Doc);
            Console.SetCursorPosition(155, 37); Console.Write(Doc);
            Console.Write("\n" + KhopDuoiBenTrai); for (int i = 1; i < 155; i++) Console.Write(Ngang); Console.Write(KhopDuoiBenPhai);
            Console.ResetColor();

            Console.SetCursorPosition(70, 2);
            Console.Write("QUẢN LÝ NHÀ HÀNG");
            Console.SetCursorPosition(3, 35);
            Console.Write("THÔNG BÁO : ");
        }


        public static void ChucNang(string doituong)
        {
            Console.SetCursorPosition(3, 37);
            Console.Write($"F2: Thêm {doituong} -- F3: Xóa {doituong} -- F4: Chỉnh sửa thông tin -- F5: Tìm kiếm -- ENTER: Chọn -- ESC: Quay lại -- PAGE UP/PAGE DOWN: Chuyển trang");
        }
        public static void ChucNangX(string doituong)
        {
            Console.SetCursorPosition(3, 37);
            Console.Write($"F2: Thêm {doituong} - F3: Xóa {doituong} - F4: Chỉnh sửa thông tin - F5: Tìm kiếm - F6: Thống kê- ENTER: Chọn - ESC: Quay lại - PU/PD: Chuyển trang");
        }
        public static void ChucNangY(string hoadon)
        {
            Console.SetCursorPosition(3, 37);
            Console.Write($"F2: Xem thông tin hóa đơn - F3: Thống kê doanh thu ngày - F4: Tháng - F5: Năm -F6: Xóa hóa đơn - ENTER: Chọn - ESC: Quay lại - PU/PD: Chuyển trang");
        }

        public static void ChucNangZ()
        {
            Console.SetCursorPosition(3, 37);
            Console.Write($"F2: Xem phục vụ - F3: Xem thông tin bàn(B/R) F4: Phục Vụ - ENTER: Chọn - ESC: Quay lại - PU/PD: Chuyển trang");
        }

        public static void Dong(int x, int y, int rong)
        {
            Console.SetCursorPosition(x, y); Console.Write("┌" + new string('─', rong - 2) + "┐");
        }
        public static void Dong1(int x, int y, int rong)
        {
            Console.SetCursorPosition(x, y); Console.Write("|");
            Console.SetCursorPosition(x + rong - 1, y); Console.Write("|");
        }

        public static void Dong2(int x, int y, int rong)
        {
            Console.SetCursorPosition(x, y); Console.Write("├" + new string('─', rong - 2) + "┤");
        }

        public static void Dong3(int x, int y, int rong)
        {
            Console.SetCursorPosition(x, y); Console.Write("└" + new string('─', rong - 2) + "┘");
        }

        public static void Menu(int x, int y, int rong, int soluong, string tieude)
        {
            if (soluong < 2) return;
            Console.SetCursorPosition((86 - tieude.Length) / 2, 6); Console.Write(tieude);
            Dong(x, y, rong);
            for (int i = 1; i < soluong; i++)
            {
                Dong1(x, ++y, rong);
            }
            Dong3(x, ++y, rong);
        }

        public static void TableNhap(int x, int y, int rong, int soluong, string tieude)
        {
            if (soluong < 2) return;
            if (soluong == 2)
            {
                Console.SetCursorPosition((51 - tieude.Length) / 2, 6); Console.Write(tieude);
                Dong(x, y, rong);
                Dong1(x, ++y, rong);
                Dong3(x, ++y, rong);
                return;
            }
            Console.SetCursorPosition((51 - tieude.Length) / 2, 6); Console.Write(tieude);
            Dong(x, y, rong);
            for (int i = 1; i < soluong; i++)
            {
                Dong1(x, ++y, rong);
                Dong2(x, ++y, rong);
            }
            Dong1(x, ++y, rong);
            Dong3(x, ++y, rong);
        }

        public static void TableNhap1(int x, int y, int rong, int soluong, string tieude)
        {
            if (soluong < 2) return;
            if (soluong == 2)
            {
                Dong(x, y, rong);
                Dong3(x, ++y, rong);
                return;
            }
            Console.SetCursorPosition((86 - tieude.Length) / 2, y - 2); Console.Write(tieude);
            Dong(x, y, rong);
            for (int i = 1; i < soluong; i++)
            {
                Dong1(x, ++y, rong);
                Dong2(x, ++y, rong);
            }
            Dong1(x, ++y, rong);
            Dong3(x, ++y, rong);
        }
        public static void TableHienThiDanhSach(int x, int y, int rong, int soluong, string tieude)
        {
            if (soluong < 2) return;
            if (soluong == 2)
            {
                Dong(x, y, rong);
                Dong3(x, ++y, rong);
                return;
            }
            Console.SetCursorPosition(50 + (104 - tieude.Length) / 2, 6); Console.Write(tieude);
            Dong(x, y, rong);
            for (int i = 1; i < soluong; i++)
            {
                Dong1(x, ++y, rong);
                Dong2(x, ++y, rong);
            }
            Dong1(x, ++y, rong);
            Dong3(x, ++y, rong);
        }

        public static void TableHienThiDanhSach1(int x, int y, int rong, int soluong, string tieude)
        {
            if (soluong < 2) return;
            if (soluong == 2)
            {
                Dong(x, y, rong);
                Dong3(x, ++y, rong);
                return;
            }
            Console.SetCursorPosition(87 + (69 - tieude.Length) / 2, 6); Console.Write(tieude);
            Dong(x, y, rong);
            for (int i = 1; i < soluong; i++)
            {
                Dong1(x, ++y, rong);
                Dong2(x, ++y, rong);
            }
            Dong1(x, ++y, rong);
            Dong3(x, ++y, rong);
        }


    }
}
