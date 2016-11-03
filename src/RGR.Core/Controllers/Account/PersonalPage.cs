using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Common;
using RGR.Core.Models;

namespace RGR.Core.Controllers.Account
{
    public class PersonalPage
    {
        private rgrContext db;

        public PersonalPage(rgrContext db)
        {
            this.db = db;
        }

        public SuitableEstate MyObjects;
        public SuitableEstate CompanyObjects;

        //private SuitableEstate getEstate(Func<EstateObjects, bool> predicate)
        //{
        //    var estate = new SuitableEstate();
        //    var dbEstate = db.EstateObjects.Where(predicate);
        //    return 
        //}
    }
}
