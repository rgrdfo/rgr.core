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
            

            switch (Type)
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

            var photos = Obj["Photos"] != null ? ((JArray)Obj["Photos"]).Select(j => j.ToObject<string>()) : new List<string>();
            var link = (photos.Any()) ? photos.First() : "";

            return $"<div class=\"row\"><div class=\"col-lg-3\" style=\"border-bottom:none;\"><h1><span>{Obj["Address"]}<br/>{Obj["City"]}</span></h1>"+
                   $"<div><a class=\"quickbox\" href=\"{link}\"><img src=\"{link}\" class=\"img-quickbox\"></a></div></div> " +   /*{QuickboxImg(photos)}*/
                   $"<div class=\"col-lg-1\"><h1>{DateToShow}</h1><br />ID: {Obj["Id"]:0000000}</div>" +
                   $"<div class=\"col-lg-1\"><h1>{Obj["Price"]: ### ### ###} ₽</h1><br />{Obj["PricePerSquare"]:### ###.##} ₽ / м²</div>";
        }

        //public static HtmlString QuickboxImg (IEnumerable<string> photos)
        //{
            
        //    StringBuilder sb = new StringBuilder();            
        //    for (var p=1; p<photos.ToList().Count; p++)
        //    {
        //        sb.Append($"<a style=\"display:none;\" class=\"quickbox\" href=\"{p}\"><img src=\"{p}\" class=\"img-quickbox\"></a>");
        //    }
        //    return new HtmlString(sb.ToString());
        //}

       

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
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div></div></div>");
                    break;
                case EstateTypes.Room:
                    sb.Append($"<div class=\"col-lg-2\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div></div></div>");
                    break;
                case EstateTypes.House:
                    sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div></div></div>");
                    break;
                case EstateTypes.Garage:
                    sb.Append($"<div class=\"col-lg-3\"><label>Тип</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div></div></div>");
                    break;
                case EstateTypes.Land:
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Этажей</label></div></div></div>");
                    break;
                case EstateTypes.Office:
                    sb.Append($"<div class=\"col-lg-3\"><label>Площадь</label></div>");
                    sb.Append($"<div class=\"col-lg-3\"><label>Этажей</label></div></div></div>");
                    break;
            }            
            
            return new HtmlString((sb.ToString()));
        }


        //Карточка объекта
        public static HtmlString FullResult(string Source)
        {              

           StringBuilder sb = new StringBuilder("<div class=\"search-result\">");         

            var passport = JsonConvert.DeserializeObject<FullPassport>(Source);         


            sb.Append($"<div class=\"search-result-row-lg\"><div class=\"obj-descript\">{passport.FullDescription}</div>");

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

            sb.Append("</div></div></div>");            
            sb.Append($"</div></div>");            
            sb.Append("</div>");
            return new HtmlString((sb.ToString()));
        }

        public static HtmlString RightBarResult(string Source)
        {
            StringBuilder sb = new StringBuilder("<div class=\"search-result-row-sm\">");
            var passport = JsonConvert.DeserializeObject<FullPassport>(Source);
            sb.Append($"<div class=\"row\"><div class=\"col-lg-12\"><h1>{passport.Price} ₽</h1><h5>₽ / м²</h5></div>");
            sb.Append($"<div class=\"col-lg-12\"><h5>ID: {passport.Id}:</h5><h5></h5></div></div></div>");
            return new HtmlString((sb.ToString()));
        }

        public static HtmlString MapResult(IEnumerable<ShortPassport> objects)
        {
            
            var sb = new StringBuilder("<div id=\"bodyResult\">");
            foreach (var obj in objects)
            {
                DateTime date = (DateTime?)obj["Date"] ?? DateTime.MinValue;
                string DateToShow = (date != DateTime.MinValue) ? date.ToString("d MMM yy") : "н/д";
                var photos = obj["Photos"] != null ? ((JArray)obj["Photos"]).Select(j => j.ToObject<string>()) : new List<string>();
                var link = (photos.Any()) ? photos.First() : "images/img-exam.png";

                sb.Append($"<div class=\"inner-body-result\"><div class=\"row\">");
                sb.Append($"<div class=\"col-lg-4\"><div><a class=\"quickbox\" href=\"{link}\"><img src=\"{link}\" class=\"img-quickbox\"></a></div></div>");
                sb.Append($"<div class=\"col-lg-4\"><a href=\"javascript://\" onclick=\"drawPlacemark({obj["Latitude"].ToString().Replace(',', '.')},{obj["Logitude"].ToString().Replace(',','.')},'{obj["Address"]}');return false;\"><h1><span>{obj["Type"]}, {obj["Area"]} м²</span></h1></a><br/>{obj["Address"]}</br>");
                //sb.Append($"<div class=\"col-lg-4\"><a href=\"javascript://\" onclick=\"drawPlacemark('{obj["Latitude"]}','{obj["Logitude"]}','{obj["Address"]}');return false;\"><h1><span>Объект, {obj["Area"]} м²</span></h1></a><br/>{obj["Address"]}</br>");
                sb.Append($"<div class=\"row\"><div class=\"col-lg-6\" style=\"padding:0;\">{DateToShow}</div><div class=\"col-lg-6\"style=\"padding:0;\"><h5>ID: {obj["Id"]}</h5></div></div></div>");
                sb.Append($"<div class=\"col-lg-4\"><h1>{obj["Price"]: ### ### ###} ₽</h1><br /><h5>{obj["PricePerSquare"]:### ###.##} ₽ / м²</h5></div>");                
                sb.Append($"</div></div>");
            }
            sb.Append($"</div>");
            return new HtmlString((sb.ToString()));
        }
    }
}
