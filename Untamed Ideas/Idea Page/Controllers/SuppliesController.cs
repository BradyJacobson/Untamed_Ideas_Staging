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
    public class SuppliesController : ControllerBase
    {
        private readonly IRepository<Data.Models.Supplies> _repository;
        public SuppliesController(IRepository<Data.Models.Supplies> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IEnumerable <Data.Models.Supplies> GetAllSupplies()
        {
            return _repository.GetMethod();
        }

        [HttpGet("{idea}",Name = "GetAllIdeaSupplies")]
        public IEnumerable<Data.Models.Supplies> GetAllIdeaSupplies(string idea)
        {
            return _repository.GetRelatedMethod(idea);
        }

        [HttpGet("single/{id}")]
        public Data.Models.Supplies GetOneSupply(string id)
        {
            return _repository.GetSpecificMethod(id);
        }

        [HttpPost]
        public ActionResult PostSupply([FromBody, Bind("Supplies1", "Idea")]Data.Models.Supplies supply)
        {
            var temp0 = _repository.GetMethod();
            if (temp0.Count() > 0)
            {
                int temp = _repository.GetMethod().Max<Data.Models.Supplies>(e => e.Id);
                supply.Id = temp + 1;
                _repository.PostMethod(supply);
            }
            else
            {
                supply.Id = 0;
                _repository.PostMethod(supply);
            }
            return NoContent();
        }

        [HttpPut("{replace}")]
        public ActionResult PutSupply(string replace, [FromBody]Data.Models.Supplies supply)
        {
            var found = _repository.GetSpecificMethod(replace);
            if(found != null)
            {
                found.Idea = supply.Idea;
                found.Supplies1 = supply.Supplies1;
                _repository.PutMethod(found);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{remove}")]
        public ActionResult DeleteSupply(string remove)
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