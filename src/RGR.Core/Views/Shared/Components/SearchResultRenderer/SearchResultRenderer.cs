using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RGR.Core.Common;
using RGR.Core.Controllers.Enums;

namespace RGR.Core.ViewComponents
{
    public class SearchResultRenderer : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<ShortPassport> Result, EstateTypes Type, string OrderingField)
        {
            ViewData["Type"] = Type;
            return await Task.Run(() => View(Result.OrderBy(estate => estate[OrderingField])));
        }
    }

    public class PersonalObjects : ViewComponent
    {
        public async Task<IViewComponentResult> ListObjects()
        {
            return View();
        }
    }
}
