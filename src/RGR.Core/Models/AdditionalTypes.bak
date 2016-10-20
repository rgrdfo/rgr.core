using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Models
{
    public struct Point
    {
        double x;
        double y;

        public override string ToString() => $"{x:000.000} / {y:000.000}";
    }





    /// <summary>
    /// Кадастровый номер
    /// </summary>
    public struct CadastrialNumber
    {
        /// <summary>
        /// Кадастровый округ
        /// </summary>
        public readonly byte County;
        /// <summary>
        /// Кадастровый район
        /// </summary>
        public readonly byte District;
        /// <summary>
        /// Кадастровый квартал
        /// </summary>
        public readonly int  Quarter;
        /// <summary>
        /// Уникальный идентификатор: номер земельного участка, сооружения и т.д.
        /// </summary>
        public readonly int  Uid;

        public CadastrialNumber (byte County, byte District, int Quarter, int Uid)
        {
            this.County   = County;
            this.District = District;
            this.Quarter  = Quarter;
            this.Uid      = Uid;
        }

        /// <summary>
        /// Преобразование строки в кадастровый номер
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static CadastrialNumber FromString (string Source)
        {
            string[] Parts = Source.Split(':');

            //Некорректное число составных частей номера
            if (Parts.Length != 4)
                throw new ArgumentException($"\"{Source}\" не является корректным кадастровым номером!");

            byte County;   //
            byte District; //
            int  Quarter;  //
            int  Uid;      //

            //Проверка составляющих кадастра на валидность:
            if (!byte.TryParse(Parts[0], out County))
                throw new ArgumentException($"Номер кадастрового округа должен быть положительным целым числом меньше 256. Текущее значение: {Parts[0]}");

            if (!byte.TryParse(Parts[1], out District))
                throw new ArgumentException($"Номер кадастрового района должен быть положительным целым числом меньше 256. Текущее значение: {Parts[1]}");

            if ((!int.TryParse(Parts[2], out Quarter)) || (Parts[2].Length != 6 && Parts[2].Length != 7) || Quarter < 0)
                throw new ArgumentException($"Номер кадастрового квартала должен быть шести- или семизначным положительным целым числом. Текущее значение: {Parts[2]}");

            if (!int.TryParse(Parts[3], out Uid) || Uid < 0)
                throw new ArgumentException($"Номер земельного участка должен быть положительным целым числом. Текущее значение: {Parts[3]}");

            return new CadastrialNumber(County, District, Quarter, Uid);
        }
  
        public override string ToString() => $"{County}:{District}:{Quarter}:{Uid}";
    }
}
