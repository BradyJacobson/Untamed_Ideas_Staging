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
    public class UsersController : ControllerBase
    {
        private readonly IRepository<Data.Models.Users> _repository;
        public UsersController(IRepository<Data.Models.Users> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IEnumerable<Data.Models.Users> GetAllUsers()
        {
            return _repository.GetMethod();
        }

        [HttpGet("{username}",Name = "GetOneUser")]
        public Data.Models.Users GetOneUser(string username)
        {
            return _repository.GetSpecificMethod(username);
        }

        [HttpPost]
        public ActionResult PostUser([FromBody, Bind("Username","Password")]Data.Models.Users user)
        {
            _repository.PostMethod(user);
            return NoContent();// CreatedAtRoute("GetOneUser", user.Username);
        }

        [HttpPut("{replace}")]
        public ActionResult PutUser(string replace, [FromBody]Data.Models.Users user)
        {
            var found = _repository.GetSpecificMethod(replace);
            if(found != null)
            {
                found.Password = user.Password;
                _repository.PutMethod(found);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{remove}")]
        public ActionResult DeleteUser(string remove)
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