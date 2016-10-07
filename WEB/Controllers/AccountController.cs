using System;
using System.Threading.Tasks;
using System.Web.Helpers;
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

            await _service.CreateUser(user);

            return new JsonResult
            {
                Data = new { success = true }
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
            throw new NotImplementedException("Function is not implemented yet");
        }

        public async Task<JsonResult> CheckEmail(string email)
        {
            throw new NotImplementedException("Function is not implemented yet");
        } 
    }
}