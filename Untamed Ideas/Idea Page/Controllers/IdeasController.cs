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
    public class IdeasController : ControllerBase
    {
        private readonly IRepository<Data.Models.Ideas> _repository;
        public IdeasController(IRepository<Data.Models.Ideas> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IEnumerable<Data.Models.Ideas> GetAllIdeas()
        {
            return _repository.GetMethod();
        }

        [HttpGet("{username}",Name = "GetAllUserIdeas")]
        public IEnumerable<Data.Models.Ideas> GetAllUserIdeas(string username)
        {
            return _repository.GetRelatedMethod(username);
        }

        [HttpGet("single/{username}")]
        public Data.Models.Ideas GetOneIdea(string username)
        {
            return _repository.GetSpecificMethod(username);
        }

        [HttpPost]
        public ActionResult PostIdea([FromBody, Bind("Ideaname","Formating","Username")]Data.Models.Ideas idea)
        {
            //TODO: Replaces values, if count < highest value.
            //TODO: Or just implement Identity!
            int temp = _repository.GetMethod().Max<Data.Models.Ideas>(e => e.Id);
            idea.Id = temp + 1;
            _repository.PostMethod(idea);
            return NoContent();
        }

        [HttpPut("{replace}")]
        public ActionResult PutIdea(string replace,[FromBody]Data.Models.Ideas idea)
        {
            var found = _repository.GetSpecificMethod(replace);
            if(found != null)
            {
                found.Ideaname = idea.Ideaname;
                found.Formating = idea.Formating;
                found.Username = idea.Username;
                _repository.PutMethod(found);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{remove}")]
        public ActionResult DeleteIdea(string remove)
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