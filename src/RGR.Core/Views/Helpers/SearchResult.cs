using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using RGR.Core.Common;
using RGR.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace RGR.Core.Views.Helpers
{
    public class SearchResult
    {
        public static IEnumerable<ShortPassport> Deserialize(string Source)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ShortPassport>>(Source);
        }


        public static HtmlString Render(IEnumerable<ShortPassport> objects, EstateTypes Type)
        {
            var sb = new StringBuilder("<div id=\"bodyResult\">");
            //var DTO = JsonConvert.DeserializeObject<List<ShortPassport>>(Source);
            //var objects = JsonConvert.DeserializeObject<SuitableEstate>(Source);//new SuitableEstate(DTO)
            int i = 0;
            sb.Append($"<div>");
            switch (Type)
            {
                case EstateTypes.Flat:
                    foreach (var floor in objects)
                    {
                        int j = i++;
                        sb.Append(CommonStart(floor, j));
                        sb.Append($"<div class=\"col-lg-2\"><h1>{floor["Rooms"]}-комнатная</h1><font>материал:</font> {floor["HouseMaterial"]}<br/><font>состояние:</font> {floor["State"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{floor["Area"]} м²</h1><font>кухня:</font> {floor["KitchenArea"]} м²<br /><font>жилая:</font> {floor["LivingArea"]} м²</div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{floor["Floor"]} этаж из {floor["FloorCount"]}</h1><font>балкон: </font>{floor["Balcony"]}<br/><font>с/у: </font> {floor["WC"]}</div>");       
                        sb.Append(CommonEnd(floor));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={floor["Id"]}\">Подробнее</a><div class=\"search-result-footer-inner\"><label>В избранные</label><input id=\"but-res-image\" class=\"but-simple\" value=\"Скрыть\" type=\"button\" onclick=\"deleteBlock('ibr-{j}')\"/></div></div></div>");
                    }
                    break;

                case EstateTypes.Room:
                    foreach (var room in objects)
                    {
                        int j = i++;
                        sb.Append(CommonStart(room, i++));
                        sb.Append($"<div class=\"col-lg-2\">{room["HouseMaterial"]}<br/>{room["HouseType"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{room["Area"]} м²<br />кухня {room["KitchenArea"]}<br /></div>");
                        sb.Append($"<div class=\"col-lg-1\">{room["Floor"]} этаж из {room["FloorCount"]}<br />{room["Description"]}</div>");
                        sb.Append(CommonEnd(room));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={room["Id"]}\">Подробнее</a><div class=\"search-result-footer-inner\"><label>В избранные</label><input id=\"but-res-image\" class=\"but-simple\" value=\"Скрыть\" type=\"button\" onclick=\"deleteBlock('ibr-{j}')\"/></div></div></div>");
                    }
                    break;

                case EstateTypes.Garage:
                    foreach (var garage in objects)
                    {
                        int j = i++;
                        sb.Append(CommonStart(garage, i++));
                        sb.Append($"<div class=\"col-lg-3\">Гараж<br />материал: {garage["HouseMaterial"]}</div>");
                        sb.Append($"<div class=\"col-lg-3\">{garage["Area"]:###.##} м²</div>");
                        sb.Append(CommonEnd(garage));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={garage["Id"]}\">Подробнее</a></div></div>");
                    }
                    break;

                case EstateTypes.House:
                    foreach (var house in objects)
                    {
                        int j = i++;
                        sb.Append(CommonStart(house, i++));
                        sb.Append($"<div class=\"col-lg-2\">{house["Rooms"]}-комн.<br />материал: {house["HouseMaterial"]}<br/>состояние: {house["State"]}<br/>отопление: {house["Heating"]}<br/>вода: {house["Water"]}<br/>электричество: {house["Electricy"]}<br/>канализация: {house["Sewer"]}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{house["Area"]:###.##} м²<br />кухня: {house["KitchenArea"]} м²<br/>жилая: {house["LivingArea"]} м²</div>");
                        sb.Append($"<div class=\"col-lg-1\">{house["FloorCount"]}<br />балкон: {house["BalconyIsPresent"]}<br/>санузел: {house["WC"]}</div>");
                        sb.Append(CommonEnd(house));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={house["Id"]}\">Подробнее</a><div class=\"search-result-footer-inner\"><label>В избранные</label><input id=\"but-res-image\" class=\"but-simple\" value=\"Скрыть\" type=\"button\" onclick=\"deleteBlock('ibr-{j}')\"/></div></div></div>");
                    }
                    break;

                case EstateTypes.Land:
                    //var lands = JsonConvert.DeserializeObject<SuitableEstate>(Source);
                    foreach (var land in objects)
                    {
                        int j = i++;
                        sb.Append(CommonStart(land, i++));
                        sb.Append($"<div class=\"col-lg-3\">{land["Area"]:###.##} м²<br /><br/>отопление: {land["Heating"]}<br/>вода: {land["Water"]}<br/>электричество: {land["Electricy"]}<br/>канализация: {land["Sewer"]}</div>");
                        sb.Append($"<div class=\"col-lg-3\">{land["Purpose"]}<br />{land["Category"]}<br />{land["Specifics"]}</div>");
                        sb.Append(CommonEnd(land));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={land["Id"]}\">Подробнее</a><div class=\"search-result-footer-inner\"><label>В избранные</label><input id=\"but-res-image\" class=\"but-simple\" value=\"Скрыть\" type=\"button\" onclick=\"deleteBlock('ibr-{j}')\"/></div></div></div>");
                    }
                    break;

                case EstateTypes.Office:
                    foreach (var office in objects)
                    {
                        int j = i++;
                        sb.Append(CommonStart(office, i++));
                        sb.Append($"<div class=\"col-lg-3\">{office["Area"]:###.##} м²<br />материал: {office["HouseMaterial"]}<br />состояние: {office["State"]}</div>");
                        sb.Append($"<div class=\"col-lg-3\">{office["FloorCount"]}<br /><br/>отопление: {office["Heating"]}<br/>вода: {office["Water"]}<br/>электричество: {office["Electricy"]}<br/>канализация: {office["Sewer"]}</div>");
                        sb.Append(CommonEnd(office));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={office["Id"]}\">Подробнее</a><div class=\"search-result-footer-inner\"><label>В избранные</label><input id=\"but-res-image\" class=\"but-simple\" value=\"Скрыть\" type=\"button\" onclick=\"deleteBlock('ibr-{j}')\"/></div></div></div>");
                    }
                    break;

                default:
                    throw new ArgumentException("Указан некорректный тип недвижимости");
            }            
            sb.Append($"</div></div>");
            return new HtmlString(sb.ToString());
        }

        public static string CommonStart(ShortPassport Obj, int i)
        {
            //TODO: Список фотографий объекта!!!11

            DateTime date = (DateTime?)Obj["Date"] ?? DateTime.MinValue;
            string DateToShow = (date != DateTime.MinValue) ? date.ToString("d MMM yy") : "н/д";

            var photos = Obj["Photos"] != null ? ((JArray)Obj["Photos"]).Select(j => j.ToObject<string>()) : new List<string>();
            var link = (photos.Any()) ? photos.First() : "images/img-exam.png";
            var visib = "visibility:visible;";
            var hidd = "visibility:hidden;";
            string style;
            if (link == "images/img-exam.png")
            {
                style = hidd;
            }
            else
            {
                style = visib;
            }
            return $"<div id=\"ibr-{i}\" class=\"inner-body-result\" onmouseover=\"visibleFunction(this.id)\" onmouseout=\"hiddenFunction(this.id)\"><div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{Obj["Address"]}<br/>{Obj["City"]}</span></h1>"+
                   $"<div style=\"position: relative;\"><a class=\"quickbox\" href=\"{link}\"><img src=\"{link}\" class=\"img-quickbox\"><img class=\"box-numb\" style=\"{style}\" src=\"images/ico-box-numb.png\"></a></div></div> " +   /*{QuickboxImg(photos)}*/
                   $"<div class=\"col-lg-1\"><h1>{DateToShow}</h1><font>ID: {Obj["Id"]:0000000}</font></div>" +
                   $"<div class=\"col-lg-1\"><h1>{Obj["Price"]: ### ### ###} <font style=\"color: inherit;\" class=\"ruble\">₽</font></h1><font>{Obj["PricePerSquare"]:### ###.##} <font class=\"ruble\">₽</font> / м²</font></div>";
        }

        public static HtmlString QuickboxImg(IEnumerable<string> photos)
        {

            StringBuilder sb = new StringBuilder();
            for (var p = 1; p < photos.ToList().Count; p++)
            {
                sb.Append($"<div class=\"col-lg-3\">");
                sb.Append($"<a class=\"quickbox\" href=\"{p}\"><img src=\"{p}\" class=\"img-quickbox\"></a>");
                sb.Append($"</div>");
            }
            return new HtmlString(sb.ToString());
        }



        public static string CommonEnd(ShortPassport Obj)
        {
            string s = (Obj.ContainsKey("Logo")) ?
                $"<img src =\"{Obj["Logo"]}\"><br/>" :
                "";
            return  $"{s}<div class=\"col-lg-2\">{Obj["Agency"]}<br/>{Obj["AgentPhone"]}</div>" +
                   "</div>";

        }
        public static HtmlString HeadResult(EstateTypes EstateType, string Uri)
        {
            var sb = new StringBuilder("<div class=\"search-box-head\">");
            sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"http://{Uri}&order=Address\" onclick=\"stayActive(searchType2)\">Адрес</a></div>");            
            sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Date\">Дата</a></div>");
            sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Price\">Цена</a></div>");
            switch (EstateType)
            {
                case EstateTypes.Flat:
                    sb.Append($"<div class=\"col-lg-2\"><a href=\"http://{Uri}&order=Rooms\">Комнаты</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Area\">Площадь</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Floor\">Этажность</a></div></div></div>");
                    break;
                case EstateTypes.Room:
                    sb.Append($"<div class=\"col-lg-2\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Area\">Площадь</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Floor\">Этажность</a></div></div></div>");
                    break;
                case EstateTypes.House:
                    sb.Append($"<div class=\"col-lg-2\"><a href=\"http://{Uri}&order=Rooms\">Комнаты</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Area\">Площадь</a></div>");
                    sb.Append($"<div class=\"col-lg-1\"><a href=\"http://{Uri}&order=Floor\">Этажность</a></div></div></div>");
                    break;
                case EstateTypes.Garage:
                    sb.Append($"<div class=\"col-lg-3\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><a href=\"http://{Uri}&order=Area\">Площадь</a></div></div></div>");
                    break;
                case EstateTypes.Land:
                    sb.Append($"<div class=\"col-lg-3\"><a href=\"http://{Uri}&order=Area\">Площадь</a></div>");
                    sb.Append($"<div class=\"col-lg-3\"><a href=\"http://{Uri}&order=FloorCount\">Этажей</a></div></div></div>");
                    break;
                case EstateTypes.Office:
                    sb.Append($"<div class=\"col-lg-3\"><a href=\"http://{Uri}&order=Area\">Площадь</a></div>");
                    sb.Append($"<div class=\"col-lg-3\"><a href=\"http://{Uri}&order=FloorCount\">Этажей</a></div></div></div>");
                    break;
            }            
            
            return new HtmlString((sb.ToString()));
        }


        //Карточка объекта
        public static HtmlString FullResult(string Source)
        {              

           StringBuilder sb = new StringBuilder("");         

            var passport = JsonConvert.DeserializeObject<FullPassport>(Source);
            //var photos;

            sb.Append($"<h1>{passport.EstateType}: {passport.Address}, {passport.TotalSquare} м²</h1>");
            sb.Append($"<div class=\"search-result-row-lg\"><div class=\"row\"></div>");
            sb.Append($"<div id=\"full-result-map\"></div>"); /*onload =\"drawPlacemark({passport.Latitude}, {passport.Longitude}, {passport.Address})\"*/
            sb.Append($"<div class=\"obj-descript\">{passport.FullDescription}</div>");

            //----- Заголовки -----//
            sb.Append($"<div class=\"row\"><div class=\"col-lg-6 first-col\"><h1>Кварира</h1></div><div class=\"col-lg-6 first-col\"><h1>Дом</h1></div>");           

            //---- Квартира ----//
            sb.Append($"<div class=\"col-lg-6 first-col\"><div class=\"search-result-box\">");
            sb.Append($"<div class=\"row\">");
            //Количество комнат 
            sb.Append($"<div class=\"col-lg-6\"><p>Количество комнат </p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Rooms}</p></div>");
            //Тип квартиры
            sb.Append($"<div class=\"col-lg-6\"><p>Тип квартиры</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.FlatType}</p></div>");
            //Этаж
            sb.Append($"<div class=\"col-lg-6\"><p>Этаж</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Sewer}</p></div>");
            //Общая площадь 
            sb.Append($"<div class=\"col-lg-6\"><p>Общая площадь </p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.TotalSquare}</p></div>");
            //Жилая / полезная площадь 
            sb.Append($"<div class=\"col-lg-6\"><p>Жилая / полезная площадь </p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.UsefulSquare}</p></div>");
            //Кухня
            sb.Append($"<div class=\"col-lg-6\"><p>Кухня</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Kitchen}</p></div>");
            //Санузел
            sb.Append($"<div class=\"col-lg-6\"><p>Санузел</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.WC}</p></div>");
            //Количество балконов
            sb.Append($"<div class=\"col-lg-6\"><p>Балкон</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Balconies}</p></div>");
            //Количество лоджий
            sb.Append($"<div class=\"col-lg-6\"><p>Лоджия</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Logias}</p></div>");
            //Вид из окон
            sb.Append($"<div class=\"col-lg-6\"><p>Вид из окон</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.WindowView}</p></div>");
            //Оценка состояния объекта
            sb.Append($"<div class=\"col-lg-6\"><p>Состояние</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.ObjectStateAssessment}</p></div>");
            //Проведен �?нтернет
            sb.Append($"<div class=\"col-lg-6\"><p>Связь</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Internet}</p></div>");
            sb.Append($"</div></div></div>");

            //---- Дом ----//
            sb.Append($"<div class=\"col-lg-6 first-col\"><div class=\"search-result-box\">");
            sb.Append($"<div class=\"row\">");
            // Тип дома 
            sb.Append($"<div class=\"col-lg-6\"><p> Тип дома </p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.BuildingType}</p></div>");
            //Материал постройки 
            sb.Append($"<div class=\"col-lg-6\"><p>Материал постройки </p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.BuildingMaterial}</p></div>");
            // Материал перекрытий
            sb.Append($"<div class=\"col-lg-6\"><p>Материал перекрытий</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.CellingMaterial}</p></div>");
            // Год постройки
            sb.Append($"<div class=\"col-lg-6\"><p>Год постройки</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.BuildingYear}</p></div>");
            //Этажность здания 
            sb.Append($"<div class=\"col-lg-6\"><p>Этажность</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.FloorsTotal}</p></div>");
            //Парковка машин
            sb.Append($"<div class=\"col-lg-6\"><p>Парковка </p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Parking}</p></div>");
            //Безопасность
            sb.Append($"<div class=\"col-lg-6\"><p>Безопасность</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>{passport.Security}</p></div>");
            //!!!---Лифт
            sb.Append($"<div class=\"col-lg-6\"><p>Лифт</p></div>");
            sb.Append($"<div class=\"col-lg-6\"><p>&&&&</p></div>");

            sb.Append($"</div></div></div>");            
            sb.Append($"<div style=\"clear: both; \"></div></div>");
            sb.Append($"</div>");
            return new HtmlString((sb.ToString()));
        }

        public static HtmlString RightBarResult(string Source)
        {
            StringBuilder sb = new StringBuilder("<div class=\"search-result-row-sm\">");
            var passport = JsonConvert.DeserializeObject<FullPassport>(Source);
            sb.Append($"<div class=\"row\"><div class=\"col-lg-12\"><h1>{passport.Price:### ### ###} <font class=\"ruble\">₽</font> <a href=\"#\"><img src=\"images/Info/ico-change.png\"/></a></h1><h5>{passport.PricePerSqMetter:### ###.##}<font class=\"ruble\">₽</font> / м²</h5></div>");
            sb.Append($"<div class=\"col-lg-12\">Торг. Договор. Ипотека.</div>");
            sb.Append($"<div class=\"col-lg-12\" id=\"col-no-border\"><div class=\"row\"><div class=\"col-lg-3\"><img src=\"images/no-photo-small.png\" /></div><div class=\"col-lg-9\">{passport.Agency}</div></div></div>");
            sb.Append($"<div class=\"col-lg-12\" id=\"col-no-border\"><div class=\"row\"><div class=\"col-lg-3\"><img src=\"images/no-photo-small.png\" /></div><div class=\"col-lg-9\"><h5>Агент:</h5>{passport.Agent}</div></div></div>");
            sb.Append($"<div class=\"col-lg-12\" style=\"padding-left: calc(30%); padding-top:0;\"><p>{passport.AgentPhone}</p><a href=\"#\"><img src=\"images/Info/ico-back-call.png\"/><span>Заказать обратный звонок</span></a><a href=\"#\"><img src=\"images/Info/ico-leave-request.png\"/><span>Оставить заявку на просмотр</span></a></div>");
            sb.Append($"<div class=\"col-lg-12\" id=\"col-no-border\"><h5>ID: {passport.Id}</h5><h5>Обновлено: {passport.UpdateTime}</h5></div>");
            //Колонки с ссылками
            sb.Append($"<div class=\"col-lg-12\" id=\"col-no-border\"><div id=\"full-result-ico-row\" class=\"row\">");            
            sb.Append($"<div class=\"col-lg-1\"></div><div class=\"col-lg-1\"></div>");
            sb.Append($"<div class=\"col-lg-1\"><a href=\"#\"><img src=\"images/Info/ico-facebook.png\" /></a></div>");
            sb.Append($"<div class=\"col-lg-1\"><a href=\"#\"><img src=\"images/Info/ico-twiter.png\" /></a></div>");
            sb.Append($"<div class=\"col-lg-1\"><a href=\"#\"><img src=\"images/Info/ico-vk.png\" /></a></div>");
            sb.Append($"<div class=\"col-lg-1\"><a href=\"#\"><img src=\"images/Info/ico-ocl.png\" /></a></div>");
            sb.Append($"<div class=\"col-lg-1\"><a href=\"#\"><img src=\"images/Info/ico-let.png\" /></a></div>");
            sb.Append($"<div class=\"col-lg-1\"></div><div class=\"col-lg-1\"></div></div></div>");
            sb.Append($"<div class=\"col-lg-12\" id=\"full-result-col-but\"><a href=\"#\" class=\"but-personal\">Редактировать</a></div></div></div>");
            return new HtmlString((sb.ToString()));
        }

        public static HtmlString MapResult(IEnumerable<ShortPassport> objects)
        {
            
            var sb = new StringBuilder("<div id=\"bodyResult\">");
            int i = 0;
            var visib = "visibility:visible;";
            var hidd = "visibility:hidden;";
            string style;
            foreach (var obj in objects)
            {
                DateTime date = (DateTime?)obj["Date"] ?? DateTime.MinValue;
                string DateToShow = (date != DateTime.MinValue) ? date.ToString("d MMM yy") : "н/д";
                var photos = obj["Photos"] != null ? ((JArray)obj["Photos"]).Select(j => j.ToObject<string>()) : new List<string>();
                var link = (photos.Any()) ? photos.First() : "images/img-exam.png";                
                int f = i++;
                if (link== "images/img-exam.png")
                {
                    style = hidd;                                       
                }
                else
                {
                    style = visib;                    
                }
                sb.Append($"<div id=\"imr-{f}\" class=\"inner-body-result\" onmouseover=\"visibleFunction(this.id)\" onmouseout=\"hiddenFunction(this.id)\" onclick=\"drawPlacemark({obj["Latitude"].ToString().Replace(',', '.')},{obj["Logitude"].ToString().Replace(',', '.')},'{obj["Address"]}');return false;\"><div class=\"row\">");
                sb.Append($"<div class=\"col-lg-4\"><div style=\"position: relative;\"><a class=\"quickbox\" href=\"{link}\"><img src=\"{link}\" class=\"img-quickbox\"><img style=\"{style}\" class=\"box-numb\" src=\"images/ico-box-numb.png\"></a></div></div>");
                if (obj["Type"].ToString()=="Квартира")
                {
                    sb.Append($"<div class=\"col-lg-4\"><a href=\"javascript://\"><h1><span>{obj["Rooms"]}-комнатная , {obj["Area"]} м²</span></h1></a><br/>{obj["Address"]}</br></br>");                    
                }
                else
                {
                    sb.Append($"<div class=\"col-lg-4\"><a href=\"javascript://\" onclick=\"drawPlacemark({obj["Latitude"].ToString().Replace(',', '.')},{obj["Logitude"].ToString().Replace(',', '.')},'{obj["Address"]}');return false;\"><h1><span>{obj["Type"]} , {obj["Area"]} м²</span></h1></a><br/>{obj["Address"]}</br></br>");
                }
                sb.Append($"<div class=\"row\"><div class=\"col-lg-6\">{DateToShow}</div><div class=\"col-lg-6\" style=\"color: #c3c3c3;\">ID: {obj["Id"]}</div></div></div>");
                sb.Append($"<div class=\"col-lg-4\"><h1 style=\"height:36px; margin:0;\">{obj["Price"]: ### ### ###} <font class=\"ruble\">₽</font></h1><font style=\"color: #c3c3c3;\">{obj["PricePerSquare"]:### ###.##} <font class=\"ruble\">₽</font> / м²</font></br></br><div class=\"search-map-footer\"><label>В избранные</label></div></div>");                
                sb.Append($"</div><img class=\"search-map-cancel\" src=\"images/ico-cancel.png\" onclick=\"deleteBlock('imr-{f}')\"></div>");
            }
            sb.Append($"</div>");
            return new HtmlString((sb.ToString()));
        }
    }
}
