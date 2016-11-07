using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using RGR.Core.Controllers.Enums;
using Microsoft.EntityFrameworkCore;
using Eastwing.Parser;
using Newtonsoft.Json;

namespace RGR.Core.Common
{

    //Словарь для передачи результатов поиска в предстваление
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
    
    //public class SuitableEstate : IEnumerable<ShortPassport>
    //{
    //    [JsonIgnore]
    //    public List<Addresses> Addresses { get; set; }
    //    [JsonIgnore]
    //    public List<ObjectMainProperties> MainProps { get; set; }
    //    [JsonIgnore]
    //    public List<ObjectAdditionalProperties> AddtProps { get; set; }
    //    [JsonIgnore]
    //    public List<GeoCities> Cities { get; set; }
    //    [JsonIgnore]
    //    public List<GeoStreets> Streets { get; set; }
    //    [JsonIgnore]
    //    public List<DictionaryValues> DictValues { get; set; }
    //    [JsonIgnore]
    //    public List<Companies> Companies { get; set; }
    //    [JsonIgnore]
    //    public List<Users> Users { get; set; }
    //    [JsonIgnore]
    //    public List<ObjectMedias> Medias { get; set; }
    //    [JsonIgnore]
    //    public List<ObjectRatingProperties> Ratings { get; set; }
    //    [JsonIgnore]
    //    public List<ObjectCommunications> Communications { get; set; }
    //    [JsonIgnore]
    //    public List<StoredFiles> Files { get; set; }

    //    public EstateTypes EstateType;

    //    [JsonRequired]
    //    private List<ShortPassport> passports = new List<ShortPassport>();
    //    private const string NA = "";

    //    [JsonIgnore]
    //    private ObjectMainProperties mainProp;
    //    [JsonIgnore]
    //    private ObjectAdditionalProperties addtProp;
    //    [JsonIgnore]
    //    private Addresses dbAddress;
    //    [JsonIgnore]
    //    private GeoStreets street;
    //    [JsonIgnore]
    //    private string streetName;
    //    [JsonIgnore]
    //    private string city;
    //    [JsonIgnore]
    //    private double? price;
    //    [JsonIgnore]
    //    private double? area;
    //    [JsonIgnore]
    //    private Users agent;
    //    [JsonIgnore]
    //    private Companies company;
    //    [JsonIgnore]
    //    private ObjectRatingProperties rating;
    //    [JsonIgnore]
    //    private ObjectCommunications landComm;
    //    [JsonIgnore]
    //    private IEnumerable<string> photos;
    //    [JsonIgnore]
    //    private string logo;

    //    /// <summary>
    //    /// Добавляет в список результатов паспорт, заполненный на основании объекта недвижимости
    //    /// </summary>
    //    /// <param name="Object">Объект недвижимости, на основании которого строится паспорт</param>
    //    public void Add(EstateObjects Estate)
    //    {
    //        if ((short)EstateType != Estate.ObjectType)
    //            throw new ArgumentException($"Попытка добавить в последовательность объект недвижимости несоответствующего типа ({(EstateTypes)Estate.ObjectType})! Данный экземпляр принимает \"{EstateType}\".");

    //        var passport  = new ShortPassport();

    //        mainProp = MainProps.FirstOrDefault(m => m.ObjectId == Estate.Id);
    //        addtProp = AddtProps.FirstOrDefault(a => a.ObjectId == Estate.Id);
    //        dbAddress = Addresses.FirstOrDefault(a => a.ObjectId == Estate.Id);  
    //        street = Streets.FirstOrDefault(s => s.Id == dbAddress.StreetId);
    //        streetName = (street != null) ? street.Name : NA;
    //        city = Cities.FirstOrDefault(c => c.Id == dbAddress.CityId).Name;
    //        price = mainProp.Price;
    //        area = mainProp.TotalArea;
    //        agent = Users.FirstOrDefault(u => u.Id == Estate.Id);
    //        company = Companies.FirstOrDefault(c => c.Id == Estate.Id);
    //        photos = Medias.Where(m => m.ObjectId == Estate.Id).Select(p => 
    //        {
    //            var id = long.Parse(p.MediaUrl.Split('/').Last());
    //            return StorageUtils.GetFileViewPath(id, Files);
    //        });
    //        logo = (company != null) ? 
    //            ((!string.IsNullOrEmpty(company.LogoImageUrl)) ? 
    //                StorageUtils.GetFileViewPath(long.Parse(company.LogoImageUrl.Split('/').Last()), Files) :
    //                NA) :
    //            NA;

