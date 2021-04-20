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
    [ApiController]
    [Authorize(Roles="Admin")]
    public class AdminController : ControllerBase
    {
        RepositoryBeer repo;
        public AdminController(RepositoryBeer repo)
        {
            this.repo = repo;
        }

        [HttpGet("[action]/{index}/{elementsPerPage}")]
        public BeerModel GetBeersToApprove(int index, int elementsPerPage)
        {
            return this.repo.GetBeersToApprove(index, elementsPerPage);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public void ApproveCurrentBeer(int id)
        {
            this.repo.ApproveCurrentBeer(id);
        }

        [HttpPost]
        public void CreateBeerByAdmin(Beer beer)
        {
            this.repo.CreateBeerByAdmin(beer);
        }
    }
}
