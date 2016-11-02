using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using RGR.Core.Common;
using RGR.Core.Controllers.Enums;
using System;
using System.Text;

namespace RGR.Core.Views.Helpers
{
    public class SearchResult
    {
        //public static HtmlString SortResult(string Source, EstateTypes EstateType)
        //{
        //    var sb = new StringBuilder("<div id=\"bodyResult\">");
        //    //List<String> adresses = new List<string>();            
        //    switch (EstateType)
        //    {
        //        case EstateTypes.Flat:
        //            var flats = JsonConvert.DeserializeObject<IEnumerable<FlatPassport>>(Source);
        //            //foreach (var flat in flats)
        //            //{
        //            //    adresses.Add(flat.Address);
        //            //}
        //            //adresses.Sort();
        //            flats = flats.OrderBy(f => f.Address);
        //            foreach (var flat in flats)
        //            {
        //                sb.Append(CommonStart(flat));
        //                sb.Append($"<div class=\"col-lg-2\"><h1>{flat.Rooms}-комнатная</h1><br/>{flat.HouseMaterial}<br/>{flat.HouseType}</div>");
        //                sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Square} м²</h1><br />кухня {flat.KitchenSquare}<br />жилая {flat.LivingSquare}</div>");
        //                sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Floor} этаж из {flat.FloorCount}</h1><br />{flat.Balcony}<br />{flat.Description}</div>");
        //                sb.Append(CommonEnd(flat));
        //                sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={flat["Id"]}\">Подробнее</a></div>");

        //                //aF = adresses.ElementAt(adresses.BinarySearch(adressFlat));
        //                //foreach (var flat in flats)
        //                //{
        //                //    if(aF==flat.Address)
        //                //    {
        //                //        sb.Append(CommonStart(flat));
        //                //        sb.Append($"<div class=\"col-lg-2\"><h1>{flat.Rooms}-комнатная</h1><br/>{flat.HouseMaterial}<br/>{flat.HouseType}</div>");
        //                //        sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Square} м²</h1><br />кухня {flat.KitchenSquare}<br />жилая {flat.LivingSquare}</div>");
        //                //        sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Floor} этаж из {flat.FloorCount}</h1><br />{flat.Balcony}<br />{flat.Description}</div>");
        //                //        sb.Append(CommonEnd(flat));
        //                //    }
        //                //}
        //            }
        //            break;
        //            //case EstateTypes.Room:
        //            //    var rooms = JsonConvert.DeserializeObject<IEnumerable<FlatPassport>>(Source);
        //            //    rooms = rooms.OrderBy(f => f.Address);
        //            //    foreach (var room in rooms)
        //            //    {
        //            //        sb.Append(CommonStart(room));
        //            //        sb.Append($"<div class=\"col-lg-2\">{room.HouseMaterial}<br/>{room.HouseType}</div>");
        //            //        sb.Append($"<div class=\"col-lg-1\">{room.Square} м²<br />кухня {room.KitchenSquare}<br /></div>");
        //            //        sb.Append($"<div class=\"col-lg-1\">{room.Floor} этаж из {room.FloorCount}<br />{room.Description}</div>");
        //            //        sb.Append(CommonEnd(room));
        //            //        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={room["Id"]}\">Подробнее</a></div>");
        //            //    }
        //            //    break;

        //    }
        //    return new HtmlString((sb.ToString()));
        //}

        public static SuitableEstate Deserialize(string Source)
        {
            return JsonConvert.DeserializeObject<SuitableEstate>(Source);
        }

