using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wish.Picture.Web.Controllers
{
    public class PictureController : Controller
    {
        public IActionResult Index()
        {
            var loginUserName = HttpContext.Session.GetString("LoginUserName");
            if (!string.IsNullOrEmpty(loginUserName))
            {
                ViewData["loginUserName"] = loginUserName;
            }
            return View();
        }
    }
}