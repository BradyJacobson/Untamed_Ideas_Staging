using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLibrary.Abstraction;

namespace Idea_Page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptionsController : ControllerBase
    {
        private readonly IRepository<Data.Models.Descriptions> _repository;
        public DescriptionsController(IRepository<Data.Models.Descriptions> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IEnumerable<Data.Models.Descriptions> GetAllDescriptions()
        {
            return _repository.GetMethod();
        }

        [HttpGet("{idea}",Name = "GetAllIdeaDescriptions")]
        public IEnumerable<Data.Models.Descriptions> GetAllIdeaDescriptions(string idea)
        {
            return _repository.GetRelatedMethod(idea);
        }

        [HttpGet("single/{id}")]
        public Data.Models.Descriptions GetOneDescription(string id)
        {
            return _repository.GetSpecificMethod(id);
        }

        [HttpPost]
        public ActionResult PostDescription([FromBody, Bind("Content","Idea")]Data.Models.Descriptions description)
        {
            var temp0 = _repository.GetMethod();
            if (temp0.Count() > 0)
            {
                int temp = temp0.Max<Data.Models.Descriptions>(e => e.Id);
                description.Id = temp + 1;
                _repository.PostMethod(description);
            }
            else
            {
                description.Id = 0;
                _repository.PostMethod(description);
            }
            return NoContent();
        }

        [HttpPut("{replace}")]
        public ActionResult PutDescription(string replace, [FromBody]Data.Models.Descriptions description)
        {
            var found = _repository.GetSpecificMethod(replace);
            if(found != null)
            {
                found.Idea = description.Idea;
                found.Content = description.Content;
                _repository.PutMethod(found);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{remove}")]
        public ActionResult DeleteDescription(string remove)
        {
            var found = _repository.GetSpecificMethod(remove);
            if(found != null)
            {
                _repository.RemoveMethod(found);
                return NoContent();
            }
            return NotFound();
        }
    }
}