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
            var sb = new StringBuilder("<table>");
            switch (EstateType)
            {
                case EstateTypes.Flat:
                    var flats = JsonConvert.DeserializeObject<List<FlatPassport>>(Source);

                    
                    foreach (var flat in flats)
                    {
                        sb.Append(CommonStart(flat));
                        sb.Append($"<td>{flat.Rooms}-комнатаная<br/><font size=\"1\">{flat.HouseMaterial}<br/>{flat.HouseType}</font></td>");
                        sb.Append($"<td>{flat.Square} кв.м.<br /><font size=\"1\">кухня {flat.KitchenSquare}<br />жилая {flat.LivingSquare}</font></td>");
                        sb.Append($"<td>{flat.Floor} этаж из {flat.FloorCount}<br /><font size=\"1\">{flat.Balcony}<br />{flat.Description}</font></td>");
                        sb.Append(CommonEnd(flat));
                    }
                    break;

                case EstateTypes.Room:
                    var rooms = JsonConvert.DeserializeObject<List<RoomPassport>>(Source);
                    foreach (var room in rooms)
                    {
                        sb.Append(CommonStart(room));
                        sb.Append($"<td>{room.HouseMaterial}<br/><font size=\"1\">{room.HouseType}</font></td>");
                        sb.Append($"<td>{room.Square} кв.м.<br /><font size=\"1\">кухня {room.KitchenSquare}<br /></font></td>");
                        sb.Append($"<td>{room.Floor} этаж из {room.FloorCount}<br /><font size=\"1\">{room.Description}</font></td>");
                        sb.Append(CommonEnd(room));
                    }
                    break;

                case EstateTypes.Garage:
                    var garages = JsonConvert.DeserializeObject<List<GaragePassport>>(Source);
                    foreach (var garage in garages)
                    {
                        sb.Append(CommonStart(garage));
                        sb.Append($"<td>Гараж<br /><font size=\"1\">материал: {garage.HouseMaterial}</font></td>");
                        sb.Append($"<td>{garage.Square:###.##} кв. м</td>");
                        sb.Append(CommonEnd(garage));
                    }
                    break;

                case EstateTypes.House:
                    var houses = JsonConvert.DeserializeObject<List<HousePassport>>(Source);
                    foreach (var house in houses)
                    {
                        sb.Append(CommonStart(house));
                        sb.Append($"<td>{house.Rooms}-комн.<br /><font size=\"1\">материал: {house.HouseMaterial}<br/>состояние: {house.State}<br/>отопление: {house.Heating}<br/>вода: {house.Water}<br/>электричество: {house.Electricy}<br/>канализация: {house.Sewer}</font></td>");
                        sb.Append($"<td>{house.Square:###.##} кв. м<br /><font size=\"1\">кухня: {house.KitchenSquare} кв. м<br/>жилая: {house.LivingSquare} кв. м</font></td>");
                        sb.Append($"<td>{house.FloorCount}<br /><font size=\"1\">балкон: {house.BalconyIsPresent}<br/>санузел: {house.WC}</font></td>");
                        sb.Append(CommonEnd(house));
                    }
                    break;

                case EstateTypes.Land:
                    var lands = JsonConvert.DeserializeObject<List<LandPassport>>(Source);
                    foreach (var land in lands)
                    {
                        sb.Append(CommonStart(land));
                        sb.Append($"<td>{land.Square:###.##} кв. м<br /><font size=\"1\"><br/>отопление: {land.Heating}<br/>вода: {land.Water}<br/>электричество: {land.Electricy}<br/>канализация: {land.Sewer}</font></td>");
                        sb.Append($"<td>{land.Purpose}<br /><font size=\"1\">{land.Category}<br />{land.Specifics}</font></td>");
                        sb.Append(CommonEnd(land));
                    }
                    break;

                case EstateTypes.Office:
                    var offices = JsonConvert.DeserializeObject<List<OfficePassport>>(Source);
                    foreach (var office in offices)
                    {
                        sb.Append(CommonStart(office));
                        sb.Append($"<td>{office.Square:###.##} кв. м<br /><font size=\"1\">материал: {office.HouseMaterial}<br />состояние: {office.State}</font></td>");
                        sb.Append($"<td>{office.FloorCount}<br /><font size=\"1\"><br/>отопление: {office.Heating}<br/>вода: {office.Water}<br/>электричество: {office.Electricy}<br/>канализация: {office.Sewer}</font></td>");
                        sb.Append(CommonEnd(office));
                    }
                    break;

                default:
                    throw new ArgumentException("Указан некорректный тип недвижимости");
            }
            sb.Append("</table>");
            return new HtmlString(sb.ToString());
        }

        private static string CommonStart(ObjectPassport Obj)
        {
            //TODO: Список фотографий объекта!!!11

            DateTime date = Obj.Date ?? DateTime.MinValue;
            string DateToShow = (date != DateTime.MinValue) ? date.ToString("d MMM yy") : "н/д";

            return $"<tr><td>{Obj.Address}<br /><font size=\"1\">{Obj.City}</font></td>" +
                   $"<td>{DateToShow}<br /><font size=\"1\">ID: {Obj.Id:0000000}</font></td>" +
                   $"<td>{Obj.Price: ### ### ###}<br /><font size=\"1\">{Obj.PricePerSquareMetter:### ###.##} руб. / кв. м</font></td>";
        }

        private static string CommonEnd(ObjectPassport Obj)
        {
            //TODO: Логотип агентства!!11
            return $"<td><font size=\"2\">{Obj.Agency}<br/>{Obj.AgentPhone}</font></td>" +
                   "</tr>";

        }
    }
}
