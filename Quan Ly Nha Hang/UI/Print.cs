using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Nha_Hang.DTO;

namespace Quan_Ly_Nha_Hang.UI
{
    public class Print
    {
        public static void XoaDong(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.Write((char)32);
            }
        }
        static int tableWidth = 105;
        public static void PrintLine()
        {

            Console.OutputEncoding = Encoding.ASCII;
            Console.WriteLine(new string('-', tableWidth));
        }
        public static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = string.Empty;
            foreach (string column in columns)
            {
                row += AlignCentre(column, width);
            }

            Console.WriteLine(row);
        }

        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
    public class Printf
    {
        static int tableWidth = 69;
        public static void PrintLine()
        {

            Console.OutputEncoding = Encoding.ASCII;
            Console.WriteLine(new string('-', tableWidth));
        }
        public static void PrintRow(Ban ban)
        {
            int width = (tableWidth - 2) / 2;
            string row = string.Empty;

            row += AlignCentre(ban.Maban, width) + AlignCentre(ban.Trangthai, width);

            Console.WriteLine(row);
        }

        public static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = string.Empty;
            foreach (string column in columns)
            {
                row += AlignCentre(column, width);
            }

            Console.WriteLine(row);
        }

        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }


    public class Printt
    {
        static int tableWidth;
        public Printt(int TableWith)
        {
            tableWidth = TableWith;
        }
        public static void PrintLine()
        {

            Console.OutputEncoding = Encoding.ASCII;
            Console.WriteLine(new string('-', tableWidth));
        }

        public static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = string.Empty;
            foreach (string column in columns)
            {
                row += AlignCentre(column, width);
            }

            Console.WriteLine(row);
        }

        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
