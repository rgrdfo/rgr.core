using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core
{
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
    }
}