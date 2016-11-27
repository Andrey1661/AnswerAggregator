using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Models.Topic;

namespace WEB.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _service;

        public TopicController(ITopicService service)
        {
            _service = service;
        }

        public ActionResult Create(Guid subjectId)
        {
            return View(new CreateTopicModel {SubjectId = subjectId});
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateTopicModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.CreateTopic(model.Title, model.SubjectId, User.Identity.Name);

            return RedirectToAction("TopicList", new {subjectId = model.SubjectId});
        }

        public async Task<ActionResult> TopicList(Guid subjectId)
        {
            ViewBag.SubjectId = subjectId;

            var topics = await _service.GetTopicList(subjectId);
            var model = topics.Select(t => new TopicModel
            {
                Id = t.Id,
                Title = t.Title
            });

            return View(model);
        }

        public async Task<ActionResult> Topic(Guid topicId)
        {
            var topic = await _service.GetTopic(topicId, 1, 10);
            var model = new TopicModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Posts = topic.Posts.Select(t => new PostModel
                {
                    Content = t.Content,
                    CreationDate = t.CreationDate,
                    Author = t.Author
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddPost(CreatePostModel model)
        {
            var dto = new PostDTO
            {
                Author = User.Identity.Name,
                Content = model.Text
            };

            await _service.AddPost(model.TopicId, dto);

            return RedirectToAction("Topic", new {topicId = model.TopicId});
        }

        [HttpPost]
        public async Task<ActionResult> AddAnswer(Guid parentPostId, CreatePostModel model)
        {
            var dto = new PostDTO
            {
                Author = User.Identity.Name,
                Content = model.Text
            };

            await _service.AddAnswer(parentPostId, dto);

            return RedirectToAction("Topic", new { topicId = model.TopicId });
        } 
    }
}