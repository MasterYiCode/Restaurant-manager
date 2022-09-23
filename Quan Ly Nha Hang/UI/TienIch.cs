using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.UI
{
    public static class TienIch
    {
        public static void SetColor(ConsoleColor back, ConsoleColor fore)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }
        public static void DiChuyen(string s, int x, int y, ConsoleColor back, ConsoleColor fore)     //(x,y); vị trí bắt đầu
        {
            SetColor(back, fore);
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }
        public static void DiChuyen(int x, int y, ConsoleColor back, ConsoleColor fore)
        {
            SetColor(back, fore);
            Console.SetCursorPosition(x, y);
        }
        public static void WriteString(int x, int y, string s)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            Console.Write(s);
            Console.CursorVisible = true;
        }
        public static void WriteString(int x, int y, string s, ConsoleColor fore)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = fore;
            Console.SetCursorPosition(x, y);
            Console.Write(s);
            Console.ResetColor();
            Console.CursorVisible = true;
        }

        public static void WriteString(int x, int y, string s, ConsoleColor fore, int tocdo)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = fore;
            for(int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
                Thread.Sleep(200);
                TienIch.DeleteRow(15, 35, 70);
                Thread.Sleep(100);
            }
            Console.ResetColor();
            Console.CursorVisible = true;
        }

        public static void EnterString(int x, int y, out string s)
        {
            Console.SetCursorPosition(x, y);
            s = Console.ReadLine();
        }

        public static void LimitLengthEnterString(int row, int column, int length, out string ResultString)
        {
            Console.SetCursorPosition(row, column);
            Console.CursorVisible = true;
            ResultString = string.Empty;
            int count = 0;

            ConsoleKeyInfo key;

            while (true)
            {
                Console.SetCursorPosition(row + count, column);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 64 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == 46)
                {
                    if (count < length)
                    {
                        Console.SetCursorPosition(row + count, column);
                        Console.WriteLine(key.KeyChar);
                        ResultString += key.KeyChar.ToString();
                        count++;
                    }
                }
                else if (key.KeyChar == 8 && (string.IsNullOrEmpty(ResultString) == false))
                {
                    Console.Write("\b \b"); //                  123467 
                    ResultString = ResultString.Substring(0, count - 1);
                    count--;
                }
            }
        }

        //public static bool LimitLengthEnterString(int row, int column, int row1, int col1 ,int length, out string ResultString)
        //{
        //    Console.SetCursorPosition(row, column);
        //    Console.CursorVisible = true;
        //    ResultString = string.Empty;
        //    int count = 0;
        //    ConsoleKeyInfo key;

        //    while (true)
        //    {
        //        Console.SetCursorPosition(row + count, column);
        //        key = Console.ReadKey(true);
        //        if (key.Key == ConsoleKey.Enter)
        //        {
        //            return true;
        //        }
        //        else if(key.Key == ConsoleKey.Escape)
        //        {
        //            int Selection = TrueFalse(row1, col1, "Bạn có muốn tiếp tục nhập không?", "TIẾP TỤC", "THOÁT");//31
        //            DeleteRow(row1, col1, 35);
        //            DeleteRow(row1, col1 + 1, 35);
        //            if (Selection == 2)
        //            {
        //                return false;
        //            }
        //            Console.SetCursorPosition(row + count, column);
        //        }

        //        if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 64 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == 46 || key.KeyChar == '/')
        //        {
        //            if (count < length)
        //            {
        //                Console.SetCursorPosition(row + count, column);
        //                Console.WriteLine(key.KeyChar);
        //                ResultString += key.KeyChar.ToString();
        //                count++;
        //            }
        //        }
        //        else if (key.KeyChar == 8 && (string.IsNullOrEmpty(ResultString) == false))
        //        {
        //            Console.Write("\b \b");
        //            ResultString = ResultString.Substring(0, count - 1);
        //            count--;
        //        }
        //    }
        //}

        public static void DeleteRow(int x, int y, int dodai)
        {
            Console.SetCursorPosition(x, y); // di chuyển con trỏ đến vị trí x, y;
            for (int i = 0; i < dodai; i++) 
                Console.Write((char)32); 
        }

        public static void Delete(int row, int col, int width, int height)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(row, col + height);
                for (int j = 0; j < width; j++)
                {
                    Console.Write((char)32);
                }
            }
            
        }
        public static void WriteColorString(string s, int col, int row, ConsoleColor back, ConsoleColor fore)
        {
            SetColors(back, fore);
            Console.SetCursorPosition(col, row);
            Console.Write(s);
        }
        public static void SetColors(ConsoleColor back, ConsoleColor fore)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }
        public static void CleanUp()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }

        public static string DisplayMoney(double money)
        {
            int count = (int)Math.Log10(money) + 1;
            string moneys = money.ToString();
            for(int i = 1; i < count; i++)
            {
                if(i % 3 == 0)
                {
                    moneys = moneys.Insert(count - i, ".");
                }
            }
            return moneys;
        }

        public static string DisplayMonAn(MonAn ma)
        {
            return ma.Tenmon + ": " + DisplayMoney((ma.Dongia) / 1000) + " K";
        }
        public static bool LimitLengthEnterString(int row, int column, int row1, int col1, int length, out string ResultString)
        {
            Console.SetCursorPosition(row, column);
            Console.CursorVisible = true;
            ResultString = string.Empty;
            int count = 0;
            ConsoleKeyInfo key;

            while (true)
            {
                Console.SetCursorPosition(row + count, column);
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    return true;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    int Selection = TrueFalse(row1, col1, "Bạn có muốn tiếp tục nhập không?", "TIẾP TỤC", "THOÁT");//31
                    DeleteRow(row1, col1, 35);
                    DeleteRow(row1, col1 + 1, 35);
                    if (Selection == 2)
                    {
                        return false;
                    }
                    Console.SetCursorPosition(row + count, column);
                }

                if ((key.KeyChar >= 48 && key.KeyChar <= 57) || (key.KeyChar >= 64 && key.KeyChar <= 90) || (key.KeyChar >= 97 && key.KeyChar <= 122) || key.KeyChar == 32 || key.KeyChar == 46 || key.KeyChar == '/')
                {
                    if (count < length)
                    {
                        Console.SetCursorPosition(row + count, column);
                        Console.WriteLine(key.KeyChar);
                        ResultString += key.KeyChar.ToString();
                        count++;
                    }
                }
                else if (key.KeyChar == 8 && (string.IsNullOrEmpty(ResultString) == false))
                {
                    Console.Write("\b \b");
                    ResultString = ResultString.Substring(0, count - 1);
                    count--;
                }
            }
        }

        public static int TrueFalse(int row, int column, string Title, string SelectionOne, string SelectionTwo)
        {
            Console.CursorVisible = false;
            int length = SelectionOne.Length > SelectionTwo.Length ? SelectionOne.Length : SelectionTwo.Length;
            if (length == SelectionOne.Length)
            {
                // One > Two
                SelectionTwo = new string(' ', (length - SelectionTwo.Length) / 2) + SelectionTwo + new string(' ', (length - SelectionTwo.Length) / 2);
            }
            else
            {
                SelectionOne = new string(' ', (length - SelectionOne.Length) / 2) + SelectionOne + new string(' ', (length - SelectionOne.Length) / 2);
            }

            int distance = (Title.Length - length * 2) / 3;
            int rowOne = row + distance;
            int rowTwo = row + distance * 2 + length;

            string[] Selection = { SelectionOne, SelectionTwo };
            int[] location = { rowOne, rowTwo };
            Console.SetCursorPosition(row, column); Console.Write(Title.Trim());
            Console.SetCursorPosition(location[0], column + 1); Console.Write(Selection[0]);
            Console.SetCursorPosition(location[1], column + 1); Console.Write(Selection[1]);
            TienIch.DiChuyen(Selection[0], location[0], column + 1, ConsoleColor.Red, ConsoleColor.Black);
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;
            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r') //Enter
                {
                    Console.CursorVisible = true;
                    Console.ResetColor();
                    return choice;
                }
                else if (cki.Key == ConsoleKey.RightArrow) // Sang phải: 2 -> 0
                {
                    TienIch.DiChuyen(Selection[choice - 1], location[choice - 1], column + 1, ConsoleColor.Black, ConsoleColor.White);
                    if (choice < 2) choice++;
                    else choice = 1;
                    TienIch.DiChuyen(Selection[choice - 1], location[choice - 1], column + 1, ConsoleColor.Red, ConsoleColor.Black);
                }
                else if (cki.Key == ConsoleKey.LeftArrow) // Sang trái 0 -> 2
                {
                    TienIch.DiChuyen(Selection[choice - 1], location[choice - 1], column + 1, ConsoleColor.Black, ConsoleColor.White);
                    if (choice > 1) choice--;
                    else choice = 2;
                    TienIch.DiChuyen(Selection[choice - 1], location[choice - 1], column + 1, ConsoleColor.Red, ConsoleColor.Black);
                }
            }
        }

    }
}
