using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

using RGR.Core.Models;


namespace RGR.Core.Controllers.Account
{
    public class SessionUtils
    {
        /// <summary>
        /// Сериализовать пользователя и записать его в сессию 
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="User"></param>
        public static void SetUser(HttpContext Context, Users User)
        {
            var json = JsonConvert.SerializeObject(User);
            Context.Session.SetString("user", json);
        }

        /// <summary>
        /// Получить текущего пользователя из сессии. Если пользователь не задан, возвращается null
        /// </summary>
        /// <param name="Context"></param>
        /// <returns></returns>
        public static Users GetUser(HttpContext Context)
        {
            if(Context.Session.Keys.Contains("user"))
                if(!string.IsNullOrEmpty(Context.Session.GetString("user")))
                {
                    string json = Context.Session.GetString("user");
                    return JsonConvert.DeserializeObject<Users>(json);
                }
            return null;        
        }
    }
}
