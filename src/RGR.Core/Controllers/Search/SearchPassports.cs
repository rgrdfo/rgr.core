using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using RGR.Core.Controllers.Enums;
using Microsoft.EntityFrameworkCore;
using Eastwing.Parser;

namespace RGR.Core.Controllers
{
    /// <summary>
    /// Карточка объекта недвижимости. Включает общие для всех типов объектов поля
    /// </summary>
    public abstract class ObjectPassport
    {
        //private EstateObjects obj;
        //private rgrContext db;

        protected const string NA = "--";

        protected List<Addresses> addr;
        protected List<ObjectMainProperties> main;
        protected List<ObjectAdditionalProperties> addt;
        protected List<GeoCities> city;
        protected List<GeoStreets> strt;
        protected List<DictionaryValues> vals;
        protected List<Companies> cmps;
        protected List<Users> usrs;
        protected List<ObjectMedias> mdia;

        protected ObjectMainProperties mainProp;

        public long Id;
        public string Address;
        public string City;
        public double? Price;
        public double? Square;
        public List<string> Photos;
        public string Agency;
        public string AgencyLogo;
        public string AgentPhone;
        public DateTime? Date;

        public double? PricePerSquareMetter => Price / Square;

        protected ObjectPassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp, List<ObjectAdditionalProperties> AdditionalProp, List<Companies> Companies, List<Users> Users, List<ObjectMedias> Media)
        {
            addr = Addresses;
            main = MainProp;
            addt = AdditionalProp;
            city = Cities;
            strt = Streets;
            cmps = Companies;
            usrs = Users;
            mdia = Media;
        }
    
