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

            var estate = await db.EstateObjects.ToListAsync();
            
            var convert = new PassportConverter()
            {
                Addresses      = await db.Addresses.ToListAsync(),
                MainProps      = await db.ObjectMainProperties.ToListAsync(),
                AddtProps      = await db.ObjectAdditionalProperties.ToListAsync(),
                Cities         = await db.GeoCities.ToListAsync(),
                Streets        = await db.GeoStreets.ToListAsync(),
                DictValues     = await db.DictionaryValues.ToListAsync(),
                Companies      = await db.Companies.ToListAsync(),
                Users          = await db.Users.ToListAsync(),
                Medias         = await db.ObjectMedias.ToListAsync(),
                Ratings        = await db.ObjectRatingProperties.ToListAsync(),
                Communications = await db.ObjectCommunications.ToListAsync(),
                Files          = await db.StoredFiles.ToListAsync()
            };

            var myEstate = estate.Where(e => e.UserId == user.Id);
            var company = db.Companies.FirstOrDefault(c => c.DirectorId == user.Id) ?? null;

            List<EstateObjects> companyEstate = null;
            if (company != null)
            {
                companyEstate = new List<EstateObjects>();
                var companyUsers = db.Users.Where(u => u.CompanyId == company.Id);
                foreach (var cUser in companyUsers)
                    companyEstate.AddRange(estate.Where(e => e.UserId == cUser.Id));
            }

            page.MyObjects = convert.GetShortPassports(myEstate);
            page.CompanyObjects = (companyEstate == null) ?
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
