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
    public class ImagesController : ControllerBase
    {
        private readonly IRepository<Data.Models.Images> _repository;
        public ImagesController(IRepository<Data.Models.Images> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IEnumerable<Data.Models.Images> GetAllImages()
        {
            return _repository.GetMethod();
        }

        [HttpGet("{idea}", Name = "GetALlIdeaImages")]
        public IEnumerable<Data.Models.Images> GetALlImageDescription(string idea)
        {
            return _repository.GetRelatedMethod(idea);
        }
        [HttpGet("single/{idea}")]
        public Data.Models.Images GetONeImage(string idea)
        {
            return _repository.GetSpecificMethod(idea);
        }

        [HttpPost("{cost}")]
        public ActionResult PostImage(string cost, [FromBody]Data.Models.Images image)
        {
            image.Representation = Convert.ToInt32(cost);
            var temp0 = _repository.GetMethod();
            if (temp0.Count() > 0)
            {
                int temp = _repository.GetMethod().Max<Data.Models.Images>(e => e.Id);
                image.Id = temp + 1;
                _repository.PostMethod(image);

            }
            else
            {
                image.Id = 0;
                _repository.PostMethod(image);
            }
            return NoContent();
        }

        [HttpPut("{replace}")]
        public ActionResult PutImage(string replace, [FromBody]Data.Models.Images image)
        {
            var found = _repository.GetSpecificMethod(replace);
            if(found != null)
            {
                found.Idea = image.Idea;
                found.Representation = image.Representation;
                found.PaidTo = image.PaidTo;
                found.TaskDone = image.TaskDone;
                _repository.PutMethod(found);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{remove}")]
        public ActionResult DeleteImage(string remove)
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