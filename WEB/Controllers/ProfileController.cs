using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Models.Proflie;

namespace WEB.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProfileDTO, ProfileModel>();
            });
        }

        private string CurrentUser { get { return User.Identity.Name; } }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var profile = await _service.GetProfile(CurrentUser);
            var model = Mapper.Map<ProfileModel>(profile);

            return View(model);
        }
        [Authorize]
        public async Task<ActionResult> Settings()
        {
            var settings = await _service.GetSettings(CurrentUser);
            var model = new UserSettingsModel();

            return new JsonResult
            {
                Data = model
            };
        } 
    }
}