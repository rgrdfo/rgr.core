using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RGR.API.Common;
using RGR.API.DTO;

namespace RGR.API.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private rgrContext db;
        public AccountController(rgrContext context)
        {
            db = context;
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        [Authorize]
        [HttpGet]
        [Route("personal")]
        public async Task<IActionResult> Personal()
        {
            var page = await PersonalPage.GenerateAsync(db, HttpContext.Session);
            //TODO: поместить роль в page
            //ViewData["RoleID"] = HttpContext.Session.GetRoleId();
            return new JsonResult(page);
        }

        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            HttpContext.Session.Clear();
            return new OkResult();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            Users user = await db.Users.FirstOrDefaultAsync(u => u.Login == dto.Email && u.PasswordHash == dto.PasswordHash);

            if (user == null)
                return new ForbidResult();

            if (user.Blocked)
                return new StatusCodeResult(422);

            await Authenticate(dto.Email, dto.PasswordHash);
            return new OkResult();
        }

        /// <summary>
        /// Аутентификация. Используется только внутри логина или регистрации
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private async Task Authenticate(string userName, string Password)
        {
            // создание одного claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создание объекта ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
            // установка сессии
            string hash = Password.GenerateMD5PasswordHash();
            Users user = await db.Users.FirstOrDefaultAsync(u => u.Login == userName && u.PasswordHash == hash);
            //ну, знаете, мало ли.
            if (user == null)
                throw new NullReferenceException("Ошибка аутентификации! Вероятно, пользователь был удалён или пара логин/пароль была изменена администратором");
            //SessionUtils.SetUser(HttpContext.Session, user);
            HttpContext.Session.SetUser(user);
            //var page = await PersonalPage.GenerateAsync(db, HttpContext.Session);
            //HttpContext.Session.SetString("MyObjectsCache", JsonConvert.SerializeObject(page.MyObjects));
        }
    }
}
