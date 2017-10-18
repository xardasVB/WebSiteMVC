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
    public class AccountController : Controller
    {
        private readonly IUserProvider _userProvider;

        public AccountController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
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
                var status = _userProvider.Login(model);
                if (status == StatusAccountViewModel.Success)
                    return RedirectToAction("Index", "Home");
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
                    ModelState.AddModelError("", "This user already exists");
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            _userProvider.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}