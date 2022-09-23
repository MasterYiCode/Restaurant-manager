using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Quan_Ly_Nha_Hang.DTO;
using Quan_Ly_Nha_Hang.BLL;
using Quan_Ly_Nha_Hang.UI;
using System.Text.RegularExpressions;

namespace Quan_Ly_Nha_Hang.GUI
{
    public class FormBan:Form
    {
        IBanBLL bBLL = new BanBLL();
        #region Nhập bàn 
        public void NhapBan()
        {
            string maban, loaiban, trangthai, ghichu;
            Console.CursorVisible = true;
            Table.TableNhap(3, 8, 45, 4, "THÊM BÀN");
            TienIch.WriteString(5, 9, $"Mã bàn       : {bBLL.MaBanMoi()}");
            TienIch.WriteString(5, 11, "Loại bàn     : ");
            TienIch.WriteString(5, 13, "Trạng thái   : ");
            TienIch.WriteString(5, 15, "Ghi chú      : ");

            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Loại bàn có 2 loại là Vip và Thuong.");
            TienIch.WriteString(3, 30, "+ Trạng thái chỉ có thể là Ban hoặc Ranh.");
            TienIch.WriteString(3, 31, "+ Ghi chú có thể có hoặc không nhập.");

            maban = bBLL.MaBanMoi();

            do
            {
                TienIch.EnterString(20, 11, out loaiban);
                if (!loaiban.ToUpper().Equals("VIP") && !loaiban.ToUpper().Equals("THUONG"))
                {
                    TienIch.WriteString(15, 35, "Loại bàn sai yêu cầu, xem lại NOTE!", ConsoleColor.Red);
                    TienIch.DeleteRow(20, 11, loaiban.Length);
                }

            } while (!loaiban.ToUpper().Equals("VIP") && !loaiban.ToUpper().Equals("THUONG"));
            TienIch.DeleteRow(15, 35, 60);

            do
            {
                TienIch.EnterString(20, 13, out trangthai);

                if (!trangthai.ToUpper().Equals("BAN") && !trangthai.ToUpper().Equals("RANH"))
                {
                    TienIch.WriteString(15, 35, "Loại bàn sai yêu cầu, xem lại NOTE!", ConsoleColor.Red);
                    TienIch.DeleteRow(20, 13, trangthai.Length);
                }

            } while (!trangthai.ToUpper().Equals("BAN") && !trangthai.ToUpper().Equals("RANH"));
            TienIch.DeleteRow(15, 35, 60);

            TienIch.EnterString(20, 15, out ghichu);

            if(ghichu.Trim() == "")
            {
                ghichu = "          ";
            }
            #region Xóa khung nhập món ăn nè
            TienIch.DeleteRow(17, 6, 15);
            for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
            for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);
            #endregion
            bBLL.ThemBan(new Ban(maban, loaiban, trangthai, ghichu));
            int tranghientai, sotrang;
            Trang(bBLL.LayDanhSachBan(), out tranghientai, out sotrang);
            HienThi(bBLL.LayDanhSachBan(), 1, sotrang);
            Console.CursorVisible = false;
        }
        #endregion

