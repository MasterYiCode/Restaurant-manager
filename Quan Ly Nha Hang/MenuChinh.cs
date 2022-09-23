using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.UI;
using Quan_Ly_Nha_Hang.GUI;


namespace Quan_Ly_Nha_Hang
{
    class ManuChinh
    {
        static void GioiThieu()
        {
            // Đồ án 1 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ReadKey();
            Console.Clear();
        }

        static void OOP()
        {

            Console.ReadKey();
            Console.Clear();
        }

        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(157, 39);
            Console.CursorVisible = false;

            string[] menu = {"  QUẢN LÝ DANH SÁCH NHÂN VIÊN  ",
                "  QUẢN LÝ DANH SÁCH KHÁCH HÀNG ",
                "     QUẢN LÍ DANH SÁCH BÀN     ",
                "   QUẢN LÝ DANH SÁCH LOẠI MÓN  ",
                "   QUẢN LÝ DANH SÁCH MÓN ĂN    ",
                "     QUẢN LÝ CHẾ ĐỘ ƯU ĐÃI     ",
                "        QUẢN LÍ HÓA ĐƠN        ",
                "             PHỤC VỤ           ",
                "              THOÁT            "};

            FormNhanVien fnv = new FormNhanVien();
            FormKhachHang fkh = new FormKhachHang();
            FormLoaiMonAn flma = new FormLoaiMonAn();
            FormBan fb = new FormBan();
            FormMonAn fma = new FormMonAn();
            FormHoaDon fhd = new FormHoaDon();
            FormPhucVu fpv = new FormPhucVu();
            FormGiamGia fgg = new FormGiamGia();

            GioiThieu();
            //OOP();

            while (true)
            {
                Console.CursorVisible = false;
                string s = @"         QUẢN LÝ NHÀ HÀNG     ";
                int choice = MenuChinh.LuaChon(s, menu, 63, 13, ConsoleColor.Black, ConsoleColor.Yellow);
                TienIch.CleanUp();

                switch (choice)
                {
                    case 1:
                        FormNhanVien.Menu(fnv);
                        break;
                    case 2:
                        FormKhachHang.Menu(fkh);
                        break;
                    case 3:
                        FormBan.Menu(fb);
                        break;
                    case 4:
                        FormLoaiMonAn.Menu(flma);
                        break;
                    case 5:
                        FormMonAn.Menu(fma);
                        break;
                    case 6:
                        FormGiamGia.Menu(fgg);
                        break;
                    case 7:
                        FormHoaDon.Menu(fhd);
                        break;
                    case 8:
                        FormPhucVu.Menu(fpv);
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }

        }
    }

}
