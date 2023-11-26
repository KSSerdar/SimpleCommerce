using E_Commerce.Business.Abstract;
using E_Commerce.Core.Data;
using E_Commerce.Core.Entitites;
using E_Commerce.DAL.DALContext;
using E_Commerce.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Commerce.Business.Concrete
{
    public class MovieService : BaseRepository<Movie>, IMovieService
    {
        private readonly CommerceContext _commerceContext;
        public MovieService(CommerceContext commerceContext) : base(commerceContext)
        {
            _commerceContext = commerceContext;
        }

        public async Task AddNewMovieAsync(NewMovie movie)
        {
            var newMovie = new Movie()
            {
                Category = movie.Category,
                Description = movie.Description,
                ImageURL = movie.ImageURL,
                EndTime = movie.EndTime,
                StartTime = movie.StartTime,
                Name = movie.Name,
                Price = movie.Price,
                CinemaID = movie.CinemaID,
                ProducerID = movie.ProducerID,
            };
            await _commerceContext.Movies.AddAsync(newMovie);
            await _commerceContext.SaveChangesAsync();
            foreach (var item in movie.ActorIDs)
            {
                var actor = new Actor_Movie()
                {
                    MovieID = newMovie.ID,
                    ActorID = item,
                };
                await _commerceContext.Actor_Movies.AddAsync(actor);
            }
                await _commerceContext.SaveChangesAsync();
        }

        public async Task<DropDownList> GetDropDown()
        {
            var result=new DropDownList();
            result.Producers=await _commerceContext.Producers.OrderBy(c=>c.Name).ToListAsync();
            result.Actors=await _commerceContext.Actors.OrderBy(c=>c.Name).ToListAsync();
            result.Cinemas=await _commerceContext.Cinemas.OrderBy(c=>c.Name).ToListAsync();
            return result; 
        }

        public async Task<Movie> GetMovieByID(int id)
        {
            var result = await _commerceContext.Movies.Include(c => c.Cinema)
                .Include(p => p.Produer)
                .Include(mc => mc.Actor_Movies)
                .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.ID == id);
            return  result;
        }

        public async Task UpdateNewMovieAsync(NewMovie movie)
        {
            var db=await _commerceContext.Movies.FirstOrDefaultAsync(c=>c.ID==movie.ID);
            if (db!=null)
            {
                db.Category = movie.Category;
                db.Description = movie.Description;
                db.ImageURL = movie.ImageURL;
                db.EndTime = movie.EndTime;
                db.StartTime = movie.StartTime;
                db.Name = movie.Name;
                db.Price = movie.Price;
                db.CinemaID = movie.CinemaID;
                db.ProducerID = movie.ProducerID;
                await _commerceContext.SaveChangesAsync();
            }
            var actordb=_commerceContext.Actor_Movies.Where(c=>c.MovieID==movie.ID).ToList();
            _commerceContext.Actor_Movies.RemoveRange(actordb);
            await _commerceContext.SaveChangesAsync();
            foreach (var item in movie.ActorIDs)
            {
                var actor = new Actor_Movie()
                {
                    MovieID = movie.ID,
                    ActorID = item,
                };
                await _commerceContext.Actor_Movies.AddAsync(actor);
                await _commerceContext.SaveChangesAsync();
            }
        }
    }
}
