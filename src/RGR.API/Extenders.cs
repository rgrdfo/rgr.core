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

        /// <summary>
        /// Пытается найти индекс БД улицы по её названию и индексу жилмассива. Если улица не найдена - возвращает null
        /// </summary>
        public static long? TryGetId(this IEnumerable<GeoStreets> Source, string Name, long? AreaId)
        {
            if (AreaId == null) return null;

            var result = Source.FirstOrDefault(s => s.AreaId == AreaId && s.Name.Contains(Name));
            return (result != null) ? result.Id : default(long?);
        }

        public static long? GetIdByName(this IEnumerable<Users> Source, string Name)
        {
            if (Name == null)
                return null;

            var names = Name.Split(' ');
            var user = Source.FirstOrDefault(u => u.LastName == names[0] && u.FirstName == names[1] && u.SurName == names[2]);

            return user?.Id;
        }
    }
}
