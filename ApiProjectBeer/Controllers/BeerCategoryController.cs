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
    [ApiController]
    public class BeerCategoryController : ControllerBase
    {
        RepositoryBeer repo;
        public BeerCategoryController(RepositoryBeer repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<BeerCategory>> GetCategories()
        {
            return this.repo.GetCategories();
        }

        [HttpGet("{id}")]
    public ActionResult<BeerCategory> GetCategory(int id)
        {
            return this.repo.SearchCategory(id);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public void DeleteCategory(int id)
        {
            this.repo.DeleteCategory(id);
        }


        [HttpPost]
        public void CreateBeer(BeerCategory category)
        {
            this.repo.CreateCategory(category);
        }

        [HttpPut]
        public void EditBeer(BeerCategory category)
        {
            this.repo.EditCategory(category);
        }
    }
}
