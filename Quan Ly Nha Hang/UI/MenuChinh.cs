using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.UI
{
    public class MenuChinh
    {
        
        public static int LuaChon(string s, string[] items, int x, int y, ConsoleColor back, ConsoleColor fore)
        {
            int num = items.Length;
            int max = items[0].Length;
            for (int i = 1; i < num; i++)
            {
                if (items[i].Length > max)
                {
                    max = items[i].Length;
                }
            }

            int[] daucachphai = new int[num];
            for (int i = 0; i < num; i++)
            {
                daucachphai[i] = max - items[i].Length + 1;
            }


            int lcol = x + max + 3;
            int lrow = y + num + 1;
            TableMenu(x, y, lcol, lrow, back, fore, true);
            TienIch.SetColors(back, fore);
            Console.SetCursorPosition(x + 1, y - 1);
            Console.Write(s);

            TienIch.WriteColorString(" " + items[0] + new string(' ', daucachphai[0]), x + 1, y + 1, fore, back);

            for (int i = 2; i <= num; i++)
            {
                TienIch.WriteColorString(items[i - 1], x + 2, y + i, back, fore);
            }

            ConsoleKeyInfo cki;
            char key;
            int luachon = 1;

            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r') // enter
                {
                    return luachon;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    TienIch.WriteColorString(" " + items[luachon - 1] + new string(' ', daucachphai[luachon - 1]), x + 1, y + luachon, back, fore);
                    if (luachon < num)
                    {
                        luachon++;
                    }
                    else
                    {
                        luachon = 1;
                    }
                    TienIch.WriteColorString(" " + items[luachon - 1] + new string(' ', daucachphai[luachon - 1]), x + 1, y + luachon, fore, back);

                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    TienIch.WriteColorString(" " + items[luachon - 1] + new string(' ', daucachphai[luachon - 1]), x + 1, y + luachon, back, fore);
                    if (luachon > 1)
                    {
                        luachon--;
                    }
                    else
                    {
                        luachon = num;
                    }
                    TienIch.WriteColorString(" " + items[luachon - 1] + new string(' ', daucachphai[luachon - 1]), x + 1, y + luachon, fore, back);
                }
            }
        }

        public static void TableMenu(int ucol, int urow, int lcol, int lrow, ConsoleColor back, ConsoleColor fore, bool fill)
        {            
            const char Ngang = '═';
            const char Doc = '║';
            const char NoiTrai = '╠';
            const char NoiPhai = '╣';
            const char KhopDuoiBenTrai = '╚';
            const char KhopDuoiBenPhai = '╝';

            TienIch.SetColors(back, fore);
            string fillLine = fill ? new string(' ', lcol - ucol - 1) : "";
            TienIch.SetColors(back, fore);
            Console.SetCursorPosition(ucol, urow - 2); Console.Write("╔");
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Ngang);

            }
            Console.SetCursorPosition(lcol, urow - 2); Console.Write("╗");
            Console.SetCursorPosition(ucol, urow - 1);
            Console.Write(Doc);
            if (fill) Console.Write(fillLine);
            Console.SetCursorPosition(lcol, urow - 1);
            Console.Write(Doc);

            Console.SetCursorPosition(ucol, urow); Console.Write(NoiTrai);
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Ngang);

            }
            Console.Write(NoiPhai);

            for (int i = urow + 1; i < lrow; i++)//------------------
            {

                Console.SetCursorPosition(ucol, i);
                Console.Write(Doc);

                if (fill) Console.Write(fillLine);
                Console.SetCursorPosition(lcol, i);
                Console.Write(Doc);
            }//-----------------------------------------------
            Console.SetCursorPosition(ucol, lrow); Console.Write(KhopDuoiBenTrai);
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Ngang);

            }
            Console.Write(KhopDuoiBenPhai);
        }

    }
}
