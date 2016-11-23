﻿using Eastwing.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RGR.Core.Common;
using RGR.Core.Common.Enums;
using RGR.Core.Models;
using RGR.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Controllers
{
    public class SearchController : Controller
    {
        private rgrContext db;
        private CityPassport city;
        public SearchController(rgrContext context, IServiceProvider serviceProvider)
        {
            db = context;
            var Utils = serviceProvider.GetService(typeof(SearchUtils));
            city = new CityPassport(db);
        }
        
        //Поиск
        public async Task<IActionResult> Search()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //HtmlString s;

            EstateTypes EstateType;
            byte estateType;
            if (!byte.TryParse(Request.Query["objType"], out estateType) && estateType < 6)
                throw new ArgumentException("Некорректный тип недвижимости!");

            EstateType = (EstateTypes)estateType;

            ViewData["Type"] = EstateType;
            ViewData["Result"] = await GetObjects(EstateType);
            //ViewData["Context"] = db;

            watch.Stop();
            ViewData["Timer"] = new HtmlString($"<br/><b>Время поиска:</b> {watch.Elapsed.TotalSeconds:0.00} с <br/>");

            return View();
        }

        //Сохранение запроса
        [Authorize]
        public async Task<IActionResult> SaveRequest(SaveRequestModel model)
        {
            string TODO = "Тестовая версия!!";
            var requests = await db.SearchRequests.ToArrayAsync();
            var uri = Request.Query["query"].ToString();

            if (requests.Where(r => r.UserId == SessionUtils.GetUserId(HttpContext.Session)).Any(r => r.SearchUrl == uri) == false)
            {
                int count = requests.Count();
                var request = new SearchRequests()
                {
                    UserId = SessionUtils.GetUserId(HttpContext.Session),
                    Title = $"Запрос {count}",
                    SearchUrl = uri,
                    TimesUsed = 0,
                    DateCreated = DateTime.UtcNow
                };

                db.SearchRequests.Add(request);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");

            //model.Query = (Request.Query.ContainsKey("query")) ? Request.Query["query"].ToString() : "";
            //ViewData["Query"] = model.Query;
            //if (ModelState.IsValid)
            //{
            //    var request = new SearchRequests()
            //    {
            //        UserId = SessionUtils.GetUserId(HttpContext.Session),
            //        Title = model.Caption,
            //        SearchUrl = model.Query,
            //        TimesUsed = 0,
            //        DateCreated = DateTime.UtcNow
            //    };
            //    if (request.UserId < 1)
            //        ModelState.AddModelError("", "Ошибка сохранения запроса: некорректный индекс пользователя!");
            //    else
            //    {
            //        db.SearchRequests.Add(request);
            //        await db.SaveChangesAsync();
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //return View(model);
        }

        public async Task<IActionResult> Info()
        {
            if (!Request.Query.ContainsKey("id"))
                throw new ArgumentException("Необъодимо указать индекс объекта!");

            long id;
            if (!long.TryParse(Request.Query["id"], out id))
                throw new ArgumentException("Введён некорректный индекс объекта!");

            EstateObjects obj = db.EstateObjects.SingleOrDefault(e => e.Id == id);

            if (obj == null)
            {
                ViewData["Data"] = $"Объект с индексом {id:0000000} не найден";
                return View();
            }

            ViewData["Data"] = JsonConvert.SerializeObject(await FullPassport.GetAsync(db, obj));

            return View();
        }

        //Поиск недвижимости и возвращение результата
        private async Task<string> GetObjects(EstateTypes EstateType)
        {

            //Получение основных таблиц
            //var main = await db.ObjectMainProperties.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == (short)EstateType).ToListAsync();
            //var addt = await db.ObjectAdditionalProperties.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == (short)EstateType).ToListAsync();
            //var rtng = await db.ObjectRatingProperties.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == (short)EstateType).ToListAsync();
            //var addr = await db.Addresses.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == (short)EstateType).ToListAsync();
            var strt = await db.GeoStreets.ToListAsync();
            //var city = await db.GeoCities.ToListAsync();
            //var vals = await db.DictionaryValues.ToListAsync();
            //var usrs = await db.Users.ToListAsync();
            var cmps = await db.Companies.ToListAsync();
            //var comm = await db.ObjectCommunications.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == (short)EstateType).ToListAsync();
            var mdia = await db.ObjectMedias.Where(m => db.EstateObjects.First(r => r.Id == m.ObjectId).ObjectType == (short)EstateType).ToListAsync();
            //var fils = await db.StoredFiles.ToListAsync();


            bool isCottage = false;      //Переключатель "коттедж/таунхаус" (для дома)
            bool pricePerMetter = false; //Переключатель "искать по цене за квадратный метр" (по умолчанию - по цене за объект)

            //Парсер для разбора составных параметров запроса на отдельные элементы
            var searchParser = new Parser()
            {
                Letters = "",
                Digits = "",
                Brackets = "",
                Separators = ","
            };

            //параметры поискового запроса, извлечённые из строки GET
            var SearchOptions = new Dictionary<string, object>();

            #region Инициализация поиска
            //Общая цена или цена за кв. м.
            if (Request.Query.ContainsKey("pricePerSqM"))
                pricePerMetter = (Request.Query["pricePerSqM"] == "on");

            //Проверяем, ищется ли коттедж
            if (Request.Query.ContainsKey("isCottage"))
                isCottage = (Request.Query["isCottage"] == "on");

            //double priceFrom = 0, priceTo = 0;
            //Попытка определить начальную цену
            if (Request.Query.ContainsKey("priceFrom"))
            {
                //priceFrom = double.Parse(Request.Query["priceFrom"]);
                double priceFrom;
                if (double.TryParse(Request.Query["priceFrom"], out priceFrom))
                    SearchOptions.Add("priceFrom", priceFrom);
            }

            //Попытка определить конечную цену
            if (Request.Query.ContainsKey("priceTo"))
            {
                //priceTo = double.Parse(Request.Query["priceTo"]);
                double priceTo;
                if (double.TryParse(Request.Query["priceTo"], out priceTo))
                    SearchOptions.Add("priceTo", priceTo);
            }

            //Попытка определить минимальную общую площадь
            if (Request.Query.ContainsKey("commonSquareFrom"))
            {
                double sqFrom;
                if (double.TryParse(Request.Query["commonSquareFrom"], out sqFrom))
                    SearchOptions.Add("sqFrom", sqFrom);
            }

            //Попытка определить максимальную общую площадь
            if (Request.Query.ContainsKey("commonSquareTo"))
            {
                double sqTo;
                if(double.TryParse(Request.Query["commonSquareTo"], out sqTo))
                    SearchOptions.Add("sqTo", sqTo);
            }

            //Попытка определить минимальную жилую площадь
            if (Request.Query.ContainsKey("livingSquareFrom"))
            {
                double sqLivFrom;
                if (double.TryParse(Request.Query["livingSquareFrom"], out sqLivFrom))
                    SearchOptions.Add("sqLivFrom", sqLivFrom);
            }

            //Попытка определить максимальную жилую площадь
            if (Request.Query.ContainsKey("livingSquareTo"))
            {
                double sqLivTo;
                if (double.TryParse(Request.Query["livingSquareTo"], out sqLivTo))
                    SearchOptions.Add("sqLivTo", sqLivTo);
            }

            //Попытка определить минимальную площадь кухни
            if (Request.Query.ContainsKey("kitchenSquareFrom"))
            {
                double sqKitchenFrom;
                if (double.TryParse(Request.Query["livingSquareFrom"], out sqKitchenFrom))
                    SearchOptions.Add("sqKitchenFrom", sqKitchenFrom);
            }

            //Попытка определить максимальную площадь кухни
            if (Request.Query.ContainsKey("kitchenSquareTo"))
            {
                double sqKitchenTo;
                if(double.TryParse(Request.Query["livingSquareTo"], out sqKitchenTo))
                    SearchOptions.Add("sqKitchenTo", sqKitchenTo);
            }

            //Минимальный этаж
            if (Request.Query.ContainsKey("minFloor"))
            {
                byte minFloor;
                if (byte.TryParse(Request.Query["minFloor"], out minFloor))
                        SearchOptions.Add("minFloor", minFloor);
            }

            //Максимальный этаж
            if (Request.Query.ContainsKey("maxFloor"))
            {
                byte maxFloor;
                if (byte.TryParse(Request.Query["maxFloor"], out maxFloor))
                    SearchOptions.Add("maxFloor", maxFloor);
            }

            //Минимум этажей в здании
            if (Request.Query.ContainsKey("minHouseFloors"))
            {
                byte minHouseFloors;
                if (byte.TryParse(Request.Query["minHouseFloors"], out minHouseFloors))
                    SearchOptions.Add("minHouseFloors", minHouseFloors);
            }

            //Максимум этажей в здании
            if (Request.Query.ContainsKey("maxHouseFloors"))
            {
                byte maxHouseFloors;
                if (byte.TryParse(Request.Query["maxHouseFloors"], out maxHouseFloors))
                    SearchOptions.Add("maxHouseFloors", maxHouseFloors);
            }

            //Город
            if (Request.Query.ContainsKey("city"))
            {
                long CityId;
                if (long.TryParse(Request.Query["city"], out CityId))
                    SearchOptions.Add("CityId", CityId);
            }

            //Район
            if (Request.Query.ContainsKey("district"))
            {
                long DistrictId;
                if (long.TryParse(Request.Query["district"], out DistrictId))
                    SearchOptions.Add("DistrictId", DistrictId);
            }

            //Жилмассив
            if (Request.Query.ContainsKey("area"))
            {
                long AreaId;
                if (long.TryParse(Request.Query["area"], out AreaId))
                    SearchOptions.Add("AreaId", AreaId);
            }

            //Улицы
            if (Request.Query.ContainsKey("street"))
            {
                var streets = searchParser.Parse(Request.Query["street"]).Where(s => s.Category != Category.Space && s.Category != Category.Separator);
                if (streets.Count() > 0)
                    SearchOptions.Add("Streets", streets.Select(s => s.Lexeme).ToArray());
            }

            //Агенства
            if (Request.Query.ContainsKey("agency"))
            {
                var agencies = searchParser.Parse(Request.Query["agency"]).Where(s => s.Category != Category.Space && s.Category != Category.Separator);
                if (agencies.Count() > 0)
                    SearchOptions.Add("Agencies", agencies.Select(s => s.Lexeme).ToArray());
            }

            //Агенты
            if (Request.Query.ContainsKey("agent"))
            {
                var agents = searchParser.Parse(Request.Query["agent"]).Where(s => s.Category != Category.Space && s.Category != Category.Separator);
                if (agents.Count() > 0)
                    SearchOptions.Add("Agents", agents.Select(s => s.Lexeme).ToArray());
            }

            //Стартовая дата для поиска
            if (Request.Query.ContainsKey("period"))
            {
                var startpoint = DateTime.MinValue;
                switch (Request.Query["period"]) //любое не перечисленное здесь значение - без ограничений
                {
                    case "day":
                        startpoint = DateTime.Today;
                        break;

                    case "week":
                        startpoint = DateTime.Today.AddDays(-7);
                        break;

                    case "month":
                        startpoint = DateTime.Today.AddMonths(-1);
                        break;

                    case "3month":
                        startpoint = DateTime.Today.AddMonths(-3);
                        break;
                }

                if (startpoint != DateTime.MinValue) //Если период не указан, нет смысла фильтровать
                    SearchOptions.Add("StartDate", startpoint);
            }
            #endregion

            //Фильтрация
            var relevant = db.EstateObjects
                .Where(estate => estate.ObjectType == (short)EstateType && estate.Status == (short)EstateStatuses.Active)
                .Include(e => e.ObjectMainProperties)
                .Include(e => e.ObjectAdditionalProperties)
                .Include(e => e.Addresses)
                .Include(e => e.ObjectCommunications)
                .Include(e => e.ObjectRatingProperties)
                .ToList();
            relevant = await Task.Run (() => relevant.Where(estate =>
            {
                var curMain = estate.ObjectMainProperties.FirstOrDefault();//(curMain);
                var curAddt = estate.ObjectAdditionalProperties.FirstOrDefault();//(addt.FirstOrDefault(m => m.ObjectId == estate.Id));
                var curAddr = estate.Addresses.FirstOrDefault();//(addr.FirstOrDefault(m => m.ObjectId == estate.Id));
                var curComm = estate.ObjectCommunications.FirstOrDefault();
                var curRtng = estate.ObjectRatingProperties.FirstOrDefault();

                if (curMain == null) return false;

                #region Фильтрация некорректных записей
                if (curMain.Price == null ||
                    curAddr == null ) return false;

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
                if (SearchOptions.ContainsKey("priceFrom"))
                {
                    if (!pricePerMetter)
                    {
                        if (curMain.Price < (double)SearchOptions["priceFrom"])
                            return false;
                    }
                    else
                    {
                        var price = curMain.Price;
                        var square = curMain.TotalArea;
                        if (price / square < (double)SearchOptions["priceFrom"])
                            return false;
                    }
                }

                //Фильтр по верхней цене
                if (SearchOptions.ContainsKey("priceTo"))
                {
                    if (!pricePerMetter)
                    {
                        if (curMain.Price > (double)SearchOptions["priceTo"])
                            return false;
                    }
                    else
                    {
                        var price = curMain.Price;
                        var square = curMain.TotalArea;
                        if (price / square > (double)SearchOptions["priceTo"])
                            return false;
                    }
                }

                //Фильтр по количеству комнат.
                if (Request.Query.ContainsKey("room1") || Request.Query.ContainsKey("room2") ||
                    Request.Query.ContainsKey("room3") || Request.Query.ContainsKey("room4") ||
                    Request.Query.ContainsKey("room5") || Request.Query.ContainsKey("room6"))
                { 
                    if (!(
                    (Request.Query["room1"] == "on" && curAddt.RoomsCount == 1) ||
                    (Request.Query["room2"] == "on" && curAddt.RoomsCount == 2) ||
                    (Request.Query["room3"] == "on" && curAddt.RoomsCount == 3) ||
                    (Request.Query["room4"] == "on" && curAddt.RoomsCount >= 4 && !isCottage) ||
                    (Request.Query["room4"] == "on" && curAddt.RoomsCount == 4 && isCottage) ||
                    (Request.Query["room5"] == "on" && curAddt.RoomsCount == 5) ||
                    (Request.Query["room6"] == "on" && curAddt.RoomsCount >= 6)
                    )) return false;
                }

                //Фильтр по типу комнат
                if (Request.Query.ContainsKey("roomSep") || //Раздельные
                    Request.Query.ContainsKey("roomAdj") || //Смежные
                    Request.Query.ContainsKey("roomBoth") || //Смежно-раздельные
                    Request.Query.ContainsKey("roomIkar") || //"Икарус"
                    Request.Query.ContainsKey("roomFree"))  //Свободная планировка
                {
                    if (!(
                    (Request.Query["roomSep"] == "on"  && curAddt.RoomPlanning == 12) ||
                    (Request.Query["roomAdj"] == "on"  && curAddt.RoomPlanning == 13) ||
                    (Request.Query["roomBoth"] == "on" && curAddt.RoomPlanning == 14) ||
                    (Request.Query["roomIkar"] == "on" && curAddt.RoomPlanning == 15) ||
                    (Request.Query["roomFree"] == "on" && curAddt.RoomPlanning == 16)
                    )) return false;
                }
                #endregion

                #region Обычный поиск (участок)
                //TODO "категория учатска"

                //Назначение
                if (Request.Query.ContainsKey("lndAppPers") || //Индивидуальное жилищное строительство
                    Request.Query.ContainsKey("lndAppDach") || //Дачное строительство
                    Request.Query.ContainsKey("lndAppLPH"))  //ЛПХ
                {
                    if (!(
                    (Request.Query["lndAppPers"] == "on" && curMain.LandAssignment.Contains("307")) ||
                    (Request.Query["lndAppDach"] == "on" && curMain.LandAssignment.Contains("309")) ||
                    (Request.Query["lndAppLPH" ] == "on" && curMain.LandAssignment.Contains("247"))
                    )) return false;
                }

                //TODO: особенности расположения
                #endregion

                #region Обычный поиск (гараж)
                //TODO: гараж/машиноместо
                #endregion

                #region обычный поиск (офисная)
                //Назначение
                if (Request.Query.ContainsKey("bldAppShop") || //Магазин
                    Request.Query.ContainsKey("bldAppOffice") || //Офис
                    Request.Query.ContainsKey("bldAppProduct") || //Производство
                    Request.Query.ContainsKey("bldAppStorage") || //Склад
                    Request.Query.ContainsKey("bldAppSalePt") || //Торговая точка
                    Request.Query.ContainsKey("bldAppCafe") || //Кафе, ресторан
                    Request.Query.ContainsKey("bldAppService") || //Сервис
                    Request.Query.ContainsKey("bldAppHotel") || //Гостиница
                    Request.Query.ContainsKey("bldAppFree")) //Свободное
                {
                    if (!(
                    (Request.Query["bldAppShop"] == "on" && curMain.ObjectAssignment.Contains("75")) ||
                    (Request.Query["bldAppOffice"] == "on" && curMain.ObjectAssignment.Contains("76")) ||
                    (Request.Query["bldAppProduct"] == "on" && curMain.ObjectAssignment.Contains("77")) ||
                    (Request.Query["bldAppStorage"] == "on" && curMain.ObjectAssignment.Contains("78")) ||
                    (Request.Query["bldAppSalePt"] == "on" && curMain.ObjectAssignment.Contains("79")) ||
                    (Request.Query["bldAppCafe"] == "on" && curMain.ObjectAssignment.Contains("377")) ||
                    (Request.Query["bldAppService"] == "on" && curMain.ObjectAssignment.Contains("378")) ||
                    (Request.Query["bldAppHotel"] == "on" && curMain.ObjectAssignment.Contains("379")) ||
                    (Request.Query["bldAppFree"] == "on" && curMain.ObjectAssignment.Contains("385"))
                    )) return false;
                }
                #endregion

                #region Удобства
                //Водоснабжение
                if (Request.Query.ContainsKey("wtrHotCenter") || //Горячая централизованно
                    Request.Query.ContainsKey("wtrHotAuton") || //Горячая автономно
                    Request.Query.ContainsKey("wtrColdCenter") || //Холодная централизованно
                    Request.Query.ContainsKey("wtrColdWell") || //Холодная - колодец, скважина
                    Request.Query.ContainsKey("wtrSummer") || //Летний водопровод
                    Request.Query.ContainsKey("wtrNone")) //Нету
                {
                    if (!(
                    (Request.Query["wtrHotCenter"] == "on" && curComm.Water.Contains("315")) ||
                    (Request.Query["wtrHotAuton"] == "on" && curComm.Water.Contains("206")) ||
                    (Request.Query["wtrColdCenter"] == "on" && curComm.Water.Contains("318")) ||
                    (Request.Query["wtrColdWell"] == "on" && curComm.Water.Contains("316")) ||
                    (Request.Query["wtrSummer"] == "on" && curComm.Water.Contains("372")) ||
                    (Request.Query["wtrNone"] == "on" && curComm.Water.Contains("205"))
                    )) return false;
                }

                //Электричество
                if (Request.Query.ContainsKey("elSupplied") || //Подведено
                    Request.Query.ContainsKey("elConnected") || //Подключение
                    Request.Query.ContainsKey("elPossible")) //Возможно подведение
                {
                    if(!( 
                    (Request.Query["elSupplied"] == "on" && curComm.Electricy.Contains("167")) ||
                    (Request.Query["elConnected"] == "on" && curComm.Electricy.Contains("168")) ||
                    (Request.Query["elPossible"] == "on" && curComm.Electricy.Contains("169"))
                    )) return false;
                }

                //Отопление
                if (Request.Query.ContainsKey("heatCentral") || //Центральное
                    Request.Query.ContainsKey("heatFuel") || //Дрова, уголь, жидкое
                    Request.Query.ContainsKey("heatGas") || //Газ
                    Request.Query.ContainsKey("heatElectr") || //Электричество
                    Request.Query.ContainsKey("heatNone")) //Нетъ
                {
                    if (!(
                    (Request.Query["heatCentral"] == "on" && curComm.Heating.Contains("306")) ||
                    (Request.Query["heatFuel"] == "on" && curComm.Heating.Contains("209")) ||
                    (Request.Query["heatGas"] == "on" && curComm.Heating.Contains("208")) ||
                    (Request.Query["heatElectr"] == "on" && curComm.Heating.Contains("304")) ||
                    (Request.Query["heatNone"] == "on" && curComm.Heating.Contains("305"))
                    )) return false;
                }

                //Канализация
                if (Request.Query.ContainsKey("sewAuto") || //Автономная
                    Request.Query.ContainsKey("sewCent") || //Централизованная
                    Request.Query.ContainsKey("sewSham") || //Шамбо
                    Request.Query.ContainsKey("sewNone")) //Нетъ
                {
                    if (!(
                    (Request.Query["sewAuto"] == "on" && curComm.Sewer == 207) ||
                    (Request.Query["sewCent"] == "on" && curComm.Sewer == 313) ||
                    (Request.Query["sewSham"] == "on" && curComm.Sewer == 314) ||
                    (Request.Query["sewNone"] == "on" && curComm.Sewer == 312)
                    )) return false;
                }
                #endregion

                #region Фильтры площади
                //Фильтр по общей площади: минимальная
                if (SearchOptions.ContainsKey("sqFrom"))
                {
                    if (curMain.TotalArea < (double)SearchOptions["sqFrom"])
                        return false;
                }

                //Фильтр по общей площади: максимальная
                if (SearchOptions.ContainsKey("sqTo"))
                {
                    if (curMain.TotalArea > (double)SearchOptions["sqTo"])
                        return false;
                }

                //Фильтр по жилой площади: минимальная
                if (SearchOptions.ContainsKey("sqLivFrom"))
                {
                    if (curMain.ActualUsableFloorArea < (double)SearchOptions["sqLivFrom"])
                        return false;
                }

                //Фильтр по жилой площади: максимальная
                if (SearchOptions.ContainsKey("sqLivTo"))
                {
                    if (curMain.ActualUsableFloorArea > (double)SearchOptions["sqLivTo"])
                        return false;
                }

                //Фильтр площади кухни: минимальная
                if (SearchOptions.ContainsKey("sqKitchenFrom"))
                {
                    if (curMain.KitchenFloorArea < (double)SearchOptions["sqKitchenFrom"])
                        return false;
                }

                //Фильтр по площади кухни: максимальная
                if (SearchOptions.ContainsKey("sqKitchenTo"))
                {
                    if (curMain.KitchenFloorArea > (double)SearchOptions["sqKitchenTo"])
                        return false;
                }
                #endregion

                #region Фильтры этажности
                //Фильтр по минимальному желаемому этажу
                if (SearchOptions.ContainsKey("minFloor"))
                {
                    if (curMain.FloorNumber < (byte)SearchOptions["minFloor"])
                        return false;
                }

                //Фильтр по максимальному желаемому этажу
                if (SearchOptions.ContainsKey("maxFloor"))
                {
                    if (curMain.FloorNumber > (byte)SearchOptions["maxFloor"])
                        return false;
                }

                //Первый и/или последний (не) предлагать!!!
                if (Request.Query.ContainsKey("noFirstFloor") || Request.Query.ContainsKey("noLastFloor"))
                {
                    if (!(
                    (Request.Query.ContainsKey("noFirstFloor") && curMain.FloorNumber > 1) ||
                    (Request.Query.ContainsKey("noLastFloor") && curMain.FloorNumber < //Этаж текущей квартиры меньше максимального числа этажей в здании
                        curMain.TotalFloors) 
                    )) return false;
                }

                //Этажей в доме: минимум
                if (SearchOptions.ContainsKey("minHouseFloors"))
                {
                    if (curMain.TotalFloors < (byte)SearchOptions["minHouseFloors"])
                        return false;
                }

                //Этажей в доме: максимум
                if (SearchOptions.ContainsKey("maxHouseFloors"))
                {
                    if (curMain.TotalFloors > (byte)SearchOptions["maxHouseFloors"])
                        return false;
                }
                #endregion

                #region Фильтры: санузел, балкон/лоджия, тип дома, материал постройки, состояние
                //Санузел
                if (Request.Query.ContainsKey("wc"))
                {
                    switch (Request.Query["wc"])
                    {
                        case "sep": //раздельный
                            string sep = curRtng.Wc;
                            if (sep == null)
                                return false;

                            if (!sep.Contains("226"))
                                return false;
                            break;

                        case "adj": //смежный
                            string adj = curRtng.Wc;
                            if (adj == null)
                                return false;

                            if (!adj.Contains("227"))
                                return false;
                            break;
                    }
                }

                //Балкон/лоджия
                if (Request.Query.ContainsKey("blPresent"))
                {
                    if (!(curAddt.BalconiesCount > 0 ||
                    curAddt.LoggiasCount > 0))
                        return false;
                    
                                
                }

                //Тип дома
                if (Request.Query.ContainsKey("houseType") )
                    //|| //Жильё низкого качества, барак
                    //Request.Query.ContainsKey("bldDorm") || //Малосемейка, общежитие
                    //Request.Query.ContainsKey("bldStal") || //Сталинка
                    //Request.Query.ContainsKey("bldHrush") || //Хрущ
                    //Request.Query.ContainsKey("bldBrovi") || //Брежневки (улучшенной планировки)
                    //Request.Query.ContainsKey("bldNew") || //Новая планировка
                    //Request.Query.ContainsKey("bldFree")) //Индивидуальная планировка
                {
                    if (!(
                        (Request.Query["houseType"] == "bldBarak" && curMain.HouseType == 138) ||
                        (Request.Query["houseType"] == "bldDorm" && curMain.HouseType == 143) ||
                        (Request.Query["houseType"] == "bldStal" && curMain.HouseType == 144) ||
                        (Request.Query["houseType"] == "bldHrush" && curMain.HouseType == 146) ||
                        (Request.Query["houseType"] == "bldBrovi" && curMain.HouseType == 145) || // Будем считать, что улучшенки
                        (Request.Query["houseType"] == "bldBrovi" && curMain.HouseType == 137) || // и брежневки - одно и то же
                        (Request.Query["houseType"] == "bldNew" && curMain.HouseType == 142) ||
                        (Request.Query["houseType"] == "bldFree" && curMain.HouseType == 139)
                     )) return false;
                }

                //Материал постройки
                if (Request.Query.ContainsKey("matWood") || //Дерево
                    Request.Query.ContainsKey("matBrick") || //Кирпич
                    Request.Query.ContainsKey("matPanel") || //Панельный
                    Request.Query.ContainsKey("matMono") || //Монолит
                    Request.Query.ContainsKey("matOther"))  //Другой
                {
                    if (!(
                    (Request.Query.ContainsKey("matWood") && curMain.BuildingMaterial.Contains("61")) ||
                        (Request.Query.ContainsKey("matBrick") && curMain.BuildingMaterial.Contains("62")) ||
                        (Request.Query.ContainsKey("matBrick") && curMain.BuildingMaterial.Contains("66")) || //Монолитно-кирпичный
                        (Request.Query.ContainsKey("matPanel") && curMain.BuildingMaterial.Contains("68")) ||
                        (Request.Query.ContainsKey("matMono") && curMain.BuildingMaterial.Contains("65")) ||
                        (Request.Query.ContainsKey("matMono") && curMain.BuildingMaterial.Contains("67")) || //В базе два значения соответствуют монолиту. Все вопросы туда.
                        (Request.Query.ContainsKey("matMono") && curMain.BuildingMaterial.Contains("66")) || //Монолитно-кирпичный
                        (Request.Query.ContainsKey("matOther") && curMain.BuildingMaterial.Contains("63")) || //МЕТАЛЛ
                        (Request.Query.ContainsKey("matOther") && curMain.BuildingMaterial.Contains("64")) || //Бетонные блоки
                        (Request.Query.ContainsKey("matOther") && curMain.BuildingMaterial.Contains("69")) || //Пенобетон
                        (Request.Query.ContainsKey("matOther") && curMain.BuildingMaterial.Contains("70"))    //Туфоблок
                    )) return false;
                }

                if (Request.Query.ContainsKey("stAfterBuilders") || //После строителей
                    Request.Query.ContainsKey("stCapRepair") || //Требуется капитальный ремонт
                    Request.Query.ContainsKey("stCosRepair") || //Требуется косметический ремонт
                    Request.Query.ContainsKey("stPassably") || //Удовлетворительное
                    Request.Query.ContainsKey("stGood") || //Хорошее
                    Request.Query.ContainsKey("stGreat") || //Отличное
                    Request.Query.ContainsKey("stEuro")) //"Евроремонт"
                {
                    if (!(
                        (Request.Query["stAfterBuilders"] == "on" && curRtng.CommonState == 85) ||
                        (Request.Query["stCapRepair"]     == "on" && curRtng.CommonState == 86) ||
                        (Request.Query["stCapRepair"]     == "on" && curRtng.CommonState == 87) || //Частичный ремонт
                        (Request.Query["stCosRepair"]     == "on" && curRtng.CommonState == 88) ||
                        (Request.Query["stPassably"]      == "on" && curRtng.CommonState == 89) ||
                        (Request.Query["stPassably"]      == "on" && curRtng.CommonState == 90) || //Нормальное
                        (Request.Query["stGood"]          == "on" && curRtng.CommonState == 91) ||
                        (Request.Query["stGreat"]         == "on" && curRtng.CommonState == 92) ||
                        (Request.Query["stEuro"]          == "on" && curRtng.CommonState == 93)
                    )) return false;
                }
                #endregion

                #region Фильтры по адресу
                //Населённый пункт (сити, ну)
                if (SearchOptions.ContainsKey("CityId"))
                {
                    if (curAddr.CityId != (long)SearchOptions["CityId"])
                        return false; 
                }

                //Район
                if (SearchOptions.ContainsKey("DistrictId"))
                {
                    if (curAddr.CityDistrictId != (long)SearchOptions["DistrictId"])
                        return false;
                }


                //Жилмассив
                if (SearchOptions.ContainsKey("AreaId"))
                {
                    if (curAddr.DistrictResidentialAreaId != (long)SearchOptions["AreaId"])
                        return false;
                }

                //Улица
                if (SearchOptions.ContainsKey("Streets"))
                {
                    long? streetId = curAddr.StreetId;
                    var geoStreet = strt.FirstOrDefault(s => s.Id == streetId);
                    if (geoStreet == null)
                        return false;

                    bool streetPresent = geoStreet.Name.ContainsOne((string[])SearchOptions["Streets"]);

                    if (!streetPresent)
                        return false;
                }
                #endregion

                #region Фильтры: Риелтор, период, наличие фото
                //Компания
                if (SearchOptions.ContainsKey("Agencies"))
                {
                    var user = estate.User;//usrs.FirstOrDefault(u => u.Id == estate.UserId);
                    if (user == null)
                        return false;

                    var comp = cmps.FirstOrDefault(c => c.Id == user.CompanyId);
                    if (comp == null)
                        return false;

                    bool companyPresent = comp.Name.ContainsOne((string[])SearchOptions["Agencies"]);

                    if (!companyPresent)
                        return false;
                }

                //Агент
                if (SearchOptions.ContainsKey("Agents"))
                {
                    var user = estate.User;//usrs.FirstOrDefault(u => u.Id == estate.UserId);
                    if (user == null)
                        return false;

                    string fio = $"{user.LastName} {user.SurName} {user.FirstName}";
                    bool companyPresent = fio.ContainsOne((string[])SearchOptions["Agencies"]);

                    if (!companyPresent)
                        return false;
                }

                //Период поиска
                if (SearchOptions.ContainsKey("StartDate"))
                {

                    if (estate.DateModified != null) //Если данные обновлялись после добавления объекта в БД
                        if (estate.DateModified < (DateTime)SearchOptions["StartDate"])
                            return false;
                    else
                        if (estate.DateCreated < (DateTime)SearchOptions["StartDate"])
                            return false;
                }

                //Наличие фото
                if (!string.IsNullOrEmpty(Request.Query["withPhotoOnly"]))
                {
                    if (Request.Query["withPhotoOnly"] == "on")
                    {
                        //Наличие хотя бы одного медиа (которые все фото)
                        if (!mdia.Any(m => m.ObjectId == estate.Id))
                            return false;
                    }
                }
                #endregion

                return true;
            }).ToList());

            if (EstateType == EstateTypes.Unset)
                EstateType = (EstateTypes)relevant.First().ObjectType;

            //Инициализация и генерирование последовательности паспортов
            var result = new SearchUtils.PassportConverter(db).GetShortPassports(relevant);

            db.Dispose();
            relevant.Clear();
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public ActionResult GetCityPassport(long CityId)
        {
            return Json(city.GetPassport(CityId));
        }
    }
}
