using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wish.Picture.Web.Repository;

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
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pwd))
            {
                return Content("登陆失败，参数不能为空");
            }

            var loginUser = new UserRepository().GetUser(userName, pwd);
            if (loginUser != null && loginUser.Count >= 1)
            {
                HttpContext.Session.SetString("LoginUserName",loginUser[0].UserName);
                return RedirectToAction("Index", "Home");
            }

            return Content("登陆失败，用户名或密码不正确");
        }

    }
}