    //        //Индекс БД
    //        passport.Add("Id", Estate.Id);
    //        //Присвоить дату создания, если нет даты изменения
    //        passport.Add("Date",  (Estate.DateModified == null) ? Estate.DateCreated : Estate.DateModified);
    //        //Демонстрируемый адрес
    //        passport.Add("Address", (streetName != null) ? $"{streetName}, {dbAddress.House}" : NA);
    //        //Город
    //        passport.Add("City", city);
    //        //Цена
    //        passport.Add("Price", price);
    //        //Общая площадь
    //        passport.Add("Area", area);
    //        //Цена за квадрат
    //        passport.Add("PricePerSquare", (price != null && area != null) ? $"{price / area : ### 000.00}" : NA);
    //        //Телефон агента
    //        passport.Add("AgentPhone", (agent != null) ? agent.Phone : NA);
    //        //Агенство
    //        passport.Add("Agency", (company != null) ? company.Name : NA);
    //        //Список фотографий объекта
    //        passport.Add("Photos", (photos.Any()) ? photos : default(string[]));
    //        //Логотип агенства
    //        if (logo != NA) passport.Add("Logo", logo);
    //        //Координаты
    //        passport.Add("Latitude", dbAddress.Latitude);
    //        passport.Add("Logitude", dbAddress.Logitude);

    //        if (EstateType == EstateTypes.Room || EstateType == EstateTypes.Flat || EstateType == EstateTypes.Office || EstateType == EstateTypes.House)
    //        {
    //            #region Общие поля для офиса, комнаты, дома и квартиры
    //            rating = Ratings.FirstOrDefault(r => r.Id == Estate.Id);

    //            //Материал постройки
    //            passport.Add("HouseMaterial", ((mainProp.BuildingMaterial != null) ? DictValues.GetFromIds(mainProp.BuildingMaterial) : NA));
    //            //Тип дома
    //            passport.Add("HouseType", (mainProp.BuildingType != null) ? DictValues.First(d => d.Id == mainProp.BuildingType).Value : NA);
    //            //Площадь кухни
    //            passport.Add("KitchenArea", mainProp.KitchenFloorArea);
    //            //Этажей в здании
    //            passport.Add("FloorCount", mainProp.TotalFloors);
    //            //Текущий этаж
    //            passport.Add("Floor", mainProp.FloorNumber);
    //            //Санузел
    //            passport.Add("WC", (rating == null) ? NA : ((rating.Wc != null) ? DictValues.GetFromIds(rating.Wc) : NA));

    //            //Краткое описание
    //            passport.Add("Description", (mainProp.ShortDescription == null) ? NA : (mainProp.ShortDescription.Length <= 55) ? mainProp.ShortDescription :
    //                mainProp.ShortDescription.Remove(49) + " (...)");
    //            #endregion
    //        }
                
    //        if (EstateType == EstateTypes.Land || EstateType == EstateTypes.Office || EstateType == EstateTypes.House )
    //        {
    //            #region Общие для участка, дома и офиса поля
    //            landComm = Communications.FirstOrDefault(c => c.Id == Estate.Id);

