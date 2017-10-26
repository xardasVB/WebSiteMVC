using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Models
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            LoginProviders = HttpContext.Current.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
        }
        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name="Remeber me")]
        public bool IsRemembered { get; set; }

        public IEnumerable<AuthenticationDescription> LoginProviders { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not the same"), Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public enum StatusAccountViewModel
    {
        Success = 0,
        Dublication = 1,
        Error = 2
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
}
