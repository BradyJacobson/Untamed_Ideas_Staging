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
    public class PictureController : ControllerBase
    {
        private readonly IRepository<Data.Models.Picture> _repository;
        public PictureController(IRepository<Data.Models.Picture> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IEnumerable<Data.Models.Picture> GetAllPictures()
        {
            return _repository.GetMethod();
        }

        [HttpGet("{idea}", Name = "GetAllIdeaPictures")]
        public IEnumerable<Data.Models.Picture> GetAllIdeaPictures(string idea)
        {
            return _repository.GetRelatedMethod(idea);
        }

        [HttpGet("single/{id}")]
        public Data.Models.Picture GetOnePicture(string id)
        {
            return _repository.GetSpecificMethod(id);
        }

        [HttpPost]
        public ActionResult PostSupply([FromBody, Bind("Content1","Content2", "Idea")]Data.Models.Picture pic)
        {
            var temp0 = _repository.GetMethod();
            if (temp0.Count() > 0)
            {
                int temp = _repository.GetMethod().Max<Data.Models.Picture>(e => e.Id);
                pic.Id = temp + 1;
                _repository.PostMethod(pic);
            }
            else
            {
                pic.Id = 0;
                _repository.PostMethod(pic);
            }
            return NoContent();
        }

        [HttpPut("{replace}")]
        public ActionResult PutSupply(string replace, [FromBody]Data.Models.Picture pic)
        {
            var found = _repository.GetSpecificMethod(replace);
            if (found != null)
            {
                found.Idea = pic.Idea;
                found.Content1 = pic.Content1;
                found.Content2 = pic.Content2;
                _repository.PutMethod(found);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{remove}")]
        public ActionResult DeleteSupply(string remove)
        {
            var found = _repository.GetSpecificMethod(remove);
            if (found != null)
            {
                _repository.RemoveMethod(found);
                return NoContent();
            }
            return NotFound();
        }
    }
}