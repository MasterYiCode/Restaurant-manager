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
    public class FormGiamGia : Form
    {
        IGiamGiaBLL ggBLL = new GiamGiaBLL();

        #region Thêm giảm giá
        public void ThemGiamGia()
        {
            Console.CursorVisible = true;
            Table.TableNhap(1, 8, 49, 5, "GIẢM GIÁ");
            TienIch.WriteString(2, 9, "Mã giảm giá    : ");
            TienIch.WriteString(2, 11, "Ngày bắt đầu   : ");
            TienIch.WriteString(2, 13, "Ngày kết thúc  : ");
            TienIch.WriteString(2, 15, "Đối tượng      : ");
            TienIch.WriteString(2, 17, "Mức độ (%)     : ");

            TienIch.WriteString(3, 28, "NOTE:");
            TienIch.WriteString(3, 29, "+ Mã giảm giá chỉ có số");
            TienIch.WriteString(3, 30, "+ Ngày phải hợp lệ.");
            TienIch.WriteString(3, 31, "+ Mức độ giảm giá nhận giá trị từ 0 -> 100%");

            string magiamgia, doituong;
            DateTime ngaybatdau, ngayketthuc;
            double mucdo;

            #region Random mã giảm giá
            Random rd = new Random();
            do
            {
                magiamgia = rd.Next(100000, 999999).ToString();

            } while (ggBLL.CheckMaGiamGia(magiamgia));
            TienIch.WriteString(19, 9, magiamgia);
            #endregion

            #region Nhập ngày bắt đầu và ngày kết thúc
            string ns;
            do
            {
                if (!TienIch.LimitLengthEnterString(19, 11, 8, 20, 20, out ns))
                {
                    #region Xóa khung nhập giảm giá
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                    #endregion
                    return;
                }
                if (!DateTime.TryParseExact(ns, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaybatdau) && !DateTime.TryParseExact(ns, "dd/MM/yyyy HH:mm:ss tt", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaybatdau))
                {
                    TienIch.WriteString(15, 35, "Ngày  không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(19, 11, ns.Length);
                }
            } while (!DateTime.TryParseExact(ns, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaybatdau) && !DateTime.TryParseExact(ns, "dd/MM/yyyy HH:mm:ss tt", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngaybatdau));
            TienIch.DeleteRow(15, 35, 40);

            do
            {
                if (!TienIch.LimitLengthEnterString(19, 13, 8, 20, 20, out ns))
                {
                    #region Xóa khung nhập giảm giá
                    TienIch.DeleteRow(17, 6, 15);
                    for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                    for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                    #endregion
                    return;
                }
                if (!DateTime.TryParseExact(ns, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngayketthuc) && !DateTime.TryParseExact(ns, "dd/MM/yyyy HH:mm:ss tt", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngayketthuc) )
                {
                    TienIch.WriteString(15, 35, "Ngày không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(19, 13, ns.Length);
                }
            } while ((!DateTime.TryParseExact(ns, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngayketthuc) && !DateTime.TryParseExact(ns, "dd/MM/yyyy HH:mm:ss tt", null, System.Globalization.DateTimeStyles.RoundtripKind, out ngayketthuc)));
            TienIch.DeleteRow(15, 35, 40);
            #endregion

            #region Chọn giới tính
            Console.CursorVisible = false;
            string[] s = { "KHACH VIP ", "KHACH HANG" };
            int[] s1 = { 19, 36};
            Console.SetCursorPosition(19, 15); Console.Write(s[0]);
            Console.SetCursorPosition(36, 15); Console.Write(s[1]);
            TienIch.DiChuyen(s[0], 19, 15, ConsoleColor.Red, ConsoleColor.Black);
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;
            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r')
                {
                    if (choice == 1) doituong = s[0].Trim();
                    else doituong = s[1].Trim();
                    break;
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Black, ConsoleColor.White);
                    if (choice < 2) choice++;
                    else choice = 1;
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Red, ConsoleColor.Black);
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Black, ConsoleColor.White);
                    if (choice > 1) choice--;
                    else choice = 2;
                    TienIch.DiChuyen(s[choice - 1], s1[choice - 1], 15, ConsoleColor.Red, ConsoleColor.Black);
                }
            }
            Console.ResetColor();
            Console.CursorVisible = true;
            #endregion

            #region Nhập % ưu đãi của nhà hàng nè
            string mucdogiamgia;
            do
            {
                TienIch.LimitLengthEnterString(19, 17, 3, out mucdogiamgia);
                if (!Double.TryParse(mucdogiamgia, out mucdo) || mucdo > 100 || mucdo < 0)
                {
                    TienIch.WriteString(15, 35, "Mức độ giảm giá không hợp lệ, bạn nhập lại xem sao?");
                    TienIch.DeleteRow(19, 17, 3);
                }
            } while (!Double.TryParse(mucdogiamgia, out mucdo) || mucdo > 100 || mucdo < 0);
            TienIch.DeleteRow(15, 35, 100);

            #endregion

            // dòng 20
            int location = TienIch.TrueFalse(11, 20, "Bạn chắc chắn muốn lưu không?", "Lưu lại", "Thoát");
            if (location == 1)
            {
                #region Xóa khung nhập giảm giá
                TienIch.DeleteRow(17, 6, 15);
                for (int i = 0; i < 17; i++) TienIch.DeleteRow(1, 8 + i, 49);
                for (int i = 0; i < 4; i++) TienIch.DeleteRow(3, 28 + i, 47);
                #endregion

                ggBLL.ThemGiamGia(new GiamGia(magiamgia, ngaybatdau, ngayketthuc, doituong, mucdo));
                HienThi(ggBLL.LayDanhSachGiamGia());
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

        #region Hiển thị danh sách giảm giá
        public void HienThi(List<GiamGia> list)
        {
            Console.SetCursorPosition(53, 11); Print.PrintRow("Mã giảm giá", "Ngày bắt đầu", "Ngày kết thúc", "Đối tượng", "Mức độ (%)");
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

        }
        public void HienThi(List<GiamGia> list, int vitribatdau, int vitriketthuc, ref int y)
        {
            if (list == null) return;
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(53, 12 + y);
                Print.PrintRow(list[i].MaGiamGia, list[i].DayStart.ToString("dd/MM/yyyy"), list[i].DayEnd.ToString("dd/MM/yyyy"), list[i].DoiTuong, list[i].SaleOff.ToString());
                y += 2;
            }
        }
        #endregion

        #region Xóa giảm giá nhaaaa cưng
        public void XoaGiamGia()
        {
            string magiamgia;
            ConsoleKeyInfo choice;
            TienIch.WriteString(53, 9, $"Mã giảm giá muốn xóa: ");// Tìm kiếm:
            TienIch.EnterString(74, 9, out magiamgia);
            if(ggBLL.CheckMaGiamGia(magiamgia) == true)
            {
                ggBLL.XoaGiamGia(magiamgia);
                HienThi(ggBLL.LayDanhSachGiamGia());
                TienIch.WriteString(15, 35, "Xóa thành công? Enter để tiếp tục?");
                choice = Console.ReadKey(true);
                if(choice.Key == ConsoleKey.Enter)
                {
                    TienIch.DeleteRow(15, 35, 100);
                    TienIch.DeleteRow(53, 9, 70);
                }

            }
            else
            {
                TienIch.WriteString(15, 35, "Mã giảm giá không tồn tại? Enter để tiếp tục?");
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.Enter)
                {
                    TienIch.DeleteRow(15, 35, 100);
                    TienIch.DeleteRow(53, 9, 70);
                }
            }

        }
        #endregion

        #region Sửa giảm giá nha cưnggg
        #endregion


        #region Menu nhaaaaaaaaaaaaaaaaaaa
        public static void Menu(FormGiamGia fgg)
        {
            IGiamGiaBLL ggBLL = new GiamGiaBLL();
            Table.Khung();
            Table.TableHienThiDanhSach(51, 8, 103, 11, "CÁC CHẾ ĐỘ GIẢM GIÁ");
            Table.ChucNang("giảm giá");
            fgg.HienThi(ggBLL.LayDanhSachGiamGia());
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.F2:
                        fgg.ThemGiamGia();
                        break;
                    case ConsoleKey.F3:
                        fgg.XoaGiamGia();
                        break;
                    case ConsoleKey.F4:
                        break;
                    case ConsoleKey.F5:

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

       

    }
}

