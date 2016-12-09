using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using RGR.Core.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Eastwing.Parser;
using Newtonsoft.Json;

namespace RGR.Core.Common
{

    //Словарь для передачи результатов поиска в представление
    public class ShortPassport : Dictionary<string, object>, IComparable<ShortPassport>
    {
        //public long Id { get; internal set; }
        public EstateTypes EstateType;

        public int CompareTo(ShortPassport other)
        {
            if (!ContainsKey("Id"))
                return 0;

            if ((long)this["Id"] > (long)other["Id"]) return 1;
            else if ((long)this["Id"] < (long)other["Id"]) return -1;
            else return 0;
        }
    }
    
    /// <summary>
    /// Полная карточка объекта, содержащая всю информацию, необходимую для демонстрации подробных сведений
    /// </summary>
    public class FullPassport
    {
        /// <summary>
        /// Строка, означающая "нет данных"
        /// </summary>
        const string NA = "--";

        public long Id;
        public string Price;
        public string PricePerSqMetter;
        public string EstateType;
        public string Address;
        public string FlatType;
        public string BuildingYear;
        public string BuildingPeriod;
        /// <summary>
        /// Материал перекрытий
        /// </summary>
        public string CellingMaterial;
        public string Heating;
        public string Water;
        public string Electricy;
        public string ReplanningLegality;
        public string Sewer;
        /// <summary>
        /// Текущий этаж
        /// </summary>
        public string FloorCurrent;
        /// <summary>
        /// Всего этажей
        /// </summary>
        public string FloorsTotal;
        public string Rooms;
        /// <summary>
        /// Количество уровней в помещении
        /// </summary>
        public string Levels;
        public string TotalSquare;
        public string UsefulSquare;
        public string RoomPlanning;
        public string State;
        public string FullDescription;
        public string BuildingMaterial;
        public string BuildingType;
        public string BuildingCompany;
        /// <summary>
        /// Строительная готовность
        /// </summary>
        public string BuildingReady;
        /// <summary>
        /// Новострой?
        /// </summary>
        public string IsNewBuilding;
        /// <summary>
        /// Кровля
        /// </summary>
        public string Roof;
        public string Basement;
        public string Security;
        /// <summary>
        /// Использование под нежилое
        /// </summary>
        public string UsingAsNonResidental;
        public string Bedrooms;
        public string Balconies;
        public string Logias;
        /// <summary>
        /// Количество эркеров
        /// </summary>
        public string BayWindows;
        /// <summary>
        /// Количество окон
        /// </summary>
        public string Windows;
        /// <summary>
        /// Количество фасадных окон
        /// </summary>
        public string WindowsFacade;
        public string BalconyLogia;
        public string KitchenArea;
        public string LivingRoomArea;
        /// <summary>
        /// Расшифровка метража
        /// </summary>
        public string MeterageExplanation;
        public string CellingHeight;
        /// <summary>
        /// Расположение квартиры
        /// </summary>
        public string FlatLocation;
        public string Replanning;
        /// <summary>
        /// Оценка состояния объекта
        /// </summary>
        public string ObjectStateAssessment;
        /// <summary>
        /// Подсобные помещения
        /// </summary>
        public string UtilityRooms;
        public string WindowQuality;
        public string WindowLocation;
        /// <summary>
        /// Вид из окон
        /// </summary>
        public string WindowView;
        public string WindowState;
        public string EntranceDoorMaterial;
        /// <summary>
        /// Столярка/двери (??)
        /// </summary>
        public string Carpentry;
        /// <summary>
        /// Пол
        /// </summary>
        public string Floor;
        /// <summary>
        /// Потолок
        /// </summary>
        public string Celling;
        public string Walls;
        public string Kitchen;
        public string KitchenDescription;
        public string WC;
        public string WCDescription;
        /// <summary>
        /// Сантехника
        /// </summary>
        public string Sanitary;
        public string Tubes;
        public string Phone;
        public string Vestibule;
        public string LocationInObject;
        /// <summary>
        /// Число собчтвенников
        /// </summary>
        public string Owners;
        /// <summary>
        /// Вид собственности
        /// </summary>
        public string OwningType;
        /// <summary>
        /// Возможность прописки
        /// </summary>
        public string RegistrationPossiblity;
        /// <summary>
        /// Доля собственника
        /// </summary>
        public string OwnerPart;
        /// <summary>
        /// Возможность ипотеки
        /// </summary>
        public string HypotecPossiblity;
        /// <summary>
        /// Количество зарегистрированных жильцов
        /// </summary>
        public string RegisteredDwellersCount;
        /// <summary>
        /// Наличие отягощений
        /// </summary>
        public string Burdening;
        /// <summary>
        /// Правоустанавливающие документы
        /// </summary>
        public string LegalDocuments;
        /// <summary>
        /// Двор
        /// </summary>
        public string YardState;
        /// <summary>
        /// Парковка машин
        /// </summary>
        public string Parking;
        public string MeterGas;
        public string MeterColdWater;
        public string MeterHotWater;
        public string MeterElectricy;
        public string Release;
        public string Internet;
        //Координаты
        public double Latitude;
        public double Longitude;
        public string Agent;
        public string Agency;
        public string AgentPhone;
        public DateTime UpdateTime;
        public bool ElevatorPresent;

