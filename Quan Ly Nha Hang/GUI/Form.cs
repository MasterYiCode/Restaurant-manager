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
    public class Form
    {
        #region Choose Object
        public delegate void Display<T>(List<T> list, int start, int end, ref int y);
        public int Chon<T>(int x, int y,List<T> list, int tranghientai, int sotrang, Action<List<T>, int> displayObject, Display<T> display)
        {
            int vitridaux = x;
            int vitridauy = y;
            int stt = list.Count - 9 * (tranghientai - 1) - 1;
            if (list.Count == 0) return -1;
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;

            Console.CursorVisible = false;

            TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
            displayObject(list, stt);

            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r')
                {
                    Console.CursorVisible = true;
                    TienIch.DiChuyen(x, y, ConsoleColor.Black, ConsoleColor.White);
                    displayObject(list, stt);
                    return stt;
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    TienIch.DiChuyen(x, y, ConsoleColor.Black, ConsoleColor.White);
                    displayObject(list, stt);
                    return -1;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (tranghientai == sotrang)
                    {
                        TienIch.DiChuyen(x, y, ConsoleColor.Black, ConsoleColor.White);
                        displayObject(list, stt);
                        int max = list.Count - (tranghientai - 1) * 9;
                        if (choice < max)
                        {
                            choice++;
                            y += 2;
                            stt--;
                        }
                        else
                        {
                            choice = 1;
                            y = vitridauy;
                            stt = list.Count - 9 * (tranghientai - 1) - 1;
                        }
                        TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                        displayObject(list, stt);
                    }
                    else
                    {
                        TienIch.DiChuyen(x, y, ConsoleColor.Black, ConsoleColor.White);
                        displayObject(list, stt);

                        if (choice < 9)
                        {
                            choice++;
                            y += 2;
                            stt--;
                        }
                        else
                        {
                            choice = 1;
                            y = vitridauy;
                            stt = list.Count - 9 * (tranghientai - 1) - 1;
                        }
                        TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                        displayObject(list, stt);
                    }


                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (tranghientai == sotrang)
                    {
                        TienIch.DiChuyen(x, y, ConsoleColor.Black, ConsoleColor.White);
                        displayObject(list, stt);
                        if (choice > 1)
                        {
                            choice--;
                            y -= 2;
                            stt++;
                        }
                        else
                        {
                            int max = list.Count - (tranghientai - 1) * 9;
                            choice = max;
                            y = vitridauy + (max - 1) * 2;
                            stt = 0;
                        }
                        TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                        displayObject(list, stt);
                    }
                    else
                    {
                        TienIch.DiChuyen(x, y, ConsoleColor.Black, ConsoleColor.White);
                        displayObject(list, stt);
                        if (choice > 1)
                        {
                            choice--;
                            y -= 2;
                            stt++;
                        }
                        else
                        {
                            choice = 9;
                            y = vitridauy + 16;
                            stt = list.Count - 9 * (tranghientai - 1) - 9;
                        }
                        TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                        displayObject(list, stt);
                    }

                }
                else
                {
                    Console.ResetColor();
                    if (cki.Key == ConsoleKey.PageDown)
                    {
                        if (sotrang > tranghientai)
                        {
                            int buocnhay = 1;
                            int vitri = list.Count - (tranghientai) * 9 - 1; ;
                            if (vitri < 9)
                            {
                                Console.ResetColor();
                                display?.Invoke(list, vitri, -1, ref buocnhay);
                                tranghientai++;
                                for (int i = 0; i < 9 - vitri; i++)
                                {
                                    TienIch.DeleteRow(vitridaux, vitridauy - 1 + buocnhay, 100);
                                    buocnhay += 2;
                                }
                                x = vitridaux; 
                                y = vitridauy; 
                                stt = vitri; 
                                choice = 1;
                                TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                                displayObject(list, stt);
                                Console.ResetColor(); Console.SetCursorPosition(145, 32); Console.Write($"     {tranghientai}/{sotrang}");

                            }
                            else
                            {
                                display?.Invoke(list, vitri, vitri - 9, ref buocnhay);
                                tranghientai++;
                                x = vitridaux; y = vitridauy; stt = vitri; choice = 1;
                                TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                                displayObject(list, stt);
                                Console.ResetColor(); Console.SetCursorPosition(145, 32); Console.Write($"     {tranghientai}/{sotrang} ");
                            }

                        }
                    }
                    else if (cki.Key == ConsoleKey.PageUp)
                    {
                        if (tranghientai > 1)
                        {
                            tranghientai--;
                            int buocnhay = 1;
                            int vitri = list.Count - (tranghientai - 1) * 9 - 1;
                            display?.Invoke(list, vitri, vitri - 9, ref buocnhay);
                            x = vitridaux; y = vitridauy; stt = vitri; choice = 1;
                            TienIch.DiChuyen(x, y, ConsoleColor.White, ConsoleColor.Black);
                            displayObject(list, stt);
                            Console.ResetColor(); Console.SetCursorPosition(145, 32); Console.Write($"     {tranghientai}/{sotrang}");
                        }
                    }
                }
            }
        }
        #endregion

        #region Hàm lấy ra trang hiện tại +  số trang + hiển thị số trang
        public void Display_Trang_SoTrang<T>(int rowsl, int colsl, int rowtrang, int coltrang ,List<T> list, out int tranghientai, out int sotrang)
        {
            tranghientai = 1;
            if (list.Count % 9 == 0) sotrang = list.Count / 9;
            else sotrang = list.Count / 9 + 1;
            Console.SetCursorPosition(rowsl, colsl); Console.Write($"Số lượng : {list.Count}  ");
            Console.SetCursorPosition(rowtrang, coltrang); Console.Write($"{tranghientai}/{sotrang}");
        }
        public void Get_Count<T>(int rowsl, int colsl,List<T> list)
        {
            TienIch.WriteString(rowsl, colsl, $"Số lượng: {list.Count}   ");
        }
        #endregion
    }
}
