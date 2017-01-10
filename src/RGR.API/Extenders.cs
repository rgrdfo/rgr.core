using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.API
{
    public static class Extenders
    {
        //Почему они сами так не сделали?
        /// <summary>
        /// Указывает, является ли данная строка пустой или null. Является более удобной записью метода string.IsNullOrEmpty
        /// </summary>
        public static bool IsNullOrEmpty (this string Source)
        {
            return string.IsNullOrEmpty(Source);
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
    }
}
