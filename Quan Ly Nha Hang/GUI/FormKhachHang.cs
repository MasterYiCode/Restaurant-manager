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
    public class FormKhachHang:Form
    {
        KhachHangBLL khBLL = new KhachHangBLL();
        public static bool CheckMaKhachHang(string makhachhang, List<KhachHang> dskh)
        {
            foreach (KhachHang kh in dskh)
            {
                if (kh.Makhachhang.Equals(makhachhang)) return true;
            }
            return false;
        }

        #region Thêm khách hàng
        public void ThemKhachHang()
        {
            Console.CursorVisible = true;

            Table.TableNhap(1, 8, 49, 5, "THÊM KHÁCH HÀNG");
            TienIch.WriteString(2, 9, "Mã khách hàng: ");
            TienIch.WriteString(2, 11, "Họ tên       : ");
            TienIch.WriteString(2, 13, "Địa chỉ      : ");
            TienIch.WriteString(2, 15, "Gmail        : ");
            TienIch.WriteString(2, 17, "Số điện thoại: ");


            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã khách hàng phải có 6 chữ số.");
            TienIch.WriteString(3, 30, "+ Gmail phải hợp lệ.");
            TienIch.WriteString(3, 31, "+ Số điện thoại phải có 10 chữ số và hợp lệ.");


            string makhachhang, hoten, diachi, gmail, sodienthoai;


            #region Nhập mã khách hàng
            khBLL.SinhMaKhachHang(out makhachhang);
            TienIch.WriteString(17, 9, makhachhang);
            #endregion

            #region Nhập họ tên khách hàng
            Regex mkh = new Regex("^(([A-Za-z][a-z]* )|([A-Za-z][a-z]*)){2,5}$");
            do
            {
                TienIch.LimitLengthEnterString(17, 11, 31, out hoten); Strings.ChuanHoa(ref hoten);
                if (!mkh.IsMatch(hoten))
                {
                    TienIch.WriteString(15, 35, "Tên khách hàng phải uy tín nhé thằng ngu", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 11, hoten.Length);
                }
            } while (!mkh.IsMatch(hoten));
            TienIch.DeleteRow(15, 35, 50);
            Strings.ChuanHoa(ref hoten);
            #endregion

            #region Nhập địa chỉ nè
            TienIch.LimitLengthEnterString(17, 13, 31, out diachi);
            #endregion

            #region Nhập Gmail khách hàng
            Regex gmeo = new Regex("^[^0-9][a-z0-9.]{3,32}(@gmail.com)$");
            do
            {
                TienIch.LimitLengthEnterString(17, 15, 31, out gmail);
                if (!gmeo.IsMatch(gmail))
                {
                    TienIch.WriteString(15, 35, "Gmail không hợp lệ. Nhập lại đi ngu vl", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 15, gmail.Length);
                }
            } while (!gmeo.IsMatch(gmail));
            TienIch.DeleteRow(15, 35, 60);
            #endregion

            #region Nhập số điện thoại
            Regex phone = new Regex("^((09|03|07|08)[0-9]{8})$");
            do
            {
                TienIch.LimitLengthEnterString(17, 17, 10, out sodienthoai);
                if (!phone.IsMatch(sodienthoai))
                {
                    TienIch.WriteString(15, 35, "Số điện thoại không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 17, sodienthoai.Length);

                }
            } while (!phone.IsMatch(sodienthoai));
            TienIch.DeleteRow(15, 35, 40);
            #endregion

            #region Xóa khung nhập nhân viên
            TienIch.DeleteRow(17, 6, 20);
            for (int i = 0; i < 11; i++) TienIch.DeleteRow(1, 8 + i, 49);
            for (int i = 0; i < 4; i++) TienIch.DeleteRow(1, 28 + i, 49);
            #endregion

            khBLL.ThemKhachHang(new KhachHang(makhachhang, hoten, diachi, gmail, sodienthoai));
            int tranghientai, sotrang;
            Trang(khBLL.LayDanhSachKhachHang(), out tranghientai, out sotrang);
            HienThi(khBLL.LayDanhSachKhachHang(), 1, sotrang);
        }
        #endregion

        #region Hiển thị danh sách khách hàng
        public void HienThi(List<KhachHang> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 11); Print.PrintRow("Mã khách hàng", "Họ tên", "Địa chỉ", "Gmail", "Số điện thoại");
            int sl = list.Count;
            int a = 1;
            if (sl == 0) return;
            if (sl < 9)
            {
                HienThi(list, sl - 1, -1, ref a);
            }
            else
            {
                HienThi(list, sl - 1, sl - 10, ref a);
            }
            Display_Count_Trang(list, tranghientai, sotrang);

        }
        public void HienThi(List<KhachHang> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(53, 12 + y);
                Print.PrintRow(list[i].Makhachhang, list[i].Hoten, list[i].Diachi, list[i].Gmail, list[i].Sodienthoai);
                y += 2;
            }
        }

        public void DisplayObject(List<KhachHang> list, int i)
        {
            Print.PrintRow(list[i].Makhachhang, list[i].Hoten, list[i].Diachi, list[i].Gmail, list[i].Sodienthoai);
        }
        #endregion

        #region Chuyển sang trang
        public void ChuyenTrang(List<KhachHang> list, ConsoleKey cki, ref int sotrang, ref int tranghientai)
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


        #region Search Nhân viên
        public string TimKiemKhachHang()
        {
            Console.CursorVisible = true;
            string result = string.Empty;
            int count1 = 0;
            List<KhachHang> dskh = khBLL.LayDanhSachKhachHang();
            List<KhachHang> dskh_temp = new List<KhachHang>();
            ConsoleKeyInfo key;
            
            TienIch.WriteString(53, 9, "Tìm kiếm: ");

            while (true)
            {
                Console.SetCursorPosition(63 + count1, 9);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    int tranghientai = 1;
                    int sotrang;
                    Display_Count_Trang(dskh_temp, out tranghientai, out sotrang);

                    int stt = Chon<KhachHang>(53, 13, dskh_temp, tranghientai, sotrang, DisplayObject, HienThi);
                    if (stt == -1) return string.Empty;
                    TienIch.DeleteRow(53, 9, 40);
                    return dskh_temp[stt].Makhachhang;
                }
                dskh_temp.Clear();

                if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32)
                {
                    if (count1 < 30)
                    {
                        Console.SetCursorPosition(63 + count1, 9);
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
                foreach (KhachHang kh in dskh)
                {
                    if (kh.Makhachhang.Contains(result) == true || kh.Hoten.ToLower().Contains(result.ToLower()) == true || kh.Sodienthoai.Contains(result))
                    {
                        KhachHang abc = new KhachHang(kh);
                        dskh_temp.Add(abc);

                    }
                }
                int a = 1;
                int vitri = dskh_temp.Count;
                if (vitri == 0)
                {
                    for (int i = 1; i <= 9 - vitri; i++) { TienIch.DeleteRow(53, 12 + a, 100); a += 2; }
                }
                else if (vitri < 9)
                {
                    HienThi(dskh_temp, vitri - 1, -1, ref a);
                    for (int i = 1; i <= 9 - vitri; i++)
                    {
                        TienIch.DeleteRow(53, 12 + a, 100); a += 2;
                    }
                }
                else HienThi(dskh_temp, vitri - 1, vitri - 9 - 1, ref a);
                int tranghientainew, sotrangnew;
                Display_Count_Trang(dskh_temp, out tranghientainew, out sotrangnew);

            }
        }
        #endregion

        #region Xóa nhân viên
        public void XoaNhanVien()
        {
            List<KhachHang> dskh = khBLL.LayDanhSachKhachHang();

            int tranghientai, sotrang;
            Display_Count_Trang(dskh, out tranghientai, out sotrang);
            HienThi(dskh, tranghientai, sotrang);

            string makhachhang = TimKiemKhachHang();

            if (makhachhang == string.Empty) return;
            int stt = khBLL.Index(dskh, makhachhang);
            khBLL.XoaKhachHang(dskh[stt].Makhachhang.Trim());
            dskh.RemoveAt(stt);

            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {dskh.Count}  ");

            int a = 1;
            int vitri = dskh.Count - 9 * (tranghientai - 1) - 1;

            if (dskh.Count % 9 == 0 && stt == 0)
            {
                HienThi(dskh, 8, -1, ref a);
                TienIch.DeleteRow(53, 12 + a, 100);
                TienIch.WriteString(150, 32, $"{tranghientai - 1}/{sotrang - 1} ");
                return;
            }
            else if (vitri < 9)
            {
                HienThi(dskh, vitri, -1, ref a);
                TienIch.DeleteRow(53, 12 + a, 100);
                return;
            }
            else
            {
                HienThi(dskh, vitri, vitri - 9, ref a);
                return;
            }
        }
        #endregion

        #region Chỉnh sửa thông tin nhân viên
        public void ChinhSuaThongTin()
        {
            Console.CursorVisible = false;
            List<KhachHang> list = khBLL.LayDanhSachKhachHang();

            int tranghientai, sotrang;
            Display_Count_Trang(list, out tranghientai, out sotrang);

            string makhachhang = TimKiemKhachHang();

            if (makhachhang == string.Empty) return;
            int index = khBLL.Index(list, makhachhang);
            
            if (index == -1) return;
            string[] khachhang = { list[index].Makhachhang, list[index].Hoten, list[index].Diachi, list[index].Gmail, list[index].Sodienthoai };
            #region Bảng nhập
            Table.TableNhap(3, 8, 45, 5, "SỬA NHÂN VIÊN");
            TienIch.WriteString(5, 9, $"Mã khách hàng: {khachhang[0]}");
            TienIch.WriteString(5, 11, $"Họ tên       : {khachhang[1]}");
            TienIch.WriteString(5, 13, $"Địa chỉ      : {khachhang[2]}");
            TienIch.WriteString(5, 15, $"Gmail        : {khachhang[3]}");
            TienIch.WriteString(5, 17, $"Số điện thoại: {khachhang[4]}");
            #endregion

            #region Note
            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã khách hàng bắt buộc phải có 6 ký tự.");
            TienIch.WriteString(3, 30, "+ Họ tên phải chuẩn.");
            TienIch.WriteString(3, 31, "+ Gmail phải hợp lệ.");
            TienIch.WriteString(3, 32, "+ Số điện thoại phải có 10 chữ số và hợp lệ.");
            #endregion
            Console.CursorVisible = true;
            int nhap = 1;
            ConsoleKeyInfo key;
            Console.SetCursorPosition(20 + khachhang[1].Length, 11);
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Regex hoten = new Regex("^(([A-Za-z][a-z]* )|([A-Za-z][a-z]*)){2,5}$");
                    Regex gmeo = new Regex("^[^0-9][a-z0-9.]{3,32}(@gmail.com)$");
                    Regex sdt = new Regex("^((09|03|07|08)[0-9]{8})$");
                    if (hoten.IsMatch(khachhang[1]) == true &&  gmeo.IsMatch(khachhang[3]) == true && sdt.IsMatch(khachhang [4]) == true)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Console.SetCursorPosition(15, 20); Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"ĐANG CẬP NHẬP {(i + 1) * 10}%");
                            Console.CursorVisible = false;
                            Thread.Sleep(100);
                            Console.CursorVisible = true;
                        }
                        Console.ResetColor();

                        khBLL.SuaKhachHang(list[index].Makhachhang, new KhachHang(khachhang[0], khachhang[1], khachhang[2], khachhang[3], khachhang[4]));

                        TienIch.DeleteRow(17, 6, 15);
                        TienIch.DeleteRow(15, 20, 35);
                        for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
                        for (int i = 0; i < 5; i++) TienIch.DeleteRow(3, 28 + i, 45);


                        int vitri = list.Count - (tranghientai - 1) * 9 - index - 1;
                        Console.SetCursorPosition(53, 13 + (vitri) * 2);
                        Print.PrintRow(khachhang[0], khachhang[1], khachhang[2], khachhang[3], khachhang[4]);
                        break;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (nhap < 5)
                    {
                        nhap++;
                    }
                    else
                    {
                        nhap = 2;
                    }
                    Console.SetCursorPosition(20 + khachhang[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (nhap > 2)
                    {
                        nhap--;
                    }
                    else
                    {
                        nhap = 5;
                    }
                    Console.SetCursorPosition(20 + khachhang[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == 47 || key.KeyChar == 92 || key.KeyChar == '@' || key.KeyChar =='.')
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(20 + khachhang[nhap - 1].Length, 9 + (nhap - 1) * 2);
                    Console.Write(key.KeyChar);
                    khachhang[nhap - 1] += key.KeyChar.ToString();
                }
                else if ((int)key.KeyChar == 8 && (string.IsNullOrEmpty(khachhang[nhap - 1]) == false))
                {
                    Console.CursorVisible = true;
                    Console.Write("\b \b");
                    khachhang[nhap - 1] = khachhang[nhap - 1].Substring(0, khachhang[nhap - 1].Length - 1);
                }
            }

        }
        #endregion

        #region Form khách hàng giao tiếp với người dùng
        public static void Menu(FormKhachHang fkh)
        {
            int tranghientai, sotrang;
            IKhachHangBLL khBLL = new KhachHangBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 103, 11, "DANH SÁCH KHÁCH HÀNG");
            Table.ChucNang("khách hàng");
            List<KhachHang> dskh = khBLL.LayDanhSachKhachHang();
            fkh.Trang(dskh, out tranghientai, out sotrang);
            fkh.HienThi(dskh, tranghientai, sotrang);
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        tranghientai = 1;
                        fkh.HienThi(khBLL.LayDanhSachKhachHang(), tranghientai, sotrang);
                        fkh.ThemKhachHang();
                        fkh.Display_Count_Trang(khBLL.LayDanhSachKhachHang(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F3:
                        fkh.HienThi(khBLL.LayDanhSachKhachHang(), tranghientai, sotrang);
                        fkh.XoaNhanVien();
                        fkh.Get_Trang(khBLL.LayDanhSachKhachHang(), out sotrang);
                        break;
                    case ConsoleKey.F4:
                        tranghientai = 1;
                        fkh.HienThi(khBLL.LayDanhSachKhachHang(), tranghientai, sotrang);
                        fkh.ChinhSuaThongTin();
                        break;
                    case ConsoleKey.F5:
                        tranghientai = 1;
                        fkh.HienThi(khBLL.LayDanhSachKhachHang(), tranghientai, sotrang);
                        fkh.TimKiemKhachHang();
                        break;
                    case ConsoleKey.PageDown:
                        fkh.ChuyenTrang(khBLL.LayDanhSachKhachHang(), ConsoleKey.PageDown, ref sotrang, ref tranghientai);
                        TienIch.WriteString(146, 32, $"  {tranghientai}/{sotrang}  ");
                        break;
                    case ConsoleKey.PageUp:
                        fkh.ChuyenTrang(khBLL.LayDanhSachKhachHang(), ConsoleKey.PageUp, ref sotrang, ref tranghientai);
                        TienIch.WriteString(146, 32, $"  {tranghientai}/{sotrang}  ");
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

        #region Thao tác lấy trang, hiển thị trang
        public void Trang(List<KhachHang> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        public void Display_Count_Trang(List<KhachHang> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(143, 32); Console.Write($"     {tranghientai}/{sotrang}  ");
        }
        public void Display_Count_Trang(List<KhachHang> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(143, 32); Console.Write($"     {tranghientai}/{sotrang}  ");
        }
        public void Get_Trang(List<KhachHang> list, out int sotrang)
        {
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        #endregion

    }
}