        public static async Task<FullPassport> GetAsync(rgrContext db, EstateObjects Estate)
        {
            var vals   = await db.DictionaryValues.ToListAsync();
            var medias = await db.ObjectMedias.Where(m => m.ObjectId == Estate.Id).ToListAsync();

            var main   = await db.ObjectMainProperties.SingleOrDefaultAsync(m => m.ObjectId == Estate.Id);
            var addt   = await db.ObjectAdditionalProperties.SingleOrDefaultAsync(a => a.ObjectId == Estate.Id);
            var rating = await db.ObjectRatingProperties.SingleOrDefaultAsync(r => r.ObjectId == Estate.Id);
            var addr   = await db.Addresses.SingleOrDefaultAsync(a => a.ObjectId == Estate.Id);
            var street = (addr != null) ? await db.GeoStreets.SingleOrDefaultAsync(s => s.Id == addr.StreetId) : null;
            var city   = (addr != null) ? await db.GeoCities.SingleOrDefaultAsync(c => c.Id == addr.CityId) : null;
            var user   = await db.Users.SingleOrDefaultAsync(u => u.Id == Estate.UserId);
            var cmpany = (user != null) ? await db.Companies.SingleOrDefaultAsync(c => c.Id == user.CompanyId) : null;
            var comm   = await db.ObjectCommunications.SingleOrDefaultAsync(c => c.Id == Estate.Id);

            var EstateType = (EstateTypes)Estate.ObjectType;
            var passport = new FullPassport();

            passport.Id = Estate.Id;
            passport.Price = main.Price.ToString() ?? NA;
            passport.PricePerSqMetter = (main.TotalArea != null) ? (main.Price / main.TotalArea).ToString() : NA;
            switch (EstateType)
            {
                #region определение типа объекта
                case EstateTypes.Flat:
                    passport.EstateType = "Квартира";
                    break;
                case EstateTypes.Room:
                    passport.EstateType = "Комната";
                    break;
                case EstateTypes.House:
                    passport.EstateType = "Дом";
                    break;
                case EstateTypes.Land:
                    passport.EstateType = "Участок";
                    break;
                case EstateTypes.Office:
                    passport.EstateType = "Для бизнеса";
                    break;
                case EstateTypes.Garage:
                    passport.EstateType = "Гараж";
                    break;
                default:
                    passport.EstateType = NA;
                    break;
                    #endregion
            }
            passport.Address = $"{city.Name}, {street.Name}, {addr.House}";
            passport.FlatType = (main.FlatType != null) ? vals.Single(v => v.Id == main.FlatType).Value : NA;
            passport.BuildingYear = addt.BuildingYear.ToString() ?? NA;
            passport.BuildingPeriod = (main.BuildingPeriod != null) ? vals.Single(v => v.Id == main.BuildingPeriod).Value : NA;
            passport.CellingMaterial = (main.FloorMaterial != null) ? vals.GetFromIds(main.FloorMaterial) : NA;
            passport.Heating = (comm != null) ? ((comm.Heating != null) ? vals.GetFromIds(comm.Heating) : NA) : NA;
            passport.Water = (comm != null) ? ((comm.Water != null) ? vals.GetFromIds(comm.Water) : NA) : NA;
            passport.Electricy = (comm != null) ? ((comm.Electricy != null) ? vals.GetFromIds(comm.Electricy) : NA) : NA;
            passport.Sewer = (comm != null) ? ((comm.Sewer != null) ? vals.Single(s => s.Id == comm.Sewer).Value : NA) : NA;
            passport.Replanning = BoolToYesNo(addt.Redesign);
            passport.ReplanningLegality = BoolToYesNo(addt.RedesignLegality);
            passport.FloorCurrent = main.FloorNumber.ToString() ?? NA;
            passport.FloorsTotal = main.TotalFloors.ToString() ?? NA;
            passport.Rooms = addt.RoomsCount.ToString() ?? NA;
            passport.Levels = main.LevelsCount.ToString() ?? NA;
            passport.TotalSquare = main.TotalArea.ToString() ?? NA;
            passport.UsefulSquare = main.ActualUsableFloorArea.ToString() ?? NA;
            passport.RoomPlanning = (addt.RoomPlanning != null) ? vals.Single(v => v.Id == addt.RoomPlanning).Value : NA;
            passport.State = (rating.CommonState != null) ? vals.Single(v => v.Id == rating.CommonState).Value : NA;
            passport.FullDescription = main.FullDescription ?? NA;
            passport.BuildingPeriod = (main.BuildingPeriod != null) ? vals.Single(v => v.Id == main.BuildingPeriod).Value : NA;
            passport.BuildingMaterial = (main.BuildingMaterial != null) ? vals.GetFromIds(main.BuildingMaterial) : NA;
            passport.BuildingType = (main.HouseType != null) ? vals.Single(v => v.Id == main.HouseType).Value : NA;
            passport.BuildingCompany = (addt.Builder != null) ? vals.Single(v => v.Id == addt.Builder).Value : NA;
            passport.BuildingReady = main.BuildingReadyPercent.ToString() ?? NA;
            passport.IsNewBuilding = BoolToYesNo(main.NewBuilding);
            passport.Roof = (addt.Roof != null) ? vals.Single(v => v.Id == addt.Roof).Value : NA;
            passport.Basement = (addt.Basement != null) ? vals.GetFromIds(addt.Basement) : NA;
            passport.Security = (main.Security != null) ? vals.GetFromIds(main.Security) : NA;
            passport.UsingAsNonResidental = BoolToYesNo(main.NonResidenceUsage);
            passport.Bedrooms = addt.BedroomsCount.ToString() ?? NA;
            passport.Balconies = addt.BalconiesCount.ToString() ?? NA;
            passport.Logias = addt.LoggiasCount.ToString() ?? NA;
            passport.BayWindows = addt.ErkersCount.ToString() ?? NA;
            passport.Windows = main.WindowsCount.ToString() ?? NA;
            passport.WindowsFacade = main.FacadeWindowsCount.ToString() ?? NA;
            passport.BalconyLogia = (rating.Balcony != null) ? vals.GetFromIds(rating.Balcony) : NA;
            passport.KitchenArea = main.KitchenFloorArea.ToString() ?? NA;
            passport.LivingRoomArea = main.BigRoomFloorArea.ToString() ?? NA;
            passport.MeterageExplanation = main.FootageExplanation ?? NA;
            passport.CellingHeight = main.CelingHeight.ToString() ?? NA;
            passport.FlatLocation = (addt.FlatLocation != null) ? vals.Single(v => v.Id == addt.FlatLocation).Value : NA;
            passport.Replanning = BoolToYesNo(addt.Redesign);
            passport.ObjectStateAssessment = NA; //TODO
            passport.UtilityRooms = rating.UtilityRooms ?? NA;
            passport.WindowLocation = (addt.WindowsLocation != null) ? vals.GetFromIds(addt.WindowsLocation) : NA;
            passport.WindowView = (addt.ViewFromWindows != null) ? vals.GetFromIds(addt.ViewFromWindows) : NA;
            passport.WindowQuality = rating.WindowsDescription ?? NA; //TODO!
            passport.WindowState = NA; //TODO
            passport.EntranceDoorMaterial = (rating.EntranceDoor != null) ? vals.GetFromIds(rating.EntranceDoor) : NA;
            passport.Carpentry = (rating.Carpentry != null) ? vals.GetFromIds(rating.Carpentry) : NA;
            passport.Floor = (rating.Floor != null) ? vals.GetFromIds(rating.Floor) : NA;
            passport.Celling = (rating.Ceiling != null) ? vals.GetFromIds(rating.Ceiling) : NA;
            passport.Walls = (rating.Walls != null) ? vals.GetFromIds(rating.Walls) : NA;
            passport.Kitchen = (rating.Kitchen != null) ? vals.GetFromIds(rating.Kitchen) : NA;
            passport.KitchenDescription = rating.KitchenDescription ?? NA;
            passport.WC = (rating.Wc != null) ? vals.GetFromIds(rating.Wc) : NA;
            passport.WCDescription = rating.Wcdescription ?? NA;
            passport.Sanitary = (comm != null) ? ((comm.SanFurniture != null) ? vals.GetFromIds(comm.SanFurniture) : NA) : NA;
            passport.Tubes = (comm != null) ? ((comm.Tubes != null) ? vals.GetFromIds(comm.Tubes) : NA) : NA;
            passport.Phone = (comm != null) ? ((comm.Phone != null) ? vals.GetFromIds(comm.Phone) : NA) : NA;
            passport.Vestibule = (rating.Vestibule != null) ? vals.GetFromIds(rating.Vestibule) : NA;
            passport.LocationInObject = (addt.Placement != null) ? vals.Single(v => v.Id == addt.Placement).Value : NA;
            passport.Owners = main.OwnersCount.ToString() ?? NA;
            passport.OwningType = (main.PropertyType != null) ? vals.Single(v => v.Id == main.PropertyType).Value : NA;
            passport.RegistrationPossiblity = BoolToYesNo(addt.RegistrationPosibility);
            passport.OwnerPart = (addt.OwnerPart != null) ? vals.Single(v => v.Id == addt.OwnerPart).Value : NA;
            passport.HypotecPossiblity = BoolToYesNo(main.MortgagePossibility);
            passport.RegisteredDwellersCount = NA; //TODO
            passport.Burdening = BoolToYesNo(main.HasWeights);
            passport.LegalDocuments = main.Documents ?? NA;
            passport.YardState = (addt.Court != null) ? vals.Single(v => v.Id == addt.Court).Value : NA;
            passport.Parking = BoolToYesNo(main.HasParking);
            passport.MeterGas = (comm != null) ? BoolToYesNo(comm.HasGasMeter) : NA;
            passport.MeterColdWater = (comm != null) ? BoolToYesNo(comm.HasColdWaterMeter) : NA;
            passport.MeterHotWater = (comm != null) ? BoolToYesNo(comm.HasHotWaterMeter) : NA;
            passport.MeterElectricy = (comm != null) ? BoolToYesNo(comm.HasElectricyMeter) : NA;
            passport.Release = main.ReleaseInfo ?? NA;
            passport.Internet = (comm != null) ? BoolToYesNo(comm.HasInternet) : NA;
            passport.Latitude = addr.Latitude ?? 0;
            passport.Longitude = addr.Logitude ?? 0;
            passport.Agency = cmpany.Name;
            passport.Agent = $"{user.FirstName} {user.SurName} {user.LastName}";
            passport.AgentPhone = user.Phone;
            passport.UpdateTime = Estate.DateModified ?? Estate.DateCreated ?? default(DateTime);
            //passport.ElevatorPresent = rating.el


            return passport;
        }

