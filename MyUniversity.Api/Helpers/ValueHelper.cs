using System;
using System.Text.RegularExpressions;

namespace MyUniversity.Api.Helpers
{
    public static class ValueHelper
    {
        public static string RemoveExtraSpace(this string source)
        {
            return Regex.Replace(source, @"\s+", " ");
        }
    }
}