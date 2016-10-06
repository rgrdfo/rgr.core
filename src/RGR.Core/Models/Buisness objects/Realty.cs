using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Models
{
    //Базовый класс для объектов недвижимости
    public abstract class Realty : BuisnessObject
    {
        
        protected CadastrialNumber cadastre;
        /// <summary>
        /// Кадастровый номер объекта
        /// </summary>
        public CadastrialNumber Cadastre => cadastre;

        //ссылка на запись в таблице лиц
        protected int owner;
        /// <summary>
        /// Ссылка на владельца
        /// </summary>
        public int Owner { get; set; }

        //позиция на карте
        protected Point location;
        /// <summary>
        /// Позиция на карте
        /// </summary>
        public Point Location => location;

        //адрес
        protected string address;
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address => address;
    }
}
