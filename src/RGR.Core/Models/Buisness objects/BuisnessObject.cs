using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Models
{
    //Базовый объект для всех объектов данных
    public abstract class BuisnessObject
    {
        //индекс объекта
        protected int id;
        public int ID => id;

        //Время создания экземпляра объекта
        protected DateTime creationTime;
        public DateTime CreationTime => creationTime;
    }
}
