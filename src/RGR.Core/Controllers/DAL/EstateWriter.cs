using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RGR.Core.Models;
using RGR.Core.Common;
using Microsoft.AspNetCore.Http;

namespace RGR.Core.Controllers.DAL
{
    internal class EstateWriter
    {
        public rgrContext db;
        public ISession Session;

        public EstateWriter(rgrContext db, ISession Session)
        {
            this.db = db;
            this.Session = Session;
        }

        /// <summary>
        /// Пытается разместить объект в БД, основываясь на данных запроса. Возвращает код ошибки ("0" - успех)
        /// </summary>
        /// <param name="Request">Запрос (Request.Query)</param>
        /// <returns></returns>
        public async Task<int> Write(IQueryCollection Request)
        {
            var estate = new EstateObjects();
            var main = new ObjectMainProperties();
            var addt = new ObjectAdditionalProperties();
            var address = new Addresses();
            var ratings = new ObjectRatingProperties();
            var media = new ObjectMedias();
            var user = Session.GetUser(db);

            estate.DateCreated = DateTime.UtcNow;
            estate.DateModified = null;
            estate.CreatedBy = Session.GetUserId();
            estate.ModifiedBy = Session.GetUserId();
            estate.UserId = Session.GetUserId();
            estate.User = user;
            estate.ObjectType = short.Parse(Request["EstateType"]);
            estate.Operation = 0;
            estate.Status = 0;
            estate.Filled = false;

            //Новый объект записывается в БД для гарантированного получения уникального индекса (и во избежание конфликтов)
            db.EstateObjects.Add(estate);
            await db.SaveChangesAsync();

            #region Заполнение основных свойств объекта
            main.Object = estate;
            main.ObjectId = estate.Id;
            main.RentPerDay = (Request.ContainsKey("RentPerDay")) ? double.Parse(Request["RentPerDay"]) : default(double?);
            main.RentPerMonth = (Request.ContainsKey("RentPerMonth")) ? double.Parse(Request["RentPerMonth"]) : default(double?);
            main.Security = (Request.ContainsKey("Security")) ? Request["Security"].ToString() : null;
            main.Currency = (Request.ContainsKey("Currency")) ? long.Parse(Request["Currency"]) : default(long?);
            main.PropertyType = (Request.ContainsKey("PropertyType")) ? long.Parse(Request["PropertyType"]) : default(long?);
            main.Negotiable = (Request.ContainsKey("Negotiable")) ? bool.Parse(Request["Negotiable"]) : default(bool?);
            main.ResidencePermit = (Request.ContainsKey("ResidencePermit")) ? bool.Parse(Request["ResidencePermit"]) : default(bool?);
            main.CelingHeight = (Request.ContainsKey("CelingHeight")) ? double.Parse(Request["CelingHeight"]) : default(double?);
            main.AtticHeight = (Request.ContainsKey("AtticHeight")) ? double.Parse(Request["AtticHeight"]) : default(double?);
            main.Yard = (Request.ContainsKey("Yard")) ? long.Parse(Request["Yard"]) : default(long?);
            main.OwnerShare = (Request.ContainsKey("OwnerShare")) ? long.Parse(Request["OwnerShare"]) : default(long?);
            main.AddCommercialBuildings = (Request.ContainsKey("AddCommercialBuildings")) ? Request["AddCommercialBuildings"].ToString() : null;
            main.ActualUsableFloorArea = (Request.ContainsKey("ActualUsableFloorArea")) ? double.Parse(Request["ActualUsableFloorArea"]) : default(double?);
            main.FirstFloorDownSet = (Request.ContainsKey("FirstFloorDownSet")) ? double.Parse(Request["FirstFloorDownSet"]) : default(double?);
            main.Title = (Request.ContainsKey("Title")) ? Request["Title"].ToString() : null;
            main.MortgagePossibility = (Request.ContainsKey("MortgagePossibility")) ? bool.Parse(Request["MortgagePossibility"]) : default(bool?);
            main.MortgageBank = (Request.ContainsKey("MortgageBank")) ? long.Parse(Request["MortgageBank"]) : default(long?);
            main.ObjectUsage = (Request.ContainsKey("ObjectUsage")) ? Request["ObjectUsage"].ToString() : null;
            main.NonResidenceUsage = (Request.ContainsKey("NonResidenceUsage")) ? bool.Parse(Request["NonResidenceUsage"]) : default(bool?);
            main.BuildingClass = (Request.ContainsKey("BuildingClass")) ? long.Parse(Request["BuildingClass"]) : default(long?);
            main.WindowsCount = (Request.ContainsKey("WindowsCount")) ? int.Parse(Request["WindowsCount"]) : default(int?);
            main.PrescriptionsCount = (Request.ContainsKey("PrescriptionsCount")) ? int.Parse(Request["PrescriptionsCount"]) : default(int?);
            main.FamiliesCount = (Request.ContainsKey("FamiliesCount")) ? int.Parse(Request["FamiliesCount"]) : default(int?);
            main.OwnersCount = (Request.ContainsKey("OwnersCount")) ? int.Parse(Request["OwnersCount"]) : default(int?);
            main.PhoneLinesCount = (Request.ContainsKey("PhoneLinesCount")) ? int.Parse(Request["PhoneLinesCount"]) : default(int?);
            main.LevelsCount = (Request.ContainsKey("LevelsCount")) ? int.Parse(Request["LevelsCount"]) : default(int?);
            main.FacadeWindowsCount = (Request.ContainsKey("FacadeWindowsCount")) ? int.Parse(Request["FacadeWindowsCount"]) : default(int?);
            main.UtilitiesRentExspensive = (Request.ContainsKey("UtilitiesRentExspensive")) ? bool.Parse(Request["UtilitiesRentExspensive"]) : default(bool?);
            main.ShortDescription = (Request.ContainsKey("ShortDescription")) ? Request["ShortDescription"].ToString() : null;
            main.FloorMaterial = (Request.ContainsKey("FloorMaterial")) ? Request["FloorMaterial"].ToString() : null;
            main.BuildingMaterial = (Request.ContainsKey("BuildingMaterial")) ? Request["BuildingMaterial"].ToString() : null;
            main.LandAssignment = (Request.ContainsKey("LandAssignment")) ? Request["LandAssignment"].ToString() : null;
            main.ObjectAssignment = (Request.ContainsKey("ObjectAssignment")) ? Request["ObjectAssignment"].ToString() : null;
            main.HasWeights = (Request.ContainsKey("HasWeights")) ? bool.Parse(Request["HasWeights"]) : default(bool?);
            main.HasPhotos = (Request.ContainsKey("HasPhotos")) ? bool.Parse(Request["HasPhotos"]) : default(bool?);
            main.NewBuilding = (Request.ContainsKey("NewBuilding")) ? bool.Parse(Request["NewBuilding"]) : default(bool?);
            main.TotalArea = (Request.ContainsKey("TotalArea")) ? double.Parse(Request["TotalArea"]) : default(double?);
            main.Landmark = (Request.ContainsKey("Landmark")) ? long.Parse(Request["Landmark"]) : default(long?);
            main.ReleaseInfo = (Request.ContainsKey("ReleaseInfo")) ? Request["ReleaseInfo"].ToString() : null;
            main.HasParking = (Request.ContainsKey("HasParking")) ? bool.Parse(Request["HasParking"]) : default(bool?);
            main.BuildingPeriod = (Request.ContainsKey("BuildingPeriod")) ? long.Parse(Request["BuildingPeriod"]) : default(long?);
            main.BigRoomFloorArea = (Request.ContainsKey("BigRoomFloorArea")) ? double.Parse(Request["BigRoomFloorArea"]) : default(double?);
            main.BuildingFloor = (Request.ContainsKey("BuildingFloor")) ? double.Parse(Request["BuildingFloor"]) : default(double?);
            main.KitchenFloorArea = (Request.ContainsKey("KitchenFloorArea")) ? double.Parse(Request["KitchenFloorArea"]) : default(double?);
            main.LandArea = (Request.ContainsKey("LandArea")) ? double.Parse(Request["LandArea"]) : default(double?);
            main.LandFloorFactical = (Request.ContainsKey("LandFloorFactical")) ? double.Parse(Request["LandFloorFactical"]) : default(double?);
            main.Loading = (Request.ContainsKey("Loading")) ? long.Parse(Request["Loading"]) : default(long?);
            main.FullDescription = (Request.ContainsKey("FullDescription")) ? Request["FullDescription"].ToString() : null;
            main.EntranceToObject = (Request.ContainsKey("EntranceToObject")) ? long.Parse(Request["EntranceToObject"]) : default(long?);
            main.AbilityForMachineryEntrance = (Request.ContainsKey("AbilityForMachineryEntrance")) ? bool.Parse(Request["AbilityForMachineryEntrance"]) : default(bool?);
            main.Documents = (Request.ContainsKey("Documents")) ? Request["Documents"].ToString() : null;
            main.Prepayment = (Request.ContainsKey("Prepayment")) ? bool.Parse(Request["Prepayment"]) : default(bool?);
            main.Notes = (Request.ContainsKey("Notes")) ? Request["Notes"].ToString() : null;
            main.RemovalReason = (Request.ContainsKey("RemovalReason")) ? long.Parse(Request["RemovalReason"]) : default(long?);
            main.LivingPeoples = (Request.ContainsKey("LivingPeoples")) ? Request["LivingPeoples"].ToString() : null;
            main.LivingPeolplesDescription = (Request.ContainsKey("LivingPeolplesDescription")) ? Request["LivingPeolplesDescription"].ToString() : null;
            main.EntryLocation = (Request.ContainsKey("EntryLocation")) ? long.Parse(Request["EntryLocation"]) : default(long?);
            main.DistanceToCity = (Request.ContainsKey("DistanceToCity")) ? double.Parse(Request["DistanceToCity"]) : default(double?);
            main.DistanceToSea = (Request.ContainsKey("DistanceToSea")) ? double.Parse(Request["DistanceToSea"]) : default(double?);
            main.FootageExplanation = (Request.ContainsKey("FootageExplanation")) ? Request["FootageExplanation"].ToString() : null;
            main.Advertising1 = (Request.ContainsKey("Advertising1")) ? Request["Advertising1"].ToString() : null;
            main.Advertising2 = (Request.ContainsKey("Advertising2")) ? Request["Advertising2"].ToString() : null;
            main.Advertising3 = (Request.ContainsKey("Advertising3")) ? Request["Advertising3"].ToString() : null;
            main.Advertising4 = (Request.ContainsKey("Advertising4")) ? Request["Advertising4"].ToString() : null;
            main.Advertising5 = (Request.ContainsKey("Advertising5")) ? Request["Advertising5"].ToString() : null;
            main.Relief = (Request.ContainsKey("Relief")) ? long.Parse(Request["Relief"]) : default(long?);
            main.SpecialOffer = (Request.ContainsKey("SpecialOffer")) ? bool.Parse(Request["SpecialOffer"]) : default(bool?);
            main.SpecialOfferDescription = (Request.ContainsKey("SpecialOfferDescription")) ? Request["SpecialOfferDescription"].ToString() : null;
            main.LeaseTime = (Request.ContainsKey("LeaseTime")) ? DateTime.Parse(Request["LeaseTime"]) : default(DateTime?);
            main.Urgently = (Request.ContainsKey("Urgently")) ? bool.Parse(Request["Urgently"]) : default(bool?);
            main.BuildingReadyPercent = (Request.ContainsKey("BuildingReadyPercent")) ? double.Parse(Request["BuildingReadyPercent"]) : default(double?);
            main.HouseType = (Request.ContainsKey("HouseType")) ? long.Parse(Request["HouseType"]) : default(long?);
            main.FlatType = (Request.ContainsKey("FlatType")) ? long.Parse(Request["FlatType"]) : default(long?);
            main.BuildingType = (Request.ContainsKey("BuildingType")) ? long.Parse(Request["BuildingType"]) : default(long?);
            main.Exchange = (Request.ContainsKey("Exchange")) ? bool.Parse(Request["Exchange"]) : default(bool?);
            main.ExchangeConditions = (Request.ContainsKey("ExchangeConditions")) ? Request["ExchangeConditions"].ToString() : null;
            main.HousingStock = (Request.ContainsKey("HousingStock")) ? bool.Parse(Request["HousingStock"]) : default(bool?);
            main.Foundation = (Request.ContainsKey("Foundation")) ? long.Parse(Request["Foundation"]) : default(long?);
            main.Price = (Request.ContainsKey("Price")) ? long.Parse(Request["Price"]) : default(long?);
            main.PricePerUnit = (Request.ContainsKey("PricePerUnit")) ? long.Parse(Request["PricePerUnit"]) : default(long?);
            main.PricePerHundred = (Request.ContainsKey("PricePerHundred")) ? long.Parse(Request["PricePerHundred"]) : default(long?);
            main.OwnerPrice = (Request.ContainsKey("OwnerPrice")) ? long.Parse(Request["OwnerPrice"]) : default(long?);
            main.PricingZone = (Request.ContainsKey("PricingZone")) ? int.Parse(Request["PricingZone"]) : default(int?);
            main.HowToReach = (Request.ContainsKey("HowToReach")) ? long.Parse(Request["HowToReach"]) : default(long?);
            main.ElectricPower = (Request.ContainsKey("ElectricPower")) ? double.Parse(Request["ElectricPower"]) : default(double?);
            main.FloorNumber = (Request.ContainsKey("FloorNumber")) ? int.Parse(Request["FloorNumber"]) : default(int?);
            main.TotalFloors = (Request.ContainsKey("TotalFloors")) ? int.Parse(Request["TotalFloors"]) : default(int?);
            main.ContractorCompany = (Request.ContainsKey("ContractorCompany")) ? long.Parse(Request["ContractorCompany"]) : default(long?);
            main.RentOverpayment = (Request.ContainsKey("RentOverpayment")) ? Request["RentOverpayment"].ToString() : null;
            main.RealPrice = (Request.ContainsKey("RealPrice")) ? long.Parse(Request["RealPrice"]) : default(long?);
            main.MetricDescription = (Request.ContainsKey("MetricDescription")) ? Request["MetricDescription"].ToString() : null;
            main.SellConditions = (Request.ContainsKey("SellConditions")) ? Request["SellConditions"].ToString() : null;
            main.Exclusive = (Request.ContainsKey("Exclusive")) ? bool.Parse(Request["Exclusive"]) : default(bool?);
            main.MultilistingBonus = (Request.ContainsKey("MultilistingBonus")) ? double.Parse(Request["MultilistingBonus"]) : default(double?);
            main.MultilistingBonusType = (Request.ContainsKey("MultilistingBonusType")) ? long.Parse(Request["MultilistingBonusType"]) : default(long?);
            main.ContactPersonId = (Request.ContainsKey("ContactPersonId")) ? long.Parse(Request["ContactPersonId"]) : Session.GetUserId();
            main.ContactCompanyId = (Request.ContainsKey("ContactCompanyId")) ? long.Parse(Request["ContactPersonId"]) : Session.GetCompanyId(db);
            main.ContactCompany = (Request.ContainsKey("ContactCompanyId")) ? db.Companies.FirstOrDefault(c => c.Id == main.ContactCompanyId) : Session.GetCompany(db);
            main.ContactPerson = (Request.ContainsKey("ContactPersonId")) ? db.Users.FirstOrDefault(u => u.Id == main.ContactPersonId) : Session.GetUser(db);
            #endregion

            #region заполнение дополнительных свойств объекта
            addt.Object = estate;
            addt.ObjectId = estate.Id;
            addt.ViewFromWindows = (Request.ContainsKey("ViewFromWindows")) ? Request["ViewFromWindows"].ToString() : null;
            addt.BuildingYear = (Request.ContainsKey("BuildingYear")) ? int.Parse(Request["BuildingYear"]) : default(int?);
            addt.AdditionalBuildings = (Request.ContainsKey("AdditionalBuildings")) ? Request["AdditionalBuildings"].ToString() : null;
            addt.ExtensionsLegality = (Request.ContainsKey("BuildingYear")) ? bool.Parse(Request["BuildingYear"]) : default(bool?);
            addt.Builder = (Request.ContainsKey("BuildingYear")) ? long.Parse(Request["BuildingYear"]) : default(long?);
            addt.BalconiesCount = (Request.ContainsKey("BalconiesCount")) ? int.Parse(Request["BalconiesCount"]) : default(int?);
            addt.RoomsCount = (Request.ContainsKey("RoomsCount")) ? int.Parse(Request["RoomsCount"]) : default(int?);
            addt.LoggiasCount = (Request.ContainsKey("LoggiasCount")) ? int.Parse(Request["LoggiasCount"]) : default(int?);
            addt.BedroomsCount = (Request.ContainsKey("BedroomsCount")) ? int.Parse(Request["BedroomsCount"]) : default(int?);
            addt.BaywindowsCount = (Request.ContainsKey("BaywindowsCount")) ? int.Parse(Request["BaywindowsCount"]) : default(int?);
            addt.Roof = (Request.ContainsKey("Roof")) ? long.Parse(Request["Roof"]) : default(long?);
            addt.ObjectName = (Request.ContainsKey("ObjectName")) ? Request["ObjectName"].ToString() : null;
            addt.Fencing = (Request.ContainsKey("Fencing")) ? long.Parse(Request["Fencing"]) : default(long?);
            addt.RoomPlanning = (Request.ContainsKey("RoomPlanning")) ? long.Parse(Request["RoomPlanning"]) : default(long?);
            addt.Basement = (Request.ContainsKey("Basement")) ? Request["Basement"].ToString() : null;
            addt.CorrectPlanning = (Request.ContainsKey("CorrectPlanning")) ? bool.Parse(Request["CorrectPlanning"]) : default(bool?);
            addt.FlatLocation = (Request.ContainsKey("FlatLocation")) ? long.Parse(Request["FlatLocation"]) : default(long?);
            addt.WindowsLocation = (Request.ContainsKey("WindowsLocation")) ? Request["WindowsLocation"].ToString() : null;
            addt.WindowsMaterial = (Request.ContainsKey("WindowsMaterial")) ? Request["WindowsMaterial"].ToString() : null;
            addt.Redesign = (Request.ContainsKey("Redesign")) ? bool.Parse(Request["Redesign"]) : default(bool?);
            addt.RedesignLegality = (Request.ContainsKey("RedesignLegality")) ? bool.Parse(Request["RedesignLegality"]) : default(bool?);
            addt.PlotForm = (Request.ContainsKey("PlotForm")) ? long.Parse(Request["PlotForm"]) : default(long?);
            addt.FlatRoomsCount = (Request.ContainsKey("FlatRoomsCount")) ? int.Parse(Request["FlatRoomsCount"]) : default(int?);
            addt.ErkersCount = (Request.ContainsKey("ErkersCount")) ? int.Parse(Request["ErkersCount"]) : default(int?);
            addt.RegistrationPosibility = (Request.ContainsKey("RegistrationPosibility")) ? bool.Parse(Request["RegistrationPosibility"]) : default(bool?);
            addt.OwnerPart = (Request.ContainsKey("OwnerPart")) ? long.Parse(Request["OwnerPart"]) : default(long?);
            addt.Burdens = (Request.ContainsKey("Burdens")) ? long.Parse(Request["Burdens"]) : default(long?);
            addt.RentDate = (Request.ContainsKey("RentDate")) ? DateTime.Parse(Request["RentDate"]) : default(DateTime?);
            addt.Court = (Request.ContainsKey("Court")) ? long.Parse(Request["Court"]) : default(long?);
            addt.Fence = (Request.ContainsKey("Fence")) ? long.Parse(Request["Fence"]) : default(long?);
            addt.Loading = (Request.ContainsKey("Loading")) ? Request["Loading"].ToString() : null;
            addt.Environment = (Request.ContainsKey("Environment")) ? long.Parse(Request["Environment"]) : default(long?);
            addt.RentWithServices = (Request.ContainsKey("RentWithServices")) ? bool.Parse(Request["RentWithServices"]) : default(bool?);
            addt.Auction = (Request.ContainsKey("Auction")) ? bool.Parse(Request["Auction"]) : default(bool?);
            addt.Placement = (Request.ContainsKey("Placement")) ? long.Parse(Request["Placement"]) : default(long?);
            addt.AgreementType = (Request.ContainsKey("AgreementType")) ? long.Parse(Request["AgreementType"]) : default(long?);
            addt.AgreementNumber = (Request.ContainsKey("AgreementNumber")) ? Request["AgreementNumber"].ToString() : null;
            addt.AgreementStartDate = (Request.ContainsKey("AgreementStartDate")) ? DateTime.Parse(Request["AgreementStartDate"]) : default(DateTime?);
            addt.AgreementEndDate = (Request.ContainsKey("AgreementEndDate")) ? DateTime.Parse(Request["AgreementEndDate"]) : default(DateTime?);
            addt.Comission = (Request.ContainsKey("Comission")) ? Request["Comission"].ToString() : null;
            addt.AgencyPayment = (Request.ContainsKey("AgencyPayment")) ? bool.Parse(Request["AgencyPayment"]) : default(bool?);
            addt.PaymentCondition = (Request.ContainsKey("PaymentCondition")) ? Request["PaymentCondition"].ToString() : null;
            #endregion

            #region Заполнение адресной записи
            address.Object = estate;
            address.ObjectId = estate.Id;
            address.CityId = long.Parse(Request["CityId"]);
            address.City = db.GeoCities.First(g => g.Id == address.CityId);
            address.RegionDistrictId = address.City.RegionDistrictId;
            address.RegionDistrict = db.GeoRegionDistricts.First(g => g.Id == address.RegionDistrictId);
            address.RegionId = address.RegionDistrict.RegionId;
            address.Region = db.GeoRegions.First(g => g.Id == address.RegionId);
            address.CountryId = address.Region.CountryId;
            address.Country = db.GeoCountries.First(g => g.Id == address.CountryId);
            address.CityDistrictId = (Request.ContainsKey("CityDistrictId")) ? long.Parse(Request["CityDistrictId"]) : default(long?);
            //address.CityDistrict = (Request.ContainsKey("CityDistrictId")) ? db.Cityd
            #endregion

            return 0;
        }
    }
}
