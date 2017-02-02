using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.API.Controllers
{
    public class HomeController : Controller
    {
        //Переадресация на заглавную страницу сайта. Происходит, если с клиента пришёл запрос
        //без каких-либо параметров (например, если пользователь ввёл адрес сайта в браузере)
        [Route("")]
        public IActionResult Index()
        {
            return Redirect("home/index.html");
        }
    }
}
