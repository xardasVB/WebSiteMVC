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
using Microsoft.AspNet.Identity.Owin;

namespace BLL.Concrete
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public StatusAccountViewModel Login(LoginViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);

            if (user != null)
            {
                var crypto = new PBKDF2();
                var password = crypto.Compute(model.Password, user.PasswordSalt);

                if (password == user.Password)
                {
                    FormsAuthentication
                        .SetAuthCookie(model.Email, model.IsRemembered);
                    return StatusAccountViewModel.Success;
                }
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
            var userDub = _userRepository.GetUserByEmail(model.Email);

            if (userDub != null)
                return StatusAccountViewModel.Dublication;

            var crypto = new PBKDF2();
            User user = new User();
            user.Email = model.Email;
            user.Password = crypto.Compute(model.Password);
            user.PasswordSalt = crypto.Salt;
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return StatusAccountViewModel.Success;
        }

        public IList<string> UserFactors()
        {
            throw new NotImplementedException();
        }

        public bool SendTwoFactorCode(string provider)
        {
            throw new NotImplementedException();
        }

        public ExternalLoginInfo GetExternalLoginInfo()
        {
            throw new NotImplementedException();
        }

        public SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        public StatusAccountViewModel CreateLogin(string email)
        {
            throw new NotImplementedException();
        }
    }
}
