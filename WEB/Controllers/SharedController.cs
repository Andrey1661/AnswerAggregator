using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult LayoutHead()
        {
            if (Request.IsAjaxRequest())
                return PartialView("_LayoutHead");

            return HttpNotFound();
        }
    }
}