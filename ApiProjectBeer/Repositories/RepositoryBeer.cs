using ApiProjectBeer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBeer.Helpers;
using ProjectBeer.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBeer.Repositories
{
    public class RepositoryBeer
    {
        BeerContext context;
        public RepositoryBeer(BeerContext context)
        {
            this.context = context;
        }

        public void CreateBeer(Beer beer)
        {
            beer.BeerId = this.GetNextId("beer");
            this.context.Beer.Add(beer);
            this.context.SaveChanges();
        }

        public void CreateCategory(BeerCategory category)
        {
            category.BeerCategoryId = this.GetNextId("beercategory");
            this.context.BeerCategory.Add(category);
            this.context.SaveChanges();
        }

        public void CreateRating(Rating rating)
        {
            rating.RatingId = this.GetNextId("rating");
            this.context.Rating.Add(rating);
            this.context.SaveChanges();
        }

        public void CreateUser(User user)
        {
            user.UserId = this.GetNextId("user");
            //user.Salt = CypherService.GetSalt();
            user.Password = CypherService.CypherContentNoSalt(user.PasswordString/*, user.Salt*/);
            user.Role = "NormalUser";
            this.context.User.Add(user);
            this.context.SaveChanges();
        }

        public void DeleteBeer(int beerId)
        {
            Beer beer = this.SearchBeer(beerId);
            this.context.Beer.Remove(beer);
            this.context.SaveChanges();
        }

        public void DeleteRating(int ratingId)
        {
            Rating rating = this.SearchRating(ratingId);
            this.context.Rating.Remove(rating);
            this.context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            User user = this.SearchUser(userId);
            this.context.User.Remove(user);
            this.context.SaveChanges();
        }

        public void EditBeer(Beer beer)
        {
            Beer beerr = this.SearchBeer(beer.BeerId);
            beerr.Category = beer.Category;
            beerr.Country = beer.Country;
            beerr.Name = beer.Name;
            beerr.Proof = beer.Proof;
        }

        public int GetNextId(string modelclass)
        {
            int maxId = 0;
            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "";
                if (modelclass == "beer")
                    sql = "selectmaxbeerid";
                else if (modelclass == "rating")
                    sql = "selectmaxratingid";
                else if (modelclass == "user")
                    sql = "selectmaxuserid";
                else if (modelclass == "beercategory")
                    sql = "selectmaxcategoryid";
                else
                    return default(int);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["maxid"] == DBNull.Value)
                        maxId = 0;
                    else
                        maxId = int.Parse(reader["maxid"].ToString());
                }
                reader.Close();
                com.Connection.Close();
            }
            return maxId + 1;
        }
        [ResponseCache(Duration = 15000)]
        public List<BeerCategory> GetCategories()
        {
            return this.context.BeerCategory.ToList();
        }

        public BeerModel GetBeers(int currentPageIndex, int? elementsPerPage)
        {
            if (elementsPerPage == null)
            {
                elementsPerPage = 8;
            }
            BeerModel beerModel = new BeerModel();

            beerModel.Beers = this.context.BeerRating.Where(x => x.IsApproved == true)
                        .OrderBy(x => x.BeerId)
                        .Skip((currentPageIndex - 1) * elementsPerPage.Value)
                        .Take(elementsPerPage.Value).ToList();

            double pageCount = (double)((decimal)this.context.Beer.Count() / Convert.ToDecimal(elementsPerPage));
            beerModel.PageCount = (int)Math.Ceiling(pageCount);

            beerModel.CurrentPageIndex = currentPageIndex;

            return beerModel;
        }

        public List<Rating> GetRatings()
        {
            return this.context.Rating.ToList();
        }

        public List<User> GetUsers()
        {
            return this.context.User.ToList();
        }

        public Beer SearchBeer(int beerId)
        {
            return this.context.Beer.Where(x => x.BeerId == beerId).FirstOrDefault();
        }

        public BeerRating SearchBeerRating(int beerId)
        {
            return this.context.BeerRating.Where(x => x.BeerId == beerId).FirstOrDefault();
        }

        public List<Rating> SearchBeerRatings(int beerId)
        {
            return this.context.Rating.Where(x => x.BeerIdFK == beerId).ToList();
        }

        public BeerModel SearchBeers(string name, int currentPageIndex, int? elementsPerPage)
        {
            if (elementsPerPage == null)
            {
                elementsPerPage = 8;
            }
            BeerModel beerModel = new BeerModel();
            if (currentPageIndex == 0)
                currentPageIndex = 1;

            beerModel.Beers = (from customer in this.context.BeerRating
                               select customer)
                               .Where(x => x.Company.ToLower().Contains(name.ToLower()) || x.Name.ToLower().Contains(name.ToLower()))
                        .OrderBy(x => x.BeerId)
                        .Skip((currentPageIndex - 1) * elementsPerPage.Value)
                        .Take(elementsPerPage.Value).ToList();

            double pageCount = (double)((decimal)beerModel.Beers.Count() / Convert.ToDecimal(elementsPerPage));
            beerModel.PageCount = (int)Math.Ceiling(pageCount);

            beerModel.CurrentPageIndex = currentPageIndex;

            return beerModel;
        }

        public Rating SearchRating(int ratingId)
        {
            return this.context.Rating.Where(x => x.RatingId == ratingId).FirstOrDefault();
        }

        public User SearchUser(string userName)
        {
            return this.context.User.Where(x => x.Username == userName).FirstOrDefault();
        }

        public User SearchUser(int userId)
        {
            User user = this.context.User.Where(x => x.UserId == userId).FirstOrDefault();
            return user;
        }

        public void EditRating(Rating rating)
        {
            Rating rate = this.SearchRating(rating.RatingId);
            rate.Description = rating.Description;
            rate.Qualification = rating.Qualification;
            this.context.SaveChanges();
        }

        public void EditUser(User user)
        {
            User usr = this.SearchUser(user.UserId);
            usr.BirthDate = user.BirthDate;
            usr.Email = user.Email;
            usr.Gender = user.Gender;
            usr.UserBio = user.UserBio;
            usr.Username = user.Username;
            this.context.SaveChanges();
        }

        public void ChangeUserPassword(int userId, string password)
        {
            User user = this.SearchUser(userId);
            user.Password = CypherService.CypherContentNoSalt(password);
            this.context.SaveChanges();
        }

        public List<BeerRating> GetTop25()
        {
            return this.context.BeerRating.Take(25).ToList();
        }

        public void EditCategory(BeerCategory category)
        {
            BeerCategory cate = this.SearchCategory(category.BeerCategoryId);
            cate.Description = category.Description;
            cate.ShortName = category.ShortName;
            this.context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            BeerCategory cate = this.SearchCategory(categoryId);
            this.context.BeerCategory.Remove(cate);
            this.context.SaveChanges();
        }

        public BeerCategory SearchCategory(int categoryId)
        {
            return this.context.BeerCategory.Where(x => x.BeerCategoryId == categoryId).FirstOrDefault();
        }

        public User ExisteUsuario(string username, string password)
        {
            User user = this.context.User.Where(x => x.Username == username).FirstOrDefault();
            //String salt = user.Salt;
            byte[] passbbdd = user.Password;
            byte[] passtemporal =
            CypherService.CypherContentNoSalt(password/*, salt*/);
            //COMPARAR ARRAY BYTES[]
            bool respuesta = HelperToolkit.CompararArrayBytes(passbbdd, passtemporal);
            if (respuesta == true)
            {
                return user;
            }
            else
            {
                return null;
            }

        }

        #region AdminRegion
        public void ApproveCurrentBeer(int beerId)
        {
            Beer beer = this.SearchBeer(beerId);
            beer.IsApproved = true;
            this.context.SaveChanges();
        }

        public void CreateBeerByAdmin(Beer beer)
        {
            beer.IsApproved = true;
            this.context.Add(beer);
            this.context.SaveChanges();
        }

        public BeerModel GetBeersToApprove(int currentPageIndex, int? elementsPerPage)
        {
            if (elementsPerPage == null)
            {
                elementsPerPage = 8;
            }
            BeerModel beerModel = new BeerModel();

            beerModel.Beers = (from customer in this.context.BeerRating
                               select customer)
                        .Where(x => x.IsApproved == false)
                        .OrderBy(x => x.BeerId)
                        .Skip((currentPageIndex - 1) * elementsPerPage.Value)
                        .Take(elementsPerPage.Value).ToList();

            double pageCount = (double)((decimal)this.context.Beer.Count() / Convert.ToDecimal(elementsPerPage));
            beerModel.PageCount = (int)Math.Ceiling(pageCount);

            beerModel.CurrentPageIndex = currentPageIndex;

            return beerModel;
        }
        #endregion
    }
}
