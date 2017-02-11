using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WEB.Utility;

namespace WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        var identity = (FormsIdentity)HttpContext.Current.User.Identity;
                        var ticket = identity.Ticket;

                        var userData = CookieHelper.DecodeUserData(ticket.UserData);
                        var role = userData.Role;

                        HttpContext.Current.User = new GenericPrincipal(identity, new[] {role});
                    }
                }
            }
        }
    }
}
