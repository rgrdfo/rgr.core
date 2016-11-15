using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RGR.Core.Common;
using RGR.Core.Models;
using RGR.Core.ViewModels;
using RGR.Core.Controllers.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace RGR.Core.Controllers
{
    public class AccountController : Controller
    {

        private rgrContext db;
        public AccountController(rgrContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.PasswordHash == PasswordUtils.GenerateMD5PasswordHash(model.Password));
                if (user != null)
                {
                    if (!user.Blocked)
                    {
                        await Authenticate(model.Login, model.Password); // аутентификация
                        user.LastLogin = DateTime.UtcNow; //обновление информации о входе
                        await db.SaveChangesAsync();
                        return RedirectToAction("Personal", "Account");
                    }
                    else
                        ModelState.AddModelError(string.Empty, $"К сожалению, пользователь {user.Login} заблокирован");

                }
                else
                    ModelState.AddModelError(string.Empty, "Проверьте логин и пароль!");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Login);
                if (user == null)
                {
                    // добавление пользователя в БД
                    db.Users.Add(new Users
                    {
                        Login = model.Login,
                        Email = model.Login,
                        PasswordHash = PasswordUtils.GenerateMD5PasswordHash(model.Password),
                        FirstName = "",
                        SurName = "",
                        LastName = "",
                        Blocked = true,
                        Activated = false,
                        Phone = ""
                    });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Login, model.Password); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
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
            string hash = PasswordUtils.GenerateMD5PasswordHash(Password);
            Users user = await db.Users.FirstOrDefaultAsync(u => u.Login == userName && u.PasswordHash == hash);
            //ну, знаете, мало ли.
            if (user == null)
                throw new NullReferenceException("Ошибка аутентификации! Вероятно, пользователь был удалён или пара логин/пароль была изменена администратором");
            //SessionUtils.SetUser(HttpContext.Session, user);
            HttpContext.Session.SetUser(user);
            //var page = await PersonalPage.GenerateAsync(db, HttpContext.Session);
            //HttpContext.Session.SetString("MyObjectsCache", JsonConvert.SerializeObject(page.MyObjects));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public async Task<IActionResult> Personal()
        {
            var page = await PersonalPage.GenerateAsync(db, HttpContext.Session);
            ViewData["RoleID"] = HttpContext.Session.GetRoleId();
            ViewData["MyObjects"] = JsonConvert.SerializeObject(page.MyObjects);
            ViewData["CompanyObjects"] = (page.CompanyObjects == null) ? "" : JsonConvert.SerializeObject(page.CompanyObjects);
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddObject()
        {   
            //Request.Qu         
            return View();
        }
    }
}



        //    private rgrContext db;
        //    public AccountController(rgrContext context)
        //    {
        //        db = context;
        //    }
        //    [HttpGet]
        //    public IActionResult Login()
        //    {
        //        return View();
        //    }
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Login(LoginModel model)
        //    {



        //        //if (ModelState.IsValid)
        //        //{
        //        //    var loginCorrect = await model.CheckLoginPossilblityAsync();
        //        //    if (loginCorrect)
        //        //    {
        //        //        await Authenticate(model.Email); // аутентификация
        //        //        ViewData["Success"] = true;
        //        //        return RedirectToAction("Index", "Home");
        //        //    }
        //        //    else
        //        //        ViewData["Success"] = false;
        //        //}
        //        //return View(model);
        //    }
        //    [HttpGet]
        //    public IActionResult Register()
        //    {
        //        return View();
        //    }
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Register(RegisterModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //Попытка зарегистрировать пользователя. В случае успеха - авторизовать
        //            if (await model.TryRegisterAsync() == true)
        //            {
        //                await Authenticate(model.Email); // аутентификация
        //                ViewData["Success"] = true;
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //                ViewData["Success"] = false;
        //        }
        //        return View(model);
        //    }

        //    private async Task Authenticate(string userName)
        //    {
        //        // создаем один claim
        //        var claims = new List<Claim>
        //                {
        //                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
        //                };
        //        // создаем объект ClaimsIdentity
        //        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
        //            ClaimsIdentity.DefaultRoleClaimType);
        //        // установка аутентификационных куки
        //        await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
        //    }

        //    public async Task<IActionResult> Logout()
        //    {
        //        await HttpContext.Authentication.SignOutAsync("Cookies");
        //        return RedirectToAction("Login", "Account");
        //        //db.Users.Add()
        //    }
        //}
    //}
