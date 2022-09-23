using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Nha_Hang.UI
{
    
    public class Strings
    {
        public static void ChuanHoa(ref string s)
        {// Do Chi      Hung
            s = s.Trim();
            string[] arr = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].ToLower();
                char[] word = arr[i].ToCharArray();
                word[0] = char.ToUpper(word[0]);
                arr[i] = new string(word);
                s += arr[i] + " ";
            }
            s = s.Trim();
        }

        public static string ChuanHoa(string s)
        {
            s = s.Trim();
            string[] arr = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].ToLower();
                char[] word = arr[i].ToCharArray();
                word[0] = char.ToUpper(word[0]);
                arr[i] = new string(word);
                s += arr[i] + " ";
            }
            return s = s.Trim();
        }

    }
    
}
