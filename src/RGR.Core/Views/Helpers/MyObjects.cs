using Microsoft.AspNetCore.Html;

using System.Text;


namespace RGR.Core.Views.Helpers
{
    public class MyObjects
    {
        public static HtmlString HeadMyObject()
        {
            var sb = new StringBuilder("<div class=\"search-box-head\">");
            sb.Append($"<div class=\"row\"><div class=\"col-lg-3\"><a href=\"#\">Адрес</a></div>");
            sb.Append($"<div class=\"col-lg-1\"><label>Дата</label></div>");
            sb.Append($"<div class=\"col-lg-1\"><label>Цена</label></div>");
            sb.Append($"<div class=\"col-lg-2\"><label>Комнаты</label></div>");
            sb.Append($"<div class=\"col-lg-1\"><label>Площадь</label></div>");
            sb.Append($"<div class=\"col-lg-1\"><label>Этажность</label></div>");
            sb.Append($"<div class=\"col-lg-2\"><label></br></label></div></div>");
            sb.Append($"</div>");
            return new HtmlString((sb.ToString()));
        }

    }
}
