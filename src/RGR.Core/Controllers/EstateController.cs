using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Common.DTO;
using RGR.Core.Common.Enums;
using RGR.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace RGR.Core.Controllers
{
    [Authorize]
    public class EstateController : Controller
    {
        private rgrContext db;
        public EstateController(rgrContext context)
        {
            db = context;
        }

        public async Task<IActionResult> AddObject()
        {
            var Estate = new EstateObjects()
            {
                UserId = HttpContext.Session.GetUserId(),
                Operation = (short)EstateOperations.Selling,
                Status = (short)EstateStatuses.Draft,
                DateCreated = DateTime.UtcNow,
                CreatedBy = HttpContext.Session.GetUserId()
            };

            db.EstateObjects.Add(Estate);
            await db.SaveChangesAsync();

            var communications = new ObjectCommunications { ObjectId = Estate.Id };
            var main = new ObjectMainProperties { ObjectId = Estate.Id };
            var rating = new ObjectRatingProperties { ObjectId = Estate.Id };
            var address = new Addresses { ObjectId = Estate.Id };
            var addt = new ObjectAdditionalProperties { ObjectId = Estate.Id };

            db.ObjectCommunications.Add(communications);
            db.ObjectMainProperties.Add(main);
            db.ObjectRatingProperties.Add(rating);
            db.Addresses.Add(address);
            db.ObjectAdditionalProperties.Add(addt);

            await db.SaveChangesAsync();

            ViewData["EstateId"] = Estate.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDraft([FromBody] NewEstateDTO Draft)
        {
            var ClientName = Draft.Client?.Split(' ') ?? new string[0];

            var Estate = db.EstateObjects
                .Include(e => e.ObjectMainProperties)
                .Include(e => e.ObjectAdditionalProperties)
                .Include(e => e.ObjectCommunications)
                .Include(e => e.ObjectRatingProperties)
                .Include(e => e.Addresses)
                .First(e => e.Id == Draft.EstateId);

            Estate.UserId = Draft.ContactUserId;
            Estate.ObjectType = Draft.EstateType;
            Estate.ClientId = db.Clients
                            .FirstOrDefault(c =>
                                ClientName.Length == 3 &&
                                c.LastName == ClientName[0] &&
                                c.FirstName == ClientName[1] &&
                                c.SurName == ClientName[2])
                            ?.Id ?? -1;

            var Main = Estate.ObjectMainProperties.First();
            Main.Security = new string[2] { Draft.Guard ? "10" : null, Draft.Alarm ? "11" : null }
                            .Aggregate((result, current) => string.IsNullOrEmpty(result) ? current : result += $",{current}");
            Main.PropertyType = Draft.PropertyType;
            Main.Negotiable = Draft.Negotiable;
            Main.TotalArea = Draft.CommonArea;
            Main.ActualUsableFloorArea = Draft.RoomsArea;
            Main.KitchenFloorArea = Draft.KitchenArea;
            Main.MortgagePossibility = Draft.Hypothec;
            Main.BuildingMaterial = Draft.BuildingMaterial;
            Main.HasPhotos = Draft.Photos?.Count > 0;

            if (Draft.BuildYear == null)
                Main.BuildingPeriod = null;
            else
            {
                Main.BuildingPeriod =
                             Draft.BuildYear <= 1917 ? 103 :
                             Draft.BuildYear >= 1918 && Draft.BuildYear <= 1953 ? 104 :
                             Draft.BuildYear >= 1954 && Draft.BuildYear <= 1965 ? 105 :
                             Draft.BuildYear >= 1966 && Draft.BuildYear <= 1991 ? 106 :
                             107;
            }
            
           


        }
    }
}
