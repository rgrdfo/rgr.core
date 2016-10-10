using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using RGR.Core.Controllers.Account;
using RGR.Core.Models;

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
                        await Authenticate(model.Login); // аутентификация
                        user.LastLogin = DateTime.UtcNow; //обновление информации о входе
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
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
                    // добавляем пользователя в бд
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

                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                    };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Account");
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
