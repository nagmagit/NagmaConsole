using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nagma
{
    static class Helpers
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Splits an array in every space character unless it is surrounded by quotation marks.
        /// </summary>
        public static string[] SmartDivision(this string str)
        {
            return Regex.Matches(str, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value.Replace("\"", String.Empty))
                .ToArray();
        }
    }
}
