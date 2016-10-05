using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.Services.Interfaces;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _service.GetUser("Test");

            return View(model);
        }
    }
}