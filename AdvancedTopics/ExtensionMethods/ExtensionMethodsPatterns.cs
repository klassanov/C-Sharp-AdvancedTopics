using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTopics.ExtensionMethods
{
    internal static class ExtensionMethodsPatterns
    {
        //1. Method name shortener -> al stands for AppendLine
        /// <summary>
        /// al: AppendLine
        /// </summary>
        public static StringBuilder al(this StringBuilder sb, string s)
        {
            return sb.AppendLine(s);
        }

        //2. Combine two or more calls together
        public static StringBuilder AppendFormatLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
        }

        //3. Composite calls -> xor all the values of a list
        public static ulong Xor(this IList<ulong> values)
        {
            var first = values[0];
            foreach (var v in values.Skip(1))
            {
                first = first ^ v;
            }

            return first;
        }

        //4. AddRange method with params
        public static void AddRange<T>(this IList<T> list, params T[] values)
        {
            foreach (var v in values)
            {
                list.Add(v);
            }
        }

        //5. Anti static extension method
        public static string f(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        //6. Factory
        public static DateTime June(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }
    }
}
