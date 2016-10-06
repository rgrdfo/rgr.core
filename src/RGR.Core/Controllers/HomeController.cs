using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RGR.Core.Models;
using Microsoft.AspNetCore.Html;

namespace RGR.Core.Controllers
{
    public class HomeController : Controller
    {
        private rgrContext db;
        public HomeController(rgrContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> ListUsers()
        {
            List<Users> Users = await db.Users.ToListAsync();

            ViewData["Message"] = "Ага, вот эти ребята:";
            ViewData["List"] = Generators.BuildUserList(Users);

            return View();
        }



        
    }
}
