using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RGR.API.Common.Enums;
using RGR.API.DTO;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RGR.API.Common;

namespace RGR.API.Controllers
{
    public class SearchController : Controller
    {
        private rgrContext db;

        public SearchController(rgrContext context)
        {
            db = context;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Search([FromBody] SearchRequestDTO Request)
        {
            //var Request = JsonConvert.DeserializeObject<SearchRequestDTO>(RequestBody);

            var strt = await db.GeoStreets.ToListAsync();
            var cmps = await db.Companies.ToListAsync();
            //var mdia = await db.ObjectMedias.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == Request.EstateType).ToListAsync();

            var relevant = db.EstateObjects
                .Where(e => e.ObjectType == Request.EstateType && e.Status == (short)EstateStatuses.Active)
                .Include(e => e.ObjectMainProperties)
                .Include(e => e.ObjectAdditionalProperties)
                .Include(e => e.Addresses)
                .Include(e => e.ObjectCommunications)
                .Include(e => e.ObjectRatingProperties)
                .Include(e => e.ObjectMedias)
                .AsEnumerable();

            relevant = await Task.Run(() => relevant.Where(estate =>
            {
                var curMain = estate.ObjectMainProperties.FirstOrDefault();
                var curAddt = estate.ObjectAdditionalProperties.FirstOrDefault();
                var curAddr = estate.Addresses.FirstOrDefault();
                var curComm = estate.ObjectCommunications.FirstOrDefault();
                var curRtng = estate.ObjectRatingProperties.FirstOrDefault();

                #region Фильтрация некорректных записей
                if (curMain.Price == null ||
                    curAddr == null) return false;

                switch (estate.ObjectType)
                {
                    case (short)EstateTypes.Flat:
                    case (short)EstateTypes.Room:
                        if (string.IsNullOrEmpty(curAddr.Flat) ||
                        string.IsNullOrEmpty(curAddr.House) ||
                        curAddr.Flat == "0" ||
                        curAddr.House == "0" ||
                        curAddt.RoomsCount == null)
                            return false;
                        break;
                }
                #endregion

                #region Обычный поиск (общее, квартира, комната)
                //Инициализация фильтра по цене
                //Фильтр по нижней цене
                if (Request.PriceFrom != null)
                {
                    if (!Request.PricePerMetter)
                    {
                        if (curMain.Price < Request.PriceFrom)
                            return false;
                    }
                    else
                    {
                        var price = curMain.Price;
                        var square = curMain.TotalArea;
                        if (price / square < Request.PriceFrom)
                            return false;
                    }
                }

                //Фильтр по верхней цене
                if (Request.PriceTo != null)
                {
                    if (!Request.PricePerMetter)
                    {
                        if (curMain.Price > Request.PriceTo)
                            return false;
                    }
                    else
                    {
                        var price = curMain.Price;
                        var square = curMain.TotalArea;
                        if (price / square > Request.PriceTo)
                            return false;
                    }
                }

                //Фильтр по количеству комнат.
                if (Request.Room1 != null || Request.Room2 != null ||
                    Request.Room3 != null || Request.Room4 != null ||
                    Request.Room5 != null || Request.Room6 != null)
                {
                    if (!(
                    (Request.Room1 == true && curAddt.RoomsCount == 1) ||
                    (Request.Room2 == true && curAddt.RoomsCount == 2) ||
                    (Request.Room3 == true && curAddt.RoomsCount == 3) ||
                    (Request.Room4 == true && curAddt.RoomsCount >= 4 && !Request.IsCottage) ||
                    (Request.Room4 == true && curAddt.RoomsCount == 4 && Request.IsCottage) ||
                    (Request.Room5 == true && curAddt.RoomsCount == 5) ||
                    (Request.Room6 == true && curAddt.RoomsCount >= 6)
                    )) return false;
                }

                //Фильтр по типу комнат
                if (Request.RoomPlanning != null)
                {
                    if (curAddt.RoomPlanning != Request.RoomPlanning) return false;
                }
                #endregion

                #region Обычный поиск (участок)
                //TODO "категория учатска"

                //Назначение
                if (Request.LandPurposePers != null || //Индивидуальное жилищное строительство
                    Request.LandPurposeDach != null || //Дачное строительство
                    Request.LandPurposeLPH  != null ) //ЛПХ
                {
                    if (!(
                    (Request.LandPurposePers == true && curMain.LandAssignment.Contains("307")) ||
                    (Request.LandPurposeDach == true && curMain.LandAssignment.Contains("309")) ||
                    (Request.LandPurposeLPH  == true && curMain.LandAssignment.Contains("247"))
                    )) return false;
                }

                //TODO: особенности расположения
                #endregion

                #region Обычный поиск (гараж)
                //TODO: гараж/машиноместо
                #endregion

                #region обычный поиск (офисная)
                //Назначение
                if (Request.BuildingPurposeShop != null  || //Магазин
                    Request.BuildingPurposeOffice != null || //Офис
                    Request.BuildingPurposeProduct != null || //Производство
                    Request.BuildingPurposeStorage != null || //Склад
                    Request.BuildingPurposeSalePt != null || //Торговая точка
                    Request.BuildingPurposeCafe != null || //Кафе, ресторан
                    Request.BuildingPurposeService != null || //Сервис
                    Request.BuildingPurposeHotel != null || //Гостиница
                    Request.BuildingPurposeFree != null)   //Свободное
                {
                    if (!(
                    (Request.BuildingPurposeShop == true  && curMain.ObjectAssignment.Contains("75")) ||
                    (Request.BuildingPurposeOffice == true && curMain.ObjectAssignment.Contains("76")) ||
                    (Request.BuildingPurposeProduct == true && curMain.ObjectAssignment.Contains("77")) ||
                    (Request.BuildingPurposeStorage == true && curMain.ObjectAssignment.Contains("78")) ||
                    (Request.BuildingPurposeSalePt == true && curMain.ObjectAssignment.Contains("79")) ||
                    (Request.BuildingPurposeCafe == true && curMain.ObjectAssignment.Contains("377")) ||
                    (Request.BuildingPurposeService == true && curMain.ObjectAssignment.Contains("378")) ||
                    (Request.BuildingPurposeHotel == true && curMain.ObjectAssignment.Contains("379")) ||
                    (Request.BuildingPurposeFree == true && curMain.ObjectAssignment.Contains("385"))
                    )) return false;
                }
                #endregion

                #region Удобства
                //Водоснабжение
                if (Request.WaterColdCenter != null || Request.WaterHotAuton != null ||
                    Request.WaterHotCenter  != null || Request.WaterColdWell != null ||
                    Request.WaterSummer     != null || Request.WaterNone != null )
                {
                    if (!(
                    (Request.WaterHotCenter  == true && curComm.Water.Contains("315")) ||
                    (Request.WaterHotAuton   == true && curComm.Water.Contains("206")) ||
                    (Request.WaterColdCenter == true && curComm.Water.Contains("318")) ||
                    (Request.WaterColdWell   == true && curComm.Water.Contains("316")) ||
                    (Request.WaterSummer     == true && curComm.Water.Contains("372")) ||
                    (Request.WaterNone       == true && curComm.Water.Contains("205")) 
                    //string.IsNullOrEmpty(Request.Query["water"])
                    )) return false;
                }

                //Электричество
                if (Request.ElectricySupplied != null ||
                    Request.ElectricyConnected != null||
                    Request.ElectricyPossible != null)
                {
                    if (!(
                    (Request.ElectricySupplied == true && curComm.Electricy.Contains("167")) ||
                    (Request.ElectricyConnected == true && curComm.Electricy.Contains("168")) ||
                    (Request.ElectricyPossible == true && curComm.Electricy.Contains("169")) 
                     //string.IsNullOrEmpty(Request.Query["electro"])
                    )) return false;
                }

                //Отопление
                if (
                        Request.HeatCentral != null ||Request.HeatFuel != null ||
                        Request.HeatGas != null ||Request.HeatElectro != null || 
                        Request.HeatNone != null )
                {
                    if (!(
                    (Request.HeatCentral == true && curComm.Heating.Contains("306")) ||
                    (Request.HeatFuel    == true && curComm.Heating.Contains("209")) ||
                    (Request.HeatGas     == true && curComm.Heating.Contains("208")) ||
                    (Request.HeatElectro == true && curComm.Heating.Contains("304")) ||
                    (Request.HeatNone    == true && curComm.Heating.Contains("305"))
                    )) return false;
                }

                //Канализация
                if (Request.Sewer != null)
                {
                    if (Request.Sewer != curComm.Sewer)
                        return false;
                }
                #endregion

                #region Фильтры площади
                //Фильтр по общей площади: минимальная
                if (Request.AreaFrom != null)
                {
                    if (curMain.TotalArea < Request.AreaFrom)
                        return false;
                }

                //Фильтр по общей площади: максимальная
                if (Request.AreaTo != null)
                {
                    if (curMain.TotalArea > Request.AreaTo)
                        return false;
                }

                //Фильтр по жилой площади: минимальная
                if (Request.AreaLivingFrom != null)
                {
                    if (curMain.ActualUsableFloorArea < Request.AreaLivingFrom)
                        return false;
                }

                //Фильтр по жилой площади: максимальная
                if (Request.AreaLivingTo != null)
                {
                    if (curMain.ActualUsableFloorArea > Request.AreaLivingTo)
                        return false;
                }

                //Фильтр площади кухни: минимальная
                if (Request.AreaKitchenFrom != null)
                {
                    if (curMain.KitchenFloorArea < Request.AreaKitchenFrom)
                        return false;
                }

                //Фильтр по площади кухни: максимальная
                if (Request.AreaKitchenTo != null)
                {
                    if (curMain.KitchenFloorArea > Request.AreaKitchenTo)
                        return false;
                }
                #endregion

                #region Фильтры этажности
                //Фильтр по минимальному желаемому этажу
                if (Request.MinFloor != null)
                {
                    if (curMain.FloorNumber < Request.MinFloor)
                        return false;
                }

                //Фильтр по максимальному желаемому этажу
                if (Request.MaxFloor != null)
                {
                    if (curMain.FloorNumber > Request.MaxFloor)
                        return false;
                }

            //Первый и/или последний (не) предлагать!!!
            if (Request.NoFirstFloor != null && curMain.FloorNumber == 1)
                    return false;

            if (Request.NoLastFloor != null && curMain.FloorNumber == curMain.TotalFloors)
                    return false;

                //Этажей в доме: минимум
                if (Request.MinHouseFloors != null)
                {
                    if (curMain.TotalFloors < Request.MinHouseFloors)
                        return false;
                }

                //Этажей в доме: максимум
                if (Request.MaxHouseFloors != null)
                {
                    if (curMain.TotalFloors > Request.MaxHouseFloors)
                        return false;
                }
                #endregion

                #region Фильтры: санузел, балкон/лоджия, тип дома, материал постройки, состояние
                //Санузел
                if (Request.WCSeparated != null)
                {
                    switch (Request.WCSeparated) //простите меня.
                    {
                        case true: //раздельный
                            string sep = curRtng.Wc;
                            if (sep == null)
                                return false;

                            if (!sep.Contains("226"))
                                return false;
                            break;

                        case false: //смежный
                            string adj = curRtng.Wc;
                            if (adj == null)
                                return false;

                            if (!adj.Contains("227"))
                                return false;
                            break;
                    }
                }

                //Балконы
                if (Request.BalconiesCount != null)
                {
                    if (curAddt.BalconiesCount != Request.BalconiesCount)
                        return false;
                }

                //Лоджии
                if (Request.LoggiasCount != null)
                {
                    if (curAddt.LoggiasCount != Request.LoggiasCount)
                        return false;
                }

                //Тип дома
                if (Request.HouseType != null)
                {
                    if (curMain?.HouseType == null)
                        return false;

                    if (curMain.HouseType != Request.HouseType)
                        return false;
                }

                //Материал постройки
                if (Request.HouseMaterialWood  != null || Request.HouseMaterialBrick != null ||
                    Request.HouseMaterialPanel != null || Request.HouseMaterialMonolite != null ||
                    Request.HouseMaterialOther != null)
                {
                    if (curMain?.BuildingMaterial == null)
                        return false;

                    if (!(
                        (Request.HouseMaterialWood == true && curMain.BuildingMaterial.Contains("61")) ||
                        (Request.HouseMaterialBrick == true && curMain.BuildingMaterial.Contains("62")) ||
                        (Request.HouseMaterialBrick == true && curMain.BuildingMaterial.Contains("66")) || //Монолитно-кирпичный
                        (Request.HouseMaterialPanel == true && curMain.BuildingMaterial.Contains("68")) ||
                        (Request.HouseMaterialMonolite == true && curMain.BuildingMaterial.Contains("65")) ||
                        (Request.HouseMaterialMonolite == true && curMain.BuildingMaterial.Contains("67")) || //В базе два значения соответствуют монолиту. Все вопросы туда.
                        (Request.HouseMaterialMonolite == true && curMain.BuildingMaterial.Contains("66")) || //Монолитно-кирпичный
                        (Request.HouseMaterialOther == true && curMain.BuildingMaterial.Contains("63")) || //МЕТАЛЛ
                        (Request.HouseMaterialOther == true && curMain.BuildingMaterial.Contains("64")) || //Бетонные блоки
                        (Request.HouseMaterialOther == true && curMain.BuildingMaterial.Contains("69")) || //Пенобетон
                        (Request.HouseMaterialOther == true && curMain.BuildingMaterial.Contains("70"))  //Туфоблок
                        //(string.IsNullOrEmpty(Request.Query["houseMat"]))
                    )) return false;
                }

                if (Request.OobjectState != null)
                {
                    if (curRtng?.CommonState == null)
                        return false;

                    if (Request.OobjectState != curRtng.CommonState)
                        return false;
                }
                #endregion

                #region Фильтры по адресу
                //Населённый пункт (сити, ну)
               if (curAddr.CityId != Request.CityId)
                   return false;

                //Район
                if (Request.DistrictId != null)
                {
                    if (curAddr.CityDistrictId != Request.DistrictId)
                        return false;
                }


                //Жилмассив
                if (Request.AreaId != null)
                {
                    if (curAddr.DistrictResidentialAreaId != Request.AreaId)
                        return false;
                }

                //Улица
                if (!Request.Streets.IsNullOrEmpty())
                {
                    long? streetId = curAddr.StreetId;
                    var geoStreet = strt.FirstOrDefault(s => s.Id == streetId);
                    if (geoStreet == null)
                        return false;

                    bool streetPresent = Request.Streets
                                            .Split(',')
                                            .Any(s => geoStreet.Name.Contains(s));

                    if (!streetPresent)
                        return false;
                }
                #endregion

                #region Фильтры: Риелтор, период, наличие фото
                //Компания
                if (!Request.Agencies.IsNullOrEmpty())
                {
                    var user = estate.User;//usrs.FirstOrDefault(u => u.Id == estate.UserId);
                    if (user == null)
                        return false;

                    var comp = cmps.FirstOrDefault(c => c.Id == user.CompanyId);
                    if (comp == null)
                        return false;

                    bool companyPresent = Request.Agencies
                                            .Split(',')
                                            .Any(a => comp.Name.Contains(a));
                    if (!companyPresent)
                        return false;
                }

                //Агент
                if (!Request.Agents.IsNullOrEmpty())
                {
                    var user = estate.User;//usrs.FirstOrDefault(u => u.Id == estate.UserId);
                    if (user == null)
                        return false;

                    string fio = $"{user.LastName} {user.SurName} {user.FirstName}";

                    bool agentPresent = Request.Agents
                                            .Split(',')
                                            .Any(a => fio.Contains(a));

                    if (!agentPresent)
                        return false;
                }

                //Период поиска
                if (Request.StartDate != null)
                {

                    if (estate.DateModified != null) //Если данные обновлялись после добавления объекта в БД
                        if (estate.DateModified < Request.StartDate)
                            return false;
                        else
                        if (estate.DateCreated < Request.StartDate)
                            return false;
                }

                //Наличие фото
                if (Request.WithPhotoOnly == true)
                {
                    //Наличие хотя бы одного медиа (которые все фото)
                    if (!estate.ObjectMedias.Any())
                        return false;
                }
                #endregion

                return true;
            }));
            var result = await Task.Run(() => new PassportConverter(db).GetShortPassports(relevant));

            return new ContentResult() { Content = JsonConvert.SerializeObject(result) };
        }

        [HttpGet]
        [Route("object")]
        public async Task<IActionResult> GetPassport()
        {
            if (!Request.Query.ContainsKey("id"))
                return new StatusCodeResult(400);

            long id;
            if (!long.TryParse(Request.Query["id"], out id))
                return new StatusCodeResult(400);

            EstateObjects obj = db.EstateObjects.SingleOrDefault(e => e.Id == id);

            if (obj == null)
                return new StatusCodeResult(422);

            return new JsonResult(await FullPassport.GetAsync(db, obj));
        }
    }
}