        #region Hiển thị danh sách món ăn
        public void HienThi(List<Ban> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 11); Print.PrintRow("Mã bàn", "Loại bàn", "Trạng thái", "Ghi chú");
            int sl = list.Count;
            int a = 1;
            if (sl == 0)
            {
                for (int i = 0; i < 9 - sl; i++)
                {
                    TienIch.DeleteRow(53, 12 + a, 100);
                    a += 2;
                }
            }
            else if (sl < 9)
            {
                HienThi(list, sl - 1, -1, ref a);
                for (int i = 0; i < 9 - sl; i++)
                {
                    TienIch.DeleteRow(53, 12 + a, 100);
                    a += 2;
                }
            }
            else
            {
                HienThi(list, sl - 1, sl - 10, ref a);
            }
            Display_Count_Trang(list, tranghientai, sotrang);

        }
        public void HienThi(List<Ban> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(53, 12 + y);
                Print.PrintRow(list[i].Maban, list[i].Loaiban, list[i].Trangthai, list[i].Ghichu) ;
                y += 2;
            }
        }

        public void DisplayObject(List<Ban> list, int i)
        {
            Print.PrintRow(list[i].Maban, list[i].Loaiban, list[i].Trangthai, list[i].Ghichu);
        }


        #endregion


        #region Tìm kiếm bàn
        public string TimKiemBan(string title)
        {
            Console.CursorVisible = true;
            string result = string.Empty;
            int count1 = 0;
            List<Ban> dsb = bBLL.LayDanhSachBan();
            List<Ban> dsb_temp = new List<Ban>();
            ConsoleKeyInfo key;

            TienIch.WriteString(53, 9, $"{title} ");

            while (true)
            {
                Console.SetCursorPosition(53 + title.Length + 1 + count1, 9);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    int tranghientai = 1;
                    int sotrang;
                    Display_Count_Trang(dsb_temp, out tranghientai, out sotrang);
     
                    //int stt = Chon_Ban(dsb_temp, ref tranghientai, sotrang);
                    int stt = Chon<Ban>(53, 13, dsb_temp, tranghientai, sotrang, DisplayObject, HienThi);
                    if (stt == -1) return string.Empty;
                    TienIch.DeleteRow(53, 9, 40);
                    return dsb_temp[stt].Maban;
                }
                dsb_temp.Clear();

                if (((int)key.KeyChar >= 48 && (int)key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32)
                {
                    if (count1 < 30)
                    {
                        Console.SetCursorPosition(53 + title.Length + 1 + count1, 9);
                        Console.WriteLine(key.KeyChar);
                        result += key.KeyChar.ToString();
                        count1++;
                    }
                }
                else if ((int)key.KeyChar == 8 && (string.IsNullOrEmpty(result) == false))
                {
                    Console.Write("\b \b");
                    result = result.Substring(0, count1 - 1);
                    count1--;
                }
                foreach (var b in dsb)
                {
                    if (b.Maban.Contains(result) == true || b.Loaiban.ToLower().Contains(result.ToLower()) == true)
                    {
                        dsb_temp.Add(new Ban(b));

                    }
                }
                int a = 1;
                int vitri = dsb_temp.Count;
                if (vitri == 0)
                {
                    for (int i = 1; i <= 9 - vitri; i++) { TienIch.DeleteRow(53, 12 + a, 100); a += 2; }
                }
                else if (vitri < 9)
                {
                    HienThi(dsb_temp, vitri - 1, -1, ref a);
                    for (int i = 1; i <= 9 - vitri; i++)
                    {
                        TienIch.DeleteRow(53, 12 + a, 100); a += 2;
                    }
                }
                else HienThi(dsb_temp, vitri - 1, vitri - 9 - 1, ref a);
                int tranghientainew, sotrangnew;
                Display_Count_Trang(dsb_temp, out tranghientainew, out sotrangnew);

            }
        }
        #endregion

        #region Xóa bàn
        public void XoaBan()
        {
            List<Ban> dsb = bBLL.LayDanhSachBan();
            int tranghientai, sotrang;
            Display_Count_Trang(dsb, out tranghientai, out sotrang);
            HienThi(dsb, tranghientai, sotrang);

            string maban = TimKiemBan("Tìm kiếm bàn để xóa:");

            int stt = bBLL.Index(dsb, maban);

            if (maban == string.Empty) return;
            else if (dsb[stt].Trangthai.ToUpper().Equals("BAN"))
            {
                TienIch.WriteString(15, 35, "Không thể xóa bàn, do bàn đang phục vụ khách, nhấn bất kì để thoát.", ConsoleColor.Red);
                Console.ReadKey();
                TienIch.DeleteRow(15, 35, 70);
            }
            else
            {
                bBLL.XoaBan(dsb[stt].Maban.Trim());
                dsb.RemoveAt(stt);

                Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {dsb.Count}  ");

                int a = 1;
                int vitri = dsb.Count - 9 * (tranghientai - 1) - 1;

                if (dsb.Count % 9 == 0 && stt == 0)
                {
                    HienThi(dsb, 8, -1, ref a);
                    TienIch.DeleteRow(53, 12 + a, 100);
                    TienIch.WriteString(150, 32, $"{tranghientai - 1}/{sotrang - 1} ");
                    return;
                }
                else if (vitri < 9)
                {
                    HienThi(dsb, vitri, -1, ref a);
                    TienIch.DeleteRow(53, 12 + a, 100);
                    return;
                }
                else
                {
                    HienThi(dsb, vitri, vitri - 9, ref a);
                    return;
                }
            }
            
        }
        // 1. Khi bàn đang phục vụ => không thể xóa bàn
        // 2. Khi bàn đang bận => cũng không thể xóa
        // 3. .........................................
        // 4. Bàn đang phục vụ không thể sửa.
        #endregion










        public void SuaBan()
        {

        }


        #region Next trang
        public void ChuyenTrang(List<Ban> list, ConsoleKey cki, ref int sotrang, ref int tranghientai)
        {
            if (cki == ConsoleKey.PageDown)
            {
                if (sotrang > tranghientai)
                {
                    int y = 1;
                    int vitri = list.Count - (tranghientai) * 9 - 1;
                    tranghientai++;
                    if (vitri < 9)
                    {
                        HienThi(list, vitri, -1, ref y);
                        for (int i = 0; i <= 9 - vitri; i++)
                        {
                            TienIch.DeleteRow(53, 12 + y, 100);
                            y += 2;
                        }
                        return;
                    }
                    HienThi(list, vitri, vitri - 9, ref y);
                }
            }
            else if (cki == ConsoleKey.PageUp)
            {
                if (tranghientai > 1)
                {
                    tranghientai--;
                    int y = 1;
                    int vitri = list.Count - (tranghientai - 1) * 9 - 1;
                    HienThi(list, vitri, vitri - 9, ref y);
                }
            }
        }
        #endregion

        #region Menu chính
        public static void Menu(FormBan fb)
        {
            int tranghientai, sotrang;
            IBanBLL bBLL = new BanBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 103, 11, "DANH SÁCH BÀN");
            Table.ChucNang("bàn");
            List<Ban> dsb = bBLL.LayDanhSachBan();
            fb.Trang(dsb, out tranghientai, out sotrang);
            fb.HienThi(dsb, tranghientai, sotrang);
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        tranghientai = 1;
                        fb.HienThi(bBLL.LayDanhSachBan(), tranghientai, 1);
                        fb.NhapBan();
                        fb.Display_Count_Trang(bBLL.LayDanhSachBan(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F3:
                        fb.HienThi(bBLL.LayDanhSachBan(), tranghientai, sotrang);
                        fb.XoaBan();
                        fb.Get_Trang(bBLL.LayDanhSachBan(), out sotrang);
                        break;
                    //case ConsoleKey.F4:
                    //    tranghientai = 1;
                    //    fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                    //    fma.ChinhSuaMonAn();
                    //    break;
                    //case ConsoleKey.F5:
                    //    tranghientai = 1;
                    //    fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                    //    fma.TimKiemMonAn();
                    //    break;
                    //case ConsoleKey.F6:
                    //    fma.ThongKe();
                    //    fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                    //    break;
                    case ConsoleKey.PageDown:
                        fb.ChuyenTrang(bBLL.LayDanhSachBan(), ConsoleKey.PageDown, ref sotrang, ref tranghientai);
                        TienIch.WriteString(150, 32, $"{tranghientai}/{sotrang}");
                        break;
                    case ConsoleKey.PageUp:
                        fb.ChuyenTrang(bBLL.LayDanhSachBan(), ConsoleKey.PageUp, ref sotrang, ref tranghientai);
                        TienIch.WriteString(150, 32, $"{tranghientai}/{sotrang}");
                        break;

                }
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.ResetColor();
                    Console.Clear();
                    break;
                }
            }
        }
        #endregion

        #region Linh tinh

        public void Trang(List<Ban> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        public void Display_Count_Trang(List<Ban> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Display_Count_Trang(List<Ban> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Trang(List<Ban> list, out int sotrang)
        {
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        #endregion


    }
}
