using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.API.Common
{
    public static class SessionUtils
    {
        /// <summary>
        /// Записать в сессию часто употребляемые данные пользователя
        /// </summary>
        public static void SetUser(this ISession Session, Users User)
        {
            Session.SetString("UserId", User.Id.ToString());
            Session.SetString("RoleId", User.RoleId.ToString());
            Session.SetString("FirstName", User.FirstName);
            Session.SetString("SurName", User.SurName);
            Session.SetString("LastName", User.LastName);
        }

        /// <summary>
        /// Получить текущего пользователя по индексу, записанному в сессии. Если пользователь не задан, возвращается null. Метод доступен только в серверной части приложения
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
        /// Получить компанию пользователя по его индексу, записанному в сессии. Если пользователь не задан, возвращается null. Метод доступен только в серверной части приложения
        /// </summary>
        internal static Companies GetCompany(this ISession Session, rgrContext db)
        {
            var user = Session.GetUser(db);
            return (user != null) ? db.Companies.FirstOrDefault(c => c.Id == user.CompanyId) : null;
        }

        /// <summary>
        /// Получение идентификатора роли пользователя. Если запись отсутствует или некорректна, возвращает -1
        /// </summary>
        public static long GetRoleId(this ISession Session)
        {
            if (Session.Keys.Contains("RoleId"))
            {
                long result;
                if (long.TryParse(Session.GetString("RoleId"), out result))
                    return result;
            }

            return -1;
        }

        /// <summary>
        /// Получение имени пользователя из сессии. Если записи нет, сесия неактивна или запись некорректна, возвращается пустая строка
        /// </summary>
        public static string GetFirstName (this ISession Session)
        {
            if (Session.Keys.Contains("FirstName"))
                return Session.GetString("FirstName");

            return "";
        }

        /// <summary>
        /// Получить ФИО пользователя из сессии
        /// </summary>
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
        /// Получение индекса БД пользователя из сессии. Если записи нет, сессия неактивна или запись некорректна, возвращается -1
        /// </summary>
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

        /// <summary>
        /// Получение индекса БД компании пользователя из сессии. Если записи нет, сессия неактивна или запись некорректна, возвращается -1
        /// </summary>
        internal static long GetCompanyId(this ISession Session, rgrContext db)
        {
            var user = GetUser(Session, db);
            return (user != null) ? user.CompanyId : -1;
        }
    }
}
