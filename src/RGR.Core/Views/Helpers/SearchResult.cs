using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;
using RGR.Core.Controllers;
using RGR.Core.Controllers.Enums;

namespace RGR.Core.Views.Helpers
{
    public class SearchResult
    {
        
        public static HtmlString Render(string Source, EstateTypes EstateType)
        {
            var sb = new StringBuilder("<div>");
            switch (EstateType)
            {
                case EstateTypes.Flat:
                    var flats = JsonConvert.DeserializeObject<List<FlatPassport>>(Source);

                    //sb.Append($"<div class=\"row\" id=\"head\"><div class=\"col-lg-2\">Адрес</div>");
                    //sb.Append($"<div class=\"col-lg-1\">Дата</div>");
                    //sb.Append($"<div class=\"col-lg-1\">Цена</div>");
                    //sb.Append($"<div class=\"col-lg-1\">Комнаты</div>");
                    //sb.Append($"<div class=\"col-lg-1\">Площадь</div>");
                    //sb.Append($"<div class=\"col-lg-1\">Этажность</div>");
                    //sb.Append($"<div class=\"col-lg-1\"> </div></div>");

                    foreach (var flat in flats)
                    {                        
                        sb.Append(CommonStart(flat));
                        sb.Append($"<div class=\"col-lg-2\"><h1>{flat.Rooms}-комнатная<br/>{flat.HouseMaterial}<br/>{flat.HouseType}</h1></div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Square} кв.м.<br />кухня {flat.KitchenSquare}<br />жилая {flat.LivingSquare}</h1></div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Floor} этаж из {flat.FloorCount}<br />{flat.Balcony}<br />{flat.Description}</h1></div>");
                        sb.Append(CommonEnd(flat));
                    }
                    break;

                case EstateTypes.Room:
                    var rooms = JsonConvert.DeserializeObject<List<RoomPassport>>(Source);
                    foreach (var room in rooms)
                    {
                        sb.Append(CommonStart(room));
                        sb.Append($"<div class=\"col-lg-2\">{room.HouseMaterial}<br/>{room.HouseType}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{room.Square} кв.м.<br />кухня {room.KitchenSquare}<br /></div>");
                        sb.Append($"<div class=\"col-lg-1\">{room.Floor} этаж из {room.FloorCount}<br />{room.Description}</div>");
                        sb.Append(CommonEnd(room));
                    }
                    break;

                case EstateTypes.Garage:
                    var garages = JsonConvert.DeserializeObject<List<GaragePassport>>(Source);
                    foreach (var garage in garages)
                    {                    
                        sb.Append(CommonStart(garage));
                        sb.Append($"<div class=\"col-lg-2\">Гараж<br />материал: {garage.HouseMaterial}</div>");
                        sb.Append($"<div class=\"col-lg-2\">{garage.Square:###.##} кв. м</div>");
                        sb.Append(CommonEnd(garage));
                    }
                    break;

                case EstateTypes.House:
                    var houses = JsonConvert.DeserializeObject<List<HousePassport>>(Source);
                    foreach (var house in houses)
                    {
                        sb.Append(CommonStart(house));
                        sb.Append($"<div class=\"col-lg-2\">{house.Rooms}-комн.<br />материал: {house.HouseMaterial}<br/>состояние: {house.State}<br/>отопление: {house.Heating}<br/>вода: {house.Water}<br/>электричество: {house.Electricy}<br/>канализация: {house.Sewer}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{house.Square:###.##} кв. м<br />кухня: {house.KitchenSquare} кв. м<br/>жилая: {house.LivingSquare} кв. м</div>");
                        sb.Append($"<div class=\"col-lg-1\">{house.FloorCount}<br />балкон: {house.BalconyIsPresent}<br/>санузел: {house.WC}</div>");
                        sb.Append(CommonEnd(house));
                    }
                    break;

                case EstateTypes.Land:
                    var lands = JsonConvert.DeserializeObject<List<LandPassport>>(Source);
                    foreach (var land in lands)
                    {                    
                        sb.Append(CommonStart(land));
                        sb.Append($"<div class=\"col-lg-2\">{land.Square:###.##} кв. м<br /><br/>отопление: {land.Heating}<br/>вода: {land.Water}<br/>электричество: {land.Electricy}<br/>канализация: {land.Sewer}</div>");
                        sb.Append($"<div class=\"col-lg-2\">{land.Purpose}<br />{land.Category}<br />{land.Specifics}</div>");
                        sb.Append(CommonEnd(land));
                    }
                    break;

                case EstateTypes.Office:
                    var offices = JsonConvert.DeserializeObject<List<OfficePassport>>(Source);
                    foreach (var office in offices)
                    {                        
                        sb.Append(CommonStart(office));
                        sb.Append($"<div class=\"col-lg-2\">{office.Square:###.##} кв. м<br />материал: {office.HouseMaterial}<br />состояние: {office.State}</div>");
                        sb.Append($"<div class=\"col-lg-2\">{office.FloorCount}<br /><br/>отопление: {office.Heating}<br/>вода: {office.Water}<br/>электричество: {office.Electricy}<br/>канализация: {office.Sewer}</div>");
                        sb.Append(CommonEnd(office));
                    }
                    break;

                default:
                    throw new ArgumentException("Указан некорректный тип недвижимости");
            }
            sb.Append("</div>");
            return new HtmlString(sb.ToString());
        }

