using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RGR.Core.Common;
using RGR.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RGR.Core.Common.SearchUtils;

namespace RGR.Core.Controllers.Account
{
    public class PersonalPage
    {
        private rgrContext db;
        private ISession session;

        private PersonalPage(rgrContext db, ISession Session)
        {
            this.db = db;
            session = Session;
        }

        public IEnumerable<ShortPassport> MyObjects { get; private set; }
        public IEnumerable<ShortPassport> CompanyObjects { get; private set; }

        /// <summary>
        /// Создаёт новый экземпляр личного кабинета
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static async Task<PersonalPage> GenerateAsync (rgrContext db, ISession Session)
        {
            var page = new PersonalPage(db, Session);
            var user = page.session.GetUser(db);

            var convert = new PassportConverter(db);

            var myEstate = await db.EstateObjects
                .Where(e => e.UserId == user.Id)
                .Include(e => e.ObjectMainProperties)
                .Include(e => e.ObjectAdditionalProperties)
                .Include(e => e.Addresses)
                .Include(e => e.ObjectCommunications)
                .Include(e => e.ObjectRatingProperties)
                .ToListAsync();

            var company = db.Companies.FirstOrDefault(c => c.DirectorId == user.Id) ?? null; 

            List<EstateObjects> companyEstate = null;
            if (company != null)
            {
                //companyEstate = new List<EstateObjects>();
                var companyUsers = db.Users.Where(u => u.CompanyId == company.Id).Select(u => u.Id);
                companyEstate = await db.EstateObjects
                    .Include(e => e.User)
                    .Where(e => companyUsers.Contains(e.UserId))
                    .Include(e => e.ObjectMainProperties)
                    .Include(e => e.ObjectAdditionalProperties)
                    .Include(e => e.Addresses)
                    .Include(e => e.ObjectCommunications)
                    .Include(e => e.ObjectRatingProperties)
                    .ToListAsync();
            }

            page.MyObjects = convert.GetShortPassports(myEstate);
            page.CompanyObjects = (companyEstate == null) ? //В подавляющем большинстве случаев будет null
                null :
                convert.GetShortPassports(companyEstate);

            db.Dispose();
            return page;
        }

        //private SuitableEstate getEstate(Func<EstateObjects, bool> predicate)
        //{
        //    var estate = new SuitableEstate();
        //    var dbEstate = db.EstateObjects.Where(predicate);
        //    return 
        //}
    }
}
