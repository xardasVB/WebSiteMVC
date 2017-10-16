using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebSiteMVC.Controllers
{
    class DBUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AccountController : Controller
    {
        private static List<DBUser> _listUsers;

        public AccountController()
        {
            _listUsers = new List<DBUser>
            {
                new DBUser { Email="q@i.ua", Password="123456" },
                new DBUser { Email="w@i.ua", Password="123456" },
                new DBUser { Email="e@i.ua", Password="123456" },
                new DBUser { Email="r@i.ua", Password="123456" },
                new DBUser { Email="t@i.ua", Password="123456" },
                new DBUser { Email="y@i.ua", Password="123456" }
            };
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBUser user = _listUsers
                    .SingleOrDefault(u => u.Email == model.Email
                    && u.Password == model.Password);
                if (user != null)
                {
                    FormsAuthentication
                        .SetAuthCookie(user.Email, model.IsRemembered);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Invalid data");
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}