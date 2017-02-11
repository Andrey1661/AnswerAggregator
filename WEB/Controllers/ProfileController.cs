using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Models.Proflie;
using WEB.Models.Topic;
using WEB.Utility;

namespace WEB.Controllers
{
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ISubjectService _subjectService;

        public ProfileController(IProfileService profileService, ISubjectService subjectService)
        {
            _profileService = profileService;
            _subjectService = subjectService;
        }

        public async Task<ActionResult> Index()
        {
            var profile = await _profileService.GetProfile(CurrentUser);
            var model = Mapper.Map<ProfileModel>(profile);

            var subjects = await _subjectService.GetSubjects(profile.GroupId, 5);

            model.Subjects = subjects.Select(t => new SubjectModel
            {
                Id = t.Id,
                Name = t.Name
            });

            return View(model);
        }

        public async Task<ActionResult> Settings()
        {
            var settings = await _profileService.GetSettings(CurrentUser);
            var model = new UserSettingsModel();

            return new JsonResult
            {
                Data = model
            };
        }

        public async Task<ActionResult> SetAvatar(HttpPostedFileBase file)
        {
            var model = new FileModel
            {
                FileName = file.FileName,
                Data = file.ToByteArray()
            };

            await _profileService.SetAvatar(User.Identity.Name, model);

            return RedirectToAction("Index");
        }
    }
}