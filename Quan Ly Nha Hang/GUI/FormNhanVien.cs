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
    public class FormNhanVien: Form
    {
        INhanVienBLL nvBLL = new NhanVienBLL();
        IPhucVuBLL pvBLL = new PhucVuBLL();

        public static bool CheckMaNhanVien(string manhanvien, List<NhanVien> dsnv)
        {
            foreach (NhanVien nv in dsnv)
            {
                if (nv.Manhanvien.Equals(manhanvien)) return true;
            }
            return false;
        }

        #region Thêm nhân viên
        public void ThemNhanVien()
        {
            Console.CursorVisible = true;
            INhanVienBLL nhanvien = new NhanVienBLL();
            Table.TableNhap(1, 8, 49, 5, "THÊM NHÂN VIÊN");
            TienIch.WriteString(2, 9, "Mã nhân viên : ");
            TienIch.WriteString(2, 11, "Họ tên       : ");
            TienIch.WriteString(2, 13, "Ngày sinh    : ");
            TienIch.WriteString(2, 15, "Giới tính    : ");
            TienIch.WriteString(2, 17, "Số điện thoại: ");
            
            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã nhân viên tối đa 10 kí tự.");
            TienIch.WriteString(3, 30, "+ Ngày sinh phải hợp lệ.");
            TienIch.WriteString(3, 31, "+ Số điện thoại phải có 10 chữ số và hợp lệ.");
           
            string manhanvien, hoten, gioitinh, sodienthoai;
            DateTime ngaysinh;

            #region Nhập mã nhân viên
            Regex mnv = new Regex("^([a-zA-Z0-9]{6})$"); 
            do
            {
                if(!TienIch.LimitLengthEnterString(17, 9, 8, 20, 6, out manhanvien))
                {
                    #region Xóa khung nhập nhân viên
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                    #endregion
                    return;
                }
                if (!mnv.IsMatch(manhanvien) || CheckMaNhanVien(manhanvien, nhanvien.LayDanhSachNhanVien()))
                {
                    TienIch.WriteString(15, 35, "Mã nhân viên phải có 6 ký tự và chưa tồn tại.", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 9, manhanvien.Length);
                }
            } while (!mnv.IsMatch(manhanvien) || CheckMaNhanVien(manhanvien, nhanvien.LayDanhSachNhanVien()));
            TienIch.DeleteRow(15, 35, 50);
            #endregion

            #region Nhập tên nhân viên
            Regex tnv = new Regex("^(([A-Za-z][a-z]* )|([A-Za-z][a-z]*)){2,5}$"); // Do Chi Hung
            do
            {
                //TienIch.LimitLengthEnterString(17, 11, 31, out hoten); Strings.ChuanHoa(ref hoten);
                if (!TienIch.LimitLengthEnterString(17, 11, 8, 20, 20, out hoten))
                {
                    #region Xóa khung nhập nhân viên
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                    #endregion
                    return;
                }
                Strings.ChuanHoa(ref hoten);
                if (!tnv.IsMatch(hoten))
                {
                    TienIch.WriteString(15, 35, "Tên nhân viên không thể chứa số.", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 11, hoten.Length);

                }
            } while (!tnv.IsMatch(hoten));
            TienIch.DeleteRow(15, 35, 60);
            #endregion

            #region Nhập ngày sinh
            string ns;
            do
            {
                //TienIch.EnterString(17, 13, out ns);
                if (!TienIch.LimitLengthEnterString(17, 13, 8, 20, 20, out ns))
                {
                    #region Xóa khung nhập nhân viên
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                    #endregion
                    return;
                }
                if (!DateTime.TryParseExact(ns, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaysinh))
                {
                    TienIch.WriteString(15, 35, "Ngày sinh không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 13, ns.Length);
                }
            } while (!DateTime.TryParseExact(ns, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaysinh));
            TienIch.DeleteRow(15, 35, 40);
            #endregion

            #region Chọn giới tính
            Console.CursorVisible = false;
            string[] s = { "Nam ", "Nu  ", "Khac" };
            int[] s1 = { 17, 27, 35 };
            Console.SetCursorPosition(17, 15); Console.Write(s[0]);
            Console.SetCursorPosition(27, 15); Console.Write(s[1]);
            Console.SetCursorPosition(35, 15); Console.Write(s[2]);
            TienIch.DiChuyen(s[0], 17, 15, ConsoleColor.Red, ConsoleColor.Black);
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;
            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r') 
                {
                    if (choice == 1) gioitinh = s[0].Trim();
                    else if (choice == 2) gioitinh = s[1].Trim();
                    else gioitinh = s[2].Trim();
                    break;
                }
                else if (cki.Key == ConsoleKey.RightArrow) 
                {
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Black, ConsoleColor.White);
                    if (choice < 3) choice++;
                    else choice = 1;
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Red, ConsoleColor.Black);
                }
                else if (cki.Key == ConsoleKey.LeftArrow) 
                {
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Black, ConsoleColor.White);
                    if (choice > 1) choice--;
                    else choice = 3;
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Red, ConsoleColor.Black);
                }
            }
            Console.ResetColor();
            Console.CursorVisible = true;
            #endregion

            #region Nhập số điện thoại
            Regex phone = new Regex("^((09|03|07|08)[0-9]{8})$");
            
            do
            {
                //TienIch.LimitLengthEnterString(17, 17, 10, out sodienthoai);
                if (!TienIch.LimitLengthEnterString(17, 17, 8, 20, 10, out sodienthoai))
                {
                    #region Xóa khung nhập nhân viên
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                    #endregion
                    return;
                }
                if (!phone.IsMatch(sodienthoai))
                {
                    TienIch.WriteString(15, 35, "Số điện thoại không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(17, 17, sodienthoai.Length);

                }
            } while (!phone.IsMatch(sodienthoai));
            TienIch.DeleteRow(15, 35, 40);
            #endregion


            // dòng 20
            int location = TienIch.TrueFalse(11, 20, "Bạn chắc chắn muốn lưu không?", "Lưu lại", "Thoát");
            if(location == 1)
            {
                #region Xóa khung nhập nhân viên
                TienIch.DeleteRow(17, 6, 15);
                for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                #endregion

                nhanvien.ThemNhanVien(new NhanVien(manhanvien, hoten, ngaysinh, gioitinh, sodienthoai));
                int tranghientai, sotrang;
                Trang(nhanvien.LayDanhSachNhanVien(), out tranghientai, out sotrang);
                HienThi(nhanvien.LayDanhSachNhanVien(), 1, sotrang);
            }
            else
            {
                #region Xóa khung nhập nhân viên
                TienIch.DeleteRow(17, 6, 15);
                for (int i = 0; i < 11; i++) TienIch.DeleteRow(1, 8 + i, 49);
                for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);
                #endregion
            }    
        }
        #endregion

        #region Hiển thị danh sách nhân viên
        public void HienThi(List<NhanVien> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 11); Print.PrintRow("Mã nhân viên", "Họ tên", "Ngày sinh", "Giới tính", "Số điện thoại");
            if (list == null) return;
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
        public void HienThi(List<NhanVien> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            if (list == null) return;
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(53, 12 + y);
                Print.PrintRow(list[i].Manhanvien, list[i].Hoten, list[i].Ngaysinh.ToString("dd/MM/yyyy"), list[i].Gioitinh, list[i].Sodienthoai);
                y += 2;
            }
        }

        public void DisplayObject(List<NhanVien> list, int i) 
        {
            Print.PrintRow(list[i].Manhanvien, list[i].Hoten, list[i].Ngaysinh.ToString("dd/MM/yyyy"), list[i].Gioitinh, list[i].Sodienthoai);
        }
        #endregion

        #region Thao tác linh tinh
        public void ChuyenTrang(List<NhanVien> list, ConsoleKey cki, ref int sotrang, ref int tranghientai)
        {
            if (cki == ConsoleKey.PageDown)
            {
                if (sotrang > tranghientai)
                {
                    int y = 1;
                    int vitri = list.Count - (tranghientai) * 9 - 1;         
                    // số lượng nhân viên - (trang hiện tại * 9) - 1 => số lượng nhân viên còn lại chưa hiển thị (trong đó hiển thị từ cuối)
                    tranghientai++;
                    if (vitri < 9)
                    {
                        HienThi( list, vitri, -1, ref y);
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
        public string TimKiemNhanVien(string title)
        {
            Console.CursorVisible = true;
            string result = string.Empty;
            int count1 = 0;
            List<NhanVien> dsnv = nvBLL.LayDanhSachNhanVien();
            List<NhanVien> dsnv_temp = new List<NhanVien>();
            ConsoleKeyInfo key;

            TienIch.WriteString(53, 9, $"{title} ");// Tìm kiếm:
            while (true)
            {
                Console.SetCursorPosition(53 + title.Length + 1 + count1, 9);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    int tranghientai, sotrang;
                    Display_Count_Trang(dsnv_temp, out tranghientai, out sotrang);

                    int stt = Chon<NhanVien>(53, 13, dsnv_temp, tranghientai, sotrang, DisplayObject, HienThi);
                    TienIch.DeleteRow(53, 9, 60);
                    if (stt == -1) return string.Empty;
                    return dsnv_temp[stt].Manhanvien;

                }
                dsnv_temp.Clear(); // sau khi nhập 1 kí tự thì danh sách sẽ thay đổi nên mình clear đi danh sách cũ nhé, thằng ngu

                if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32)
                {
                    if (count1 < 30)
                    {
                        Console.SetCursorPosition(53 + title.Length + 1 + count1, 9);
                        Console.WriteLine(key.KeyChar);
                        result += key.KeyChar.ToString();
                        count1++;
                    }
                }
                // string.IsNullOrEmpty(result): kiểm tra chuỗi có null hoặc Empty không nhé
                else if ((int)key.KeyChar == 8 && (string.IsNullOrEmpty(result) == false))  
                {
                    Console.Write("\b \b");
                    result = result.Substring(0, count1 - 1);
                    count1--;
                }
                foreach (NhanVien nv in dsnv)
                {
                    if (nv.Manhanvien.Contains(result) == true || nv.Hoten.ToLower().Contains(result.ToLower()) == true ||  nv.Sodienthoai.Contains(result) == true)
                    {
                        NhanVien abc = new NhanVien(nv);
                        dsnv_temp.Add(abc);

                    }
                }
                int a = 1;
                int vitri = dsnv_temp.Count;
                if (vitri == 0)
                {
                    for (int i = 1; i <= 9 - vitri; i++)
                    { 
                        TienIch.DeleteRow(53, 12 + a, 100);
                        a += 2;
                    }
                }
                else if (vitri < 9)
                {
                    HienThi(dsnv_temp, vitri - 1, -1, ref a);
                    for (int i = 1; i <= 9 - vitri; i++)
                    {
                        TienIch.DeleteRow(53, 12 + a, 100); a += 2;
                    }
                }
                else HienThi(dsnv_temp, vitri - 1, vitri - 9 - 1, ref a);
                int tranghientainew, sotrangnew;
                Display_Count_Trang(dsnv_temp, out tranghientainew, out sotrangnew);

            }
        }
        #endregion

        #region Xóa nhân viên
        public void XoaNhanVien()
        {
            List<NhanVien> dsnv = nvBLL.LayDanhSachNhanVien();
            List<PhucVu> dspv = pvBLL.LayDanhSachPhucVu();

            int tranghientai, sotrang;
            Display_Count_Trang(dsnv, out tranghientai,out sotrang);
            HienThi(dsnv, tranghientai, sotrang);

            string manhanvien = TimKiemNhanVien("Chọn mã or tên nhân viên muốn xóa:");
            if (manhanvien == string.Empty) return;
            int index= nvBLL.Index(dsnv, manhanvien);

            Table.TableNhap(1, 8, 49, 5, "XÓA NHÂN VIÊN");
            TienIch.WriteString(2, 9, $"Mã nhân viên : {dsnv[index].Manhanvien}");
            TienIch.WriteString(2, 11, $"Họ tên       : {dsnv[index].Hoten}");
            TienIch.WriteString(2, 13, $"Ngày sinh    : {dsnv[index].Ngaysinh.ToString("dd/MM/yyyy")}");
            TienIch.WriteString(2, 15, $"Giới tính    : {dsnv[index].Gioitinh}");
            TienIch.WriteString(2, 17, $"Số điện thoại: {dsnv[index].Sodienthoai}");


            int location = TienIch.TrueFalse(7, 20, "Bạn có chắc chắn muốn xóa không?", "Tôi đồng ý", "Thoát");

            if (location == 1)
            {
                nvBLL.XoaNhanVien(manhanvien);
                dsnv.RemoveAt(index);
                Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {dsnv.Count}  ");
            }
            HienThi(dsnv, tranghientai, sotrang);
            TienIch.DeleteRow(17, 6, 15);
            for (int i = 0; i < 15; i++) TienIch.DeleteRow(1, 8 + i, 49);
            
            //Sau khi xóa xong hiển thị lại danh sách ra
        }
        #endregion

        #region Hàm này cần sửa lại chỗ check nhân viên khi ấn enter, hoặc sửa lại mã không thể sửa
        public void ChinhSuaThongTin()
        {
            Console.CursorVisible = false;
            List<NhanVien> list = nvBLL.LayDanhSachNhanVien();

            int tranghientai, sotrang;
            Display_Count_Trang(list,out tranghientai, out sotrang);

            string manhanvien = TimKiemNhanVien("Chọn mã hoặc tên nhân viên muốn sửa:");

            int index = nvBLL.Index(list, manhanvien);

            if (index == -1) return;
            string[] nhanvien = { list[index].Manhanvien, list[index].Hoten, list[index].Ngaysinh.ToString("dd/MM/yyyy"), list[index].Gioitinh, list[index].Sodienthoai };
            #region Bảng nhập
            Table.TableNhap(3, 8, 45, 5, "SỬA NHÂN VIÊN");
            TienIch.WriteString(5, 9, $"Mã nhân viên : {nhanvien[0]}");
            TienIch.WriteString(5, 11, $"Họ tên       : {nhanvien[1]}");
            TienIch.WriteString(5, 13, $"Ngày sinh    : {nhanvien[2]}");
            TienIch.WriteString(5, 15, $"Giới tính    : {nhanvien[3]}");
            TienIch.WriteString(5, 17, $"Số điện thoại: {nhanvien[4]}");
            #endregion

            #region Note
            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã nhân viên tối đa 10 kí tự.");
            TienIch.WriteString(3, 30, "+ Ngày sinh phải hợp lệ.");
            TienIch.WriteString(3, 31, "+ Số điện thoại phải có 10 chữ số và hợp lệ.");
            #endregion
            Console.CursorVisible = true;
            int nhap = 1;
            ConsoleKeyInfo key;
            Console.SetCursorPosition(20 + nhanvien[0].Length, 9);
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Regex mnv = new Regex("^[0-9]{6}$");
                    Regex ht = new Regex("^([A-Z][a-z ]*)*$");
                    Regex ns = new Regex("^(0?[1-9]|[12][0-9]|3[01])[/-](0?[1-9]|1[012])[/-][0-9]{4}$");
                    Regex gt = new Regex("^(Nam|Nu|Khac)$");
                    Regex sdt = new Regex("^((09|03|07|08)[0-9]{8})$");
                    if (mnv.IsMatch(nhanvien[0]) == true && ht.IsMatch(nhanvien[1]) == true && ns.IsMatch(nhanvien[2]) == true && gt.IsMatch(nhanvien[3]) == true && sdt.IsMatch(nhanvien[4]) == true && nhanvien[0] == list[index].Manhanvien)
                    {
                        int Selection = TienIch.TrueFalse(9, 20, "Bạn có chắc chắn muốn sửa không?", "Chắc chắn", "Sửa Clmm");
                        TienIch.DeleteRow(9, 20, 40);
                        TienIch.DeleteRow(9, 21, 40);
                        if (Selection == 1)
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

                            nvBLL.SuaNhanVien(list[index].Manhanvien, new NhanVien(nhanvien[0], nhanvien[1], DateTime.ParseExact(nhanvien[2], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), nhanvien[3], nhanvien[4]));

                            TienIch.DeleteRow(17, 6, 15);
                            TienIch.DeleteRow(15, 20, 35);
                            for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
                            for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);

                            HienThi(nvBLL.LayDanhSachNhanVien(), 1, sotrang);
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(20 + nhanvien[0].Length, 9);
                            nhap = 1;
                        }
                    }
                    
                }
                if(key.Key == ConsoleKey.Escape)
                {
                    TienIch.DeleteRow(17, 6, 15);
                    TienIch.DeleteRow(15, 20, 35);
                    for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);

                    HienThi(nvBLL.LayDanhSachNhanVien(), 1, sotrang);
                    return;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (nhap < 5)
                    {
                        nhap++;
                    }
                    else
                    {
                        nhap = 1;
                    }
                    Console.SetCursorPosition(20 + nhanvien[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if (key.Key == ConsoleKey.UpArrow) 
                {
                    if (nhap > 1)
                    {
                        nhap--;
                    }
                    else
                    {
                        nhap = 5;
                    }
                    Console.SetCursorPosition(20 + nhanvien[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == 47 || key.KeyChar == 92)
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(20 + nhanvien[nhap - 1].Length, 9 + (nhap - 1) * 2);
                    Console.Write(key.KeyChar);
                    nhanvien[nhap - 1] += key.KeyChar.ToString();
                }
                else if (key.KeyChar == 8 && (string.IsNullOrEmpty(nhanvien[nhap - 1]) == false))
                {
                    Console.CursorVisible = true;
                    Console.Write("\b \b");
                    nhanvien[nhap - 1] = nhanvien[nhap - 1].Substring(0, nhanvien[nhap - 1].Length - 1);
                }
            }

        }
        #endregion

        #region MENU
        public static void Menu(FormNhanVien a)
        {
            int tranghientai, sotrang;
            INhanVienBLL nvBLL = new NhanVienBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 103, 11, "DANH SÁCH NHÂN VIÊN");
            Table.ChucNang("nhân viên");
            List<NhanVien> dsnv = nvBLL.LayDanhSachNhanVien();
            a.Trang(dsnv, out tranghientai, out sotrang);
            a.HienThi(dsnv, tranghientai, sotrang);
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        tranghientai = 1;
                        a.HienThi(nvBLL.LayDanhSachNhanVien(), tranghientai, sotrang);
                        a.ThemNhanVien();
                        a.Display_Count_Trang(nvBLL.LayDanhSachNhanVien(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F3:
                        a.HienThi(nvBLL.LayDanhSachNhanVien(), tranghientai, sotrang);
                        a.XoaNhanVien();
                        a.Get_Trang(nvBLL.LayDanhSachNhanVien(), out sotrang);                    
                        break;
                    case ConsoleKey.F4:
                        tranghientai = 1;
                        a.HienThi(nvBLL.LayDanhSachNhanVien(), tranghientai, sotrang);
                        a.ChinhSuaThongTin();
                        break;
                    case ConsoleKey.F5:
                        tranghientai = 1;
                        a.HienThi(nvBLL.LayDanhSachNhanVien(), tranghientai, sotrang);
                        a.TimKiemNhanVien("Enter employee code or name:");
                        break;
                    case ConsoleKey.PageDown:
                        a.ChuyenTrang(nvBLL.LayDanhSachNhanVien(), ConsoleKey.PageDown, ref sotrang, ref tranghientai);
                        TienIch.WriteString(150, 32, $"{tranghientai}/{sotrang}");
                        break;
                    case ConsoleKey.PageUp:
                        a.ChuyenTrang(nvBLL.LayDanhSachNhanVien(), ConsoleKey.PageUp, ref sotrang, ref tranghientai);
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

        #region Trang hiện tại/ số trang
        public void Trang(List<NhanVien> list,out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        public void Display_Count_Trang(List<NhanVien> list, out int tranghientai, out int sotrang)
        {
            
            tranghientai = 1;
            if (list == null)
            {
                sotrang = 1;
            }
            else
            {
                if (list.Count % 9 == 0) sotrang = list.Count / 9;
                else sotrang = list.Count / 9 + 1;
            }
            
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Display_Count_Trang(List<NhanVien> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Trang(List<NhanVien> list, out int sotrang)
        {
            if (list == null) sotrang = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        #endregion

    }
}

