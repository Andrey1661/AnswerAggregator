using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Models.Account;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            var user = new UserDTO
            {
                Login = model.Login,
                Password = model.Password,
                Email = model.Email
            };

            await _service.CreateUser(user);

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> ConfirmAccount(Guid token)
        {
            await _service.ConfirmAccount(token);
            return RedirectToAction("Index", "Home");
        }
    }
}