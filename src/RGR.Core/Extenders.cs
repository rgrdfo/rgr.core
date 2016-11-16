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
        /// <param name="Source"></param>
        /// <param name="Items"></param>
        /// <returns></returns>
        public static bool ContainsOne(this string Source, IEnumerable<string> Items)
        {
            foreach (var item in Items)
            {
                if (Source.Contains(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Склеивает коллекцию в одну строку. Для склейки используется экземпляр StringBuilder
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string Flatten(this IEnumerable<string> Source)
        {
            if (!Source.Any() || Source == null)
                return null;

            var sb = new StringBuilder("");
            foreach (var str in Source)
            {
                sb.Append(str);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Возвращает список значений, разделённых запятой
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Ids">Строка, содержащая индексы значений словаря</param>
        /// <returns></returns>
        public static string GetFromIds(this IEnumerable<DictionaryValues> Source, string Ids)
        {
            if (!Source.Any()) return null;

            return Ids.Split(',')
                        .Select(i => Source.First(d => d.Id == long.Parse(i)).Value)
                        .Aggregate((result, current) => (string.IsNullOrEmpty(result) ? current : result += $", {current}"));
        }


        /// <summary>
        /// Пытается найти индекс БД улицы по её названию и индексу жилмассива. Если улица не найдена - возвращает null
        /// </summary>
        public static long? TryGetId(this IEnumerable<GeoStreets> Source, string Name, long? AreaId)
        {
            if (AreaId == null) return null;

            var result = Source.FirstOrDefault(s => s.AreaId == AreaId && s.Name.Contains(Name));
            return (result != null) ? result.Id : default(long?);
        }
    }
}