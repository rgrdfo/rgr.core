using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using RGR.Core.Models;
using System.Threading.Tasks;

namespace RGR.Core
{
    //Удобно.
    static class Extenders
    {
        /// <summary>
        /// Принимает в качестве параметра коллекцию или последовательность строк, и проверяет, содержится ли хотя бы одна из них внутри данной строки
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Items"></param>
        /// <returns></returns>
        public static bool ContainsOne(this string source, IEnumerable<string> Items)
        {
            foreach (var item in Items)
            {
                if (source.Contains(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Склеивает коллекцию в одну строку. Для склейки используется экземпляр StringBuilder
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Flatten(this IEnumerable<string> source)
        {
            if (!source.Any() || source == null)
                return null;

            var sb = new StringBuilder("");
            foreach (var str in source)
            {
                sb.Append(str);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Возвращает список значений, разделённых запятой
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Ids">Строка, содержащая индексы значений словаря</param>
        /// <returns></returns>
        public static string GetFromIds(this List<DictionaryValues> source, string Ids)
        {
            if (!source.Any()) return null;

            return Ids.Split(',')
                        .Select(i => source.First(d => d.Id == long.Parse(i)).Value)
                        .Aggregate((result, current) => (string.IsNullOrEmpty(result) ? current : result += $", {current}"));
        }
    }
}