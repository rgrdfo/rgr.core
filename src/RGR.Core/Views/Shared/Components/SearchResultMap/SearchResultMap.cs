using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RGR.Core.Common;
using RGR.Core.Common.Enums;

namespace RGR.Core.ViewComponents
{
    public class SearchResultMap : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<ShortPassport> Result, string OrderingField)
        {
            //ViewData["Type"] = Type;
            return await Task.Run(() => View(Result.OrderBy(estate => estate[OrderingField])));
        }
    }
}