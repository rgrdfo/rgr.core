using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RGR.Core.Common;

namespace RGR.Core.ViewComponents
{
    public class SearchResultRenderer : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SuitableEstate Result, string OrderingField)
        {
            return await Task.Run(() => View(Result.OrderBy(estate => estate[OrderingField])));
        }
    }
}