        public static HtmlString Render(SuitableEstate objects, EstateTypes Type)
        {
            var sb = new StringBuilder("<div id=\"bodyResult\">");
            //var DTO = JsonConvert.DeserializeObject<List<ShortPassport>>(Source);
            //var objects = JsonConvert.DeserializeObject<SuitableEstate>(Source);//new SuitableEstate(DTO)
            

            switch (objects.EstateType)
            {
                case EstateTypes.Flat:
                    foreach (var floor in objects)
                    {                        
                        sb.Append(CommonStart(floor));
                        sb.Append($"<div class=\"col-lg-2\"><h1>{floor["Rooms"]}-комнатная</h1><br/>{floor["HouseMaterial"]}<br/>{floor["HouseType"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{floor["Area"]} м²</h1><br />кухня {floor["KitchenArea"]}<br />жилая {floor["LivingArea"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{floor["Floor"]} этаж из {floor["FloorCount"]}</h1><br />{floor["Description"]}</div>");       
                        sb.Append(CommonEnd(floor));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={floor["Id"]}\">Подробнее</a></div>");
                    }
                    break;

                case EstateTypes.Room:
                    foreach (var room in objects)
                    {
                        sb.Append(CommonStart(room));
                        sb.Append($"<div class=\"col-lg-2\">{room["HouseMaterial"]}<br/>{room["HouseType"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{room["Area"]} м²<br />кухня {room["KitchenArea"]}<br /></div>");
                        sb.Append($"<div class=\"col-lg-1\">{room["Floor"]} этаж из {room["FloorCount"]}<br />{room["Description"]}</div>");
                        sb.Append(CommonEnd(room));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={room["Id"]}\">Подробнее</a></div>");
                    }
                    break;

                case EstateTypes.Garage:
                    foreach (var garage in objects)
                    {                    
                        sb.Append(CommonStart(garage));
                        sb.Append($"<div class=\"col-lg-3\">Гараж<br />материал: {garage["HouseMaterial"]}</div>");
                        sb.Append($"<div class=\"col-lg-3\">{garage["Area"]:###.##} м²</div>");
                        sb.Append(CommonEnd(garage));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={garage["Id"]}\">Подробнее</a></div>");
                    }
                    break;

