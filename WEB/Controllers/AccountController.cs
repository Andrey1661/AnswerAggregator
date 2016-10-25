using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Enviroment;
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

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginData = await _service.GetUserLoginData(model.UserName, model.Password);

            if (loginData != null)
            {
                AuthenticationManager.SetCookie(loginData.Email, model.RememberMe, loginData.Role);
                return RedirectToLocalUrl(model.ReturnUrl);
            }

            ModelState.AddModelError("UserName", "Пользователь с такими данными не существует");
            return View(model);
        }

        [Authorize]
        public ActionResult SignOut(string returnUrl)
        {
            AuthenticationManager.SignOut();
            return RedirectToLocalUrl(returnUrl);
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
                var token = await _service.CreateVerificationToken(model.Login);
                var link = Url.Action("ConfirmAccount", "Account", new {token = token.Value}, Request.Url.Scheme);

                await _service.SendConfirmationMessage(model.Email, link);
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


        private ActionResult RedirectToLocalUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}