using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DTO;
using BL.Services.Interfaces;
using WEB.Models.Topic;
using WEB.Utility;

namespace WEB.Controllers
{
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _service;

        public TopicController(ITopicService service)
        {
            _service = service;
        }

        public FileResult GetFile(string downloadName, string path)
        {
            var file = Path.Combine("..", path);
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, downloadName);
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

            await _service.CreateTopic(model.Title, model.SubjectId, UserData.Id);

            return RedirectToAction("TopicList", new {subjectId = model.SubjectId});
        }

        public async Task<ActionResult> TopicList(Guid subjectId)
        {
            ViewBag.SubjectId = subjectId;

            var topics = await _service.GetTopicList(subjectId);
            var model = topics.Select(t => new TopicViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Author = t.Author,
                CreationDate = t.CreationDate
            });

            return View(model);
        }

        public async Task<ActionResult> Topic(Guid topicId)
        {
            var topic = await _service.GetTopic(topicId, 1, 10);
            var model = new TopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Posts = topic.Posts.Select(t => new PostViewModel
                {
                    Content = t.Content,
                    CreationDate = t.CreationDate,
                    Author = t.Author,
                    AttachedFiles = t.AttachedFiles
                }).ToList(),
                Author = topic.Author,
                CreationDate = topic.CreationDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddPost(CreatePostModel model)
        {
            var post = new PostModel
            {
                TopicId = model.TopicId,
                AttachedFiles = model.AttachedFiles.Select(t => new FileModel
                {
                     Data = t.ToByteArray(),
                     FileName = t.FileName
                }).ToArray(),
                Content = model.Text,
                AuthorId = UserData.Id
            };

            await _service.AddPost(post);

            return RedirectToAction("Topic", new {topicId = model.TopicId});
        }

        [HttpPost]
        public async Task<ActionResult> AddAnswer(Guid parentPostId, CreatePostModel model)
        {
            throw new NotImplementedException();
        } 
    }
}