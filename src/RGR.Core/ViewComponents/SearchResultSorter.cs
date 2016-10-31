﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RGR.Core.Controllers;

namespace RGR.Core.ViewComponents
{
    public class SearchResultSorter : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SuitableEstate Result, Func<ShortPassport, object> SortingOrder)
        {
            return await Task.Run(() => View(Result.OrderBy(SortingOrder)));
        }
    }
}