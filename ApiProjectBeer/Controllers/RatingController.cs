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
    /* Both for rating and beerrating (view with avg score) */
    public class RatingController : ControllerBase
    {
        RepositoryBeer repo;
        public RatingController(RepositoryBeer repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Rating>> GetRatings()
        {
            return this.repo.GetRatings();
        }

        [HttpGet("{id}")]
        public ActionResult<List<Rating>> SearchBeerRatings(int id)
        {
            return this.repo.SearchBeerRatings(id);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<BeerRating> SearchBeerRating(int id)
        {
            return this.repo.SearchBeerRating(id);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<Rating> SearchRating(int id)
        {
            return this.repo.SearchRating(id);
        }

        [HttpDelete]
        public void DeleteRating(int id)
        {
            this.repo.DeleteRating(id);
        }

        [HttpPost]
        public void CreateRating(Rating rating)
        {
            this.repo.CreateRating(rating);
        }

        [HttpPut]
        public void EditRating(Rating rating)
        {
            this.repo.EditRating(rating);
        }
    }
}
