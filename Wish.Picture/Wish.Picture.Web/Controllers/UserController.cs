using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wish.Picture.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UserLogin(string userName, string pwd)
        {
            if (userName == "admin" && pwd == "admin123")
            {
                HttpContext.Session.SetString("LoginUserName","admin");
                return RedirectToAction("Index", "Home");
            }

            return Content("登陆失败，用户名或密码不正确");
        }

    }
}