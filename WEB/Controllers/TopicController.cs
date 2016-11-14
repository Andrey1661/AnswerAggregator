using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public async Task<ActionResult> TopicList(Guid subjectId)
        {
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
    }
}