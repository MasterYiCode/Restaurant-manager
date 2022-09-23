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
    public class FormHoaDon
    {
        IHoaDonBLL hdBLL = new HoaDonBLL();

        #region Hiển thị thông tin một hóa đơn
        public void ThongTin_HoaDon(HoaDon hd)
        {
            Console.SetCursorPosition(5, 9); Console.Write($"Mã phục vụ : {hd.Maphucvu}      ");
            Console.SetCursorPosition(5, 11); Console.Write($"Bàn        : {hd.Ban}          ");
            Console.SetCursorPosition(5, 13); Console.Write($"Nhân viên  : {hd.Tennhanvien}       ");
            Console.SetCursorPosition(5, 15); Console.Write($"Khách hàng : {hd.Tenkhachhang}           ");
            Console.SetCursorPosition(5, 17); Console.Write($"Giảm giá   : {hd.Giamgia}                ");
            Console.SetCursorPosition(5, 19); Console.Write($"Tổng tiền  : {TienIch.DisplayMoney(hd.Tongthanhtoan)}           ");
            Console.SetCursorPosition(5, 21); Console.Write($"Ngày       : {hd.Thoigianthanhtoan.ToString("dd/MM/yyyy")}       ");
            Console.SetCursorPosition(5, 23); Console.Write($"Time       : {hd.Thoigianvao.ToString("HH:mm tt")} - {hd.Thoigianthanhtoan.ToString("HH:mm tt")}      ");
            Console.SetCursorPosition(5, 25);Console.Write($"Trạng thái : {hd.Trangthai}");
        }

        #endregion

        #region Hiển thị danh sách hóa đơn
        public void HienThi(List<HoaDon> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 11); Print.PrintRow("Mã phục vụ", "Clerk", "Customer", "Tổng thanh toán", "Day", "Status");
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
        public void HienThi(List<HoaDon> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(53, 12 + y);
                Print.PrintRow(list[i].Maphucvu, list[i].Tennhanvien,list[i].Tenkhachhang ,TienIch.DisplayMoney(list[i].Tongthanhtoan), list[i].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[i].Trangthai);
                y += 2;
            }
        }
        #endregion

        #region Thống kê doanh thu theo tháng
        public void ThongKeDoanhThuTheoNam()
        {

            TienIch.DeleteRow(0, 27, 51);
            TienIch.WriteString(0, 27, "║", ConsoleColor.Yellow);
            TienIch.WriteString(50, 27, "║", ConsoleColor.Yellow);

            List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
            Dictionary<int, double> doanhthu = new Dictionary<int, double>();
            foreach (HoaDon hd in dshd)
            {
                if (doanhthu.ContainsKey(hd.Thoigianthanhtoan.Month))
                {
                    doanhthu[hd.Thoigianthanhtoan.Month] += hd.Tongthanhtoan;
                }
                else
                {
                    doanhthu.Add(hd.Thoigianthanhtoan.Month, hd.Tongthanhtoan);
                }
            }

            int i = 1;
            Table.TableNhap(3, 8, 45, 12, "THỐNG KÊ DOANH THU THEO THÁNG");
            foreach (KeyValuePair<int, double> doanhthuthang in doanhthu)
            {
                if(doanhthuthang.Key >= 10)
                {
                    TienIch.WriteString(4, 8 + i, $"Tháng {doanhthuthang.Key}             :      {TienIch.DisplayMoney(doanhthuthang.Value)}");
                    i += 2;
                }
                else
                {
                    TienIch.WriteString(4, 8 + i, $"Tháng {doanhthuthang.Key}              :      {TienIch.DisplayMoney(doanhthuthang.Value)}");
                    i += 2;
                }
                
            }

            TienIch.WriteString(15, 35, "Nhấn ESC để thoát nhé thằng ngu", ConsoleColor.Red);

            ConsoleKeyInfo key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Escape)
            {
                TienIch.DeleteRow(7, 6, 33);
                for (int J = 0; J < 26; J++) TienIch.DeleteRow(3, 8 + J, 45);
                TienIch.DeleteRow(15, 35, 100);
            }
            
            

        }
        #endregion

        #region Xem thông tin các hóa đơn

        #region Search Nhân viên
        public int TimKiemHoaDon()
        {
            string result = string.Empty;
            int count1 = 0;
            List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
            List<HoaDon> dshd_temp = new List<HoaDon>();
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
                    if (dshd_temp.Count % 9 == 0) sotrang = dshd_temp.Count / 9;
                    else sotrang = dshd_temp.Count / 9 + 1;
                    Console.SetCursorPosition(53, 32); Console.Write($"Số lượng: {dshd_temp.Count}  ");
                    Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");

                    XemALLThongTin(dshd_temp, tranghientai, sotrang);
                    TienIch.DeleteRow(53, 9, 40);
                }
                dshd_temp.Clear();

                if (((int)key.KeyChar >= 48 && (int)key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == '/')
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

                DateTime result_temp;
                if(DateTime.TryParseExact(result, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out result_temp))
                {
                    dshd_temp = hdBLL.DanhSachHoaDon_Theo(result_temp);
                }
                else
                {
                    dshd_temp = hdBLL.LayDanhSachHoaDon();
                }
                


                
                int a = 1;
                int vitri = dshd_temp.Count;
                if (vitri == 0)
                {
                    for (int i = 1; i <= 9 - vitri; i++) { TienIch.DeleteRow(53, 12 + a, 100); a += 2; }
                }
                else if (vitri < 9)
                {
                    HienThi(dshd_temp, vitri - 1, -1, ref a);
                    for (int i = 1; i <= 9 - vitri; i++)
                    {
                        TienIch.DeleteRow(53, 12 + a, 100); a += 2;
                    }
                }
                else HienThi(dshd_temp, vitri - 1, vitri - 9 - 1, ref a);
                int tranghientainew, sotrangnew;
                Display_Count_Trang(dshd_temp, out tranghientainew, out sotrangnew);

            }
        }
        #endregion

        public void XemALLThongTin(List<HoaDon> list,int tranghientai, int sotrang)// back : red đỏ,fore: white trắng
        {
            int x1 = 53;
            int y1 = 13;
            int stt = list.Count - 9 * (tranghientai - 1) - 1;
            if (list.Count == 0) return;
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;

            Table.TableNhap(3, 8, 45, 9, "THÔNG TIN CHI TIẾT HÓA ĐƠN");

            Console.CursorVisible = false;

            TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
            Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
            Console.ResetColor();
            ThongTin_HoaDon(list[stt]);

            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
               if (cki.Key == ConsoleKey.Escape)
                {
                    TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                    Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                    #region Xóa khung nhập nhân viên
                    TienIch.DeleteRow(7, 6, 32);
                    for (int i = 0; i < 19; i++) TienIch.DeleteRow(3, 8 + i, 45);
                    #endregion
                    return;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (tranghientai == sotrang)
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                        int max = list.Count - (tranghientai - 1) * 9;
                        if (choice < max)
                        {
                            choice++;
                            y1 += 2;
                            stt--;
                        }
                        else
                        {
                            choice = 1;
                            y1 = 13;
                            stt = list.Count - 9 * (tranghientai - 1) - 1;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                        Console.ResetColor();
                        ThongTin_HoaDon(list[stt]);
                    }
                    else
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);

                        if (choice < 9)
                        {
                            choice++;
                            y1 += 2;
                            stt--;
                        }
                        else
                        {
                            choice = 1;
                            y1 = 13;
                            stt = list.Count - 9 * (tranghientai - 1) - 1;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                        Console.ResetColor();
                        ThongTin_HoaDon(list[stt]);
                    }


                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (tranghientai == sotrang)
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                        if (choice > 1)
                        {
                            choice--;
                            y1 -= 2;
                            stt++;
                        }
                        else
                        {
                            int max = list.Count - (tranghientai - 1) * 9;
                            choice = max;
                            y1 = 13 + (max - 1) * 2;
                            stt = 0;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                        Console.ResetColor();
                        ThongTin_HoaDon(list[stt]);
                    }
                    else
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai); ;
                        if (choice > 1)
                        {
                            choice--;
                            y1 -= 2;
                            stt++;
                        }
                        else
                        {
                            choice = 9;
                            y1 = 29;
                            stt = list.Count - 9 * (tranghientai - 1) - 9;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                        Console.ResetColor();
                        ThongTin_HoaDon(list[stt]);
                    }

                }
                else
                {
                    Console.ResetColor();
                    if (cki.Key == ConsoleKey.PageDown)
                    {
                        if (sotrang > tranghientai)
                        {
                            int y = 1;
                            int vitri = list.Count - (tranghientai) * 9 - 1; ;
                            if (vitri < 9)
                            {
                                Console.ResetColor();
                                HienThi(list, vitri, -1, ref y);
                                tranghientai++;
                                for (int i = 0; i < 9 - vitri; i++)
                                {
                                    TienIch.DeleteRow(53, 12 + y, 100);
                                    y += 2;
                                }
                                x1 = 53; y1 = 13; stt = vitri; choice = 1;
                                TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                                Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                                Console.ResetColor(); Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
                                ThongTin_HoaDon(list[stt]);

                            }
                            else
                            {
                                HienThi(list, vitri, vitri - 9, ref y);
                                tranghientai++;
                                x1 = 53; y1 = 13; stt = vitri; choice = 1;
                                TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                                Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                                Console.ResetColor(); Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
                                ThongTin_HoaDon(list[stt]);
                            }

                        }
                    }
                    else if (cki.Key == ConsoleKey.PageUp)
                    {
                        if (tranghientai > 1)
                        {
                            tranghientai--;
                            int y = 1;
                            int vitri = list.Count - (tranghientai - 1) * 9 - 1;
                            HienThi(list, vitri, vitri - 9, ref y);
                            x1 = 53; y1 = 13; stt = vitri; choice = 1;
                            TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                            Print.PrintRow(list[stt].Maphucvu, list[stt].Tennhanvien, list[stt].Tenkhachhang, TienIch.DisplayMoney(list[stt].Tongthanhtoan), list[stt].Thoigianthanhtoan.ToString("dd/MM/yyyy"), list[stt].Trangthai);
                            Console.ResetColor(); Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
                            ThongTin_HoaDon(list[stt]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Xem thông tin hóa đơn theo ngày


        #endregion

        #region Thống kê doanh thu theo ngày + tháng
        public void ThongKeDoanhThuNgay()
        {
            Console.CursorVisible = true;
            List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
            double doanhthu = 0;

            Table.TableNhap(3, 12, 45, 3, "");
            TienIch.WriteString(15, 13, "THỐNG KÊ DOANH THU NGÀY");
            TienIch.WriteString(4, 15, "Ngày/Tháng/Năm: ");

            #region Nhập ngày tháng năm muốn thống kê nhỉ 
            DateTime ngaythongke;
            string ngay;
            do
            {
                TienIch.EnterString(20, 15, out ngay);
                if (!DateTime.TryParseExact(ngay, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaythongke) || ngaythongke > DateTime.Now)
                {
                    TienIch.WriteString(15, 35, "Ngày không hợp lệ!", ConsoleColor.Red);
                    TienIch.DeleteRow(20, 15, ngay.Length);
                }
            } while (!DateTime.TryParseExact(ngay, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaythongke) || ngaythongke > DateTime.Now);
            TienIch.DeleteRow(15, 35, 40);
            #endregion

            foreach(HoaDon hd in dshd)
            {
                if(hd.Thoigianthanhtoan.ToString("dd/MM/yyyy").Equals(ngaythongke.ToString("dd/MM/yyyy")))
                {
                    doanhthu += hd.Tongthanhtoan;
                }
            }

            TienIch.WriteString(4, 17, $"Tổng doanh thu: {TienIch.DisplayMoney(doanhthu)}         ");

            Console.CursorVisible = false;

            TienIch.WriteString(15, 35, "Nhấn Enter để thoát chức năng.", ConsoleColor.Red);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Enter)
            {
                for (int i = 0; i < 8; i++) TienIch.DeleteRow(3, 12 + i, 47);
                return;
            }
        }

        public void ThongKeDoanhThuThang()
        {
            Console.CursorVisible = true;
            List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
            double doanhthu = 0;

            Table.TableNhap(3, 12, 45, 3, "");
            TienIch.WriteString(14, 13, "THỐNG KÊ DOANH THU THÁNG");
            TienIch.WriteString(4, 15, "Tháng/Năm     : ");

            #region Nhập ngày tháng năm muốn thống kê nhỉ 
            DateTime ngaythongke;
            string ngay;
            do
            {
                TienIch.EnterString(20, 15, out ngay);
                if (!DateTime.TryParseExact(ngay, "MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaythongke) || ngaythongke > DateTime.Now)
                {
                    TienIch.WriteString(15, 35, "Ngày không hợp lệ!", ConsoleColor.Red);
                    TienIch.DeleteRow(20, 15, ngay.Length);
                }
            } while (!DateTime.TryParseExact(ngay, "MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaythongke) || ngaythongke > DateTime.Now);
            TienIch.DeleteRow(15, 35, 40);
            #endregion

            foreach (HoaDon hd in dshd)
            {
                if (hd.Thoigianthanhtoan.ToString("MM/yyyy").Equals(ngaythongke.ToString("MM/yyyy")))
                {
                    doanhthu += hd.Tongthanhtoan;
                }
            }

            TienIch.WriteString(4, 17, $"Tổng doanh thu: {TienIch.DisplayMoney(doanhthu)}         ");

            Console.CursorVisible = false;

            TienIch.WriteString(15, 35, "Nhấn Enter để thoát chức năng.", ConsoleColor.Red);
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                for (int i = 0; i < 8; i++) TienIch.DeleteRow(3, 12 + i, 47);
                return;
            }
        }
        #endregion

        public void XoaHoaDon()
        {
            Console.CursorVisible = true;
            DateTime dateTime;
            Table.TableNhap(1, 8, 49, 2, "XÓA HÓA ĐƠN");
            TienIch.WriteString(2, 9, "Tháng/Năm or Năm : ");
            string temp;
            do
            {
                if (!TienIch.LimitLengthEnterString(21, 9, 8, 20, 20, out temp))
                {
                    #region Xóa khung nhập
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    #endregion
                    return;
                }
                if (!DateTime.TryParseExact(temp, "MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out dateTime) && !DateTime.TryParseExact(temp, "yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out dateTime) || dateTime.Year > DateTime.Now.Year)
                {
                    TienIch.WriteString(15, 35, "Dữ liệu không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(21,9, temp.Length);
                }
            } while (!DateTime.TryParseExact(temp, "MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out dateTime) && !DateTime.TryParseExact(temp, "yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out dateTime) || dateTime.Year > DateTime.Now.Year);
            TienIch.DeleteRow(15, 35, 40);

            int Selection = TienIch.TrueFalse(11, 12, "Bạn chắc chắn muốn xóa chứ ?", "YES", "NO");
            TienIch.DeleteRow(11, 12, 30);
            TienIch.DeleteRow(11, 13, 30);
            if(Selection == 1)
            {
                List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
                if (dshd == null) return;
                if(temp.Length > 4)
                {
                    for (int i = 0; i < dshd.Count; i++)
                    {
                        if (dshd[i].Thoigianthanhtoan.Month == dateTime.Month && dshd[i].Thoigianthanhtoan.Year == dateTime.Year)
                        {
                            hdBLL.XoaHoaDon(dshd[i].Maphucvu);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dshd.Count; i++)
                    {
                        if (dshd[i].Thoigianthanhtoan.Year == dateTime.Year)
                        {
                            hdBLL.XoaHoaDon(dshd[i].Maphucvu);
                        }
                    }
                }
                TienIch.WriteString(15, 35, "Xóa thành công!", ConsoleColor.Red, 1000);
                #region Xóa khung nhập
                TienIch.DeleteRow(17, 6, 15);
                for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                #endregion


            }


        }

        public static void Menu(FormHoaDon a)
        {
            int tranghientai, sotrang;
            IHoaDonBLL hdBLL = new HoaDonBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 103, 11, "DANH SÁCH HÓA ĐƠN");
            Table.ChucNangY("hóa đơn");
            List<HoaDon> dshd = hdBLL.LayDanhSachHoaDon();
            a.Trang(dshd, out tranghientai, out sotrang);
            a.HienThi(dshd, tranghientai, sotrang);
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        tranghientai = 1;
                        a.HienThi(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        a.XemALLThongTin(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        a.Display_Count_Trang(hdBLL.LayDanhSachHoaDon(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F3:
                        tranghientai = 1;
                        a.HienThi(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        a.ThongKeDoanhThuNgay();
                        a.Display_Count_Trang(hdBLL.LayDanhSachHoaDon(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F4:
                        tranghientai = 1;
                        a.HienThi(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        a.ThongKeDoanhThuThang();
                        a.Display_Count_Trang(hdBLL.LayDanhSachHoaDon(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F5:
                        tranghientai = 1;
                        a.HienThi(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        a.ThongKeDoanhThuTheoNam();
                        a.Display_Count_Trang(hdBLL.LayDanhSachHoaDon(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F6:
                        tranghientai = 1;
                        a.HienThi(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        a.XoaHoaDon();
                        a.Display_Count_Trang(hdBLL.LayDanhSachHoaDon(), out tranghientai, out sotrang);
                        a.HienThi(hdBLL.LayDanhSachHoaDon(), tranghientai, sotrang);
                        break;
                    case ConsoleKey.PageDown:
                        a.ChuyenTrang(hdBLL.LayDanhSachHoaDon(), ConsoleKey.PageDown, ref sotrang, ref tranghientai);
                        TienIch.WriteString(150, 32, $"{tranghientai}/{sotrang}");
                        break;
                    case ConsoleKey.PageUp:
                        a.ChuyenTrang(hdBLL.LayDanhSachHoaDon(), ConsoleKey.PageUp, ref sotrang, ref tranghientai);
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
        
        #region Next trang
        public void ChuyenTrang(List<HoaDon> list, ConsoleKey cki, ref int sotrang, ref int tranghientai)
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
        #region Linh tinh

        public void Trang(List<HoaDon> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if(list == null)
            {
                tranghientai = 1;
                sotrang = 1;
                return;
            }
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        public void Display_Count_Trang(List<HoaDon> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Display_Count_Trang(List<HoaDon> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Trang(List<HoaDon> list, out int sotrang)
        {
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        #endregion
    }
}
