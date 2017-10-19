using BLL.Abstract;
using BLL.Infrastructure.Identity.Service;
using BLL.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
        private SignInService _signInManager;
        private UserService _userManager;
        private readonly IUserProvider _userProvider;

        public AccountController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public SignInService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInService>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userManager = value;
            }
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

        #region AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult LoginPopup(LoginViewModel model)
        {
            string json = "";
            int result = 0;
            string message = "";
            if (ModelState.IsValid)
            {
                var status = _userProvider.Login(model);
                if (status == StatusAccountViewModel.Success)
                {
                    result = 1;
                    message = " Login successfuly";
                }
                else
                    message = "Invalid data";
            }
            message = "Validation";
            json = JsonConvert.SerializeObject(new
            {
                result = result,
                message = message
            });
            return Content(json, "application/json");
        }
        #endregion
    }
}