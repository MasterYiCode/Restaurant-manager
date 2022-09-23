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
    public class FormLoaiMonAn
    {
        IMonAnBLL maBLL = new MonAnBLL();
        ILoaiMonAnBLL lmaBLL = new LoaiMonAnBLL();

        #region Thêm loại món ăn
        public void ThemLoaiMonAn()
        {
            Console.CursorVisible = true;
            Table.TableNhap(3, 8, 45, 3, "THÊM LOẠI MÓN ĂN");
            TienIch.WriteString(5, 9, "Mã loại món  : ");
            TienIch.WriteString(5, 11, "Tên loại món : ");

            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã loại món ăn bắt buộc phải có 6 kí tự.");

            string maloaimon, tenloaimon;

            Regex mlma = new Regex("^([a-zA-Z0-9]{6})$");
            do
            {
                TienIch.EnterString(20, 9, out maloaimon);
                if (!mlma.IsMatch(maloaimon) || lmaBLL.CheckMaLoaiMonAn(maloaimon))
                {
                    TienIch.WriteString(15, 35, "Mã món ăn bắt buộc có 6 ký tự và chưa tồn tại", ConsoleColor.Red);
                    TienIch.DeleteRow(20, 9, maloaimon.Length);
                }
            } while (!mlma.IsMatch(maloaimon) || lmaBLL.CheckMaLoaiMonAn(maloaimon));
            TienIch.DeleteRow(15, 35, 50);
    
            TienIch.EnterString(20, 11, out tenloaimon); Strings.ChuanHoa(ref tenloaimon);

            #region Xóa khung nhập món ăn nè
            TienIch.DeleteRow(17, 6, 17);
            for (int i = 0; i < 9; i++) TienIch.DeleteRow(3, 8 + i, 45);
            for (int i = 0; i < 2; i++) TienIch.DeleteRow(3, 28 + i, 45);
            #endregion
            lmaBLL.ThemLoaiMonAn(new LoaiMonAn(maloaimon, tenloaimon));
            int tranghientai, sotrang;
            Trang(lmaBLL.LayDanhSachLoaiMonAn(), out tranghientai, out sotrang);
            HienThi(lmaBLL.LayDanhSachLoaiMonAn(), 1, sotrang);
            Console.CursorVisible = false;
        }
        #endregion
        #region Hiển thị danh sách loại món ăn
        public void HienThi(List<LoaiMonAn> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(52, 11); Print.PrintRow("Mã loại món", "Tên loại món");
            int sl = list.Count;
            int a = 1;
            if (sl == 0)
            {
                for (int i = 0; i < 9 - sl; i++)
                {
                    TienIch.DeleteRow(52, 12 + a, 100);
                    a += 2;
                }
            }
            else if (sl < 9)
            {
                HienThi(list, sl - 1, -1, ref a);
                for (int i = 0; i < 9 - sl; i++)
                {
                    TienIch.DeleteRow(52, 12 + a, 100);
                    a += 2;
                }
            }
            else
            {
                HienThi(list, sl - 1, sl - 10, ref a);
            }
            //Display_Count_Trang(list, tranghientai, sotrang);

        }
        public void HienThi(List<LoaiMonAn> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(52, 12 + y);
                Print.PrintRow(list[i].Maloai, list[i].Tenloai);
                y += 2;
            }
        }
        #endregion
        #region Xóa loại món ăn
        public void XoaLoaiMonAn()
        {
            List<LoaiMonAn> dslma =  lmaBLL.LayDanhSachLoaiMonAn();
            Regex maloaihople = new Regex("^([a-zA-Z0-9]{6})$");

            Console.CursorVisible = true;

            TienIch.WriteString(53, 9, "Nhập mã muốn xóa: ");
            string deletemaloai;
            int index;
            do
            {
                TienIch.EnterString(71, 9, out deletemaloai);
                index = lmaBLL.Index(dslma, deletemaloai);
                if (deletemaloai.Length < 6 || !maloaihople.IsMatch(deletemaloai))
                {
                    TienIch.WriteString(15, 35, "Mã loại món bắt buộc phải có 6 chữ số, Enter tiếp tục, ESC thoát.", ConsoleColor.Red);
                    TienIch.DeleteRow(71, 9, deletemaloai.Length);
                }
                else if (!lmaBLL.CheckMaLoaiMonAn(deletemaloai))
                {
                    TienIch.WriteString(15, 35, "Mã loại món không tồn tại, Enter tiếp tục, ESC thoát.", ConsoleColor.Red);
                    TienIch.DeleteRow(71, 9, deletemaloai.Length);
                }else if (lmaBLL.CheckMonAn(dslma[index].Tenloai))
                {
                    TienIch.WriteString(15, 35, "Lưu ý: Món ăn đang tồn tại món có mã loại món ăn này, bạn cần xóa những món ăn có loại món này, Enter tiếp tục, ESC thoát.", ConsoleColor.Red);
                    TienIch.DeleteRow(71, 9, deletemaloai.Length);
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    TienIch.DeleteRow(53, 9, 50);
                    return;
                }
                TienIch.DeleteRow(15, 35, 130);

            } while (!maloaihople.IsMatch(deletemaloai) || deletemaloai.Length < 6 || !lmaBLL.CheckMaLoaiMonAn(deletemaloai) || lmaBLL.CheckMonAn(dslma[index].Tenloai));

            Console.CursorVisible = true;
            Table.TableNhap(3, 8, 45, 3, "XÓA LOẠI MÓN ĂN");
            TienIch.WriteString(5, 9, $"Mã loại món  : {dslma[index].Maloai}");
            TienIch.WriteString(5, 11, $"Tên loại món : {dslma[index].Tenloai}");

            Console.CursorVisible = false;
            string[] luachon = { "CÓ   ", "KHÔNG" };
            int[] s1 = { 13, 31};

            Console.SetCursorPosition(13, 13); Console.Write(luachon[0]);
            Console.SetCursorPosition(31, 13); Console.Write(luachon[1]);

            TienIch.DiChuyen(luachon[0], 13, 13, ConsoleColor.Red, ConsoleColor.Black);
            ConsoleKeyInfo cki;
            int choice = 1;
            while (true)
            {
              
                cki = Console.ReadKey(true);
                if (cki.KeyChar == '\r')
                {
                    Console.ResetColor();
                    if (choice == 1)
                    { 
                        lmaBLL.XoaLoaiMonAn(deletemaloai);
                        HienThi(lmaBLL.LayDanhSachLoaiMonAn(), 1, 1);
                        TienIch.WriteString(15, 35, "Xóa thành công, Enter tiếp tục, ESC thoát.");
                        break;
                    }
                    else
                    {
                        TienIch.WriteString(15, 35, "Xóa thất bại rồi thằng ngu.");
                    }
                    TienIch.DeleteRow(13, 35, 60);
                    break;
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    TienIch.DiChuyen(luachon[choice - 1], s1[choice - 1], 13, ConsoleColor.Black, ConsoleColor.White);
                    if (choice < 2) choice++;
                    else choice = 1;
                    TienIch.DiChuyen(luachon[choice - 1], s1[choice - 1], 13, ConsoleColor.Red, ConsoleColor.Black);
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    TienIch.DiChuyen(luachon[choice - 1], s1[choice - 1], 13, ConsoleColor.Black, ConsoleColor.White);
                    if (choice > 1) choice--;
                    else choice = 2;
                    TienIch.DiChuyen(luachon[choice - 1], s1[choice - 1], 13, ConsoleColor.Red, ConsoleColor.Black);
                }
            }

            #region Xóa khung nhập nhân viên
            TienIch.DeleteRow(53, 9, 50);
            TienIch.DeleteRow(15, 35, 60);
            TienIch.DeleteRow(15, 6, 30);
            for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
            for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);
            #endregion
        }
        #endregion
        #region Sửa loại món ăn
        public void SuaLoaiMonAn()
        {
            List<LoaiMonAn> dslma = lmaBLL.LayDanhSachLoaiMonAn();
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            Regex maloaihople = new Regex("^([a-zA-Z0-9]{6})$");

            Console.CursorVisible = true;

            TienIch.WriteString(53, 9, "Nhập mã muốn sửa: ");
            string deletemaloai;
            int index;
            do
            {
                TienIch.EnterString(71, 9, out deletemaloai);
                index = lmaBLL.Index(dslma, deletemaloai);
                if (deletemaloai.Length < 6 || !maloaihople.IsMatch(deletemaloai))
                {
                    TienIch.WriteString(15, 35, "Mã loại món bắt buộc phải có 6 chữ số, Enter tiếp tục, ESC thoát.", ConsoleColor.Red);
                    TienIch.DeleteRow(71, 9, deletemaloai.Length);
                }
                else if (!lmaBLL.CheckMaLoaiMonAn(deletemaloai))
                {
                    TienIch.WriteString(15, 35, "Mã loại món không tồn tại, Enter tiếp tục, ESC thoát.", ConsoleColor.Red);
                    TienIch.DeleteRow(71, 9, deletemaloai.Length);
                }
                else if (lmaBLL.CheckMonAn(dslma[index].Tenloai))
                {
                    TienIch.WriteString(15, 35, "Lưu ý: Món ăn đang tồn tại những món có loại món này, bạn sửa bạn sẽ sửa tất món có loại món này, Enter tiếp tục, ESC thoát.", ConsoleColor.Red);
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    TienIch.DeleteRow(53, 9, 50);
                    return;
                }
                TienIch.DeleteRow(15, 35, 130);

            } while (!maloaihople.IsMatch(deletemaloai) || deletemaloai.Length < 6 || !lmaBLL.CheckMaLoaiMonAn(deletemaloai) || !lmaBLL.CheckMonAn(dslma[index].Tenloai));

            Console.CursorVisible = true;

            string[] loaimonan = { dslma[index].Maloai, dslma[index].Tenloai };

            Table.TableNhap(3, 8, 45, 3, "SỬA LOẠI MÓN ĂN");
            TienIch.WriteString(5, 9, $"Mã loại món  : {loaimonan[0]}");
            TienIch.WriteString(5, 11, $"Tên loại món : {loaimonan[1]}");

            ConsoleKeyInfo choice;
            int nhap = 1;
            Console.SetCursorPosition(20 + loaimonan[nhap - 1].Length, 9);
            while (true)
            {
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.Enter)
                {
                    Strings.ChuanHoa(ref loaimonan[1]);
                    if (maloaihople.IsMatch(loaimonan[0]))
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

                        lmaBLL.SuaLoaiMonAn(dslma[index].Maloai, new LoaiMonAn(loaimonan[0], loaimonan[1]));
                        for(int i = 0; i < dsma.Count; i++)
                        {
                            if (dsma[i].Loaimon.ToUpper().Equals(dslma[index].Tenloai.ToUpper()))
                            {

                                dsma[i].Loaimon = loaimonan[1];
                                
                            }
                        }
                        maBLL.Update(dsma);
                    

                        TienIch.DeleteRow(17, 6, 20);
                        TienIch.DeleteRow(15, 20, 35);
                        for (int i = 0; i < 11; i++) TienIch.DeleteRow(3, 8 + i, 45);
                        for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 45);

                        HienThi(lmaBLL.LayDanhSachLoaiMonAn(), 1, 1);
                        break;
                        
                    
                    }
                }
                if (choice.Key == ConsoleKey.DownArrow)
                {
                    if (nhap < 2)
                    {
                        nhap++;
                    }
                    else
                    {
                        nhap = 1;
                    }
                    Console.SetCursorPosition(20 + loaimonan[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if (choice.Key == ConsoleKey.UpArrow)
                {
                    if (nhap > 1)
                    {
                        nhap--;
                    }
                    else
                    {
                        nhap = 2;
                    }
                    Console.SetCursorPosition(20 + loaimonan[nhap - 1].Length, 9 + (nhap - 1) * 2);
                }
                else if ((choice.KeyChar >= 48 && choice.KeyChar <= 57) || (choice.KeyChar >= 65 && choice.KeyChar <= 90) || (choice.KeyChar >= 97 && choice.KeyChar <= 122) || choice.KeyChar == 32 || choice.KeyChar == 47 || choice.KeyChar == 92)
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(20 + loaimonan[nhap - 1].Length, 9 + (nhap - 1) * 2);
                    Console.Write(choice.KeyChar);
                    loaimonan[nhap - 1] += choice.KeyChar.ToString();
                }
                else if (choice.KeyChar == 8 && (string.IsNullOrEmpty(loaimonan[nhap - 1]) == false))
                {
                    Console.CursorVisible = true;
                    Console.Write("\b \b");
                    loaimonan[nhap - 1] = loaimonan[nhap - 1].Substring(0, loaimonan[nhap - 1].Length - 1);
                }
            }

        }
        #endregion
        #region Menu Loại món ăn
        public static void Menu(FormLoaiMonAn flma)
        {
            int tranghientai, sotrang;
            ILoaiMonAnBLL lmaBLL = new LoaiMonAnBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 104, 11, "DANH SÁCH LOẠI MÓN ĂN");
            Table.ChucNang("loại món");
            List<LoaiMonAn> dslma = lmaBLL.LayDanhSachLoaiMonAn();
            flma.Trang(dslma, out tranghientai, out sotrang);
            flma.HienThi(dslma, tranghientai, sotrang);
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        tranghientai = 1;
                        flma.HienThi(lmaBLL.LayDanhSachLoaiMonAn(), tranghientai, sotrang);
                        flma.ThemLoaiMonAn();
                        flma.Display_Count_Trang(lmaBLL.LayDanhSachLoaiMonAn(), out tranghientai, out sotrang);
                        break;
                    case ConsoleKey.F3:
                        tranghientai = 1;
                        flma.HienThi(lmaBLL.LayDanhSachLoaiMonAn(), 1,sotrang );
                        flma.XoaLoaiMonAn();
                        flma.Get_Trang(lmaBLL.LayDanhSachLoaiMonAn(), out sotrang);
                        break;
                    case ConsoleKey.F4:
                        tranghientai = 1;
                        flma.HienThi(lmaBLL.LayDanhSachLoaiMonAn(), tranghientai, sotrang);
                        flma.SuaLoaiMonAn();
                        break;
                    //case ConsoleKey.F5:
                    //    tranghientai = 1;
                    //    a.HienThi(lmaBLL.LayDanhSachLoaiMonAn(), tranghientai, sotrang);
                    //    a.TimKiemNhanVien();
                    //    break;
                    case ConsoleKey.PageDown:
                        flma.ChuyenTrang(lmaBLL.LayDanhSachLoaiMonAn(), ConsoleKey.PageDown, ref sotrang, ref tranghientai);
                        TienIch.WriteString(150, 32, $"{tranghientai}/{sotrang}");
                        break;
                    case ConsoleKey.PageUp:
                        flma.ChuyenTrang(lmaBLL.LayDanhSachLoaiMonAn(), ConsoleKey.PageUp, ref sotrang, ref tranghientai);
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
        #region Linh Tinh Tí Thôi
        public void ChuyenTrang(List<LoaiMonAn> list, ConsoleKey cki, ref int sotrang, ref int tranghientai)
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
        public void Trang(List<LoaiMonAn> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        public void Display_Count_Trang(List<LoaiMonAn> list, out int tranghientai, out int sotrang)
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
        public void Display_Count_Trang(List<LoaiMonAn> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(53, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Trang(List<LoaiMonAn> list, out int sotrang)
        {
            if (list == null) sotrang = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
        }
        #endregion
    }
}
