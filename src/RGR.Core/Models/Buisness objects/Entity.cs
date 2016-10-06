using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Models
{
    //Базовый класс для субъектов
    public abstract class Entity : BuisnessObject
    {
        //Имя/названия лица
        public virtual string Name { get; set;}

        public Entity() { } //Публичный конструктор
        
        //Внутренний конструктор для получения данных из DO
        internal Entity(int ID, DateTime CreationTime)
        {
            this.id = ID;
            this.creationTime = CreationTime;
        }
    }



    //Физлица
    public class Person : Entity
    {
        //ФИО и полное имя
        public string FirstName;
        public string SecondName;
        public string LastName;
        public override string Name => $"{FirstName} {SecondName} {LastName}";

        //Дата рождения
        internal DateTime birthday;
        public DateTime Birthday => birthday;

        public Person() { } //Публичный конструктор

        //Внутренний конструктор для получения данных из DO
        internal Person(int ID, DateTime CreationTime, string FirstName, string SecondName, string LastName, DateTime Birthday)
            :base(ID, CreationTime)
        {
            this.FirstName  = FirstName;
            this.SecondName = SecondName;
            this.LastName   = LastName;
            this.birthday   = Birthday;
        }
    }


    //Оршанизации
    public class Organization : Entity
    {
        //ссылки на членов организации
        public List<int> Members;

        public Organization() { } //Публичный конструктор
        
        //Внутренний конструктор для получения данных из DO
        internal Organization(int ID, DateTime CreationTime, int[] Members)
            :base(ID, CreationTime)
        {
            this.Members = Members.ToList();
        }
    }
}
