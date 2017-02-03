using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.API.DTO;
using RGR.API.Common.Enums;
using RGR.API.Common;
using Microsoft.EntityFrameworkCore;
using Eastwing.Parser;
using System.Text;
using RGR.Core.Controllers.Estate;
using System.Globalization;

namespace RGR.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class EstateController : Controller
    {
        private rgrContext db;
        public EstateController(rgrContext context)
        {
            db = context;
        }

        [Route("add")]
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

            return new JsonResult(new
            {
                Cities = db.GeoCities.ToDictionary(c => c.Id, c => c.Name),
                Users = db.Users
                            .Where(u => u.CompanyId == HttpContext.Session.GetUser(db).CompanyId)
                            .Select(u => $"{u.LastName} {u.FirstName} {u.SurName}"),
                EstateId = Estate.Id
            });
        }

        [HttpPost]
        [Route("publish")]
        public async Task<IActionResult> Publish(EstateDTO Draft)
        {
            var ClientName = Draft.Client?.Split(' ') ?? new string[0];

            var Estate = db.EstateObjects
                .Include(e => e.ObjectMainProperties)
                .Include(e => e.ObjectAdditionalProperties)
                .Include(e => e.ObjectCommunications)
                .Include(e => e.ObjectRatingProperties)
                .Include(e => e.Addresses)
                .First(e => e.Id == Draft.EstateId);

            //Estate.UserId = HttpContext.Session.GetUserId();
            Estate.ObjectType = Draft.EstateType;
            Estate.ClientId = db.Clients
                            .FirstOrDefault(c =>
                                ClientName.Length == 3 &&
                                c.LastName == ClientName[0] &&
                                c.FirstName == ClientName[1] &&
                                c.SurName == ClientName[2])
                            ?.Id ?? -1;

            #region Разбор адреса
            var Address = Estate.Addresses.First();

            Address.CityId = db.GeoCities.FirstOrDefault(c => c.Name.Contains(Draft.City))?.Id ?? -1;
            if (Address.CityId == -1)
                throw new ArgumentException("Ошибка определения населённого пункта");

            var rd = db.GeoCities.First(c => c.Id == Address.CityId).RegionDistrictId;
            Address.RegionId = db.GeoRegionDistricts.First(d => d.Id == rd).RegionId;
            Address.CountryId = db.GeoRegions.First(r => r.Id == Address.RegionId).CountryId;

            var street = Draft.Street.TrimStart("улица ".ToCharArray());
            Address.StreetId = db.GeoStreets.FirstOrDefault(s => s.Name.Contains(street))?.Id ?? -1;
            Address.DistrictResidentialAreaId = db.GeoStreets.First(s => s.Id == Address.StreetId).AreaId;
            Address.CityDistrictId = db.GeoResidentialAreas.First(a => a.Id == Address.DistrictResidentialAreaId).DistrictId;

            Address.House = Draft.House;
            Address.Flat = Draft.FlatNumber;

            var sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Address.Latitude = Convert.ToDouble(Draft.Latitude.Replace(".", sep));
            if (Address.Latitude == 0)
                Address.Latitude = null;

            Address.Logitude = Convert.ToDouble(Draft.Longitude.Replace(".", sep));
            if (Address.Logitude == 0)
                Address.Logitude = null;
            #endregion

            #region ObjectMainPropetries
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
            Main.FloorMaterial = Draft.CellingMaterial;
            Main.HasParking = Draft.HasParking;
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

            Main.HouseType = Draft.HouseType;
            Main.Price = Draft.Price;
            Main.FloorNumber = Draft.FloorNumber;
            Main.TotalFloors = Draft.FloorCount;
            Main.MultilistingBonus = Draft.Bonus;
            Main.MultilistingBonusType = Draft.BonusIsAbsolute ? 356 : 355;
            long id = db.Users.GetIdByName(Draft.ContactUser) ?? HttpContext.Session.GetUserId();
            Main.ContactPersonId = id;
            Main.ContactCompanyId = db.Companies
                                    .Include(c => c.Users)
                                    .FirstOrDefault(c => c.Users
                                                            .Select(u => u.Id)
                                                            .Contains(id)
                                    )?.Id;
            Main.FullDescription = Draft.Description;
            #endregion

            #region ObjectAdditionalPropetries
            var Addt = Estate.ObjectAdditionalProperties.First();

            Addt.BuildingYear = Draft.BuildYear;
            Addt.BalconiesCount = Draft.BalconiesCount;
            Addt.LoggiasCount = Draft.LogiasCount;
            Addt.RoomsCount = Draft.RoomsCount;
            Addt.RoomPlanning = Draft.RoomsType;
            Addt.AgreementType = Draft.ContractType;
            Addt.AgreementNumber = Draft.ContractNumber;
            Addt.Comission = Draft.Comission.ToString();

            if (Draft.ContractDate == default(DateTime))
                Addt.AgreementStartDate = null;
            else
                Addt.AgreementStartDate = Draft.ContractDate;

            if (Draft.ContractEndDate == default(DateTime))
                Addt.AgreementEndDate = null;
            else
                Addt.AgreementEndDate = Draft.ContractEndDate;



            #endregion

            #region ObjectRatingProperties
            var Rating = Estate.ObjectRatingProperties.First();

            Rating.Kitchen = Draft.Kitchen;
            Rating.CommonState = Draft.State;
            Rating.Wc = Draft.SeparatedWC ? "226" : "227";
            #endregion

            await db.SaveChangesAsync();

            var validationResult = await Estate.ValidateAsync(db);
            switch (validationResult)
            {
                case ValidationCode.Ok:
                    Estate.Status = (short)EstateStatuses.Active;
                    await db.SaveChangesAsync();
                    return new OkResult();

                case ValidationCode.Replace:
                    Estate.Status = (short)EstateStatuses.Active;
                    await db.SaveChangesAsync();
                    return new ContentResult() { Content = "Объект создан успешно, но заместил собой ранее существовавший объект" };

                case ValidationCode.ZeroNumber:
                    return new BadRequestObjectResult("Номер квартиры не может состоять из нулей!");

                case ValidationCode.ObjectExists:
                    return new BadRequestObjectResult("Ваш объект оставлен в статусе \"Черновик\"");

                case ValidationCode.ContractObjectExists:
                    return new BadRequestObjectResult("Ваш договорной объект оставлен в статусе \"Черновик\"");

                case ValidationCode.ContractRequired:
                    return new BadRequestObjectResult("Требуется указать номер и дату договора!");

                default:
                    throw new ArgumentException("Некорректный код валидации");
            }
            


        }
    }
}
