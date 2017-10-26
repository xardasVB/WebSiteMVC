﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using BLL.Infrastructure.Identity.Service;
using Microsoft.AspNet.Identity;
using DAL;
using DAL.Abstract;
using Microsoft.Owin.Security.Facebook;

[assembly: OwinStartup(typeof(BLL.Concrete.Startup))]

namespace BLL.Concrete
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<EFContext>(EFContext.Create);
            app.CreatePerOwinContext<UserService>(UserService.Create);
            app.CreatePerOwinContext<RoleService>(RoleService.Create);
            app.CreatePerOwinContext<SignInService>(SignInService.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(MyOptions.OptionCookies());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = "124769014878488",
                AppSecret = "0b46a857e3df19b660d4a22f958b040a"
            };

            facebookOptions.Scope.Add("email");
            facebookOptions.Fields.Add("name");
            facebookOptions.Fields.Add("email");

            app.UseFacebookAuthentication(facebookOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