                case EstateTypes.House:
                    foreach (var house in objects)
                    {
                        sb.Append(CommonStart(house));
                        sb.Append($"<div class=\"col-lg-2\">{house["Rooms"]}-комн.<br />материал: {house["HouseMaterial"]}<br/>состояние: {house["State"]}<br/>отопление: {house["Heating"]}<br/>вода: {house["Water"]}<br/>электричество: {house["Electricy"]}<br/>канализация: {house["Sewer"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{house["Area"]:###.##} м²<br />кухня: {house["KitchenArea"]} м²<br/>жилая: {house["LivingArea"]} м²</div>");
                        sb.Append($"<div class=\"col-lg-1\">{house["FloorCount"]}<br />балкон: {house["BalconyIsPresent"]}<br/>санузел: {house["WC"]}</div>");
                        sb.Append(CommonEnd(house));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={house["Id"]}\">Подробнее</a></div>");
                    }
                    break;

                case EstateTypes.Land:
                    //var lands = JsonConvert.DeserializeObject<SuitableEstate>(Source);
                    foreach (var land in objects)
                    {                    
                        sb.Append(CommonStart(land));
                        sb.Append($"<div class=\"col-lg-3\">{land["Area"]:###.##} м²<br /><br/>отопление: {land["Heating"]}<br/>вода: {land["Water"]}<br/>электричество: {land["Electricy"]}<br/>канализация: {land["Sewer"]}</div>");
                        sb.Append($"<div class=\"col-lg-3\">{land["Purpose"]}<br />{land["Category"]}<br />{land["Specifics"]}</div>");
                        sb.Append(CommonEnd(land));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={land["Id"]}\">Подробнее</a></div>");
                    }
                    break;

                case EstateTypes.Office:
                    foreach (var office in objects)
                    {                        
                        sb.Append(CommonStart(office));
                        sb.Append($"<div class=\"col-lg-3\">{office["Area"]:###.##} м²<br />материал: {office["HouseMaterial"]}<br />состояние: {office["State"]}</div>");
                        sb.Append($"<div class=\"col-lg-3\">{office["FloorCount"]}<br /><br/>отопление: {office["Heating"]}<br/>вода: {office["Water"]}<br/>электричество: {office["Electricy"]}<br/>канализация: {office["Sewer"]}</div>");
                        sb.Append(CommonEnd(office));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={office["Id"]}\">Подробнее</a></div>");
                    }
                    break;

                default:
                    throw new ArgumentException("Указан некорректный тип недвижимости");
            }            
            sb.Append($"</div>");
            return new HtmlString(sb.ToString());
        }

        public static string CommonStart(ShortPassport Obj)
        {
            //TODO: Список фотографий объекта!!!11

            DateTime date = (DateTime?)Obj["Date"] ?? DateTime.MinValue;
            string DateToShow = (date != DateTime.MinValue) ? date.ToString("d MMM yy") : "н/д";

            return $"<div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{Obj["Address"]}<br/>{Obj["City"]}</span></h1></div>" +
                   $"<div class=\"col-lg-1\"><h1>{DateToShow}</h1><br />ID: {Obj["Id"]:0000000}</div>" +
                   $"<div class=\"col-lg-1\"><h1>{Obj["Price"]: ### ### ###}</h1><br />{Obj["PricePerSquare"]:### ###.##} ₽ / м²</div>";
        }

        public static string CommonEnd(ShortPassport Obj)
        {
            string s = (Obj.ContainsKey("Logo")) ?
                $"<img src =\"{Obj["Logo"]}\"><br/>" :
                "";
            return  $"{s}<div class=\"col-lg-2\">{Obj["Agency"]}<br/>{Obj["AgentPhone"]}</div>" +
                   "</div>";

        }
        public static HtmlString HeadResult(EstateTypes EstateType)
        {
            var sb = new StringBuilder("<div class=\"search-box-head\">");
            sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"#\">Адрес</a></div>");            
            sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
            sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
            switch (EstateType)
            {
                case EstateTypes.Flat:
                    sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
                    break;
                case EstateTypes.Room:
                    sb.Append($"<div class=\"col-lg-2\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
                    break;
                case EstateTypes.House:
                    sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
                    break;
                case EstateTypes.Garage:
                    sb.Append($"<div class=\"col-lg-3\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    break;
                case EstateTypes.Land:
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Этажей</label></div>");
                    break;
                case EstateTypes.Office:
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Этажей</label></div>");
                    break;
            }            
            sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div>");
            sb.Append($"</div>");
            return new HtmlString((sb.ToString()));
        }


        //Карточка объекта
        public static HtmlString FullResult(string Source)
        {              
           StringBuilder sb = new StringBuilder("<div>");
            var passport = JsonConvert.DeserializeObject<FullPassport>(Source);

            //---------------------//
            sb.Append($"<div class=\"search-result-section\"><p>Технические</p></div>");
            //---------------------//

            //Тип квартиры
            sb.Append($"<div class=\"row\"><div class=\"col-lg-4\"><p>Тип квартиры</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.FlatType}</p></div>");
            // Год постройки
            sb.Append($"<div class=\"col-lg-4\"><p>Год постройки</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BuildingYear}</p></div>");
            // Материал перекрытий
            sb.Append($"<div class=\"col-lg-4\"><p>Материал перекрытий</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.CellingMaterial}</p></div>");
            //Отопление
            sb.Append($"<div class=\"col-lg-4\"><p>Отопление</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Heating}</p></div>");
            //Водоснабжение
            sb.Append($"<div class=\"col-lg-4\"><p>Водоснабжение</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Water}</p></div>");            
            // Законность перепланировки 
            sb.Append($"<div class=\"col-lg-4\"><p>Законность перепланировки</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.ReplanningLegality}</p></div>");
            //Канализация
            sb.Append($"<div class=\"col-lg-4\"><p>Канализация</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Sewer}</p></div>");         
            //Этаж
            sb.Append($"<div class=\"col-lg-4\"><p>Этаж</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Sewer}</p></div>");
            //Этажность здания 
            sb.Append($"<div class=\"col-lg-4\"><p>Этажность здания </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.FloorsTotal}</p></div>");
            //Количество комнат 
            sb.Append($"<div class=\"col-lg-4\"><p>Количество комнат </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Rooms}</p></div>");
            //Количество уровней 
            sb.Append($"<div class=\"col-lg-4\"><p>Количество уровней </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Levels}</p></div>");
            //Общая площадь 
            sb.Append($"<div class=\"col-lg-4\"><p>Общая площадь </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.TotalSquare}</p></div>");
            //Жилая / полезная площадь 
            sb.Append($"<div class=\"col-lg-4\"><p>Жилая / полезная площадь </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.UsefulSquare}</p></div>");
            // Планировка комнат 
            sb.Append($"<div class=\"col-lg-4\"><p> Планировка комнат </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.RoomPlanning}</p></div>");
            //Полное описание 
            sb.Append($"<div class=\"col-lg-4\"><p>Полное описание </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.FullDescription}</p></div>");
            //Материал постройки 
            sb.Append($"<div class=\"col-lg-4\"><p>Материал постройки </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BuildingMaterial}</p></div>");
            // Тип дома 
            sb.Append($"<div class=\"col-lg-4\"><p> Тип дома </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BuildingType}</p></div>");
            //Застройщик
            sb.Append($"<div class=\"col-lg-4\"><p>Застройщик</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BuildingCompany}</p></div>");
            //Строительная готовность 
            sb.Append($"<div class=\"col-lg-4\"><p>Строительная готовность </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BuildingReady}</p></div>");
            //Новострой
            sb.Append($"<div class=\"col-lg-4\"><p>Новострой</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.IsNewBuilding}</p></div>");
            //Кровля
            sb.Append($"<div class=\"col-lg-4\"><p>Кровля</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Roof}</p></div>");
            //Подвал
            sb.Append($"<div class=\"col-lg-4\"><p>Подвал</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Basement}</p></div>");
            //Безопасность
            sb.Append($"<div class=\"col-lg-4\"><p>Безопасность</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Security}</p></div>");
            // Использование под нежилое 
            sb.Append($"<div class=\"col-lg-4\"><p>Использование под нежилое</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.UsingAsNonResidental}</p></div>");
            //Количество спален
            sb.Append($"<div class=\"col-lg-4\"><p>Количество спален</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Bedrooms}</p></div>");
            //Количество балконов
            sb.Append($"<div class=\"col-lg-4\"><p>Количество балконов</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Balconies}</p></div>");
            //Количество лоджий
            sb.Append($"<div class=\"col-lg-4\"><p>Количество лоджий</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Logias}</p></div>");
            //Количество эркеров
            sb.Append($"<div class=\"col-lg-4\"><p>Количество эркеров</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BayWindows}</p></div>");
            //Количество окон
            sb.Append($"<div class=\"col-lg-4\"><p>Количество окон</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Windows}</p></div>");
            //Количество фасадных окон
            sb.Append($"<div class=\"col-lg-4\"><p>Количество фасадных окон</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WindowsFacade}</p></div>");
            //Балкон / Лоджия
            sb.Append($"<div class=\"col-lg-4\"><p>Балкон / Лоджия</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.BalconyLogia}</p></div>");
            //Площадь кухни
            sb.Append($"<div class=\"col-lg-4\"><p>Площадь кухни</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.KitchenArea}</p></div>");
            //Площадь зала 
            sb.Append($"<div class=\"col-lg-4\"><p>Площадь зала</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.LivingRoomArea}</p></div>");
            //Расшифровка метража
            sb.Append($"<div class=\"col-lg-4\"><p>Расшифровка метража</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.MeterageExplanation}</p></div>");
            //Высота потолков
            sb.Append($"<div class=\"col-lg-4\"><p>Высота потолков</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.CellingHeight}</p></div>");
            //Расположение квартиры
            sb.Append($"<div class=\"col-lg-4\"><p>Расположение квартиры</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.FlatLocation}</p></div>");
            //Перепланировка
            sb.Append($"<div class=\"col-lg-4\"><p>Перепланировка</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Replanning}</p></div>");
            //Оценка состояния объекта
            sb.Append($"<div class=\"col-lg-4\"><p>Оценка состояния объекта</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.ObjectStateAssessment}</p></div>");
            //Подсобные помещения
            sb.Append($"<div class=\"col-lg-4\"><p>Подсобные помещения</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.UtilityRooms}</p></div>");
            //Расположение окон 
            sb.Append($"<div class=\"col-lg-4\"><p>Расположение окон </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WindowLocation}</p></div>");
            //Вид из окон
            sb.Append($"<div class=\"col-lg-4\"><p>Вид из окон</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WindowView}</p></div>");
            //Окна
            sb.Append($"<div class=\"col-lg-4\"><p>Окна</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WindowView}</p></div>");
            //Окна / состояние
            sb.Append($"<div class=\"col-lg-4\"><p>Окна / состояние</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WindowState}</p></div>");
            //Входная дверь
            sb.Append($"<div class=\"col-lg-4\"><p>Входная дверь</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.EntranceDoorMaterial}</p></div>");
            //Столярка / двери
            sb.Append($"<div class=\"col-lg-4\"><p>Столярка / двери</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Carpentry}</p></div>");
            //Пол
            sb.Append($"<div class=\"col-lg-4\"><p>Пол</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Floor}</p></div>");
            //Потолок
            sb.Append($"<div class=\"col-lg-4\"><p>Потолок</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Celling}</p></div>");
            //Стены
            sb.Append($"<div class=\"col-lg-4\"><p>Стены</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Walls}</p></div>");
            //Кухня
            sb.Append($"<div class=\"col-lg-4\"><p>Кухня</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Kitchen}</p></div>");
            //Описание кухни
            sb.Append($"<div class=\"col-lg-4\"><p>Описание кухни</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.KitchenDescription}</p></div>");
            //Санузел
            sb.Append($"<div class=\"col-lg-4\"><p>Санузел</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WC}</p></div>");
            //Санузел / описание
            sb.Append($"<div class=\"col-lg-4\"><p>Описание санузла</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.WCDescription}</p></div>");
            //Сантехника
            sb.Append($"<div class=\"col-lg-4\"><p>Сантехника</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Sanitary}</p></div>");
            //Трубы
            sb.Append($"<div class=\"col-lg-4\"><p>Трубы</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Tubes}</p></div>");
            //Телефон
            sb.Append($"<div class=\"col-lg-4\"><p>Телефон</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Phone}</p></div>");
            //Тамбур
            sb.Append($"<div class=\"col-lg-4\"><p>Тамбур</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Vestibule}</p></div>");
            //Расположение в объекте
            sb.Append($"<div class=\"col-lg-4\"><p>Расположение в объекте</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.LocationInObject}</p></div></div>");

            //---------------------//
            sb.Append($"<div class=\"search-result-section\"><p>Юридические</p></div>");
            //---------------------//

            //Количество собственников 
            sb.Append($"<div class=\"row\"><div class=\"col-lg-4\"><p>Количество собственников </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Owners}</p></div>");            
            //Вид собственности
            sb.Append($"<div class=\"col-lg-4\"><p>Вид собственности</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.OwningType}</p></div>");
            //Возможность прописки
            sb.Append($"<div class=\"col-lg-4\"><p>Возможность прописки</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.RegistrationPossiblity}</p></div>");
            //Доля собственника
            sb.Append($"<div class=\"col-lg-4\"><p>Доля собственника</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.OwnerPart}</p></div>");
            //Возможность ипотеки
            sb.Append($"<div class=\"col-lg-4\"><p>Возможность ипотеки</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.HypotecPossiblity}</p></div>");
            //Количество зарегистрированных
            sb.Append($"<div class=\"col-lg-4\"><p>Количество зарегистрированных</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.RegisteredDwellersCount}</p></div>");
            //Наличие отягощений
            sb.Append($"<div class=\"col-lg-4\"><p>Наличие отягощений</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Burdening}</p></div>");
            //Правоустанавливающие документы
            sb.Append($"<div class=\"col-lg-4\"><p>Правоустанавливающие документы</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.LegalDocuments}</p></div></div>");

            //----------------------//
            sb.Append($"<div class=\"search-result-section\"><p>Инфраструктурные</p></div>");
            //---------------------//

            //Двор
            sb.Append($"<div class=\"row\"><div class=\"col-lg-4\"><p>Двор</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.YardState}</p></div>");
            //Парковка машин
            sb.Append($"<div class=\"col-lg-4\"><p>Парковка машин</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Parking}</p></div></div>");

            //---------------------//
            sb.Append($"<div class=\"search-result-section\"><p>Эксплуатационные</p></div>");
            //---------------------//

            //Установлен счетчик газа 
            sb.Append($"<div class=\"row\"><div class=\"col-lg-4\"><p>Установлен счетчик газа </p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.MeterGas}</p></div>");
            //Установлен счетчик холодной воды
            sb.Append($"<div class=\"col-lg-4\"><p>Установлен счетчик холодной воды</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.MeterColdWater}</p></div>");
            //Установлен счетчик горячей воды
            sb.Append($"<div class=\"col-lg-4\"><p>Установлен счетчик горячей воды</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.MeterHotWater}</p></div>");
            //Установлен электрический счетчик
            sb.Append($"<div class=\"col-lg-4\"><p>Установлен электрический счетчик</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.MeterElectricy}</p></div>");
            //Освобождение
            sb.Append($"<div class=\"col-lg-4\"><p>Освобождение</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Release}</p></div>");
            //Проведен Интернет
            sb.Append($"<div class=\"col-lg-4\"><p>Проведен Интернет</p></div>");
            sb.Append($"<div class=\"col-lg-9\"><p>{passport.Internet}</p></div></div>");
            sb.Append("</div>");
            return new HtmlString((sb.ToString()));
        }



    }
}
