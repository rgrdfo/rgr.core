using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.Core.Controllers.Search
{
    /// <summary>
    /// Карточка сохранённого запроса
    /// </summary>
    public class RequestPassport
    {
        private rgrContext db;
        private List<SearchRequests> requests;
        private List<Users> users;

        public string Title { get; private set; }
        public string Query { get; private set; }
        public long UserId { get; private set; }
        public int TimesUsed { get; private set; }
        public DateTime? DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }

        private RequestPassport(rgrContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Создание и инициализация нового экземпляра карточки сохранённого запроса
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Title"></param>
        /// <param name="Query"></param>
        /// <returns></returns>
        //public static async Task<RequestPassport> Create(rgrContext db, string Title, string Query)
        //{
        //    var passport = new RequestPassport(db);

        //    passport.requests = await db.SearchRequests.ToListAsync();
        //    passport.users = await db.Users.ToListAsync();


        //}
    }
}
