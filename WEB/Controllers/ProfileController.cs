using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB.Models.Profile;

namespace WEB.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Members()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Settings(SettingsModel model)
        {
          throw new NotImplementedException();
        }
        
    }
}