        protected void Set(EstateObjects obj)
        {
            Id = obj.Id;

            Date = (obj.DateModified == null) ? obj.DateCreated : obj.DateModified; //Присвоить дату создания, если нет даты изменения

            Addresses dbAddress = addr.Single(x => x.ObjectId == obj.Id);
            mainProp = main.Single(x => x.ObjectId == obj.Id);

            if (dbAddress.StreetId > 0) //Если это условие не выполняется - запись некорректна
            {
                //string StreetName = "";
                var street = strt.SingleOrDefault(x => x.Id == dbAddress.StreetId);
                var StreetName = (street != null)? street.Name : null;
                Address = (StreetName != null) ? $"{StreetName}, {dbAddress.House}, кв. {dbAddress.Flat}" : NA;
            }
            else
                Address = "";
    
            if (dbAddress.CityId > 0)
            {
                City = city.Single(x => x.Id == dbAddress.CityId).Name;
            }
            else
                City = "";
    
            Price  = main.Single(x => x.ObjectId == obj.Id).Price;
            Square = main.Single(x => x.ObjectId == obj.Id).TotalArea;

            //var parser = new Parser()
            //{
            //    Letters = "",
            //    Separators = "",
            //    Brackets = "()"
            //};

            var agent = usrs.SingleOrDefault(u => u.Id == obj.Id);
            AgentPhone = (agent != null) ? agent.Phone:NA;

            //if (agent == null)
            //    AgentPhone = NA;
            //else
            //{
            //    var rawPhone = parser.Parse(agent.Phone)
            //        .Where(t => t.Category == Category.Integer)
            //        .Select(t => t.Lexeme)
            //        .Flatten();
            //    AgentPhone = rawPhone;
            //}

            var company = cmps.SingleOrDefault(c => c.Id == obj.Id);
            Agency = (company != null) ? company.Name:NA;


        }
    }
    /// <summary>
    /// Карточка комнаты для удобного оперирования при выводе результатов поиска
    /// </summary>
    public class RoomPassport : ObjectPassport
    {
        //private List<DictionaryValues> vals;

        public string HouseMaterial;
        public string HouseType;
        public string State;
        public double? KitchenSquare;
        public int? FloorCount;
        public int? Floor;
        public string Balcony;
        public bool? BalconyIsPresent;
        public string Description;

        

        public RoomPassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp,
            List<ObjectAdditionalProperties> AdditionalProp, List<DictionaryValues> DictValues, List<Companies> Companies, List<Users> Users, List<ObjectMedias> Media)
            : base(Addresses, Cities, Streets, MainProp, AdditionalProp, Companies, Users, Media)
        {
            vals = DictValues;
        }

        protected ObjectAdditionalProperties addtProp;
        new public void Set(EstateObjects Room)
        {
            base.Set(Room);

            addtProp = addt.Single(x => x.ObjectId == Room.Id);

            HouseMaterial = mainProp.BuildingMaterial ?? NA;

            if (mainProp.BuildingType != null)
                HouseType = vals.Single(x => x.Id == mainProp.BuildingType).Value;

            KitchenSquare = mainProp.KitchenFloorArea;

            FloorCount = mainProp.TotalFloors;
            Floor = mainProp.FloorNumber;

            if (addtProp.BalconiesCount != null)
            {
                BalconyIsPresent = addtProp.BalconiesCount > 0;
            }
            else
                BalconyIsPresent = null;

            Description = mainProp.ShortDescription;
        }
    }
    /// <summary>
    /// Карточка квартиры для удобного оперирования при выводе результатов поиска
    /// </summary>
    public class FlatPassport : RoomPassport
    {
        protected List<ObjectRatingProperties> rtng;
    
        public int? Rooms;
        public double? LivingSquare;
        public string WC;
    
        public FlatPassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp,
            List<ObjectAdditionalProperties> AdditionalProp, List<DictionaryValues> DictValues, List<Companies> Companies, List<Users> Users, 
            List<ObjectMedias> Media, List<ObjectRatingProperties> Rating)
            : base(Addresses, Cities, Streets, MainProp, AdditionalProp, DictValues, Companies, Users, Media)
        {
            vals = DictValues;
            rtng = Rating;
        }
        
        new public void Set (EstateObjects Flat)
        {
            base.Set(Flat);
        
            Rooms = addtProp.RoomsCount;
            LivingSquare = mainProp.ActualUsableFloorArea;

            var rating = rtng.SingleOrDefault(r => r.Id == Flat.Id);
            WC = (rating != null)?(rating.Wc ?? NA) : NA;
        }


        public FlatPassport Reinit(EstateObjects obj)
        {
            Id = obj.Id;

            Date = (obj.DateModified == null) ? obj.DateCreated : obj.DateModified; //Присвоить дату создания, если нет даты изменения

            Addresses dbAddress = addr.Single(x => x.ObjectId == obj.Id);
            mainProp = main.Single(x => x.ObjectId == obj.Id);

            if (dbAddress.StreetId > 0) //Если это условие не выполняется - запись некорректна
            {
                //string StreetName = "";
                var street = strt.SingleOrDefault(x => x.Id == dbAddress.StreetId);
                var StreetName = (street != null) ? street.Name : null;
                Address = (StreetName != null) ? $"{StreetName}, {dbAddress.House}, кв. {dbAddress.Flat}" : NA;
            }
            else
                Address = "";

            if (dbAddress.CityId > 0)
            {
                City = city.Single(x => x.Id == dbAddress.CityId).Name;
            }
            else
                City = "";

            Price = main.Single(x => x.ObjectId == obj.Id).Price;
            Square = main.Single(x => x.ObjectId == obj.Id).TotalArea;

            var agent = usrs.SingleOrDefault(u => u.Id == obj.Id);
            AgentPhone = (agent != null) ? agent.Phone : NA;

            var company = cmps.SingleOrDefault(c => c.Id == obj.Id);
            Agency = (company != null) ? company.Name : NA;

            Rooms = addtProp.RoomsCount;
            LivingSquare = mainProp.ActualUsableFloorArea;

            var rating = rtng.SingleOrDefault(r => r.Id == obj.Id);
            WC = (rating != null) ? (rating.Wc ?? NA) : NA;

            return this;
        }


    }
    /// <summary>
    /// Карточка земельного участка для удобного оперирования при выводе результатов поиска
    /// </summary>
    public class LandPassport : ObjectPassport
    {
        private List<ObjectCommunications> comm;

        public string Purpose;
        public bool? Heating = null;
        public bool? Water = null;
        public bool? Electricy = null;
        public bool? Sewer = null;
        public string Category = NA; //TODO
        public string Specifics = NA;//TODO

        //public double? PricePerSquareMetter => Price / Square;

        public LandPassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp,
            List<ObjectAdditionalProperties> AdditionalProp, List<DictionaryValues> DictValues, List<Companies> Companies, List<Users> Users, 
            List<ObjectMedias> Media, List<ObjectCommunications> Communications)
            : base(Addresses, Cities, Streets, MainProp, AdditionalProp, Companies, Users, Media)
        {
            vals = DictValues;
            comm = Communications;
        }

        new public void Set(EstateObjects Land)
        {
            base.Set(Land);

            Purpose = mainProp.LandAssignment ?? NA;

            var landComm = comm.SingleOrDefault(c => c.Id == Land.Id);
            if (landComm != null)
            {
                Heating = landComm.Heating != "305";
                Water = landComm.Water != "205";
                Electricy = landComm.Electricy != "167";
                Sewer = landComm.Sewer != 312;
            }
        }
    }
    /// <summary>
    /// Карточка деловой недвижимости для удобного оперирования при выводе результатов поиска
    /// </summary>
    public class OfficePassport : RoomPassport
    {
        private List<ObjectCommunications> comm;

        public string Purpose;
        public bool? Heating = null;
        public bool? Water = null;
        public bool? Electricy = null;
        public bool? Sewer = null;
        public string Category = NA; //TODO
        public string Specifics = NA;//TODO


        //public double? PricePerSquareMetter => Price / Square;

        public OfficePassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp,
            List<ObjectAdditionalProperties> AdditionalProp, List<DictionaryValues> DictValues, List<Companies> Companies, List<Users> Users,
            List<ObjectMedias> Media, List<ObjectCommunications> Communications)
            : base(Addresses, Cities, Streets, MainProp, AdditionalProp, DictValues, Companies, Users, Media)
        {
            vals = DictValues;
            comm = Communications;
        }

        new public void Set(EstateObjects Land)
        {
            base.Set(Land);

            Purpose = mainProp.ObjectAssignment ?? NA;

            var landComm = comm.SingleOrDefault(c => c.Id == Land.Id);
            if (landComm != null)
            {
                Heating = landComm.Heating != "305";
                Water = landComm.Water != "205";
                Electricy = landComm.Electricy != "167";
                Sewer = landComm.Sewer != 312;
            }
        }
    }
    /// <summary>
    /// Карточка гаража для удобного оперирования при выводе результатов поиска
    /// </summary>
    public class GaragePassport : ObjectPassport
    {
        public string HouseMaterial;

        public GaragePassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp,
            List<ObjectAdditionalProperties> AdditionalProp, List<DictionaryValues> DictValues, List<Companies> Companies, List<Users> Users, List<ObjectMedias> Media)
            : base(Addresses, Cities, Streets, MainProp, AdditionalProp, Companies, Users, Media)
        {
            vals = DictValues;
        }

        new public void Set(EstateObjects Garage)
        {
            base.Set(Garage);

            HouseMaterial = mainProp.BuildingMaterial ?? NA;
        }
    }
    /// <summary>
    /// Карточка дома для удобного оперирования при выводе результатов поиска
    /// </summary>
    public class HousePassport : FlatPassport
    {
        private List<ObjectCommunications> comm;

        public bool? Heating = null;
        public bool? Water = null;
        public bool? Electricy = null;
        public bool? Sewer = null;

        public HousePassport(List<Addresses> Addresses, List<GeoCities> Cities, List<GeoStreets> Streets, List<ObjectMainProperties> MainProp,
            List<ObjectAdditionalProperties> AdditionalProp, List<DictionaryValues> DictValues, List<Companies> Companies, List<Users> Users, 
            List<ObjectMedias> Media, List<ObjectRatingProperties> Rating, List<ObjectCommunications> Communications)
            : base(Addresses, Cities, Streets, MainProp, AdditionalProp, DictValues, Companies, Users, Media, Rating)
        {
            vals = DictValues;
            comm = Communications;
        }

        new public void Set(EstateObjects House)
        {
            base.Set(House);

            var landComm = comm.SingleOrDefault(c => c.Id == House.Id);
            if (landComm != null)
            {
                Heating = landComm.Heating != "305";
                Water = landComm.Water != "205";
                Electricy = landComm.Electricy != "167";
                Sewer = landComm.Sewer != 312;
            }
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
        public string KitchenSquare;
        public string LivingRoomSquare;
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
            passport.CellingMaterial = (main.FloorMaterial != null) ? GetDictionaryValues(main.FloorMaterial, vals) : NA;
            passport.Heating = (comm != null) ? ((comm.Heating != null) ? GetDictionaryValues(comm.Heating, vals) : NA) : NA;
            passport.Water = (comm != null) ? ((comm.Water != null) ? GetDictionaryValues(comm.Water, vals) : NA) : NA;
            passport.Electricy = (comm != null) ? ((comm.Electricy != null) ? GetDictionaryValues(comm.Electricy, vals) : NA) : NA;
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
            passport.BuildingMaterial = (main.BuildingMaterial != null) ? GetDictionaryValues(main.BuildingMaterial, vals) : NA;
            passport.BuildingType = (main.HouseType != null) ? vals.Single(v => v.Id == main.HouseType).Value : NA;
            passport.BuildingCompany = (addt.Builder != null) ? vals.Single(v => v.Id == addt.Builder).Value : NA;
            passport.BuildingReady = main.BuildingReadyPercent.ToString() ?? NA;
            passport.IsNewBuilding = BoolToYesNo(main.NewBuilding);
            passport.Roof = (addt.Roof != null) ? vals.Single(v => v.Id == addt.Roof).Value : NA;
            passport.Basement = (addt.Basement != null) ? GetDictionaryValues(addt.Basement, vals) : NA;
            passport.Security = (main.Security != null) ? GetDictionaryValues(main.Security, vals) : NA;
            passport.UsingAsNonResidental = BoolToYesNo(main.NonResidenceUsage);
            passport.Bedrooms = addt.BedroomsCount.ToString() ?? NA;
            passport.Balconies = addt.BalconiesCount.ToString() ?? NA;
            passport.Logias = addt.LoggiasCount.ToString() ?? NA;
            passport.BayWindows = addt.ErkersCount.ToString() ?? NA;
            passport.Windows = main.WindowsCount.ToString() ?? NA;
            passport.WindowsFacade = main.FacadeWindowsCount.ToString() ?? NA;
            passport.BalconyLogia = (rating.Balcony != null) ? GetDictionaryValues(rating.Balcony, vals) : NA;
            passport.KitchenSquare = main.KitchenFloorArea.ToString() ?? NA;
            passport.LivingRoomSquare = main.BigRoomFloorArea.ToString() ?? NA;
            passport.MeterageExplanation = main.FootageExplanation ?? NA;
            passport.CellingHeight = main.CelingHeight.ToString() ?? NA;
            passport.FlatLocation = (addt.FlatLocation != null) ? vals.Single(v => v.Id == addt.FlatLocation).Value : NA;
            passport.Replanning = BoolToYesNo(addt.Redesign);
            passport.ObjectStateAssessment = NA; //TODO
            passport.UtilityRooms = rating.UtilityRooms ?? NA;
            passport.WindowLocation = (addt.WindowsLocation != null) ? GetDictionaryValues(addt.WindowsLocation, vals) : NA;
            passport.WindowView = (addt.ViewFromWindows != null) ? GetDictionaryValues(addt.ViewFromWindows, vals) : NA;
            passport.WindowQuality = rating.WindowsDescription ?? NA; //TODO!
            passport.WindowState = NA; //TODO
            passport.EntranceDoorMaterial = (rating.EntranceDoor != null) ? GetDictionaryValues(rating.EntranceDoor, vals) : NA;
            passport.Carpentry = (rating.Carpentry != null) ? GetDictionaryValues(rating.Carpentry, vals) : NA;
            passport.Floor = (rating.Floor != null) ? GetDictionaryValues(rating.Floor, vals) : NA;
            passport.Celling = (rating.Ceiling != null) ? GetDictionaryValues(rating.Ceiling, vals) : NA;
            passport.Walls = (rating.Walls != null) ? GetDictionaryValues(rating.Walls, vals) : NA;
            passport.Kitchen = (rating.Kitchen != null) ? GetDictionaryValues(rating.Kitchen, vals) : NA;
            passport.KitchenDescription = rating.KitchenDescription ?? NA;
            passport.WC = (rating.Wc != null) ? GetDictionaryValues(rating.Wc, vals) : NA;
            passport.WCDescription = rating.Wcdescription ?? NA;
            passport.Sanitary = (comm != null) ? ((comm.SanFurniture != null) ? GetDictionaryValues(comm.SanFurniture, vals) : NA) : NA;
            passport.Tubes = (comm != null) ? ((comm.Tubes != null) ? GetDictionaryValues(comm.Tubes, vals) : NA) : NA;
            passport.Phone = (comm != null) ? ((comm.Phone != null) ? GetDictionaryValues(comm.Phone, vals) : NA) : NA;
            passport.Vestibule = (rating.Vestibule != null) ? GetDictionaryValues(rating.Vestibule, vals) : NA;
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

        /// <summary>
        /// Разбор строки, содержащей коды словаря. Возвращает строку, содержащую соответствующие значения
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        private static string GetDictionaryValues(string ids, List<DictionaryValues> vals)
        {
            var arr = ids.Split(',');
            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i > 0 && arr.Length > 1)
                    result += ", ";

                result += vals.Single(v => v.Id == int.Parse(arr[i])).Value;
            }
            return result;
        }
    }


    //Карточка города для удобного оперирования при работе с поиском. Заполняется автоматически из таблиц БД
    public class CityPassport
    {
        protected GeoCities city;
        protected rgrContext db;
    
        public long Id => city.Id;
        public string Name => city.Name;
    
        public long RegionId => city.RegionDistrictId;
        public string Region => db.GeoRegionDistricts.First(x => x.Id == city.RegionDistrictId).Name;
    
        protected Dictionary<long, string> districts;
        public Dictionary<long, string> Districts => districts;
    
        public static async Task<CityPassport> Create(rgrContext DataBase, GeoCities city)
        {
            var passport = new CityPassport();
            passport.db = DataBase;
            passport.city = city;
            passport.districts = new Dictionary<long, string>();
    
            //Список районов города, подрайоннов и улиц
            await Task.Run(()=>
            {
                foreach (var distr in passport.db.GeoDistricts.Where(x => x.CityId == passport.Id))
                {
                    passport.districts.Add(distr.Id, distr.Name);
                }
            });
    
            return passport;
        }
    
        public async Task <Dictionary<long, string>> GetAreas(long DistrictId)
        {
            Dictionary<long, string> result = new Dictionary<long, string>();
    
            await Task.Run(() =>
            {
                foreach (var area in db.GeoResidentialAreas.Where(x => x.DistrictId == DistrictId))
                {
                    result.Add(area.Id, area.Name);
                }
            });
    
            return result;
        }
    
        public async Task<Dictionary<long, string>> GetStreets(long AreaId)
        {
            Dictionary<long, string> result = new Dictionary<long, string>();
    
            await Task.Run(() => 
            {
                foreach (var street in db.GeoStreets.Where(x => x.AreaId == AreaId))
                {
                    result.Add(street.Id, street.Name);
                }
            });
    
            return result;
        }
    }

    
}
