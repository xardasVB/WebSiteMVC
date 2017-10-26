using BLL.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IUserProvider
    {
        StatusAccountViewModel Register(RegisterViewModel model);
        StatusAccountViewModel Login(LoginViewModel model);
        StatusAccountViewModel CreateLogin(string email);
        IEnumerable<string> UserRoles(string email);
        void Logout();
        IList<string> UserFactors();
        bool SendTwoFactorCode(string provider);
        ExternalLoginInfo GetExternalLoginInfo();
        SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent);
    }
}
