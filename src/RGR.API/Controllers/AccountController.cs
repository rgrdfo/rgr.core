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
using RGR.API.Controllers.Account;
using Newtonsoft.Json;

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

        [Authorize]
        [HttpGet]
        [Route("personal")]
        public async Task<IActionResult> Personal()
        {
            var page = await PersonalPage.GenerateAsync(db, HttpContext.Session);

            //Генерация DTO на основе экземпляра PersonalPage
            var json = JsonConvert.SerializeObject(new
            {
                RoleId = page.RoleId,
                MyObjects = page.MyObjects,
                CompanyObjects = page.CompanyObjects
            });

            return new ContentResult() { Content = json };
        }

        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            HttpContext.Session.Clear();
            return new OkResult();
        }

        /// <summary>
        /// Попытка залогиниться. Если пользователя с указанными данными не существует, возвращается 403;
        /// если пользователь заблокирован - 422
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            Users user = await db.Users.FirstOrDefaultAsync(u => u.Login.ToUpper() == dto.Email.ToUpper() && u.PasswordHash == dto.Password.GenerateMD5PasswordHash());

            if (user == null)
                return new ForbidResult();

            if (user.Blocked)
                return new StatusCodeResult(422);

            await Authenticate(dto.Email, dto.Password);
            return new OkResult();
        }

        /// <summary>
        /// Регистрация нового пользователя. Если пользователь с указанным логином существует, 
        /// на клиент возвращается статус 422
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] UserDTO dto)
        {
            var registered = db.Users.FirstOrDefault(u => u.Login.ToUpper() == dto.Email.ToUpper());
            if (registered != null)
                return new StatusCodeResult(422);

            db.Users.Add(new Users()
            {
                Email = dto.Email,
                Login = dto.Email,
                PasswordHash = dto.Password.GenerateMD5PasswordHash(),
                FirstName = dto.FirstName,
                SurName = dto.SurName,
                LastName = dto.LastName,
                Blocked = false,
                Phone = dto.Phone1,
                Phone2 = dto.Phone2,
                Birthdate = dto.BirthDate,
                CertificateNumber = dto.CertificateNumber,
                RoleId = dto.RoleId ?? 1,
                CompanyId = dto.CompanyId ?? -1,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            });

            await db.SaveChangesAsync();

            await Authenticate(dto.Email, dto.Password);

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
