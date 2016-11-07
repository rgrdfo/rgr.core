using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using RGR.Core.Controllers.Enums;
using Microsoft.AspNetCore.Http;
using Eastwing.Parser;
using Newtonsoft.Json;
using RGR.Core.Controllers.Account;
using Microsoft.EntityFrameworkCore;
using System.Text;

using static System.Net.WebUtility;

namespace RGR.Core.Common
{
    public struct ExplainedRequest
    {
        public string Title;
        public string Explanation;
    }

    public class SearchUtils
    {
        public static string GetSavedRequests(DbSet<SearchRequests> Requests)
        {
            var uriParser = new Parser()
            {
                Letters = "+-АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_%,()",
                Separators = "?&",
                Brackets = ""
            };

            var valParser = new Parser()
            {
                Letters = "",
                Digits = "",
                Separators = ",",
                Brackets = ""
            };

            var ExplainedRequests = Requests.Select(r =>
                new ExplainedRequest
                {
                    Title = r.Title,
                    Explanation = Explain(r.SearchUrl, uriParser, valParser)
                });

            return JsonConvert.SerializeObject(ExplainedRequests);
        }

        //генерация описания запроса на основе строки запроса
        private static string Explain(string query, Parser uriParser, Parser valParser)
        {
            var sb = new StringBuilder();
            var dict = new Dictionary<string, string>();

            //Парсинг строки запроса, исключение пробелов и разделителей
            var tokens = uriParser.Parse(query).Where(t => t.Category != Category.Separator && t.Category != Category.Space).ToList();

            //Построение словаря
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Category == Category.Equals)
                {
                    //Незатейливая проверка, что текущий ключ не пустой
                    if (i + 2 < tokens.Count)
                        if (tokens[i + 2].Category == Category.Equals)
                            continue;

                    //Добавление в словарь ключа и его значения
                    dict.Add(tokens[i - 1].Lexeme.ToUpper(), tokens[i + 1].Lexeme);
                }
            }

            # region Расшифровка словаря
            if (dict.ContainsKey("OBJTYPE"))
            {
                switch (dict["OBJTYPE"])
                {
                    #region Тип объекта
                    case "0":
                        sb.Append("Квартиры");
                        break;

                    case "1":
                        sb.Append("Комнаты");
                        break;

                    case "2":
                        sb.Append("Дома");
                        break;

                    case "3":
                        sb.Append("Участки");
                        break;

                    case "4":
                        sb.Append("Офисная недвижимость");
                        break;

                    case "5":
                        sb.Append("Гаражи / парковочные места");
                        break;
                        #endregion
                }
                sb.Append(' ');
            }

            if (dict.ContainsKey("PRICEFROM"))
            {
                sb.Append($"по цене от {dict["PRICEFROM"]:### ### ##0}₽ ");
            }

            if (dict.ContainsKey("PRICETO"))
            {
                string prefix = (dict.ContainsKey("PRICEFROM")) ? "" : prefix = "по цене ";
                sb.Append($"{prefix}до {dict["PRICETO"]:### ### ##0}₽, ");
            }

            if (dict.ContainsKey("LIVINGSQUAREFROM"))
            {
                sb.Append($"жилой площадью от {dict["LIVINGSQUAREFROM"]:### ###.00} м² ");
            }

            if (dict.ContainsKey("LIVINGSQUARETO"))
            {
                string prefix = (dict.ContainsKey("LIVINGSQUAREFROM")) ? "" : "жилой площадью ";
                sb.Append($"{prefix}до {dict["LIVINGSQUARETO"]:### ###.00} м² ");
            }

            if (dict.ContainsKey("MINFLOOR"))
            {
                bool maxSetted = dict.ContainsKey("MAXFLOOR");

                string prefix = maxSetted ? "на" : "не ниже";
                string postfix = maxSetted ? "-" : " этажа";

                sb.Append($", {prefix} {dict["MINFLOOR"]}{postfix}");
            }

            if (dict.ContainsKey("MAXFLOOR"))
            {
                bool minSetted = dict.ContainsKey("MINFLOOR");

                string prefix = minSetted ? "" : ", не выше ";
                string postfix = minSetted ? "этажах" : "этажа";

                sb.Append($"{prefix}{dict["MAXFLOOR"]} {postfix} ");
            }

