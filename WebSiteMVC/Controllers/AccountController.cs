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
using System.Threading.Tasks;
using WebSiteMVC.Models;

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
            LoginViewModel model = new LoginViewModel();
            return View(model);
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

        #region Facebook
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public ActionResult SendCode(string returnUrl, bool rememberMe)
        {
            var userFactors = _userProvider.UserFactors();
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!_userProvider.SendTwoFactorCode(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }


        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = _userProvider.GetExternalLoginInfo();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = _userProvider.ExternalSignIn(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    var status = _userProvider.CreateLogin(loginInfo.Email);

                    if (status == StatusAccountViewModel.Success)
                        return RedirectToLocal(returnUrl);
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = _userProvider.GetExternalLoginInfo();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var result = _userProvider.CreateLogin(model.Email);

                if (result == StatusAccountViewModel.Success)
                {
                    return RedirectToLocal(returnUrl);
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult LoginPopup(LoginViewModel model)
        {
            string json = ""; int result = 0; string message = "";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult RegisterPopup(RegisterViewModel model)
        {
            string json = ""; int result = 0; string message = "";
            if (ModelState.IsValid)
            {
                var status = _userProvider.Register(model);
                if (status == StatusAccountViewModel.Success)
                {
                    result = 1;
                    message = " Registeres successfuly";
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