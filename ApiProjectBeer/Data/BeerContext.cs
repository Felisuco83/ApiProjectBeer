using Microsoft.EntityFrameworkCore;
using ProjectBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProjectBeer.Data
{
    public class BeerContext: DbContext
    {
        public BeerContext(DbContextOptions<BeerContext> options) : base(options) { }
        public DbSet<Beer> Beer { get; set; }
        public DbSet<BeerCategory> BeerCategory { get; set; }
        public DbSet<BeerRating> BeerRating { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<User> User { get; set; }
    }
}
