﻿//using Danko.TextJobs;
using Eastwing.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RGR.Core.Controllers.Enums;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using RGR.Core.ViewModels;
using RGR.Core.Controllers.Account;
using RGR.Core.Controllers.Search;

namespace RGR.Core.Controllers
{
    public class SearchController : Controller
    {
        private rgrContext db;
        public SearchController(rgrContext context, IServiceProvider serviceProvider)
        {
            db = context;
            var Utils = serviceProvider.GetService(typeof(SearchUtils));
        }
        
        //Поиск
        public async Task<IActionResult> Search()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //HtmlString s;

            EstateTypes EstateType;
            switch (Request.Query["objType"])
            {
                #region Установка типа недвижимости
                case "0":
                    EstateType = EstateTypes.Flat;
                    break;

                case "1":
                    EstateType = EstateTypes.Room;
                    break;

                case "2":
                    EstateType = EstateTypes.House;
                    break;

                case "3":
                    EstateType = EstateTypes.Land;
                    break;

                case "4":
                    EstateType = EstateTypes.Garage;
                    break;

                case "5":
                    EstateType = EstateTypes.Office;
                    break;

                default:
                    EstateType = EstateTypes.Unset;
                    break;
                    #endregion
            }
            ViewData["Type"] = EstateType;
            ViewData["Result"] = await GetObjects(EstateType);
            ViewData["Context"] = db;

            watch.Stop();
            ViewData["Timer"] = new HtmlString($"<br/><b>Время поиска:</b> {watch.Elapsed.TotalSeconds:0.00} с <br/><b>БД:</b> {db.Database.GetDbConnection().ConnectionString}");

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

