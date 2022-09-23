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

    public class FormPhucVu: Form
    {
        IBanBLL bBLL = new BanBLL();
        IPhucVuBLL pvBLL = new PhucVuBLL();
        INhanVienBLL nvBLL = new NhanVienBLL();
        IKhachHangBLL khBLL = new KhachHangBLL();
        IMonAnBLL maBLL = new MonAnBLL();
        GoiMonBLL gmBLL = new GoiMonBLL();
        ILoaiMonAnBLL lma = new LoaiMonAnBLL();
        IHoaDonBLL hdBLL = new HoaDonBLL();
        IGiamGiaBLL ggBLL = new GiamGiaBLL();

        #region Xác định chế độ giảm giá theo đối tượng
        public double XacDinhCheDoGiamGia(string doituong)
        {
            double mucdogiamgia = 0;
            List<GiamGia> dsgg = ggBLL.LayDanhSachGiamGia();
            if (dsgg == null) return mucdogiamgia;
            doituong = doituong.ToUpper();
            if (doituong.Equals("KHACH HANG"))
            {
                for(int i = 0; i < dsgg.Count; i++)
                {
                    if (!"KHACH HANG".Equals(doituong.ToUpper()) && (DateTime.Now >= dsgg[i].DayStart && DateTime.Now <= dsgg[i].DayEnd))
                    {
                        mucdogiamgia += dsgg[i].SaleOff;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dsgg.Count; i++)
                {
                    if ("KHACH HANG".Equals(doituong.ToUpper()) == false && (DateTime.Now >= dsgg[i].DayStart && DateTime.Now <= dsgg[i].DayEnd))
                    {
                        mucdogiamgia += dsgg[i].SaleOff;
                    }
                }
            }
            return mucdogiamgia/100;
        }
        #endregion

        #region Hiển thị danh sách bàn
        public void HienThi(List<Ban> list, int tranghientai, int sotrang)
        {
            Console.SetCursorPosition(87, 10); Printf.PrintRow("Mã bàn", "Trạng thái");
            int sl = list.Count;
            int next = 2;
            if (sl == 0)
            {
                for (int i = 0; i < 9 - sl; i++)
                {
                    Console.SetCursorPosition(87, 10 + next);
                    TienIch.DeleteRow(87, 10 + next, 67);
                    next += 2;
                }
            }
            else if (sl < 9)
            {
                HienThi(list, sl - 1, -1, ref next);
                for (int i = 0; i < 9 - sl; i++)
                {
                    TienIch.DeleteRow(87, 10 + next, 67);
                    next += 2;
                }
            }
            else
            {
                HienThi(list, sl - 1, sl - 10, ref next);
            }
            Display_Trang_SoTrang<Ban>( 87, 32, 150, 32 ,list, out tranghientai,out sotrang);


        }
        public void HienThi(List<Ban> list, int vitribatdau, int vitriketthuc, ref int next)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(87, 10 + next);
                Printf.PrintRow(list[i]);
                next += 2;
            }
        }
        public void DisplayObject(List<Ban> list, int i)
        {
            Printf.PrintRow(list[i]);
        }
        #endregion

        #region Hiển thị danh sách món ăn
        public void HienThi(List<MonAn> list, int tranghientai, int sotrang, int x, int y, int soluong)
        {
            int sl = list.Count;
            int next = 2;
            if (sl == 0)
            {
                for (int i = 0; i < soluong - sl; i++)
                {
                    Console.SetCursorPosition(x,y + next);
                    TienIch.DeleteRow(x, y + next, 65);
                    next += 2;
                }
            }
            else if (sl < 7)
            {
                HienThi(list, sl - 1, -1,x,y, ref next);
                for (int i = 0; i < soluong - sl; i++)
                {
                    TienIch.DeleteRow(x, y + next, 65);
                    next += 2;
                }
            }
            else
            {
                HienThi(list, sl - 1, sl - (soluong + 1),x,y, ref next);
            }
           

        }
        public void HienThi(List<MonAn> list, int vitribatdau, int vitriketthuc,int x, int y, ref int next)
        {
            for (int i = vitribatdau; i > vitriketthuc; i--)
            {
                Console.SetCursorPosition(x, y + next);
                Printf.PrintRow(list[i].Tenmon, list[i].Dongia.ToString(), list[i].Soluong.ToString());
                next += 2;
            }
        }
        #endregion

        #region Chọn bàn để phục vụ hoặc để thanh toán
        public int Chon_Ban(int x1, int y1, List<Ban> list, int tranghientai, int sotrang)
        {
            int stt = list.Count - 9 * (tranghientai - 1) - 1;
            if (list.Count == 0) return -1;
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;

            Console.CursorVisible = false;

            TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
            Printf.PrintRow(list[stt]);

            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r')
                {
                    Console.CursorVisible = true;
                    TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                    Printf.PrintRow(list[stt]);
                    return stt;
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                    Printf.PrintRow(list[stt]);
                    return -1;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (tranghientai == sotrang)
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Printf.PrintRow(list[stt]);
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
                            y1 = 12;
                            stt = list.Count - 9 * (tranghientai - 1) - 1;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Printf.PrintRow(list[stt]);
                    }
                    else
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Printf.PrintRow(list[stt]);

                        if (choice < 9)
                        {
                            choice++;
                            y1 += 2;
                            stt--;
                        }
                        else
                        {
                            choice = 1;
                            y1 = 12;
                            stt = list.Count - 9 * (tranghientai - 1) - 1;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Printf.PrintRow(list[stt]);
                    }


                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (tranghientai == sotrang)
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Printf.PrintRow(list[stt]);
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
                            y1 = 12 + (max - 1) * 2;
                            stt = 0;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Printf.PrintRow(list[stt]);
                    }
                    else
                    {
                        TienIch.DiChuyen(x1, y1, ConsoleColor.Black, ConsoleColor.White);
                        Printf.PrintRow(list[stt]);
                        if (choice > 1)
                        {
                            choice--;
                            y1 -= 2;
                            stt++;
                        }
                        else
                        {
                            choice = 9;
                            y1 = 28;
                            stt = list.Count - 9 * (tranghientai - 1) - 9;
                        }
                        TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                        Printf.PrintRow(list[stt]);
                    }

                }
                else
                {
                    Console.ResetColor();
                    if (cki.Key == ConsoleKey.PageDown)
                    {
                        if (sotrang > tranghientai)
                        {
                            int y = 2;
                            int vitri = list.Count - (tranghientai) * 9 - 1; ;
                            if (vitri < 9)
                            {
                                Console.ResetColor();
                                HienThi(list, vitri, -1, ref y);
                                tranghientai++;
                                for (int i = 0; i < 9 - vitri; i++)
                                {
                                    TienIch.DeleteRow(87, 10 + y, 67);
                                    y += 2;
                                }
                                x1 = 87; y1 = 12; stt = vitri; choice = 1;
                                TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                                Printf.PrintRow(list[stt]);
               
                                Console.ResetColor(); Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");

                            }
                            else
                            {
                                HienThi(list, vitri, vitri - 9, ref y);
                                tranghientai++;
                                x1 = 87; y1 = 12; stt = vitri; choice = 1;
                                TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                                Printf.PrintRow(list[stt]);
                                Console.ResetColor(); Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
                            }

                        }
                    }
                    else if (cki.Key == ConsoleKey.PageUp)
                    {
                        if (tranghientai > 1)
                        {
                            tranghientai--;
                            int y = 2;
                            int vitri = list.Count - (tranghientai - 1) * 9 - 1;
                            HienThi(list, vitri, vitri - 9, ref y);
                            x1 = 87; y1 = 12; stt = vitri; choice = 1;
                            TienIch.DiChuyen(x1, y1, ConsoleColor.White, ConsoleColor.Black);
                            Printf.PrintRow(list[stt]);
                            Console.ResetColor(); Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
                        }
                    }
                }
            }
        }
        #endregion

        #region Khách đến xếp bàn cho khách

        public void SapXepBan()
        {
            List<NhanVien> dsnv = nvBLL.LayDanhSachNhanVien();
            List<Ban> dsb_ranh = bBLL.DanhSachBanRanh();
            TienIch.WriteString(3, 37, "Enter: Chọn - ESC: Thoát");


            Table.TableHienThiDanhSach1(86, 9, 69, 11, "DANH SÁCH BÀN RẢNH");
            int tranghientai, sotrang;
            Display_Trang_SoTrang<Ban>(dsb_ranh, out tranghientai, out sotrang);// hiển thị trang/số trang và lấy giá trị trang hiện tại, số trang
            Get_Count<Ban>(dsb_ranh); // Hiển thị số lượng bàn rảnh
            HienThi(dsb_ranh, tranghientai, sotrang);

            #region Hiển thị giao diện nhập
            Table.TableNhap1(20, 8, 45, 7, "XẾP BÀN CHO KHÁCH");
            TienIch.WriteString(21, 9, "Mã bàn        : ");
            TienIch.WriteString(21, 11, "Mã phục vụ    : ");
            TienIch.WriteString(21, 13, "Mã nhân viên  : ");
            TienIch.WriteString(21, 15, "Tên nhân viên : ");
            TienIch.WriteString(21, 17, "Phone Customer: ");
            TienIch.WriteString(21, 19, "Tên khách hàng: ");
            TienIch.WriteString(21, 21, "Thời gian vào : ");

            TienIch.WriteString(20, 28, "NOTE:");
            TienIch.WriteString(20, 29, "+ Mã phục vụ bắt buộc phải có 6 ký tự và chưa tồn tại.");
            TienIch.WriteString(20, 30, "+ Nhân viên phục vụ phải tồn tại trong danh sách nhân viên.");
            TienIch.WriteString(20, 31, "+ Số điện thoại phải có 10 chữ số và hợp lệ.");
            #endregion

            string maphucvu, maban, manhanvien, tennhanvien, phonecustomer, khachhang = string.Empty;

            int index = Chon_Ban(87, 12, dsb_ranh, tranghientai, sotrang);// chọn bàn cần phục vụ
            if (index == -1)
            {
                TienIch.WriteString(15, 35, "Đặt không thành công, mong quý khách thông cảm cho nhà hàng.", ConsoleColor.Red);
                Console.ReadKey();
                TienIch.DeleteRow(15, 35, 50);
                for (int i = 1; i < 30; i++) TienIch.DeleteRow(20, 6 + i, 60);
                return;
            }
            maban = dsb_ranh[index].Maban;
            TienIch.WriteString(37, 9, $"{maban} - { dsb_ranh[index].Loaiban}");

            dsb_ranh.RemoveAt(index);
            HienThi(dsb_ranh, 1, 1);

            Random rd = new Random();
            do
            {
                maphucvu = rd.Next(10000000, 99999999).ToString();
            } while (pvBLL.CheckMaPhucVu(maphucvu));
            TienIch.WriteString(37, 11, maphucvu);

            string[] goimon = {string.Empty, string.Empty, string.Empty};

            int choice = 1;// sự lựa chọn
            ConsoleKeyInfo Char;
            Console.SetCursorPosition(37 + goimon[0].Length, 13);
            int checkma = 0, checkten = 0;

            while (true)
            {
                Char = Console.ReadKey(true);
                if(Char.Key == ConsoleKey.Enter)
                {
                    Regex phone = new Regex("^((09|03|07|08)[0-9]{8})$");
                    Strings.ChuanHoa(goimon[1]);
                    checkma = nvBLL.CheckMaNhanVien(goimon[0]);
                    checkten = nvBLL.CheckMaNhanVien(Strings.ChuanHoa(goimon[1]));
                    if (checkma == -1)
                    {
                        TienIch.WriteString(15, 35, "Mã nhân viên phải có 6 ký tự và tồn tại.", ConsoleColor.Red, 1000);
                    }else if(checkten == -1)
                    {
                        TienIch.WriteString(15, 35, "Tên nhân viên không tồn tại.", ConsoleColor.Red, 1000);
                    }else if (!phone.IsMatch(goimon[2]))
                    {
                        TienIch.WriteString(15, 35, "Số điện thoại không hợp lệ.", ConsoleColor.Red, 1000);
                    }
                    else
                    {
                        TienIch.WriteString(37, 21, $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt")}");
                        int Selection = TienIch.TrueFalse(26, 24, "Bạn có chắc chắn muốn nhập chưa?", "Chắc chắn", "Từ từ");
                        TienIch.DeleteRow(26, 24, 57);
                        TienIch.DeleteRow(26, 25, 57);
                        if(Selection == 1)
                        {
                            manhanvien = goimon[0];tennhanvien = goimon[1];phonecustomer = goimon[2];
                            pvBLL.ThemPhucVu(new PhucVu(maphucvu, maban, manhanvien, tennhanvien, phonecustomer, khachhang, DateTime.Now));
                            bBLL.SuaTrangThai(maban, "Ban");
                            Console.ResetColor();
                            for (int i = 0; i < 30; i++) TienIch.DeleteRow(20, 6 + i, 60);
                            return;
                        }
                    }
                    TienIch.DeleteRow(21, 24, 60);
                    TienIch.DeleteRow(21, 25, 60);
                    Console.SetCursorPosition(37 + goimon[0].Length, 13);
                    choice = 1;


                }
                else if(Char.Key == ConsoleKey.Escape)
                {
                    int Selection = TienIch.TrueFalse(26, 24, "Bạn có muốn thoát không nhỉ bạn?", "YES", "NO");
                    TienIch.DeleteRow(21, 24, 60);
                    TienIch.DeleteRow(21, 25, 60);
                    if (Selection == 1)
                    {
                        Console.ResetColor();
                        for (int i = 0; i < 30; i++) TienIch.DeleteRow(20, 6 + i, 60);
                        return;
                    }
                    Console.SetCursorPosition(37 + goimon[0].Length, 13);
                    choice = 1;
                }
                else if(Char.Key == ConsoleKey.DownArrow) // di chuyển xuống, mũi tên xuống
                {
                    if(choice < 3){
                        choice++;
                    }
                    else
                    {
                        choice = 1;
                    }
                    Console.SetCursorPosition(37 + goimon[choice - 1].Length, 13 + (choice - 1) * 2);
                }
                else if(Char.Key == ConsoleKey.UpArrow) // mũi tên lên
                {
                    if(choice > 1)
                    {
                        choice--;
                    }
                    else
                    {
                        choice = 3;
                    }
                    Console.SetCursorPosition(37 + goimon[choice - 1].Length, 13 + (choice - 1) * 2);
                }else if((Char.KeyChar >= 48 && Char.KeyChar <= 57) || (Char.KeyChar >= 65 && Char.KeyChar <= 90) || (Char.KeyChar >= 97 && Char.KeyChar <= 122) || Char.KeyChar == 32 || Char.KeyChar == 47 || Char.KeyChar == 92)
                {
                    if(goimon[0].Length < 26 && goimon[1].Length < 26 && goimon[2].Length < 26)
                    {
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(37 + goimon[choice - 1].Length, 13 + (choice - 1) * 2);
                        Console.Write(Char.KeyChar);
                        goimon[choice - 1] += Char.KeyChar.ToString();

                        checkma = nvBLL.CheckMaNhanVien(goimon[0]);
                        checkten = nvBLL.CheckMaNhanVien(Strings.ChuanHoa(goimon[1]));
                        if (choice == 1)
                        {
                            if (checkma != -1)
                            {
                                goimon[1] = dsnv[checkma].Hoten;
                                TienIch.DeleteRow(37, 15, 26);
                                TienIch.WriteString(37, 15, goimon[1]);
                            }

                        }
                        else if (choice == 2)
                        {
                            
                            if (checkten != -1)
                            {
                                goimon[0] = dsnv[checkten].Manhanvien;
                                TienIch.DeleteRow(37, 13, 26);
                                TienIch.WriteString(37, 13, goimon[0]);
                            }
                        }
                        else
                        {
                            khachhang = khBLL.CheckPhone(goimon[2]);
                            if (khachhang == "")
                            {
                                TienIch.DeleteRow(37, 19, 26);
                                khachhang = "Khach Hang";
                                TienIch.WriteString(37, 19, khachhang);
                            }
                            else
                            {
                                TienIch.DeleteRow(37, 19, 26);
                                TienIch.WriteString(37, 19, khachhang);
                            }
                        }
                        Console.SetCursorPosition(37 + goimon[choice - 1].Length, 13 + (choice - 1) * 2);
                    }
                    
                }
                else if (Char.KeyChar == 8 && string.IsNullOrEmpty(goimon[choice - 1]) == false)
                {
                    Console.CursorVisible = true;
                    Console.Write("\b \b");
                    goimon[choice - 1] = goimon[choice - 1].Substring(0, goimon[choice - 1].Length - 1);

                    checkma = nvBLL.CheckMaNhanVien(goimon[0]);
                    checkten = nvBLL.CheckMaNhanVien(Strings.ChuanHoa(goimon[1]));
                    if (choice == 1)
                    {
                        if (checkma != -1)
                        {
                            goimon[1] = dsnv[checkma].Hoten;
                            TienIch.DeleteRow(37, 15, 26);
                            TienIch.WriteString(37, 15, goimon[1]);
                        }

                    }
                    else if (choice == 2)
                    {
                        if (checkten != -1)
                        {
                            goimon[0] = dsnv[checkten].Manhanvien;
                            TienIch.DeleteRow(37, 13, 26);
                            TienIch.WriteString(37, 13, goimon[0]);

                        }
                    }
                    else
                    {
                        khachhang = khBLL.CheckPhone(goimon[2]);
                        if (khachhang == "")
                        {
                            TienIch.DeleteRow(37, 19, 26);
                            khachhang = "Khách hàng";
                            TienIch.WriteString(37, 19, khachhang);
                        }
                        else
                        {
                            TienIch.DeleteRow(37, 19, 26);
                            TienIch.WriteString(37, 19, khachhang);
                        }
                    }
                    Console.SetCursorPosition(37 + goimon[choice - 1].Length, 13 + (choice - 1) * 2);
                }
            }
          

        }

        public void XepBan()
        {
            
            int tranghientai, sotrang;
            Table.TableHienThiDanhSach1(86, 9, 69, 11, "DANH SÁCH BÀN RẢNH");
            List<NhanVien> dsnv = nvBLL.LayDanhSachNhanVien();
            List<Ban> dsb_ranh = bBLL.DanhSachBanRanh();

            Display_Trang_SoTrang<Ban>(dsb_ranh, out tranghientai, out sotrang);
            Get_Count<Ban>(dsb_ranh);
            HienThi(dsb_ranh, tranghientai, sotrang);

            Table.TableNhap1(20, 10, 45, 7, "XẾP BÀN CHO KHÁCH");
            TienIch.WriteString(21, 11, "Mã phục vụ    : ");
            TienIch.WriteString(21, 13, "Mã bàn        : ");
            TienIch.WriteString(21, 15, "Mã nhân viên  : ");
            TienIch.WriteString(21, 17, "Tên nhân viên : ");
            TienIch.WriteString(21, 19, "Phone Customer: ");
            TienIch.WriteString(21, 21, "Tên khách hàng: ");
            TienIch.WriteString(21, 23, "Thời gian vào : ");

            TienIch.WriteString(20, 28, "NOTE:");
            TienIch.WriteString(20, 29, "+ Mã phục vụ bắt buộc phải có 6 ký tự và chưa tồn tại.");
            TienIch.WriteString(20, 30, "+ Nhân viên phục vụ phải tồn tại trong danh sách nhân viên.");
            TienIch.WriteString(20, 31, "+ Số điện thoại phải có 10 chữ số và hợp lệ.");

            //=================================================================
            //TEST NHẬP
            string maphucvu, maban, manhanvien, tennhanvien,phonecustomer, khachhang;
            DateTime TimeVao;
            #region Nhập mã phục vụ
            Regex mpv = new Regex("^([a-zA-Z0-9]{6})$");
            do
            {
                TienIch.LimitLengthEnterString(37, 11, 6, out maphucvu);
                if (!mpv.IsMatch(maphucvu) || pvBLL.CheckMaPhucVu(maphucvu))
                {
                    TienIch.WriteString(15, 35, "Mã phục vụ phải chưa tồn tại.", ConsoleColor.Red);
                    TienIch.DeleteRow(37, 11, maphucvu.Length);
                }
            } while (!mpv.IsMatch(maphucvu) || pvBLL.CheckMaPhucVu(maphucvu));
            TienIch.DeleteRow(15, 35, 50);
            #endregion

            int index = Chon_Ban(87, 12, dsb_ranh, tranghientai, sotrang);
            if (index == -1)
            {
                TienIch.WriteString(15, 35, "Hết bàn, mong quý khách thông cảm cho nhà hàng.", ConsoleColor.Red);
                Console.ReadKey();
                TienIch.DeleteRow(15, 35, 50);
                for (int i = 1; i < 30; i++) TienIch.DeleteRow(20, 6 + i, 50);
                return;
            }
            maban = dsb_ranh[index].Maban;
            dsb_ranh.RemoveAt(index);
            HienThi(dsb_ranh, 1, 1);
            TienIch.WriteString(37, 13, $"{maban}");

            #region Nhập mã nhân viên phục vụ
            Regex mnv = new Regex("^([a-zA-Z0-9]{6})$");
            int check;
            do
            {
                TienIch.LimitLengthEnterString(37, 15, 26, out manhanvien);
                check = nvBLL.CheckMaNhanVien(manhanvien);
                if (!mnv.IsMatch(maphucvu) || check == -1)
                {
                    TienIch.WriteString(15, 35, "Mã nhân viên phải có 6 ký tự và tồn tại.", ConsoleColor.Red);
                    TienIch.DeleteRow(37, 15, manhanvien.Length);
                }
            } while (!mnv.IsMatch(maphucvu) || check == -1);
            TienIch.DeleteRow(15, 35, 50);
            
            TienIch.DeleteRow(37, 15, manhanvien.Length);
            manhanvien = dsnv[check].Manhanvien;
            tennhanvien = dsnv[check].Hoten;
            TienIch.WriteString(37, 15, manhanvien);
            TienIch.WriteString(37, 17, tennhanvien);
            #endregion

            // Nhập số điện thoại của khách hàng
            #region Nhập số điện thoại
            Regex phone = new Regex("^((09|03|07|08)[0-9]{8})$");
            do
            {
                TienIch.LimitLengthEnterString(37, 19, 26, out phonecustomer);
                if (!phone.IsMatch(phonecustomer))
                {
                    TienIch.WriteString(15, 35, "Số điện thoại không hợp lệ.", ConsoleColor.Red);
                    TienIch.DeleteRow(37, 19, phonecustomer.Length);

                }
            } while (!phone.IsMatch(phonecustomer));
            TienIch.DeleteRow(15, 35, 40);

            khachhang = khBLL.CheckPhone(phonecustomer);
            if(khachhang == "")
            { 
                TienIch.LimitLengthEnterString(37, 21, 26, out khachhang);
            }
            else
            {
                TienIch.WriteString(37, 21, khachhang);
            }

            #endregion
            TimeVao = DateTime.Now;
            TienIch.WriteString(37, 23, $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt")}");
            pvBLL.ThemPhucVu(new PhucVu(maphucvu, maban, manhanvien, tennhanvien,phonecustomer ,khachhang, TimeVao));
            bBLL.SuaTrangThai(maban, "Ban");
            Console.ReadKey();
            Console.ResetColor();
            for (int i = 0; i < 30; i++) TienIch.DeleteRow(20, 6 + i, 60);

        }
        #endregion

        #region Gọi món

        public void MenuMonAn()
        {
            Printt printf = new Printt(28);
            List<MonAn> dsma = maBLL.LayDanhSachMonAn();
            List<LoaiMonAn> dslma = lma.LayDanhSachLoaiMonAn();
            int distance = 3;// khoảng cách
            int column = 10;
            for(int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(distance, column - 2);Printt.PrintRow(dslma[i].Tenloai);
                for(int j = 0; j < dsma.Count; j++)
                {
                    if (dslma[i].Tenloai.ToUpper().Equals(dsma[j].Loaimon.ToUpper()))
                    {
                        Console.SetCursorPosition(distance, column++); Printt.PrintRow(TienIch.DisplayMonAn(dsma[j]));
                    }
                }
                column = 10;
                distance += 27;
            }
        }
        public int GoiMon()
        {
            List<PhucVu> dspv = pvBLL.LayDanhSachPhucVu();
            List<GoiMon> dsgm = gmBLL.LayDanhSachGoiMon();
            List<Ban> dsb_ban = bBLL.DanhSachBanCoKhach();

            int tranghientai, sotrang;
            Display_Trang_SoTrang<Ban>(dsb_ban, out tranghientai, out sotrang);
            HienThi(dsb_ban, tranghientai, sotrang);
            List<MonAn> dsOrder = new List<MonAn>();

            int index = Chon_Ban(87, 12, dsb_ban, 1, 1);
            if (index == -1)
            {
                return -1;
            }

            for (int i = 0; i < 30; i++) TienIch.DeleteRow(87, 6 + i, 67); // Sau khi chọn bàn xong xóa đi bảng bàn, để hiện thông tin order

            // Tìm index của mã phục vụ  có mã bàn đã chọn
            int index1 = -1;
            for(int i = 0; i < dspv.Count; i++)
            {
                if (dspv[i].Maban.Equals(dsb_ban[index].Maban))
                {
                    index1 = i;
                }
            }//=======================================================

            Table.Menu(1, 5, 84, 10, "MENU");
            MenuMonAn();
            string maphucvu, maban, tenmon;
            int soluong = 0;
            double Tongthanhtoan = 0;

            //Chọn bàn xong gọi món cho bàn đó nhỉ?.
            maphucvu = dspv[index1].Maphucvu;
            maban = dspv[index1].Maban;

            Table.TableHienThiDanhSach1(86, 9, 69, 12, "     KHÁCH HÀNG ORDER      ");
            TienIch.WriteString(113, 10, "THÔNG TIN ORDER");
            TienIch.WriteString(90, 12, $"Mã phục vụ: {maphucvu}                                Mã bàn: {maban}  ");

            Table.TableNhap1(20, 18, 45, 4, "ORDER");
            TienIch.WriteString(21, 19, $"Mã phục vụ    : {dspv[index1].Maphucvu}");
            TienIch.WriteString(21, 21, $"Mã bàn        : {dspv[index1].Maban}");
            TienIch.WriteString(21, 23, "Món ăn        : ");
            TienIch.WriteString(21, 25, "Số lượng      : ");

            //==========================================================================================================================================================
            // Nếu bàn đã từng gọi món  => hiển thị
            int checktontai = gmBLL.Index(dsgm, maphucvu);
            if (checktontai != -1)
            {
                dsOrder = dsgm[checktontai].Order;
                Tongthanhtoan = dsgm[checktontai].Tongthanhtoanhientai;
                Console.SetCursorPosition(88, 14); Printf.PrintRow("Tên món", "Giá thành", "Số lượng");
                for (int i = 0; i < dsOrder.Count; i++)
                {
                    Console.SetCursorPosition(88, 14 + (i + 1) * 2); Printf.PrintRow(dsOrder[i].Tenmon, dsOrder[i].Dongia.ToString(), dsOrder[i].Soluong.ToString());
                }

                Console.SetCursorPosition(108, 32); Console.WriteLine($"Tổng thanh toán: {TienIch.DisplayMoney(Tongthanhtoan)}         ");
            }
            // Nhập món ăn khách order + số lượng. Nhâp xong
            double check = 0;
            while (true)
            {
                
                #region Nhập tên món + số lượng
                do
                {
                    bool checkten = TienIch.LimitLengthEnterString(37, 23, 25, 28, 26, out tenmon);
                    tenmon = Strings.ChuanHoa(tenmon);
                    if(checkten == false)
                    {
                        
                        int choice = TienIch.TrueFalse(27, 28, "Bạn có muốn lưu lại thông tin?", "Lưu lại", "Bỏ"); // lưu lại những món đã nhập và thoát khỏi cái đang nhập
                        if(choice == 1)
                        {
                            gmBLL.XoaGoiMon(maphucvu);
                            gmBLL.Insert(new GoiMon(maphucvu, dsOrder, Tongthanhtoan));

                        }
                        for (int i = 0; i < 28; i++)
                        {
                            TienIch.DeleteRow(1, 5 + i, 84);
                        }
                        TienIch.DeleteRow(108, 32, 40);
                        TienIch.DeleteRow(25, 34, 40);
                        TienIch.DeleteRow(25, 35, 40);
                        return 0;
                    }

                    check = maBLL.CheckMonAn(tenmon);
                    if (check == -1)
                    {
                        TienIch.WriteString(15, 35, "Món ăn không tồn tại.", ConsoleColor.Red, 1000);
                        TienIch.DeleteRow(37, 23, tenmon.Length);
                    }
                } while (check == -1);

                do
                {
                    try
                    {
                        string sol;
                        bool checksol = TienIch.LimitLengthEnterString(37, 25, 25, 28, 26, out sol);

                        if (checksol == false)
                        {
                            for (int i = 0; i < 28; i++)
                            {
                                TienIch.DeleteRow(1, 5 + i, 84);
                            }
                            TienIch.DeleteRow(108, 32, 40);
                            TienIch.DeleteRow(25, 34, 40);
                            TienIch.DeleteRow(25, 35, 40);
                            //return;
                        }

                        soluong = Convert.ToInt32(sol);

                    }
                    catch (Exception ex)
                    {
                        TienIch.WriteString(15, 35, ex.Message, ConsoleColor.Red, 1000);
                        TienIch.DeleteRow(37, 25, 27); 
                    }
                    if(soluong <= 0)
                    {
                        TienIch.WriteString(15, 35, "Số lượng phải lớn hơn bằng 0.", ConsoleColor.Red, 1000);
                        TienIch.DeleteRow(37, 25, 27);
                    }
                    else if (maBLL.CheckSoLuong(tenmon, soluong) == false)
                    {
                        TienIch.WriteString(15, 35, "Số lượng không đủ.", ConsoleColor.Red, 1000);
                        TienIch.DeleteRow(37, 25, 27);
                        //TienIch.DeleteRow(37, 17, 8);
                    }
                   
                        
                } while (soluong <= 0 || maBLL.CheckSoLuong(tenmon, soluong) == false);
                #endregion

                //===========================================================================================================
                if (dsOrder.Count == 0)
                {
                    dsOrder.Add(new MonAn(tenmon, check, soluong));
                    Tongthanhtoan += check * soluong;
                    maBLL.UpdateSoLuong(tenmon, soluong, "XUAT");
                }
                else if (maBLL.Index_(dsOrder, tenmon) != -1)
                {

                    dsOrder[maBLL.Index_(dsOrder, tenmon)].Soluong += soluong;
                    Tongthanhtoan += check * soluong;
                    maBLL.UpdateSoLuong(tenmon, soluong, "XUAT");
                }
                else
                {

                    dsOrder.Add(new MonAn(tenmon, check, soluong));
                    Tongthanhtoan += check * soluong;
                    maBLL.UpdateSoLuong(tenmon, soluong, "XUAT");

                }

                Console.SetCursorPosition(88, 14); Printf.PrintRow("Tên món", "Giá thành", "Số lượng");
                for(int i = 0; i < dsOrder.Count; i++)
                {
                    if(i < 8)
                        Console.SetCursorPosition(88, 14 + (i + 1) * 2); Printf.PrintRow(dsOrder[i].Tenmon, dsOrder[i].Dongia.ToString(), dsOrder[i].Soluong.ToString());
                    
                }
                Console.SetCursorPosition(108, 32); Console.WriteLine($"Tổng thanh toán: {TienIch.DisplayMoney(Tongthanhtoan)}       ");

                int location = TienIch.TrueFalse(25, 28, "Bạn có muốn gọi thêm món nào không?", "Có", "Không");
                if (location == 2)
                {
                    break;
                }
                TienIch.DeleteRow(25, 28, 40);
                TienIch.DeleteRow(25, 29, 40);
                TienIch.DeleteRow(37, 23, 20);
                TienIch.DeleteRow(37, 25, 20);
            }

            // Xoá đi mã phục vụ này ở trong tệp, và insert cái này vào cuối tệp
            gmBLL.XoaGoiMon(maphucvu);
            gmBLL.Insert(new GoiMon(maphucvu, dsOrder, Tongthanhtoan));

            for (int i = 0; i < 28; i++)
            {
                TienIch.DeleteRow(1, 5 + i, 84);
            }
            TienIch.DeleteRow(108, 32, 40);
            TienIch.DeleteRow(25, 34, 40);
            TienIch.DeleteRow(25, 35, 40);
            return 0;

        }

        public void GoiNhieu()
        {
            TienIch.WriteString(3, 37, "Enter: Chọn - ESC: Thoát");
            while (true)
            {
                int xxxx = GoiMon();
                if(xxxx == -1)
                {
                    break;
                }
                
            }
            TienIch.DeleteRow(3, 37, 140);
        }
        #endregion

        #region Thanh toán
        public void ThanhToan()
        {
            List<PhucVu> dspv = pvBLL.LayDanhSachPhucVu();
            List<GoiMon> dsgm = gmBLL.LayDanhSachGoiMon();
            List<Ban> dsb_ban = bBLL.DanhSachBanCoKhach();
            List<KhachHang> dskh = khBLL.LayDanhSachKhachHang();

            int tranghientai, sotrang;
            Display_Trang_SoTrang<Ban>(dsb_ban, out tranghientai, out sotrang);
            HienThi(dsb_ban, tranghientai, sotrang);

            int index = Chon_Ban(87, 12, dsb_ban, tranghientai, sotrang);
            if (index == -1) return;

            #region ============================================================
            int index1 = -1; // Lấy ra index của Mã phục vụ trong danh sách phục vụ
            for (int i = 0; i < dspv.Count; i++)
            {
                if (dspv[i].Maban.Equals(dsb_ban[index].Maban))
                {
                    index1 = i;
                }
            }
            int index2 = -1; // Lấy ra index của mã phục vụ xxxxxx trong dsgm
            for (int i = 0; i < dsgm.Count; i++)
            {
                if (dsgm[i].Maphucvu.Equals(dspv[index1].Maphucvu))
                {
                    index2 = i;
                }
            }
            #endregion

            Table.TableNhap1(10, 8, 68, 13, "THANH TOÁN HÓA ĐƠN");
            TienIch.WriteString(14, 9, $"Khách hàng: {dspv[index1].Tenkhachhang}           Phone: {dspv[index1].Phone}");
            TienIch.WriteString(14, 11, $"Mã phục vụ: {dspv[index1].Maphucvu}                    Bàn: {dspv[index1].Maban} ({dsb_ban[index].Loaiban})   ");
            TienIch.WriteString(14, 13, $"Thời gian: {dspv[index].Thoigianvao.ToString("dd/MM/yyyy")} ({dspv[index].Thoigianvao.ToString("HH:mm")} - {DateTime.Now.ToString("HH:mm")} {DateTime.Now.ToString("tt")})   ");

            Console.SetCursorPosition(11, 15);
            Printf.PrintRow("Tên món", "Đơn giá", "Số lượng");

            double giamgia = 0, tongthanhtoan = 0;
            if(dsb_ban[index].Loaiban.ToUpper().Equals("VIP")){tongthanhtoan = 500000;}     // Bàn vip thì  + 500;

            if(index2 != -1)
            {
                HienThi(dsgm[index2].Order, 1, 1, 11, 15, 7);
                giamgia = dsgm[index2].Tongthanhtoanhientai * XacDinhCheDoGiamGia(dspv[index1].Tenkhachhang.ToUpper());
                tongthanhtoan += dsgm[index2].Tongthanhtoanhientai - giamgia;
            }
            else
            {
                giamgia = 0;
                tongthanhtoan = 0;
            }
            
            TienIch.WriteString(14, 31, $"Giảm giá: {TienIch.DisplayMoney(giamgia)}     ");
            TienIch.WriteString(14, 33, $"Tổng thanh toán: {TienIch.DisplayMoney(tongthanhtoan)}    ");


            int choice = TienIch.TrueFalse(10, 34, "                                                                     ", "THANH TOÁN", "THOÁT");
            if (choice == 1)
            {
                hdBLL.ThemHoaDon(new HoaDon(dspv[index1].Maphucvu, dspv[index1].Maban, dspv[index1].Tennhanvien, dspv[index1].Tenkhachhang, giamgia.ToString(), tongthanhtoan, dspv[index1].Thoigianvao, DateTime.Now, "Da thanh toan"));
                bBLL.SuaTrangThai(dsb_ban[index].Maban, "Ranh");
                dspv.RemoveAt(index1); pvBLL.UpdateList(dspv);
                if (index2 != -1)
                {
                    dsgm.RemoveAt(index2); gmBLL.UpdateList(dsgm);
                }
                Console.ResetColor();
                for (int i = 0; i < 30; i++)
                {
                    TienIch.DeleteRow(6, 6 + i, 75);
                }
                TienIch.WriteString(3, 35, "THÔNG BÁO: ");
                return;
            }
            else if (choice == 2)
            {
                Console.ResetColor();
                for (int i = 0; i < 30; i++)
                {
                    TienIch.DeleteRow(6, 6 + i, 75);
                }
                TienIch.WriteString(3, 35, "THÔNG BÁO: ");
                return;
            }
        }
        #endregion
        public void CacChucNangPhucVu()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            string[] menu = {"         XẾP BÀN         ",
                             "         GỌI MÓN         ",
                             "        THANH TOÁN       ",
                             "          THOÁT          "};

            while (true)
            {
                Console.CursorVisible = false;
                HienThi(bBLL.LayDanhSachBan(), 1, 1);
                string s = "          QUẢN LÝ";
                int choice = MenuChinh.LuaChon(s, menu, 29, 12+ 4, ConsoleColor.Black, ConsoleColor.Yellow);
                Console.ResetColor();
                for (int i = 1; i < 30; i++) TienIch.DeleteRow(20, 6 + i, 50);
                switch (choice)
                {
                    case 1:
                        Console.CursorVisible = true;
                        Console.ResetColor();
                        SapXepBan();
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ResetColor();
                        //GoiMon();
                        GoiNhieu();
                        break;
                    case 3:
                        ThanhToan();
                        break;
                }
                if (choice == 4)
                {

                    TienIch.CleanUp();
                    break;
                }
            }
        }
        public static void Menu(FormPhucVu fpv)
        {
            Table.KhungPhucVu();
            Table.TableHienThiDanhSach1(86, 9, 69, 11, "     DANH SÁCH BÀN      ");

            IBanBLL bBLL = new BanBLL();
            List<Ban> dsb = bBLL.LayDanhSachBan();
            List<Ban> dsb_ranh = bBLL.DanhSachBanRanh();
            List<Ban> dsb_ban = bBLL.DanhSachBanCoKhach();
            dsb_ranh = bBLL.DanhSachBanRanh();

            fpv.HienThi(dsb, 1, 1);
            fpv.CacChucNangPhucVu();
            
        }

        //Các hàm hiển thị Trang hiện tại/ số trang

        #region Hàm lấy ra trang hiện tại +  số trang + hiển thị số trang
        public void Display_Trang_SoTrang<T>(List<T> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
            Console.SetCursorPosition(87, 32); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(150, 32); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Count<T>(List<T> list)
        {
            TienIch.WriteString(87, 32, $"Số lượng: {list.Count}   ");
        }
        #endregion

    }
}
