using Microsoft.AspNetCore.Mvc;
using RGR.API.DTO;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.API.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private rgrContext db;

        public SearchController(rgrContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Search([FromBody] SearchRequestDTO Request)
        {
            try
            {
                return new ContentResult() { Content = db.EstateObjects.First().Id.ToString()};
            }
            catch(Exception e)
            {
                return new ContentResult() { Content = e.Message };
            }
        }
    }
}
