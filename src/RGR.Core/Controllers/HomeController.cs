﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RGR.Core.Models;
using Microsoft.AspNetCore.Html;
using RGR.Core.Controllers.Account;

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

        [Authorize]
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

        public IActionResult Partners()
        {
            return View();
        }
        public IActionResult Education()
        {
            return View();
        }




    }
}