        #region Фильтрация объектов
        //Поиск недвижимости и возвращение результата
        private async Task<string> GetObjects(EstateTypes EstateType)
        {
            //Получение основных таблиц
            var main = await db.ObjectMainProperties.ToListAsync();
            var addt = await db.ObjectAdditionalProperties.ToListAsync();
            var rtng = await db.ObjectRatingProperties.ToListAsync();
            var addr = await db.Addresses.ToListAsync();
            var strt = await db.GeoStreets.ToListAsync();
            var city = await db.GeoCities.ToListAsync();
            var vals = await db.DictionaryValues.ToListAsync();
            var usrs = await db.Users.ToListAsync();
            var cmps = await db.Companies.ToListAsync();
            var comm = await db.ObjectCommunications.ToListAsync();
            var mdia = await db.ObjectMedias.ToListAsync();

            bool isCottage = false;      //Переключатель "коттедж/таунхаус" (для дома)
            bool pricePerMetter = false; //Переключатель "искать по цене за квадратный метр" (по умолчанию - по цене за объект)

            #region Инициализация поиска
            //Общая цена или цена за кв. м.
            if (!string.IsNullOrEmpty(Request.Query["pricePerSqM"]))
                pricePerMetter = (Request.Query["pricePerSqM"] == "on");

            //Проверяем, ищется ли коттедж
            if (Request.Query.ContainsKey("isCottage"))
                isCottage = (Request.Query["isCottage"] == "on");
            
            //Попытка определить начальную цену
            double priceFrom = 0;
            if (!string.IsNullOrEmpty(Request.Query["priceFrom"]))
                double.TryParse(Request.Query["priceFrom"], out priceFrom);

            //Попытка определить конечную цену
            double priceTo = 0;
            if (!string.IsNullOrEmpty(Request.Query["priceFrom"]))
                double.TryParse(Request.Query["priceTo"], out priceTo);

            //Попытка определить минимальную общую площадь
            double sqFrom = 0;
            if (!string.IsNullOrEmpty(Request.Query["commonSquareFrom"]))
                double.TryParse(Request.Query["commonSquareFrom"], out sqFrom);

            //Попытка определить максимальную общую площадь
            double sqTo = 0;
            if (!string.IsNullOrEmpty(Request.Query["commonSquareTo"]))
                double.TryParse(Request.Query["commonSquareTo"], out sqTo);

            //Попытка определить минимальную жилую площадь
            double sqLivFrom = 0;
            if (!string.IsNullOrEmpty(Request.Query["livingSquareFrom"]))
                double.TryParse(Request.Query["livingSquareFrom"], out sqLivFrom);

            //Попытка определить максимальную жилую площадь
            double sqLivTo = 0;
            if (!string.IsNullOrEmpty(Request.Query["livingSquareTo"]))
                double.TryParse(Request.Query["livingSquareTo"], out sqLivTo);

            //Попытка определить минимальную площадь кухни
            double sqKitchenFrom = 0;
            if (!string.IsNullOrEmpty(Request.Query["kitchenSquareFrom"]))
                double.TryParse(Request.Query["livingSquareFrom"], out sqKitchenFrom);

            //Попытка определить максимальную площадь кухни
            double sqKitchenTo = 0;
            if (!string.IsNullOrEmpty(Request.Query["kitchenSquareTo"]))
                double.TryParse(Request.Query["livingSquareTo"], out sqKitchenTo);

            //Минимальный этаж
            byte minFloor = 0;
            if (!string.IsNullOrEmpty(Request.Query["minFloor"]))
                byte.TryParse(Request.Query["minFloor"], out minFloor);

            //Максимальный этаж
            byte maxFloor = 0;
            if (!string.IsNullOrEmpty(Request.Query["maxFloor"]))
                byte.TryParse(Request.Query["maxFloor"], out maxFloor);
            #endregion

            //Минимум этажей в здании
            byte minHouseFloors = 0;
            if (!string.IsNullOrEmpty(Request.Query["minHouseFloors"]))
                byte.TryParse(Request.Query["minHouseFloors"], out minHouseFloors);

            //Максимум этажей в здании
            byte maxHouseFloors = 0;
            if (!string.IsNullOrEmpty(Request.Query["maxHouseFloors"]))
                byte.TryParse(Request.Query["maxHouseFloors"], out maxHouseFloors);

            //Город
            long CityId = -1;
            if (!string.IsNullOrEmpty(Request.Query["city"]))
                long.TryParse(Request.Query["city"], out CityId);

            //Фильтрация
            var relevant = await db.EstateObjects.Where(estate => estate.ObjectType == (short)EstateType).ToArrayAsync();
            relevant = relevant.Where(estate =>
            {
                #region Обычный поиск (общее)
                //Инициализация фильтра по цене
                //Фильтр по нижней цене
                if (priceFrom > 0)
                {
                    if (!pricePerMetter)
                    {
                        if (main.FirstOrDefault(m => m.ObjectId == estate.Id).Price < priceFrom)
                            return false;
                    }
                    else
                    {
                        var price = main.Single(x => x.ObjectId == estate.Id).Price;
                        var square = main.Single(x => x.ObjectId == estate.Id).TotalArea;
                        if (price / square < priceFrom)
                            return false;
                    }
                }

                //Фильтр по верхней цене
                if (priceTo > 0)
                {
                    if (!pricePerMetter)
                    {
                        if (main.FirstOrDefault(m => m.ObjectId == estate.Id).Price > priceTo)
                            return false;
                    }
                    else
                    {
                        var price = main.Single(x => x.ObjectId == estate.Id).Price;
                        var square = main.Single(x => x.ObjectId == estate.Id).TotalArea;
                        if (price / square > priceTo)
                            return false;
                    }
                }

                //Фильтр по количеству комнат.
                if (Request.Query.ContainsKey("room1") || Request.Query.ContainsKey("room2") ||
                    Request.Query.ContainsKey("room3") || Request.Query.ContainsKey("room4") ||
                    Request.Query.ContainsKey("room5") || Request.Query.ContainsKey("room6"))
                { 
                    if (!(
                    (Request.Query["room1"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount == 1) ||
                    (Request.Query["room2"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount == 2) ||
                    (Request.Query["room3"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount == 3) ||
                    (Request.Query["room4"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount >= 4 && !isCottage) ||
                    (Request.Query["room4"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount == 4 && isCottage) ||
                    (Request.Query["room5"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount == 5) ||
                    (Request.Query["room6"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomsCount >= 6)
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
                    (Request.Query["roomSep"] == "on"  && addt.First(a => a.ObjectId == estate.Id).RoomPlanning == 12) ||
                    (Request.Query["roomAdj"] == "on"  && addt.First(a => a.ObjectId == estate.Id).RoomPlanning == 13) ||
                    (Request.Query["roomBoth"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomPlanning == 14) ||
                    (Request.Query["roomIkar"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomPlanning == 15) ||
                    (Request.Query["roomFree"] == "on" && addt.First(a => a.ObjectId == estate.Id).RoomPlanning == 16)
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
                    (Request.Query["lndAppPers"] == "on" && main.First(m => m.ObjectId == estate.Id).LandAssignment.Contains("307")) ||
                    (Request.Query["lndAppDach"] == "on" && main.First(m => m.ObjectId == estate.Id).LandAssignment.Contains("309")) ||
                    (Request.Query["lndAppLPH" ] == "on" && main.First(m => m.ObjectId == estate.Id).LandAssignment.Contains("247"))
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
                    (Request.Query["bldAppShop"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("75")) ||
                    (Request.Query["bldAppOffice"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("76")) ||
                    (Request.Query["bldAppProduct"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("77")) ||
                    (Request.Query["bldAppStorage"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("78")) ||
                    (Request.Query["bldAppSalePt"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("79")) ||
                    (Request.Query["bldAppCafe"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("377")) ||
                    (Request.Query["bldAppService"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("378")) ||
                    (Request.Query["bldAppHotel"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("379")) ||
                    (Request.Query["bldAppFree"] == "on" && main.Single(m => m.ObjectId == estate.Id).ObjectAssignment.Contains("385"))
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
                    (Request.Query["wtrHotCenter"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Water.Contains("315")) ||
                    (Request.Query["wtrHotAuton"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Water.Contains("206")) ||
                    (Request.Query["wtrColdCenter"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Water.Contains("318")) ||
                    (Request.Query["wtrColdWell"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Water.Contains("316")) ||
                    (Request.Query["wtrSummer"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Water.Contains("372")) ||
                    (Request.Query["wtrNone"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Water.Contains("205"))
                    )) return false;
                }

                //Электричество
                if (Request.Query.ContainsKey("elSupplied") || //Подведено
                    Request.Query.ContainsKey("elConnected") || //Подключение
                    Request.Query.ContainsKey("elPossible")) //Возможно подведение
                {
                    if(!( 
                    (Request.Query["elSupplied"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Electricy.Contains("167")) ||
                    (Request.Query["elConnected"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Electricy.Contains("168")) ||
                    (Request.Query["elPossible"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Electricy.Contains("169"))
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
                    (Request.Query["heatCentral"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Heating.Contains("306")) ||
                    (Request.Query["heatFuel"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Heating.Contains("209")) ||
                    (Request.Query["heatGas"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Heating.Contains("208")) ||
                    (Request.Query["heatElectr"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Heating.Contains("304")) ||
                    (Request.Query["heatNone"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Heating.Contains("305"))
                    )) return false;
                }

                //Канализация
                if (Request.Query.ContainsKey("sewAuto") || //Автономная
                    Request.Query.ContainsKey("sewCent") || //Централизованная
                    Request.Query.ContainsKey("sewSham") || //Шамбо
                    Request.Query.ContainsKey("sewNone")) //Нетъ
                {
                    if (!(
                    (Request.Query["sewAuto"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Sewer == 207) ||
                    (Request.Query["sewCent"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Sewer == 313) ||
                    (Request.Query["sewSham"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Sewer == 314) ||
                    (Request.Query["sewNone"] == "on" && comm.Single(c => c.ObjectId == estate.Id).Sewer == 312)
                    )) return false;
                }
                #endregion

                #region Фильтры площади
                //Фильтр по общей площади: минимальная
                if (sqFrom > 0)
                {
                    if (main.FirstOrDefault(x => x.ObjectId == estate.Id).TotalArea < sqFrom)
                        return false;
                }

                //Фильтр по общей площади: максимальная
                if (sqTo > 0)
                {
                    if (main.FirstOrDefault(x => x.ObjectId == estate.Id).TotalArea > sqTo)
                        return false;
                }

                //Фильтр по жилой площади: минимальная
                if (sqLivFrom > 0)
                {
                    if (main.FirstOrDefault(x => x.ObjectId == estate.Id).ActualUsableFloorArea < sqLivFrom)
                        return false;
                }

                //Фильтр по жилой площади: максимальная
                if (!string.IsNullOrEmpty(Request.Query["livingSquareTo"]))
                {
                    if (sqLivTo > 0)
                    {
                        if (main.FirstOrDefault(x => x.ObjectId == estate.Id).ActualUsableFloorArea > sqLivTo)
                            return false;
                    }
                }

                //Фильтр площади кухни: минимальная
                if (!string.IsNullOrEmpty(Request.Query["kitchenSquareFrom"]))
                {
                    if (sqKitchenFrom > 0)
                    {
                        if (main.FirstOrDefault(x => x.ObjectId == estate.Id).KitchenFloorArea < sqKitchenFrom)
                            return false;
                    }
                }

                //Фильтр по площади кухни: максимальная
                if (!string.IsNullOrEmpty(Request.Query["kitchenSquareTo"]))
                {
                    if (sqKitchenTo > 0)
                    {
                        if (main.FirstOrDefault(x => x.ObjectId == estate.Id).KitchenFloorArea > sqKitchenTo)
                            return false;
                    }
                }
                #endregion

                #region Фильтры этажности
                //Фильтр по минимальному желаемому этажу
                if (minFloor > 0)
                {
                    if (main.Single(x => x.ObjectId == estate.Id).FloorNumber < minFloor)
                        return false;
                }

                //Фильтр по максимальному желаемому этажу
                if (maxFloor > 0)
                {
                    if (main.Single(x => x.ObjectId == estate.Id).FloorNumber > maxFloor)
                        return false;
                }

                //Первый и/или последний (не) предлагать!!!
                if (Request.Query.ContainsKey("noFirstFloor") || Request.Query.ContainsKey("noLastFloor"))
                {
                    if (!(
                    (Request.Query.ContainsKey("noFirstFloor") && main.Single(x => x.ObjectId == estate.Id).FloorNumber > 1) ||
                    (Request.Query.ContainsKey("noLastFloor") && main.Single(x => x.ObjectId == estate.Id).FloorNumber < //Этаж текущей квартиры меньше максимального числа этажей в здании
                        main.Single(x => x.ObjectId == estate.Id).TotalFloors) 
                    )) return false;
                }

                //Этажей в доме: минимум
                if (minHouseFloors > 0)
                {
                    if (main.Single(x => x.ObjectId == estate.Id).TotalFloors < minHouseFloors)
                        return false;
                }

                //Этажей в доме: максимум
                if (maxHouseFloors > 0)
                {
                    if (main.Single(x => x.ObjectId == estate.Id).TotalFloors > maxHouseFloors)
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
                            string sep = rtng.Single(r => r.ObjectId == estate.Id).Wc;
                            if (sep == null)
                                return false;

                            if (!sep.Contains("226"))
                                return false;
                            break;

                        case "adj": //смежный
                            string adj = rtng.Single(r => r.ObjectId == estate.Id).Wc;
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
                    if (!(addt.Single(a => a.ObjectId == estate.Id).BalconiesCount > 0 ||
                    addt.Single(a => a.ObjectId == estate.Id).LoggiasCount > 0))
                        return false;
                    
                                
                }

                //Тип дома
                if (Request.Query.ContainsKey("bldBarak") || //Жильё низкого качества, барак
                    Request.Query.ContainsKey("bldDorm") || //Малосемейка, общежитие
                    Request.Query.ContainsKey("bldStal") || //Сталинка
                    Request.Query.ContainsKey("bldHrush") || //Хрущ
                    Request.Query.ContainsKey("bldBrovi") || //Брежневки (улучшенной планировки)
                    Request.Query.ContainsKey("bldNew") || //Новая планировка
                    Request.Query.ContainsKey("bldFree")) //Индивидуальная планировка
                {
                    if (!(
                        (Request.Query.ContainsKey("bldBarak") && main.Single(m => m.ObjectId == estate.Id).HouseType == 138) ||
                        (Request.Query.ContainsKey("bldDorm") && main.Single(m => m.ObjectId == estate.Id).HouseType == 143) ||
                        (Request.Query.ContainsKey("bldStal") && main.Single(m => m.ObjectId == estate.Id).HouseType == 144) ||
                        (Request.Query.ContainsKey("bldHrush") && main.Single(m => m.ObjectId == estate.Id).HouseType == 146) ||
                        (Request.Query.ContainsKey("bldBrovi") && main.Single(m => m.ObjectId == estate.Id).HouseType == 145) || // Будем считать, что улучшенки
                        (Request.Query.ContainsKey("bldBrovi") && main.Single(m => m.ObjectId == estate.Id).HouseType == 137) || // и брежневки - одно и то же
                        (Request.Query.ContainsKey("bldNew") && main.Single(m => m.ObjectId == estate.Id).HouseType == 142) ||
                        (Request.Query.ContainsKey("bldFree") && main.Single(m => m.ObjectId == estate.Id).HouseType == 139)
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
                    (Request.Query.ContainsKey("matWood") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("61")) ||
                        (Request.Query.ContainsKey("matBrick") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("62")) ||
                        (Request.Query.ContainsKey("matBrick") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("66")) || //Монолитно-кирпичный
                        (Request.Query.ContainsKey("matPanel") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("68")) ||
                        (Request.Query.ContainsKey("matMono") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("65")) ||
                        (Request.Query.ContainsKey("matMono") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("67")) || //В базе два значения соответствуют монолиту. Все вопросы туда.
                        (Request.Query.ContainsKey("matMono") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("66")) || //Монолитно-кирпичный
                        (Request.Query.ContainsKey("matOther") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("63")) || //МЕТАЛЛ
                        (Request.Query.ContainsKey("matOther") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("64")) || //Бетонные блоки
                        (Request.Query.ContainsKey("matOther") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("69")) || //Пенобетон
                        (Request.Query.ContainsKey("matOther") && main.Single(m => m.ObjectId == estate.Id).BuildingMaterial.Contains("70"))    //Туфоблок
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
                        (Request.Query["stAfterBuilders"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 85) ||
                        (Request.Query["stCapRepair"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 86) ||
                        (Request.Query["stCapRepair"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 87) || //Частичный ремонт
                        (Request.Query["stCosRepair"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 88) ||
                        (Request.Query["stPassably"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 89) ||
                        (Request.Query["stPassably"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 90) || //Нормальное
                        (Request.Query["stGood"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 91) ||
                        (Request.Query["stGreat"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 92) ||
                        (Request.Query["stEuro"] == "on" && rtng.Single(r => r.ObjectId == estate.Id).CommonState == 93)
                    )) return false;
                }
                #endregion

                #region Фильтры по адресу
                //Населённый пункт (сити, ну)
                if (CityId > -1)
                {
                    if (addr.FirstOrDefault(a => a.ObjectId == estate.Id).CityId != CityId)
                    return false; 
                }

                //Район
                if (!string.IsNullOrEmpty(Request.Query["district"]))
                {
                    long DistrictId;
                    if (long.TryParse(Request.Query["district"], out DistrictId))
                    {
                        relevant = relevant.Where(estate => addr.Single(a => a.ObjectId == estate.Id).CityDistrictId == DistrictId).ToArray();
                    }
                }

                //Жилмассив
                if (!string.IsNullOrEmpty(Request.Query["area"]))
                {
                    long AreaId;
                    if (long.TryParse(Request.Query["area"], out AreaId))
                    {
                        relevant = relevant.Where(estate => addr.Single(a => a.ObjectId == estate.Id).DistrictResidentialAreaId == AreaId).ToArray();
                    }
                }

                var searchParser = new Parser()
                {
                    Letters = "",
                    Digits = "",
                    Brackets = "",
                    Separators = ","
                };

                //Улица
                if (!string.IsNullOrEmpty(Request.Query["street"]))
                {
                    //Разбор строки со списком улиц на отдельные названия
                    var streets = searchParser.Parse(Request.Query["street"]);
                    relevant = relevant.Where(estate =>
                    {
                        long? streetId = addr.Single(a => a.ObjectId == estate.Id).StreetId;
                        var geoStreet = strt.SingleOrDefault(s => s.Id == streetId);
                        if (geoStreet == null)
                            return false;
                        foreach (var street in streets)
                        {
                            if (geoStreet.Name.Contains(street.Lexeme))
                                return true;
                        }
                        return false;
                    }).ToArray();
                }
                #endregion

                return true;
            }).ToArray();

            return ConvertToPassports(EstateType, relevant, addr, city, strt, main, addt, vals, cmps, usrs, mdia, comm, rtng);
        }
        #endregion

        //Построение списка результатов
        private string ConvertToPassports(EstateTypes EstateType, EstateObjects[] relevant, List<Addresses> addr, List<GeoCities> city, 
            List<GeoStreets> strt, List<ObjectMainProperties> main, List<ObjectAdditionalProperties> addt, List<DictionaryValues> vals,
            List<Companies> cmps, List<Users> usrs, List<ObjectMedias> mdia, List<ObjectCommunications> comm, List<ObjectRatingProperties> rtng)
        {
            if (EstateType == EstateTypes.Unset)
                EstateType = (EstateTypes)relevant.First().ObjectType;

            switch (EstateType)
            {
                case EstateTypes.Flat:
                    var flat_result = new List<FlatPassport>();
                    
                    foreach (var flat in relevant)
                    {
                        var flpassport = new FlatPassport(addr, city, strt, main, addt, vals, cmps, usrs, mdia, rtng);
                        flpassport.Set(flat);
                        flat_result.Add(flpassport);
                    }
                    return JsonConvert.SerializeObject(flat_result); 

                case EstateTypes.Room:
                    var room_result = new List<RoomPassport>();
                    foreach (var room in relevant)
                    {
                        var passport = new RoomPassport(addr, city, strt, main, addt, vals, cmps, usrs, mdia);
                        passport.Set(room);
                        room_result.Add(passport);
                    }
                    return JsonConvert.SerializeObject(room_result);

                case EstateTypes.House:
                    var house_result = new List<HousePassport>();
                    foreach (var house in relevant)
                    {
                        var passport = new HousePassport(addr, city, strt, main, addt, vals, cmps, usrs, mdia, rtng, comm);
                        passport.Set(house);
                        house_result.Add(passport);
                    }
                    return JsonConvert.SerializeObject(house_result);

                case EstateTypes.Land:
                    var land_result = new List<LandPassport>();
                    foreach (var house in relevant)
                    {
                        var passport = new LandPassport(addr, city, strt, main, addt, vals, cmps, usrs, mdia, comm);
                        passport.Set(house);
                        land_result.Add(passport);
                    }
                    return JsonConvert.SerializeObject(land_result);

                case EstateTypes.Office:
                    var office_result = new List<OfficePassport>();
                    foreach (var house in relevant)
                    {
                        var passport = new OfficePassport(addr, city, strt, main, addt, vals, cmps, usrs, mdia, comm);
                        passport.Set(house);
                        office_result.Add(passport);
                    }
                    return JsonConvert.SerializeObject(office_result);

                case EstateTypes.Garage:
                    var garage_result = new List<GaragePassport>();
                    foreach (var garage in relevant)
                    {
                        var passport = new GaragePassport(addr, city, strt, main, addt, vals, cmps, usrs, mdia);
                        passport.Set(garage);
                        garage_result.Add(passport);
                    }
                    return JsonConvert.SerializeObject(garage_result);

                default:
                    throw new ArgumentException("Указан некорректный тип недвижимости");
            }
        }


    }
}