        private static string CommonStart(ObjectPassport Obj)
        {
            //TODO: Список фотографий объекта!!!11

            DateTime date = Obj.Date ?? DateTime.MinValue;
            string DateToShow = (date != DateTime.MinValue) ? date.ToString("d MMM yy") : "н/д";

            return $"<div class=\"row\"><div class=\"col-lg-3\"><h1><span>{Obj.Address}</span><br /><span>{Obj.City}</span></h1></div>" +
                   $"<div class=\"col-lg-1\"><h1>{DateToShow}</h1><br />ID: {Obj.Id:0000000}</div>" +
                   $"<div class=\"col-lg-1\"><h1>{Obj.Price: ### ### ###}</h1><br />{Obj.PricePerSquareMetter:### ###.##} руб. / кв. м</div>";
        }

        private static string CommonEnd(ObjectPassport Obj)
        {
            //TODO: Логотип агентства!!11
            return  $"<div class=\"col-lg-2\">{Obj.Agency}<br/>{Obj.AgentPhone}</div>" +
                   "</div>";

        }
        public static HtmlString HeadResult(EstateTypes EstateType)
        {
            var sb = new StringBuilder("<div class=\"row\">");
            sb.Append($"<div class=\"col-lg-3\">Адрес</div>");
            sb.Append($"<div class=\"col-lg-1\">Дата</div>");
            sb.Append($"<div class=\"col-lg-1\">Цена</div>");
            if (EstateType == EstateTypes.Flat && EstateType==EstateTypes.House)
            {
                sb.Append($"<div class=\"col-lg-3\">Комнаты</div>");
                sb.Append($"<div class=\"col-lg-1\">Площадь</div>");
                sb.Append($"<div class=\"col-lg-1\">Этажность</div>");
            }
            else if (EstateType==EstateTypes.Room) {
                sb.Append($"<div class=\"col-lg-2\">Тип</div>");
                sb.Append($"<div class=\"col-lg-1\">Площадь</div>");
                sb.Append($"<div class=\"col-lg-1\">Этажность</div>");
            }
            else if (EstateType == EstateTypes.Garage)
            {
                sb.Append($"<div class=\"col-lg-1\">Тип</div>");
                sb.Append($"<div class=\"col-lg-1\">Площадь</div>");
            }
            else if (EstateType == EstateTypes.Land && EstateType == EstateTypes.Office)
            {
                sb.Append($"<div class=\"col-lg-1\">Площадь</div>");
                sb.Append($"<div class=\"col-lg-1\">Этажей</div>");
            }
            sb.Append($"<div class=\"col-lg-2\"></br></div>");
            sb.Append($"</div>");

            return new HtmlString((sb.ToString()));
            //switch (EstateType)
            //{
            //    case EstateTypes.Flat:
            //        return $"<div class=\"col-lg-3\">Комнаты</div>" +
            //        $"<div class=\"col-lg-1\">Площадь</div>" +
            //        $"<div class=\"col-lg-1\">Этажность</div>";                    
            //    case EstateTypes.Room:
            //        return $"<div class=\"col-lg-2\">Тип</div>" +
            //            $"<div class=\"col-lg-1\">Площадь</div>" +
            //            $"<div class=\"col-lg-1\">Этажность</div>";
            //    case EstateTypes.House:
            //        return $"<div class=\"col-lg-2\">Комнаты</div>" +
            //            $"<div class=\"col-lg-1\">Площадь</div>" +
            //            $"<div class=\"col-lg-1\">Этажность</div>";
            //}
        }

    }
}
