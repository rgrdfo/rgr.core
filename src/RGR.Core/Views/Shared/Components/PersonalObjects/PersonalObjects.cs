using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using NUglify.Html;
using RGR.Core.Common;
using RGR.Core.Controllers.Enums;
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
            var grouped1 = Source.GroupBy(l => l["Type"]);
            var grouped = grouped1.Select(l => l.ToList());

            return View(await Task.Run(() => Render(grouped)));
        }


        // Вкладка Комната
        public static string StringBuilderRoom(List<ShortPassport> rooms)
        {
            var sb = new StringBuilder("<div id = \"typeObj1\" class=\"tab-pane in active \">");
            if (rooms != null)
            {
                sb.Append($"<h5><b> Новых совпадений по объектам:{(rooms != null ? rooms.Count() : 0)} </b ></h5>");
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
                    sb.Append($"<div class=\"col-lg-1\"><h1>{room["Date"]}</h1><br />ID: {room["Id"]:0000000}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{room["Price"]: ### ### ###} ₽</h1><br />{room["PricePerSquare"]:### ###.##} ₽ / м²</div>");
                    sb.Append($"<div class=\"col-lg-2\">{room["HouseMaterial"]}<br/>{room["HouseType"]}</div>");
                    sb.Append($"<div class=\"col-lg-1\">{room["Area"]} м²<br />кухня: {room["KitchenArea"]} м²<br /></div>");
                    sb.Append($"<div class=\"col-lg-1\">{room["Floor"]} этаж из {room["FloorCount"]}<br />{room["Description"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\">{room["Agency"]}<br/>{room["AgentPhone"]}</div></div>");
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
                sb.Append($"<h5><b> Новых совпадений по объектам:{(flats != null ? flats.Count() : 0)} </b ></h5>");
                foreach (var flat in flats)
                {
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
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Date"]}</h1><br />ID: {flat["Id"]:0000000}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Price"]: ### ### ###} ₽</h1><br />{flat["PricePerSquare"]:### ###.##} ₽ / м²</div>");
                    sb.Append($"<div class=\"col-lg-2\"><h1>{flat["Rooms"]}-комнатная</h1><br/>{flat["HouseMaterial"]}<br/>{flat["HouseType"]}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Area"]} м²</h1><br />кухня: {flat["KitchenArea"]} м²<br />жилая: {flat["LivingArea"]} м²</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{flat["Floor"]} этаж из {flat["FloorCount"]}</h1><br />{flat["Description"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\">{flat["Agency"]}<br/>{flat["AgentPhone"]}</div></div>");
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
                sb.Append($"< h5 >< b > Новых совпадений по объектам:{(houses != null ? houses.Count() : 0)} </ b ></ h5 >");
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
                    sb.Append($"<div class=\"col-lg-1\"><h1>{house["DateToShow"]}</h1><br />ID: {house["Id"]:0000000}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{house["Price"]: ### ### ###} ₽</h1><br />{house["PricePerSquare"]:### ###.##} ₽ / м²</div>");
                    sb.Append($"<div class=\"col-lg-2\">{house["Rooms"]}-комн.<br />материал: {house["HouseMaterial"]}<br/>состояние: {house["State"]}<br/>отопление: {house["Heating"]}<br/>вода: {house["Water"]}<br/>электричество: {house["Electricy"]}<br/>канализация: {house["Sewer"]}</div>");
                    sb.Append($"<div class=\"col-lg-1\">{house["Area"]:###.##} м²<br />кухня: {house["KitchenArea"]} м²<br/>жилая: {house["LivingArea"]} м²</div>");
                    sb.Append($"<div class=\"col-lg-1\">{house["FloorCount"]}<br />балкон: {house["BalconyIsPresent"]}<br/>санузел: {house["WC"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\">{house["Agency"]}<br/>{house["AgentPhone"]}</div></div>");
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
                sb.Append($"< h5 >< b > Новых совпадений по объектам:{(garages != null ? garages.Count() : 0)} </ b ></ h5 >");
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
                    sb.Append($"<div class=\"col-lg-1\"><h1>{garage["DateToShow"]}</h1><br />ID: {garage["Id"]:0000000}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{garage["Price"]: ### ### ###} ₽</h1><br />{garage["PricePerSquare"]:### ###.##} ₽ / м²</div>");
                    sb.Append($"<div class=\"col-lg-3\">Гараж<br />материал: {garage["garageMaterial"]}</div>");
                    sb.Append($"<div class=\"col-lg-3\">{garage["Area"]:###.##} м²</div>");
                    sb.Append($"<div class=\"col-lg-2\">{garage["Agency"]}<br/>{garage["AgentPhone"]}</div></div>");
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
                sb.Append($"< h5 >< b > Новых совпадений по объектам:{(lands != null ? lands.Count() : 0)} </ b ></ h5 >");
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
                    sb.Append($"<div class=\"col-lg-1\"><h1>{land["DateToShow"]}</h1><br />ID: {land["Id"]:0000000}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{land["Price"]: ### ### ###} ₽</h1><br />{land["PricePerSquare"]:### ###.##} ₽ / м²</div>");
                    sb.Append($"<div class=\"col-lg-3\">{land["Area"]:###.##} м²<br /><br/>отопление: {land["Heating"]}<br/>вода: {land["Water"]}<br/>электричество: {land["Electricy"]}<br/>канализация: {land["Sewer"]}</div>");
                    sb.Append($"<div class=\"col-lg-3\">{land["Purpose"]}<br />{land["Category"]}<br />{land["Specifics"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\">{land["Agency"]}<br/>{land["AgentPhone"]}</div></div>");
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
                sb.Append($"< h5 >< b > Новых совпадений по объектам:{(offices != null ? offices.Count() : 0)} </ b ></ h5 >");
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
                    sb.Append($"<div class=\"col-lg-1\"><h1>{office["DateToShow"]}</h1><br />ID: {office["Id"]:0000000}</div>");
                    sb.Append($"<div class=\"col-lg-1\"><h1>{office["Price"]: ### ### ###} ₽</h1><br />{office["PricePerSquare"]:### ###.##} ₽ / м²</div>");
                    sb.Append($"<div class=\"col-lg-3\">{office["Area"]:###.##} м²<br />материал: {office["HouseMaterial"]}<br />состояние: {office["State"]}</div>");
                    sb.Append($"<div class=\"col-lg-3\">{office["FloorCount"]}<br /><br/>отопление: {office["Heating"]}<br/>вода: {office["Water"]}<br/>электричество: {office["Electricy"]}<br/>канализация: {office["Sewer"]}</div>");
                    sb.Append($"<div class=\"col-lg-2\">{office["Agency"]}<br/>{office["AgentPhone"]}</div></div>");
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
                var rooms = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Room));
                var flats = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Flat));
                var houses = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.House));
                var lands = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Land));
                var garages = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Garage));
                var offices = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Office));
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

        //public static int StatisticsObject(IEnumerable<ShortPassport> Source, int flag)
        //{
        //    int count;
        //    var grouped1 = Source.GroupBy(l => l["Type"]);
        //    var grouped = grouped1.Select(l => l.ToList());
        //    if (grouped == null)
        //    {
        //        return count = 0;                
        //    }
        //    else
        //    {
        //        var rooms = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Room));
        //        var flats = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Flat));
        //        var houses = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.House));
        //        var lands = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Land));
        //        var garages = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Garage));
        //        var offices = grouped.FirstOrDefault(g => g.Any(f => (long)f["Type"] == (long)EstateTypes.Office));
        //        switch (flag)
        //        {
        //            case 1:
        //                count = rooms.Count() + flats.Count();
        //                return count;
        //            case 2:
        //                count = houses.Count();
        //                return count;
        //            case 3:
        //                count = garages.Count();
        //                return count;
        //            case 4:
        //                count = lands.Count();
        //                return count;
        //            case 5:
        //                count = offices.Count();
        //                return count;
        //        }
        //    }
                
        //}
    }
}


        
    


