using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.ExtentionMethods
{
    public static class StringExtentions
    {
        public static bool ContainsAny(this string s, char[] chars)
        {
            bool isContain = false;
            foreach (char c in chars)
            {
                if (!s.Contains(c))
                    continue;
                isContain = true;
            }

            return isContain;
        }

        public static bool ContainsNonChar(this string s)
        {
            List<char> chars = new() {' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            var sChars = s.ToLower().ToCharArray();
            foreach (var sChar in sChars)
            {
                if (chars.Contains(sChar))
                    continue;
                return true;
            }
            return false;
        }
    }
}
