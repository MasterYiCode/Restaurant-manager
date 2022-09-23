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
    public class FormMonAn:Form
    {
        IMonAnBLL maBLL = new MonAnBLL();
        ILoaiMonAnBLL lmaBLL = new LoaiMonAnBLL();

        #region Thêm món ăn
        public void ThemMonAn()
        {
            Console.CursorVisible = true;
            Table.TableNhap(3, 8, 45, 5, "THÊM MÓN ĂN");
            TienIch.WriteString(5, 9, "Mã món ăn    : ");
            TienIch.WriteString(5, 11, "Tên món ăn   : ");
            TienIch.WriteString(5, 13, "Đơn giá      : ");
            TienIch.WriteString(5, 15, "Số lượng     : ");
            TienIch.WriteString(5, 17, "Loại món     : ");


            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã món ăn bắt buộc phải có 6 kí tự.");
            TienIch.WriteString(3, 30, "+ Đơn giá, số lượng phải lớn hơn bằng 0.");
            TienIch.WriteString(3, 31, "+ Loại món ăn bạn nhập phải tồn tại.");

            string mamonan, tenmon, loaimon;
            double dongia = -1;
            int soluong = -1;

            #region Nhập mã món ăn
            Regex mma = new Regex("^([a-zA-Z0-9]{6})$");
            do
            {
                TienIch.EnterString(20, 9, out mamonan);
                if (!mma.IsMatch(mamonan) || maBLL.CheckMaMonAn(mamonan))
                {
                    TienIch.WriteString(15, 35, "Mã món ăn bắt buộc có 6 ký tự và chưa tồn tại", ConsoleColor.Red);
                    TienIch.DeleteRow(20, 9, mamonan.Length);
                }
            } while (!mma.IsMatch(mamonan) || maBLL.CheckMaMonAn(mamonan));
            TienIch.DeleteRow(15, 35, 50);
            #endregion

            #region Nhập tên món ăn
            TienIch.EnterString(20, 11, out tenmon); Strings.ChuanHoa(ref tenmon);
            #endregion

            #region Nhập giá
            do
            {
                try
                {
                    Console.SetCursorPosition(20, 13); dongia = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception loi)
                {
                    TienIch.WriteString(15, 35, loi.Message, ConsoleColor.Red);
                    TienIch.DeleteRow(20, 13, 15);
                }

            } while (dongia < 0);
            TienIch.DeleteRow(15, 35, 50);
            #endregion

            #region Nhập số lượng
            do
            {
                try
                {
                    Console.SetCursorPosition(20, 15); soluong = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception loi)
                {
                    TienIch.WriteString(15, 35, loi.Message, ConsoleColor.Red);
                    TienIch.DeleteRow(20, 15, 15);
                }

            } while (soluong < 0);
            TienIch.DeleteRow(15, 35, 50);
            #endregion

            #region Nhập loại món ăn nè
            string result = string.Empty;
            int count1 = 0;
            List<LoaiMonAn> dslma = lmaBLL.LayDanhSachLoaiMonAn();
            List<LoaiMonAn> dsnv_temp = new List<LoaiMonAn>();
            ConsoleKeyInfo key;
            //=====================================================================
            int x = 2;
            for (int i = dslma.Count - 1; i >= dslma.Count - 4; i--)
            {
                TienIch.WriteString(20, 17 + x, dslma[i].Tenloai); x++;
            }
            //====================================================================
            while (true)
            {
                Console.SetCursorPosition(20 + count1, 17);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    if (maBLL.CheckLoaiMonAn(result))
                    {
                        TienIch.DeleteRow(15, 35, 55);
                        TienIch.DeleteRow(20, 19, 20);
                        TienIch.DeleteRow(20, 20, 20);
                        TienIch.DeleteRow(20, 21, 20);
                        Strings.ChuanHoa(ref result);
                        loaimon = result;
                        break;
                    }
                    else
                    {
                        TienIch.WriteString(15, 35, "Loại món không tồn tại, bạn có thể vào thêm loại món", ConsoleColor.Red);
                    }
                }
                dsnv_temp.Clear();

                if (((int)key.KeyChar >= 48 && (int)key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32)
                {
                    if (count1 < 30)
                    {
                        Console.SetCursorPosition(20 + count1, 17);
                        Console.WriteLine(key.KeyChar);
                        result += key.KeyChar.ToString();
                        count1++;
                    }
                }
                else if (key.KeyChar == 8 && (string.IsNullOrEmpty(result) == false))
                {
                    Console.Write("\b \b");
                    result = result.Substring(0, count1 - 1);
                    count1--;
                }
                foreach (LoaiMonAn lma in dslma)
                {
                    if (lma.Tenloai.ToLower().Contains(result.ToLower()) == true)
                    {
                        LoaiMonAn abc = new LoaiMonAn(lma);
                        dsnv_temp.Add(abc);

                    }
                }
                int a = 2;
                int vitri = dsnv_temp.Count;
                if (vitri == 0)
                {
                    for (int i = 1; i <= 4 - vitri; i++) { TienIch.DeleteRow(20, 17 + a, 20); a++; }
                }
                else if (vitri < 4)
                {
                   
                    for (int i = vitri - 1; i >= 0; i--)
                    {
                        TienIch.WriteString(20, 17 + a, dsnv_temp[i].Tenloai + new string(' ',15)); a++;
                    }
                    for (int i = 1; i <= 4 - vitri; i++)
                    {
                        TienIch.DeleteRow(20, 17 + a, 30); a++;
                    }
                }
                else
                {
                    for (int i = vitri - 1; i >= vitri - 4; i--)
                    {
                        TienIch.WriteString(20, 17 + a, dsnv_temp[i].Tenloai + new string(' ', 15)); a++;
                    }
                }   

            }
            #endregion

            #region Xóa khung nhập món ăn nè
            TienIch.DeleteRow(17, 6, 15);
            for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
            for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);
            #endregion
            maBLL.ThemMonAn(new MonAn(mamonan, tenmon, dongia, soluong, loaimon));
            int tranghientai, sotrang;
            Trang(maBLL.LayDanhSachMonAn(), out tranghientai, out sotrang);
            HienThi(maBLL.LayDanhSachMonAn(), 1, sotrang) ;
            Console.CursorVisible = false;
        }
        #endregion

        #region Hiển thị danh sách món ăn
        public void HienThi(List<MonAn> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 11); Print.PrintRow("Mã món", "Tên món", "Đơn giá", "Số lượng tồn", "Loại món");
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
        public void HienThi(List<MonAn> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(53, 12 + y);
                Print.PrintRow(list[i].Mamon, list[i].Tenmon, TienIch.DisplayMoney(list[i].Dongia), list[i].Soluong.ToString(), list[i].Loaimon);
                y += 2;
            }
        }

        public void DisplayObject(List<MonAn> list, int i)
        {
            Print.PrintRow(list[i].Mamon, list[i].Tenmon, TienIch.DisplayMoney(list[i].Dongia), list[i].Soluong.ToString(), list[i].Loaimon);
        }
        #endregion

        #region Thống kê món ăn theo loại món ăn
        public void ThongKe()
        {
            List<LoaiMonAn> dslma = lmaBLL.LayDanhSachLoaiMonAn();
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            List<MonAn> dsma_temp = new List<MonAn>();

            TienIch.WriteString(11, 6, "THỐNG KÊ MÓN ĂN THEO LOẠI MÓN");
            for(int index = 0; index < dslma.Count; index++)
            {
                TienIch.WriteString(11, 8 + index,$"{index}. "+ dslma[index].Tenloai);
            }

            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey(true);
                if((int)key.KeyChar - 47 <= dslma.Count   && (int)key.KeyChar - 47 > 0)
                {
                    TienIch.WriteString(83, 9, $"THỐNG KÊ MÓN ĂN THEO LOẠI MÓN {dslma[(int)(key.KeyChar) - 47 - 1].Tenloai.ToUpper()}      ");
                    for(int i = 0; i < dsma.Count; i++)
                    {
                        if (dslma[(int)(key.KeyChar) - 47 - 1].Tenloai.Equals(dsma[i].Loaimon))
                        {
                            dsma_temp.Add(dsma[i]);
                        }
                    }
                    int tranghientai, sotrang;
                    Trang(dsma_temp, out tranghientai, out sotrang);
                    HienThi(dsma_temp, 1, sotrang);

                }

                else if(key.Key == ConsoleKey.Escape)
                {
                    for(int i = 0; i < 20; i++)
                    {
                        TienIch.DeleteRow(4, 6 + i, 40);
                    }
                    break;
                }
                else
                {
                    TienIch.WriteString(15, 35, "Lựa chọn không hợp lệ, chọn lại đi bạn, ESC để thoát, Enter để tiếp tục.", ConsoleColor.Red);
                    ConsoleKeyInfo keey = Console.ReadKey(true);
                    if(keey.Key == ConsoleKey.Escape)
                    {
                        TienIch.DeleteRow(15, 35, 100);
                        break;
                    }
                    TienIch.DeleteRow(15, 35, 100);
                }
                dsma_temp.Clear();
            }
        }
        #endregion

        #region Next trang
        public void ChuyenTrang(List<MonAn> list, ConsoleKey cki, ref int sotrang, ref int tranghientai)
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

        #region Search món ăn
        public string TimKiemMonAn(string title)
        {
            Console.CursorVisible = true;
            string result = string.Empty;
            int count1 = 0;
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            List<MonAn> dsma_temp = new List<MonAn>();
            ConsoleKeyInfo key;

            TienIch.WriteString(53, 9, $"{title} ");// Tìm kiếm:
            while (true)
            {
                Console.SetCursorPosition(53 + title.Length + 1 + count1, 9);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    int tranghientai, sotrang;
                    Display_Count_Trang(dsma_temp, out tranghientai, out sotrang);
                    int stt = Chon<MonAn>(53, 13, dsma_temp, tranghientai, sotrang, DisplayObject, HienThi);
                    TienIch.DeleteRow(53, 9, 60);
                    if (stt == -1) return string.Empty;
                    return dsma_temp[stt].Mamon;

                }
                dsma_temp.Clear(); // sau khi nhập 1 kí tự thì danh sách sẽ thay đổi nên mình clear đi danh sách cũ nhé, thằng ngu

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
                foreach (MonAn ma in dsma)
                {
                    if (ma.Mamon.Contains(result) == true || ma.Tenmon.ToLower().Contains(result.ToLower()) == true)
                    {
                        dsma_temp.Add(new MonAn(ma));

                    }
                }
                int a = 1;
                int vitri = dsma_temp.Count;
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
                    HienThi(dsma_temp, vitri - 1, -1, ref a);
                    for (int i = 1; i <= 9 - vitri; i++)
                    {
                        TienIch.DeleteRow(53, 12 + a, 100); a += 2;
                    }
                }
                else HienThi(dsma_temp, vitri - 1, vitri - 9 - 1, ref a);
                int tranghientainew, sotrangnew;
                Display_Count_Trang(dsma_temp, out tranghientainew, out sotrangnew);

            }
        }
        #endregion

        public void XoaMonAn()
        {
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            int tranghientai, sotrang;

            Display_Count_Trang(dsma, out tranghientai, out sotrang);
            HienThi(dsma, tranghientai, sotrang);

            string mamon = TimKiemMonAn("Tìm kiếm món ăn muốn xóa: ");
            int index = maBLL.Index(dsma, mamon);


            if (index == -1) return;
            maBLL.XoaMonAn(dsma[index].Mamon.Trim());
            Table.TableNhap(1, 8, 49, 5, "XÓA MÓN ĂN");
            TienIch.WriteString(2, 9, $"Mã nhân viên : {dsma[index].Mamon}");
            TienIch.WriteString(2, 11, $"Họ tên       : {dsma[index].Tenmon}");
            TienIch.WriteString(2, 13, $"Ngày sinh    : {dsma[index].Dongia}");
            TienIch.WriteString(2, 15, $"Giới tính    : {dsma[index].Soluong}");
            TienIch.WriteString(2, 17, $"Số điện thoại: {dsma[index].Loaimon}");


            int location = TienIch.TrueFalse(7, 20, "Bạn có chắc chắn muốn xóa không?", "Tôi đồng ý", "Thoát");

            if (location == 1)
            {
                maBLL.XoaMonAn(mamon);
                dsma.RemoveAt(index);
                Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {dsma.Count}  ");
            }
            HienThi(dsma, tranghientai, sotrang);
            TienIch.DeleteRow(17, 6, 15);
            for (int i = 0; i < 15; i++) TienIch.DeleteRow(1, 8 + i, 49);

        }

        #region Chú ý fix lỗi loại món không được sửa hoặc nếu sửa thì phải chỉnh sang loại món đang tồn tại
        public void ChinhSuaMonAn()
        {
            Console.CursorVisible = false;
            List<MonAn> list = maBLL.LayDanhSachMonAn();

            int tranghientai, sotrang;
            Display_Count_Trang(list, out tranghientai, out sotrang);

            string mamon = TimKiemMonAn("Tìm kiếm món ăn muốn sửa: ");
            int index = maBLL.Index(list, mamon);
            if (index == -1) return;

            string[] monan = { list[index].Mamon, list[index].Tenmon, list[index].Dongia.ToString(), list[index].Soluong.ToString(), list[index].Loaimon };
            #region Bảng nhập
            Table.TableNhap(3, 8, 45, 5, "SỬA MÓN ĂN");
            TienIch.WriteString(5, 9, $"Mã món       : {monan[0]}");
            TienIch.WriteString(5, 11, $"Tên món      : {monan[1]}");
            TienIch.WriteString(5, 13, $"Đơn giá      : {monan[2]}");
            TienIch.WriteString(5, 15, $"Số lượng tồn : {monan[3]}");
            TienIch.WriteString(5, 17, $"Loại món     : {monan[4]}");
            #endregion
            #region Note
            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã món ăn bắt buộc có 6 ký tự và duy nhất.");
            TienIch.WriteString(3, 30, "+ Đơn giá, số lượng tồn phải lớn hơn bằng 0.");
            TienIch.WriteString(3, 31, "+ Loại món phải là loại đã tồn tại.");
            #endregion

            Console.CursorVisible = true;
            int nhap = 1;
            ConsoleKeyInfo key;
            Console.SetCursorPosition(20 + monan[0].Length, 9);
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Regex mma = new Regex("^[0-9]{6}$"); 
                    Regex tenmon = new Regex("^(([A-Za-z][a-z ]*)|([A-Za-z][a-z]*)|([0-9]*[ ])|([0-9]))*$");
                    
                    if (mma.IsMatch(monan[0]) && tenmon.IsMatch(monan[1]) && Convert.ToDouble(monan[2]) >= 0 && Convert.ToInt32(monan[3]) >= 0 )
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

                        maBLL.SuaMonAn(list[index].Mamon, new MonAn(monan[0], monan[1], Convert.ToDouble(monan[2]), Convert.ToInt32(monan[3]), monan[4]));

                        TienIch.DeleteRow(17, 6, 15);
                        TienIch.DeleteRow(15, 20, 35);
                        for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
                        for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);


                        int vitri = list.Count - (tranghientai - 1) * 9 - index - 1;
                        Console.SetCursorPosition(53, 13 + (vitri) * 2);
                        Print.PrintRow(monan[0], monan[1], TienIch.DisplayMoney(Convert.ToDouble(monan[2])), monan[3], monan[4]);
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
                        nhap = 1;
                    }
                    Console.SetCursorPosition(20 + monan[nhap - 1].Length, 9 + (nhap - 1) * 2);
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
                    Console.SetCursorPosition(20 + monan[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if ((key.KeyChar >= 48 && (int)key.KeyChar <= 57) || (key.KeyChar >= 65 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == 47 || key.KeyChar == 92)
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(20 + monan[nhap - 1].Length, 9 + (nhap - 1) * 2);
                    Console.Write(key.KeyChar);
                    monan[nhap - 1] += key.KeyChar.ToString();
                }
                else if ((int)key.KeyChar == 8 && (string.IsNullOrEmpty(monan[nhap - 1]) == false))
                {
                    Console.CursorVisible = true;
                    Console.Write("\b \b");
                    monan[nhap - 1] = monan[nhap - 1].Substring(0, monan[nhap - 1].Length - 1);
                }
            }

        }
        #endregion

        

        public void CapNhapMonAn() 
        {
            //Input thêm món;
            //Out: Cập nhập số lượng vào tệp Món ăn
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            string mamonan = TimKiemMonAn("Tìm món ăn muốn cập nhập: ");
            if (mamonan == string.Empty) return;
            int index = maBLL.Index(dsma, mamonan);
            

            Console.CursorVisible = true;
            Table.TableNhap(3, 8, 45, 5, "THÊM SỐ LƯỢNG");
            TienIch.WriteString(5, 9, $"Mã món ăn    : {dsma[index].Mamon}");
            TienIch.WriteString(5, 11, $"Tên món ăn   : {dsma[index].Tenmon}");
            TienIch.WriteString(5, 13, $"Đơn giá      : {dsma[index].Dongia}");
            TienIch.WriteString(5, 15, $"Số lượng thêm: ");
            TienIch.WriteString(5, 17, $"Loại món     : {dsma[index].Loaimon}");


            #region Nhập số lượng
            int soluong = 0;
            do
            {
                try
                {
                    Console.SetCursorPosition(20, 15); soluong = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception loi)
                {
                    if(loi is FormatException)
                    {
                        TienIch.WriteString(15, 35, loi.Message, ConsoleColor.Red);
                        TienIch.DeleteRow(20, 15, 15);
                    }else if(soluong < 0)
                    {
                        TienIch.WriteString(15, 35, "Số lượng phải lớn hơn 0.", ConsoleColor.Red);
                        TienIch.DeleteRow(20, 15, 15);
                    }
                }

            } while (soluong < 0);
            TienIch.DeleteRow(15, 35, 50);
            #endregion

            int selection = TienIch.TrueFalse(7, 20, "Bạn có chắc chắn muốn thêm không?", "Thêm❤", "Thoát");

            dsma[index].Soluong += soluong;
            Console.WriteLine(dsma[index].Soluong);
            maBLL.Update(dsma);

        }

        #region Menu chính
        public static void Menu(FormMonAn fma)
        {
            int tranghientai, sotrang;
            IMonAnBLL maBLL = new MonAnBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 103, 11, "DANH SÁCH MÓN ĂN");
            Table.ChucNangX("món ăn");
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            fma.Trang(dsma, out tranghientai, out sotrang);
            fma.HienThi(dsma, tranghientai, sotrang);
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        tranghientai = 1;
                        fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                        fma.ThemMonAn();
                        fma.Display_Count_Trang(maBLL.LayDanhSachMonAn(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F3:
                        fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                        fma.XoaMonAn();
                        fma.Get_Trang(maBLL.LayDanhSachMonAn(), out sotrang);
                        break;
                    case ConsoleKey.F4:
                        tranghientai = 1;
                        fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                        fma.ChinhSuaMonAn();
                        break;
                    case ConsoleKey.F5:
                        tranghientai = 1;
                        fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                        fma.TimKiemMonAn("Search: ");
                        break;
                    case ConsoleKey.F6:
                        fma.ThongKe();
                        fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                        break;
                    case ConsoleKey.F7:
                        fma.CapNhapMonAn();
                        fma.HienThi(maBLL.LayDanhSachMonAn(), tranghientai, sotrang);
                        break;
                    case ConsoleKey.PageDown:
                        fma.ChuyenTrang(maBLL.LayDanhSachMonAn(), ConsoleKey.PageDown, ref sotrang, ref tranghientai);
                        TienIch.WriteString(150, 32, $"{tranghientai}/{sotrang}");
                        break;
                    case ConsoleKey.PageUp:
                        fma.ChuyenTrang(maBLL.LayDanhSachMonAn(), ConsoleKey.PageUp, ref sotrang, ref tranghientai);
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

        public void Trang(List<MonAn> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        public void Display_Count_Trang(List<MonAn> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Display_Count_Trang(List<MonAn> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Trang(List<MonAn> list, out int sotrang)
        {
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        #endregion
    }
}
