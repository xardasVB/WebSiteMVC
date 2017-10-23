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

namespace BLL.Concrete
{
    public class UserIdentityProvider: IUserProvider
    {
        private readonly IUserRepository _userRepository;
        private SignInService _signInManager;
        private UserService _userManager;
        private RoleService _roleManager;

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

        public RoleService RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<RoleService>();
            }
            private set
            {
                _roleManager = value;
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
            FormsAuthentication.SignOut();
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
            var result = UserManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
                return StatusAccountViewModel.Success;

            return StatusAccountViewModel.Error;
        }
    }
}