    //            //отопление
    //            passport.Add("Heating", (landComm != null) ? ((landComm.Heating != "305") ? "есть" : "нет") : NA);
    //            passport.Add("Water", (landComm != null) ? ((landComm.Water != "205") ? "есть" : "нет") : NA);
    //            passport.Add("Electricy", (landComm != null) ? ((landComm.Electricy != "167") ? "есть" : "нет") : NA);
    //            passport.Add("Sewer", (landComm != null) ? ((landComm.Sewer != 312) ? "есть" : "нет") : NA);
    //            #endregion
    //        }
            
    //        if (EstateType == EstateTypes.Land)
    //        {
    //            //Назначение участка
    //            passport.Add("Purpose", mainProp.LandAssignment ?? NA);
    //        }

    //        if (EstateType == EstateTypes.Office)
    //        {
    //            #region Специфичные для офиса поля
    //            passport.Add("Purpose", DictValues.GetFromIds(mainProp.ObjectAssignment) ?? NA);
    //            passport.Add("Category", NA); //TODO
    //            passport.Add("Specifics", NA);//TODO
    //            #endregion
    //        }

    //        if (EstateType == EstateTypes.Flat || EstateType == EstateTypes.House)
    //        {
    //            #region Общие для квартиры и дома поля
    //            //Число комнат
    //            passport.Add("Rooms", addtProp.RoomsCount);
    //            //Жилая площадь
    //            passport.Add("LivingArea", mainProp.ActualUsableFloorArea);
    //            #endregion
    //        }

    //        if (EstateType == EstateTypes.Garage)
    //        {
    //            passport.Add("HouseMaterial", DictValues.GetFromIds(mainProp.BuildingMaterial) ?? NA);
    //        }

    //        if (EstateType == EstateTypes.Unset)
    //            throw new ArgumentException("Как ты этого добился, демон?!");

    //        passports.Add(passport);
    //    }

    //    /// <summary>
    //    /// Генерирует набор паспортов на основе набора объектов недвижимости
    //    /// </summary>
    //    /// <param name="Range"></param>
    //    public void AddRange(IEnumerable<EstateObjects> Range)
    //    {
    //        foreach (var item in Range)
    //        {
    //            Add(item);
    //        }
    //    }

    //    public SuitableEstate()
    //    {
    //        passports = new List<ShortPassport>();
    //    }

    //    ///// <summary>
    //    ///// Инициализирует новый экземпляр последовательности на основе списка паспортов
    //    ///// </summary>
    //    ///// <param name="list"></param>
    //    //public SuitableEstate (List<ShortPassport> list)
    //    //{
    //    //    passports = list;
    //    //}

    //    public void Clear()
    //    {
    //        passports.Clear();
    //    }

    //    /// <summary>
    //    /// Сортирует представленные последовательностью паспорта по заданному ключу и возвращает отсортированную последовательность
    //    /// </summary>
    //    /// <typeparam name="T">Тип ключа сортировки</typeparam>
    //    /// <param name="keySelector">Метод, предоставляющий ключ</param>
    //    /// <returns></returns>
    //    public SuitableEstate OrderBy<T>(Func<ShortPassport, T> keySelector)
    //    {
    //        passports = passports.OrderBy(keySelector).ToList();
    //        return this;
    //    }

    //    public IEnumerator<ShortPassport> GetEnumerator()
    //    {
    //        foreach (var passport in passports)
    //        {
    //            yield return passport;
    //        }
    //    }
    //    private IEnumerator GetEnumerator1()
    //    {
    //        return this.GetEnumerator();
    //    }
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator1();
    //    }
    //}

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
    }

    //internal static class Translator
    //{
    //    public static string GetDictionaryValues(string ids, List<DictionaryValues> vals)
    //    {
    //        return ids.Split(',')
    //                    .Select(i => vals.First(d => d.Id == long.Parse(i)).Value)
    //                    .Aggregate((result, current) => (string.IsNullOrEmpty(result) ? current : result += $", {current}"));
    //    }

        

    //}


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
