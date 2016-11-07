using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.Core.Common
{
    public static class SessionUtils
    {
        /// <summary>
        /// Записать в сессию индекс БД пользователя, а так же некоторые часто употребимые данные (например, имя)
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="User"></param>
        public static void SetUser(this ISession Session, Users User)
        {
            Session.SetString("UserId", User.Id.ToString());
            Session.SetString("FirstName", User.FirstName);
            Session.SetString("SurName", User.SurName);
            Session.SetString("LastName", User.LastName);
        }

        /// <summary>
        /// Получить текущего пользователя из сессии. Если пользователь не задан, возвращается null
        /// </summary>
        internal static Users GetUser(this ISession Session, rgrContext db)
        {
            if (Session.Keys.Contains("UserId"))
            {
                string strId = Session.GetString("UserId");
                if (!string.IsNullOrEmpty(strId))
                {
                    var id = long.Parse(strId);
                    return db.Users.SingleOrDefault(s => s.Id == id);
                }
            }
                
            return null;        
        }

        /// <summary>
        /// Получение имени пользователя из сессии. Если записи нет, сесия неактивна или запись некорректна, возвращается пустая строка
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static string GetFirstName (this ISession Session)
        {
            if (Session.Keys.Contains("FirstName"))
                return Session.GetString("FirstName");
            else
                return "";
        }

        /// <summary>
        /// Получить ФИО пользователя из сессии
        /// </summary>
        /// <param name="Session">Ссылка на текущую сессию</param>
        /// <param name="LastNameFirst">Выводить имя в формате "Фамилия Имя Отчество" (по умолчанию) или "Имя Отчество Фамилия"</param>
        /// <returns></returns>
        public static string GetFullName(this ISession Session, bool LastNameFirst = true)
        {
            string firstName, surName, lastName;

            firstName = (Session.Keys.Contains("FirstName")) ? Session.GetString("FirstName") : "";
            surName   = (Session.Keys.Contains("SurName"))   ? Session.GetString("SurName")   : "";
            lastName  = (Session.Keys.Contains("LastName"))  ? Session.GetString("LastName")  : "";

            if(LastNameFirst)
                return $"{lastName} {firstName} {surName}";
            else
                return $"{firstName} {surName} {lastName}";
        }

        /// <summary>
        /// Получение индекса БД пользователя из сессии. Если записи нет, сесия неактивна или запись некорректна, возвращается -1
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static long GetUserId(this ISession Session)
        {
            if (Session.Keys.Contains("UserId"))
            {
                long id;
                if (long.TryParse(Session.GetString("UserId"), out id))
                    return id;
            }
                return -1;
        }
    }
}
