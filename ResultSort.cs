using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;
using RGR.Core.Controllers;
using RGR.Core.Controllers.Enums;
using Microsoft.AspNetCore.Mvc;
using RGR.Core.Controllers;
using RGR.Core.ViewModels;


namespace RGR.Core.ViewComponents
{
    public class ResultSort : ViewComponent
    {
        public static async Task<IViewComponentResult> InvokeAsync(string Source, EstateTypes EstateType)
        {
            var sb = new StringBuilder("<div id=\"bodyResult\">");
            switch (EstateType)
            {
                case EstateTypes.Flat:
                    var flats = JsonConvert.DeserializeObject<IEnumerable<FlatPassport>>(Source);
                    flats = flats.OrderBy(f => f.Address);
                    foreach (var flat in flats)
                    {
                        sb.Append(CommonStart(flat));
                        sb.Append($"<div class=\"col-lg-2\"><h1>{flat.Rooms}-комнатная</h1><br/>{flat.HouseMaterial}<br/>{flat.HouseType}</div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Square} кв.м.</h1><br />кухня {flat.KitchenSquare}<br />жилая {flat.LivingSquare}</div>");
                        sb.Append($"<div class=\"col-lg-1\"><h1>{flat.Floor} этаж из {flat.FloorCount}</h1><br />{flat.Balcony}<br />{flat.Description}</div>");
                        sb.Append(CommonEnd(flat));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={flat.Id}\">Подробнее</a></div>");
                    }
                    break;
                case EstateTypes.Room:
                    var rooms = JsonConvert.DeserializeObject<IEnumerable<FlatPassport>>(Source);
                    rooms = rooms.OrderBy(f => f.Address);
                    foreach (var room in rooms)
                    {
                        sb.Append(CommonStart(room));
                        sb.Append($"<div class=\"col-lg-2\">{room.HouseMaterial}<br/>{room.HouseType}</div>");
                        sb.Append($"<div class=\"col-lg-1\">{room.Square} кв.м.<br />кухня {room.KitchenSquare}<br /></div>");
                        sb.Append($"<div class=\"col-lg-1\">{room.Floor} этаж из {room.FloorCount}<br />{room.Description}</div>");
                        sb.Append(CommonEnd(room));
                        sb.Append($"<div class=\"search-result-footer\"><a href=\"Info?id={room.Id}\">Подробнее</a></div>");
                    }
                    break;
            }
            return View((sb.ToString()));
        }
    }
}
