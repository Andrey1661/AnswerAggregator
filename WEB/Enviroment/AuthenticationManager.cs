using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WEB.Enviroment
{
    public static class AuthenticationManager
    {
        public static void SetCookie(string userName, bool persistent, string role = "")
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                userName,
                DateTime.Now, 
                DateTime.Now.Add(FormsAuthentication.Timeout),
                persistent,
                role,
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