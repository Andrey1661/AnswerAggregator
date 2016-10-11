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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new UserDTO
            {
                Login = model.Login,
                Password = model.Password,
                Email = model.Email
            };

            var result = await _service.CreateUser(user);

            if (result.Success)
            {
                await _service.SendConfirmationMessage(model.Email);
            }

            return new JsonResult
            {
                Data = new {success = true}
            };
        }

        public async Task<ActionResult> ConfirmAccount(Guid token)
        {
            var result = await _service.ConfirmAccount(token);

            if (result.Success)
                return RedirectToAction("Success", "Account");

            return RedirectToAction("Index", "Home");
        }

        public async Task<JsonResult> CheckLogin(string login)
        {
            bool free = await _service.CheckLoginOccuped(login);

            return new JsonResult
            {
                Data = new {success = free}
            };
        }

        public async Task<JsonResult> CheckEmail(string email)
        {
            bool free = await _service.CheckEmailOccuped(email);

            return new JsonResult
            {
                Data = new {success = free}
            };
        } 
    }
}