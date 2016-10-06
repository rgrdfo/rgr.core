using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using RGR.Core.Models;

namespace RGR.Core.Controllers
{
    public class CarouselItem
    {
        public string Header;
        public string Text;
        public decimal Price;
    }

    public class Generators
    {

        public static HtmlString BuildUserList(List<Users> Users)
        {
            string result = "";

            foreach (var User in Users)
            {
                result += $"{User.Id:00000}) {User.Login} - {User.FirstName} {User.SurName} {User.LastName} <br />";
            }

            return new HtmlString(result);
        }

        //Генерация карусели для главной страницы
        static HtmlString BuildMainCarousel(CarouselItem[] Source)
        {
            /*//var result = new StringBuilder();
            string result = "< div class=\"carousel-box\">"
            + "<div id=\"myCarousel\" class=\"carousel slide\" data-ride=\"carousel\" data-interval=\"6000\">";
            */

            //Построение списка элементов
            string result = "<ol class=\"carousel - indicators\">";
            string active; //используется в нулевом элементе списка
            for (int i = 0; i < Source.Length; i++)
            {
                //Нулевой элемент устанавливается активным
                active = (i == 0) ? " class=\"active\"" : "";
                //Генерация списка
                result += $"<li data-target=\"#myCarousel\" data-slide-to=\"{i}\"{active}></li>";
            }
            result += "</ol>" //Продолжение следует!
            
            //генерация содержимого карусели
            + "< div class=\"carousel-inner\" role=\"listbox\">";
            for (int i = 0; i < Source.Length; i++)
            {
                //Нулевой элемент устанавливается активным
                active = (i == 0) ? " active" : "";
                result += $"<div class=\"item{active}\">"
                    + "<img width = \"860\" height = \"399\" src = \"~/images/FROM_DB\" alt = \"FROM_DB\" class=\"img-responsive\" />"
                    + "<div class=\"carousel - caption\" role=\"option\">"
                    + "<h1>"
                    + "INFO HERE!"
                    + "</h1><br />"
                    + "<p><font size=\"2\">FULL INFORMATION HERE!!!</font></p>"
                    + "</div></div>";
            }
            result += "</div>";

            return new HtmlString(result);
        }

    }
}