            if (dict.ContainsKey("MINHOUSEFLOORS"))
            {
                string postfix = dict.ContainsKey("MAXHOUSEFLOORS") ? " этажей" : "";
                sb.Append($"в доме от {dict["MINHOUSEFLOORS"]}{postfix}");
            }

            if (dict.ContainsKey("MAXHOUSEFLOORS"))
            {
                string prefix = dict.ContainsKey("MINHOUSEFLOORS") ? "в доме до " : "";
                sb.Append($"{prefix}{dict["MAXHOUSEFLOORS"]} этажей, ");
            }

            if (dict.ContainsKey("WC"))
            {
                sb.Append(dict["WC"].ToUpper() == "SEP" ? ", с раздельным санузлом" : ", со смежным санузлом");
            }



            #endregion

            return sb.ToString();
        }

        public class PassportConverter
        {
            public List<Addresses> Addresses { get; set; }
            public List<ObjectMainProperties> MainProps { get; set; }
            public List<ObjectAdditionalProperties> AddtProps { get; set; }
            public List<GeoCities> Cities { get; set; }
            public List<GeoStreets> Streets { get; set; }
            public List<DictionaryValues> DictValues { get; set; }
            public List<Companies> Companies { get; set; }
            public List<Users> Users { get; set; }
            public List<ObjectMedias> Medias { get; set; }
            public List<ObjectRatingProperties> Ratings { get; set; }
            public List<ObjectCommunications> Communications { get; set; }
            public List<StoredFiles> Files { get; set; }
            public List<ShortPassport> GetShortPassports(IEnumerable<EstateObjects> EstateObjects)
            {
                const string NA = "";
                var result = new List<ShortPassport>();



                foreach (var Estate in EstateObjects)
                {
                    var passport = new ShortPassport();
                    var mainProp = MainProps.FirstOrDefault(m => m.ObjectId == Estate.Id);
                    var addtProp = AddtProps.FirstOrDefault(a => a.ObjectId == Estate.Id);
                    var dbAddress = Addresses.FirstOrDefault(a => a.ObjectId == Estate.Id);
                    var street = Streets.FirstOrDefault(s => s.Id == dbAddress.StreetId);
                    var streetName = (street != null) ? street.Name : NA;
                    if (streetName == NA)
                        continue;
                    var city = Cities.FirstOrDefault(c => c.Id == dbAddress.CityId).Name;
                    var price = mainProp.Price;
                    var area = mainProp.TotalArea;
                    var agent = Users.FirstOrDefault(u => u.Id == Estate.Id);
                    var company = Companies.FirstOrDefault(c => c.Id == Estate.Id);


                    var photos = Medias.Where(m => m.ObjectId == Estate.Id).Select(p => StorageUtils.GetFileViewPath(p.MediaUrl, Files));


                    var logo = (company != null) ?
                        ((!string.IsNullOrEmpty(company.LogoImageUrl)) ?
                            StorageUtils.GetFileViewPath(company.LogoImageUrl, Files) :
                            NA) :
                        NA;

                    //Индекс БД
                    passport.Add("Id", Estate.Id);
                    //Присвоить дату создания, если нет даты изменения
                    passport.Add("Date", (Estate.DateModified == null) ? Estate.DateCreated : Estate.DateModified);
                    //Демонстрируемый адрес
                    passport.Add("Address", (streetName != null) ? $"{streetName}, {dbAddress.House}" : NA);
                    //Город
                    passport.Add("City", city);
                    //Цена
                    passport.Add("Price", price);
                    //Общая площадь
                    passport.Add("Area", area);
                    //Цена за квадрат
                    passport.Add("PricePerSquare", (price != null && area != null) ? $"{price / area: ### 000.00}" : NA);
                    //Телефон агента
                    passport.Add("AgentPhone", (agent != null) ? agent.Phone : NA);
                    //Агенство
                    passport.Add("Agency", (company != null) ? company.Name : NA);
                    //Список фотографий объекта
                    passport.Add("Photos", (photos != null && photos.Any()) ? photos : null);
                    //Логотип агенства
                    if (logo != NA) passport.Add("Logo", logo);
                    //Координаты
                    passport.Add("Latitude", dbAddress.Latitude);
                    passport.Add("Logitude", dbAddress.Logitude);

                    if (Estate.ObjectType == (short)EstateTypes.Room ||
                        Estate.ObjectType == (short)EstateTypes.Flat ||
                        Estate.ObjectType == (short)EstateTypes.Office ||
                        Estate.ObjectType == (short)EstateTypes.House)
                    {
                        #region Общие поля для офиса, комнаты, дома и квартиры
                        var rating = Ratings.FirstOrDefault(r => r.Id == Estate.Id);

                        //Материал постройки
                        passport.Add("HouseMaterial", ((mainProp.BuildingMaterial != null) ? DictValues.GetFromIds(mainProp.BuildingMaterial) : NA));
                        //Тип дома
                        passport.Add("HouseType", (mainProp.BuildingType != null) ? DictValues.First(d => d.Id == mainProp.BuildingType).Value : NA);
                        //Площадь кухни
                        passport.Add("KitchenArea", mainProp.KitchenFloorArea);
                        //Этажей в здании
                        passport.Add("FloorCount", mainProp.TotalFloors);
                        //Текущий этаж
                        passport.Add("Floor", mainProp.FloorNumber);
                        //Санузел
                        passport.Add("WC", (rating == null) ? NA : ((rating.Wc != null) ? DictValues.GetFromIds(rating.Wc) : NA));

                        //Краткое описание
                        passport.Add("Description", (mainProp.ShortDescription == null) ? NA : (mainProp.ShortDescription.Length <= 55) ? mainProp.ShortDescription :
                            mainProp.ShortDescription.Remove(49) + " (...)");
                        #endregion
                    }

                    if (Estate.ObjectType == (short)EstateTypes.Land ||
                        Estate.ObjectType == (short)EstateTypes.Office ||
                        Estate.ObjectType == (short)EstateTypes.House)
                    {
                        #region Общие для участка, дома и офиса поля
                        var landComm = Communications.FirstOrDefault(c => c.Id == Estate.Id);

                        //отопление
                        passport.Add("Heating", (landComm != null) ? ((landComm.Heating != "305") ? "есть" : "нет") : NA);
                        passport.Add("Water", (landComm != null) ? ((landComm.Water != "205") ? "есть" : "нет") : NA);
                        passport.Add("Electricy", (landComm != null) ? ((landComm.Electricy != "167") ? "есть" : "нет") : NA);
                        passport.Add("Sewer", (landComm != null) ? ((landComm.Sewer != 312) ? "есть" : "нет") : NA);
                        #endregion
                    }

                    if (Estate.ObjectType == (short)EstateTypes.Land)
                    {
                        //Назначение участка
                        passport.Add("Purpose", mainProp.LandAssignment ?? NA);
                    }

                    if (Estate.ObjectType == (short)EstateTypes.Office)
                    {
                        #region Специфичные для офиса поля
                        passport.Add("Purpose", DictValues.GetFromIds(mainProp.ObjectAssignment) ?? NA);
                        passport.Add("Category", NA); //TODO
                        passport.Add("Specifics", NA);//TODO
                        #endregion
                    }

                    if (Estate.ObjectType == (short)EstateTypes.Flat ||
                        Estate.ObjectType == (short)EstateTypes.House)
                    {
                        #region Общие для квартиры и дома поля
                        //Число комнат
                        passport.Add("Rooms", addtProp.RoomsCount);
                        //Жилая площадь
                        passport.Add("LivingArea", mainProp.ActualUsableFloorArea);
                        #endregion
                    }

                    if (Estate.ObjectType == (short)EstateTypes.Garage)
                    {
                        passport.Add("HouseMaterial", DictValues.GetFromIds(mainProp.BuildingMaterial) ?? NA);
                    }

                    if (Estate.ObjectType == (short)EstateTypes.Unset)
                        throw new ArgumentException("Как ты этого добился, демон?!");

                    result.Add(passport);
                }

                return result;
            }
        }

        
    }
}
