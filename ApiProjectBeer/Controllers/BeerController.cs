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
    public class BeerController : ControllerBase
    {
        RepositoryBeer repo;
        public BeerController(RepositoryBeer repo)
        {
            this.repo = repo;
        }

        [Route("[action]/{index}/{elementsPerPage}")]
        [HttpGet]
        public ActionResult<BeerModel> GetBeers(int index, int? elementsPerPage)
        {
            return this.repo.GetBeers(index, elementsPerPage);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<List<BeerRating>> Top25()
        {
            return this.repo.GetTop25();
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<Beer> SearchBeer(int id)
        {
            return this.repo.SearchBeer(id);
        }

        [Route("[action]/{name}/{index}/{elementsPerPage}")]
        [HttpGet]
        public ActionResult<BeerModel> SearchBeers(string name, int index, int? elementsPerPage)
        {
            return this.repo.SearchBeers(name,index,elementsPerPage);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public void DeleteBeer(int id)
        {
            this.repo.DeleteBeer(id);
        }

        
        [HttpPost]
        public void CreateBeer(Beer beer)
        {
            this.repo.CreateBeer(beer);
        }

        [HttpPut]
        public void EditBeer(Beer beer)
        {
            this.repo.EditBeer(beer);
        }
    }
}
