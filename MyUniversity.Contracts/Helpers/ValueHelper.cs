using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyUniversity.Contracts.Helpers
{
    public static class ValueHelper
    {
        public static string RemoveExtraSpace(this string source)
        {
            return Regex.Replace(source, @"\s+", " ");
        }

        public static string ByteArrayToString(this byte[] ba)
        {
            var hex = new StringBuilder(ba.Count() * 2);
            foreach (var b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static byte[] StringToByteArray(this string hexString)
        {
            int length = hexString.Length;
            int upperBound = length / 2;
            if (length % 2 == 0)
            {
                upperBound -= 1;
            }
            else
            {
                hexString = "0" + hexString;
            }
            byte[] bytes = new byte[upperBound + 1];
            for (int i = 0; i <= upperBound; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return bytes;
        }
    }
}