        /// <summary>
        /// Конвертирование переменной типа bool? в строку "да / нет / нет данных"
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        private static string BoolToYesNo(bool? Source)
        {
            return (Source != null) ? ((Source == true) ? "да" : "нет") : NA;
        }

    }




    //internal static class Translator
    //{
        /// <summary>
        /// Разбор строки, содержащей коды словаря. Возвращает строку, содержащую соответствующие значения
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
    //    public static string GetDictionaryValues(string ids, List<DictionaryValues> vals)
    //    {
    //        return ids.Split(',')
    //                    .Select(i => vals.First(d => d.Id == long.Parse(i)).Value)
    //                    .Aggregate((result, current) => (string.IsNullOrEmpty(result) ? current : result += $", {current}"));
    //    }

        

    //}


    //Карточка города для удобного оперирования при работе с поиском. Заполняется автоматически из таблиц БД
    //public class CityPassport
    //{
    //    protected GeoCities city;
    //    protected rgrContext db;
    
    //    public long Id => city.Id;
    //    public string Name => city.Name;
    
    //    public long RegionId => city.RegionDistrictId;
    //    public string Region => db.GeoRegionDistricts.First(x => x.Id == city.RegionDistrictId).Name;
    
    //    protected Dictionary<long, string> districts;
    //    public Dictionary<long, string> Districts => districts;
    
