using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBeer.Models;
using ProjectBeer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProjectBeer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        RepositoryBeer repo;
        public UserController(RepositoryBeer repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return this.repo.GetUsers();
        }

        [Route("[action]/{username}")]
        [HttpGet]
        public ActionResult<User> SearchUserByUserName(string username)
        {
            return this.repo.SearchUser(username);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<User> SearchUserById(int id)
        {
            return this.repo.SearchUser(id);
        }

        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            this.repo.DeleteUser(id);
        }


        [HttpPost]
        public void CreateUser(User user)
        {
            this.repo.CreateUser(user);
        }

        [HttpPut]
        public void EditUser(User user)
        {
            this.repo.EditUser(user);
        }

        [Route("[action]/{id}/{password}")]
        [HttpGet]
        public void ChangeUserPassword(int id, string password)
        {
            this.repo.ChangeUserPassword(id, password);
        }

        [Route("[action]/{username}/{password}")]
        [HttpGet]
        public ActionResult<User> ExisteUsuario(string username, string password)
        {
            return this.repo.ExisteUsuario(username, password);
        }

    }
}
