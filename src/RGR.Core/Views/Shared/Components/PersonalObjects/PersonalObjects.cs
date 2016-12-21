using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using NUglify.Html;
using RGR.Core.Common;
using RGR.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RGR.Core.Views.Shared.Components
{
    public class PersonalObjects : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<ShortPassport> Source)
        {
            var grouped1 = Source.GroupBy(l => l["TypeId"]);
            var grouped = grouped1.Select(l => l.ToList());

            return View(await Task.Run(() => Render(grouped)));
        }


        // Вкладка Комната
        public static string StringBuilderRoom(List<ShortPassport> rooms)
        {
            var sb = new StringBuilder("<div id = \"typeObj1\" class=\"tab-pane in active \">");
            if (rooms != null)
            {
                sb.Append($"<h5> Новых совпадений по объектам: <b>{(rooms != null ? rooms.Count() : 0)} </b></h5>");
                foreach (var room in rooms)
                {
                    sb.Append($"<div class=\"object-conteiner\"><div class=\"account-box-head\">");
                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\">Адрес</div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div></div>");

                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1>{room["Address"]}<br/>{room["City"]}</h1></div>");
                    //sb.Append($"< img src =\"{room["Logo"]}\"></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{room["Date"]}</h1><br /><font>ID: {room["Id"]:0000000}</font></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{room["Price"]: ### ### ###}  <font class=\"ruble\">₽</font> </h1><br /><font>{room["PricePerSquare"]:### ###.##}  <font class=\"ruble\">₽</font>  / м²</font></div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1></h1><br /><font>дом: </font>{room["HouseMaterial"]}<br/><font>тип: </font>{room["HouseType"]}<br/><font>состояние: </font>{room["State"]}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{room["Area"]} м²</h1><br /><font>кухня: </font> {room["KitchenArea"]} м²</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{room["Floor"]} этаж из {room["FloorCount"]}</h1><br /><font>балкон: </font>{room["Balcony"]}<br/><font>с/у: </font> {room["WC"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{room["Agency"]}</h1><br/>{room["AgentPhone"]}</div></div>");
                    sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={room["Id"]}\">Подробнее</a></div></div>");
                }
                sb.Append($"</div>");
                return sb.ToString();
            }
            else
            {
                sb.Append($"<h4>У вас пока нет не одного объекта!</h4></div>");
                return sb.ToString();
            }
        }
        // Вкладка Квартира (id=4)
        public static string StringBuilderFlat(List<ShortPassport> flats)
        {
            var sb = new StringBuilder("<div id = \"typeObj2\" class=\"tab-pane \">");
            if (flats != null)
            {
                sb.Append($"<h5> Новых совпадений по объектам: <b>{(flats != null ? flats.Count() : 0)} </b ></h5>");
                foreach (var flat in flats)
                {
                    DateTime date = (DateTime?)flat["Date"] ?? DateTime.MinValue;
                    string DateToShow = (date != DateTime.MinValue) ? date.ToString("dd.MM.yyyy") : "н/д";

                    sb.Append($"<div class=\"object-conteiner\"><div class=\"account-box-head\">");
                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\">Адрес</div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div></div>");

                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1>{flat["Address"]}<br/>{flat["City"]}</h1></div>");
                    //sb.Append($"< img src =\"{flat["Logo"]}\"></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{DateToShow}</h1><br /><font>ID: {flat["Id"]:0000000}</font></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Price"]: ### ### ###}  <font class=\"ruble\">₽</font> </h1><br /><font>{flat["PricePerSquare"]:### ###.##}  <font class=\"ruble\">₽</font>  / м²</font></div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{flat["Rooms"]}-комнатная</h1><br/><font>дом:</font> {flat["HouseMaterial"]}<br/><font>материал:</font> {flat["HouseType"]}<br/><font>состояние:</font> {flat["State"]}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Area"]} м²</h1><br /><font>кухня:</font> {flat["KitchenArea"]} м²<br /><font>жилая:</font> {flat["LivingArea"]} м²</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Floor"]} этаж из {flat["FloorCount"]}</h1><br/><font>балкон: </font>{flat["Balcony"]}<br/><font>комнаты: </font> {flat["Rooms"]}<br/><font>с/у: </font> {flat["WC"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{flat["Agency"]}</h1><br/>{flat["AgentPhone"]: # ### ###}</div></div>");
                    sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={flat["Id"]}\">Подробнее</a></div></div>");
                }
                sb.Append($"</div>");
                return sb.ToString();
            }
            else
            {
                sb.Append($"<h4>У вас пока нет не одного объекта!</h4></div>");
                return sb.ToString();
            }
        }

        // Вкладка Дом (id=1)
        public static string StringBuilderHouse(List<ShortPassport> houses)
        {
            var sb = new StringBuilder("<div id = \"typeObj3\" class=\"tab-pane\">");
            if (houses != null)
            {
                sb.Append($"< h5 > Новых совпадений по объектам: <b>{(houses != null ? houses.Count() : 0)} </ b ></ h5 >");
                foreach (var house in houses)
                {
                    sb.Append($"<div class=\"object-conteiner\"><div class=\"search-box-head\">");
                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"#\">Адрес</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div></div>");

                    sb.Append($"< div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{house["Address"]}<br/>{house["City"]}</span></h1></div>");
                    //sb.Append($"< img src =\"{house["Logo"]}\"></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{house["DateToShow"]}</h1><br /><font>ID: <font>{house["Id"]:0000000}</font></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{house["Price"]: ### ### ###}  <font class=\"ruble\">₽</font> </h1><br /><font>{house["PricePerSquare"]:### ###.##}  <font class=\"ruble\">₽</font>  / м²</font></div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{house["Rooms"]}-комн.</h1<br /><font>дом:</font> {house["HouseMaterial"]}<br/><font>состояние: </font> {house["State"]}<br/><font>отопление: </font>{house["Heating"]}<br/><font>вода: </font> {house["Water"]}<br/><font>электричество: </font> {house["Electricy"]}<br/><font>канализация: </font> {house["Sewer"]}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{house["Area"]:###.##} м²</h1<br /><font>кухня: <.font> {house["KitchenArea"]} м²<br/><font>жилая: </font> {house["LivingArea"]} м²</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{house["FloorCount"]}</h1<br /><font>балкон: </font>{house["Balcony"]}<br/><font>комнаты: </font> {house["Rooms"]}<br/><font>с/у: </font> {house["WC"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{house["Agency"]}</h1><br/>{house["AgentPhone"]}</div></div>");
                    sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={house["Id"]}\">Подробнее</a></div></div>");
                }
                sb.Append($"</div>");
                return sb.ToString();

            }
            else
            {
                sb.Append($"<h4>У вас пока нет не одного объекта!</h4></div>");
                return sb.ToString();
            }


        }

        // Вкладка Гараж (id=3)
        public static string StringBuilderGarage(List<ShortPassport> garages)
        {
            var sb = new StringBuilder("<div id = \"typeObj4\" class=\"tab-pane\">");
            if (garages != null)
            {
                sb.Append($"< h5 > Новых совпадений по объектам: <b>{(garages != null ? garages.Count() : 0)} </ b ></ h5 >");
                foreach (var garage in garages)
                {
                    sb.Append($"<div class=\"object-conteiner\"><div class=\"account-box-head\">");
                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"#\">Адрес</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div></div>");

                    sb.Append($"< div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{garage["Address"]}<br/>{garage["City"]}</span></h1>");
                    sb.Append($"< img src =\"{garage["Logo"]}\"></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{garage["DateToShow"]}</h1><br /><font>ID: {garage["Id"]:0000000}</font></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{garage["Price"]: ### ### ###}  <font class=\"ruble\">₽</font> </h1><br /><font>{garage["PricePerSquare"]:### ###.##}  <font class=\"ruble\">₽</font>  / м²</font></div>");
                    sb.Append($"<div class=\"col-lg-3\"><h1>Гараж</h1><br /><font>материал:</font> {garage["garageMaterial"]}</div>");
                    sb.Append($"<div class=\"col-lg-3\"><h1>{garage["Area"]:###.##} м²</h1></div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{garage["Agency"]}</h1><br/>{garage["AgentPhone"]}</div></div>");
                    sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={garage["Id"]}\">Подробнее</a></div></div>");

                }
                sb.Append($"</div>");
                return sb.ToString();
            }
            else
            {
                sb.Append($"<h4>У вас пока нет не одного объекта!</h4></div>");
                return sb.ToString();
            }
        }

        // Вкладка Участок (id=2)
        public static string StringBuilderLand(List<ShortPassport> lands)
        {
            var sb = new StringBuilder("<div id = \"typeObj5\" class=\"tab-pane\">");
            if (lands != null)
            {
                sb.Append($"< h5 > Новых совпадений по объектам: <b>{(lands != null ? lands.Count() : 0)} </ b ></ h5 >");
                foreach (var land in lands)
                {
                    sb.Append($"<div class=\"object-conteiner\"><div class=\"account-box-head\">");
                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"#\">Адрес</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Этажей</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div></div>");

                    sb.Append($"< div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{land["Address"]}<br/>{land["City"]}</span></h1>");
                    sb.Append($"< img src =\"{land["Logo"]}\"></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{land["DateToShow"]}</h1><br /><font>ID: {land["Id"]:0000000}</font></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{land["Price"]: ### ### ###} <font class=\"ruble\">₽</font> </h1><br /><font>{land["PricePerSquare"]:### ###.##}  <font class=\"ruble\">₽</font>  / м²</font></div>");
                    sb.Append($"<div class=\"col-lg-3\"><h1>{land["Area"]:###.##} м²</h1><br /><br/><font>отопление: </font>{land["Heating"]}<br/><font>вода: </font> {land["Water"]}<br/><font>электричество: </font>{land["Electricy"]}<br/><font>канализация: </font>{land["Sewer"]}</div>");
                    sb.Append($"<div class=\"col-lg-3\"><h1>{land["Purpose"]}</h1><br /><font>категория: </font>{land["Category"]}<br /><font>особенности расположения: </font>{land["Specifics"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{land["Agency"]}</h1><br/>{land["AgentPhone"]}</div></div>");
                    sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={land["Id"]}\">Подробнее</a></div></div>");
                }
                sb.Append($"</div>");
                return sb.ToString();
            }
            else
            {
                sb.Append($"<h4>У вас пока нет не одного объекта!</h4></div>");
                return sb.ToString();
            }
        }

        // Вкладка Для бизнеса (id=5)
        public static string StringBuilderOffice(List<ShortPassport> offices)
        {
            var sb = new StringBuilder("<div id = \"typeObj6\" class=\"tab-pane\">");
            if (offices != null)
            {
                sb.Append($"< h5 >Новых совпадений по объектам: <b>{(offices != null ? offices.Count() : 0)} </ b ></ h5 >");
                foreach (var office in offices)
                {
                    sb.Append($"<div class=\"object-conteiner\"><div class=\"account-box-head\">");
                    sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"#\">Адрес</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Этажей</label></div>");
                    sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div></div>");

                    sb.Append($"< div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{office["Address"]}<br/>{office["City"]}</span></h1>");
                    sb.Append($"< img src =\"{office["Logo"]}\"></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{office["DateToShow"]}</h1><br /><font>ID: {office["Id"]:0000000}</font></div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{office["Price"]: ### ### ###}  <font class=\"ruble\">₽</font> </h1><br /><font>{office["PricePerSquare"]:### ###.##}  <font class=\"ruble\">₽</font>  / м²</font></div>");
                    sb.Append($"<div class=\"col-lg-3\"><h1>{office["Area"]:###.##} м²</h1><br /><font>дом: </font>{office["HouseMaterial"]}<br /><font>состояние: </font>{office["State"]}</div>");
                    sb.Append($"<div class=\"col-lg-3\"><h1>{office["FloorCount"]}</h1><br /><br/><font>отопление: /font>{office["Heating"]}<br/><font>вода: </font>{office["Water"]}<br/><font>электричество: </font>{office["Electricy"]}<br/><font>канализация: </font>{office["Sewer"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{office["Agency"]}</h1><br/>{office["AgentPhone"]}</div></div>");
                    sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={office["Id"]}\">Подробнее</a></div></div>");
                }
                sb.Append($"</div>");
                return sb.ToString();
            }
            else
            {
                sb.Append($"<h4>У вас пока нет не одного объекта!</h4></div>");
                return sb.ToString(); ;
            }
        }


        public static HtmlString Render(IEnumerable<List<ShortPassport>> grouped)
        {

            if (grouped == null)
            {
                StringBuilder sb = new StringBuilder("<h4>У вас пока нет не одного объекта!</h4>");
                return new HtmlString(sb.ToString());
            }
            else
            {
                var sb = new StringBuilder("<div id=\"bodyResult\"><div class=\"tab-content\">");
                var rooms = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Room));
                var flats = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Flat));
                var houses = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.House));
                var lands = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Land));
                var garages = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Garage));
                var offices = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Office));
                sb.Append(StringBuilderRoom(rooms));
                sb.Append(StringBuilderHouse(houses));
                sb.Append(StringBuilderLand(lands));
                sb.Append(StringBuilderGarage(garages));
                sb.Append(StringBuilderFlat(flats));
                sb.Append(StringBuilderOffice(offices));
                sb.Append($"</div></div>");
                return new HtmlString(sb.ToString());
            }

        }

        public static int StatisticsObject(IEnumerable<ShortPassport> Source, int flag)
        {
            int count;
            var grouped1 = Source.GroupBy(l => l["Type"]);
            var grouped = grouped1.Select(l => l.ToList());
            if (grouped == null)
            {
                return 0;
            }
            else
            {
                var rooms = grouped.FirstOrDefault(g => g.Any(f => (long)f ["TypeId"] == (long)EstateTypes.Room));
                var flats = grouped.FirstOrDefault(g => g.Any(f => (long)f  ["TypeId"] == (long)EstateTypes.Flat));
                var houses = grouped.FirstOrDefault(g => g.Any(f => (long)f ["TypeId"] == (long)EstateTypes.House));
                var lands = grouped.FirstOrDefault(g => g.Any(f => (long)f  ["TypeId"] == (long)EstateTypes.Land));
                var garages = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Garage));
                var offices = grouped.FirstOrDefault(g => g.Any(f => (long)f["TypeId"] == (long)EstateTypes.Office));                
                switch (flag)
                {
                    case 1:
                        count = (rooms?.Count() ?? 0) + (flats?.Count() ?? 0);
                        return count;
                    case 2:
                        count = houses?.Count() ?? 0;
                        return count;
                    case 3:
                        count = garages?.Count() ?? 0;
                        return count;
                    case 4:
                        count = lands?.Count() ?? 0;
                        return count;
                    case 5:
                        count = offices?.Count() ?? 0;
                        return count;
                    default:
                        throw new ArgumentException("Указан некорректный тип недвижимости");
                }
            }

        }
    }
}


        
    