    //    public static async Task<CityPassport> Create(rgrContext DataBase, GeoCities city)
    //    {
    //        var passport = new CityPassport();
    //        passport.db = DataBase;
    //        passport.city = city;
    //        passport.districts = new Dictionary<long, string>();
    
    //        //Список районов города, подрайоннов и улиц
    //        await Task.Run(()=>
    //        {
    //            foreach (var distr in passport.db.GeoDistricts.Where(x => x.CityId == passport.Id))
    //            {
    //                passport.districts.Add(distr.Id, distr.Name);
    //            }
    //        });
    
    //        return passport;
    //    }
    
    //    public async Task <Dictionary<long, string>> GetAreas(long DistrictId)
    //    {
    //        Dictionary<long, string> result = new Dictionary<long, string>();
    
    //        await Task.Run(() =>
    //        {
    //            foreach (var area in db.GeoResidentialAreas.Where(x => x.DistrictId == DistrictId))
    //            {
    //                result.Add(area.Id, area.Name);
    //            }
    //        });
    
    //        return result;
    //    }
    
    //    public async Task<Dictionary<long, string>> GetStreets(long AreaId)
    //    {
    //        Dictionary<long, string> result = new Dictionary<long, string>();
    
    //        await Task.Run(() => 
    //        {
    //            foreach (var street in db.GeoStreets.Where(x => x.AreaId == AreaId))
    //            {
    //                result.Add(street.Id, street.Name);
    //            }
    //        });
    
    //        return result;
    //    }
    //}

    
}
