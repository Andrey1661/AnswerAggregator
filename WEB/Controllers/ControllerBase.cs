using System.Web.Mvc;
using System.Web.Security;
using WEB.Enviroment;
using WEB.Utility;

namespace WEB.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected UserData UserData
        {
            get
            {
                var identity = User.Identity as FormsIdentity;

                if (identity != null)
                {
                    var encodedData = identity.Ticket.UserData;
                    var result = CookieHelper.DecodeUserData<UserData>(encodedData);

                    return result;
                }

                return null;
            }
        }
    }
}