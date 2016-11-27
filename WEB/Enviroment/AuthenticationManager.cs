using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using WEB.Utility;

namespace WEB.Enviroment
{
    public static class AuthenticationManager
    {
        public static void SetCookie(string userName, bool persistent, UserData data)
        {
            var encodedUserData = CookieHelper.EncodeData(data);

            var ticket = new FormsAuthenticationTicket(
                1,
                userName,
                DateTime.Now, 
                DateTime.Now.Add(FormsAuthentication.Timeout),
                persistent,
                encodedUserData,
                FormsAuthentication.FormsCookiePath
                );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();   
        }
    }
}