using BLL.Abstract;
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
        private readonly IUserProvider _userProvider;
        private static List<DBUser> _listUsers;

        public AccountController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
            _listUsers = new List<DBUser>
            {
                new DBUser {Email = "q@i.ua", Password="123456" },
                new DBUser {Email = "w@i.ua", Password="123456" },
                new DBUser {Email = "e@i.ua", Password="123456" }
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = _userProvider.Register(model);
                if (status == StatusAccountViewModel.Success)
                    return RedirectToAction("Login");
                else if (status == StatusAccountViewModel.Dublication)
                {
                    ModelState.AddModelError("", "This user already exists");
                }
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