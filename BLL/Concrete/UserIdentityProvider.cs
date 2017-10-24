using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Abstract;
using DAL.Entity;
using SimpleCrypto;
using System.Web.Security;
using BLL.Infrastructure.Identity.Service;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using DAL.Entity.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace BLL.Concrete
{
    public class UserIdentityProvider: IUserProvider
    {
        private readonly IUserRepository _userRepository;
        private SignInService _signInManager;
        private UserService _userManager;

        public UserIdentityProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public SignInService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<SignInService>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public StatusAccountViewModel Login(LoginViewModel model)
        {
            var result = SignInManager
                .PasswordSignIn(
                model.Email,
                model.Password,
                model.IsRemembered,
                true);

            switch (result)
            {
                case SignInStatus.Success:
                    return StatusAccountViewModel.Success;
            }
            return StatusAccountViewModel.Error;
        }

        public void Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
        
        public IEnumerable<string> UserRoles(string email)
        {
            return _userRepository
                .GetUserByEmail(email)
                .Roles
                .Select(r => r.Name);
        }

        public StatusAccountViewModel Register(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = UserManager.Create(user, model.Password);

            if (result.Succeeded)
                return StatusAccountViewModel.Success;

            return StatusAccountViewModel.Error;
        }
    }
}
