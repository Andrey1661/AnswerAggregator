using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Enviroment;
using WEB.Models.Account;

namespace WEB.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRegistrationDataService _studyDataService;

        public AccountController(IUserService userService, IRegistrationDataService studyDataService)
        {
            _userService = userService;
            _studyDataService = studyDataService;
        }

        public ActionResult Login()
        {
            if(Request.IsAjaxRequest())
                return PartialView("_LoginBox");

            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_LoginBox", model);

            var loginData = await _userService.GetUserLoginData(model.UserName, model.Password);

            if (loginData != null)
            {
                AuthenticationManager.SetCookie(loginData.Email, model.RememberMe,
                    new UserData(loginData.Id, loginData.Role));

                return Json(new {success = true, url = GetLocalUrl(model.ReturnUrl)});
            }

            ModelState.AddModelError("UserName", "Пользователь с такими данными не существует");
            return PartialView("_LoginBox", model);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Mapper.Map<UserModel>(model);
            var result = await _userService.CreateUser(user);
             
            if (result.Success)
            {
                var token = await _userService.CreateVerificationToken(model.Email);
                var link = Url.Action("ConfirmAccount", "Account", new {token = token.Value}, Request.Url.Scheme);

                await _userService.SendConfirmationMessage(model.Email, link);

                return Json(new {success = true});
            }

            return Json(new {success = false, message = result.Errors.First()});
        }

        public async Task<ActionResult> ConfirmAccount(Guid token)
        {
            var result = await _userService.ConfirmAccount(token);

            if (result.Success)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> ForgotPassword(string loginOrEmail)
        {
            var user = await _userService.GetUserLoginData(loginOrEmail);

            if (user != null)
            {
                var token = await _userService.CreatePasswordResetToken(user.Id);
                var link = Url.Action("ResetPassword", "Account", new {token = token.Value}, Request.Url.Scheme);

                await _userService.SendPasswordResetMessage(user.Email, link);

                return Json(new {success = true});
            }

            ModelState.AddModelError("loginOrEmail", "Аккаунт с такими данными не зарегистрирован в системе");
            return View("_ForgotPasswordDialog", (object)loginOrEmail);
        }

        public async Task<ActionResult> ResetPassword(Guid token)
        {
            var result = await _userService.CheckUserPasswordResetToken(token);

            if (result.Success)
            {
                var model = new ResetPasswordModel {PasswordResetToken = token};
                return View(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.ResetUserPassword(model.PasswordResetToken, model.NewPassword);

            if (result.Success)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("Summary", result.Errors.First());
            return View(model);
        } 


        public async Task<JsonResult> CheckLogin(string login)
        {
            bool free = await _userService.CheckLoginOccuped(login);

            return new JsonResult
            {
                Data = new {success = free}
            };
        }

        public async Task<JsonResult> CheckEmail(string email)
        {
            bool free = await _userService.CheckEmailOccuped(email);

            return new JsonResult
            {
                Data = new {success = free}
            };
        }

        public async Task<JsonResult> GetUniversityList()
        {
            var list = (await _studyDataService.GetUniversities()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetInstituteList(string university)
        {
            var list = await _studyDataService.GetInstitutes(university);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetGroupList(string institute, int course)
        {
            var list = await _studyDataService.GetGroups(institute, course);
            return Json(list.Select(t => new {id = t.Id, name = t.Name}), JsonRequestBehavior.AllowGet);
        }


        private ActionResult RedirectToLocalUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        private string GetLocalUrl(string sourceUrl)
        {
            if (Url.IsLocalUrl(sourceUrl)) 
                return sourceUrl;

            return Url.Action("Index", "Home");
        }
    }
}