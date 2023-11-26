using E_Commerce.Core.Data;
using E_Commerce.Core.Entitites;
using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IMovieService:IRepository<Movie>
    {
        Task<Movie>GetMovieByID(int id);
        Task<DropDownList>GetDropDown();
        Task AddNewMovieAsync(NewMovie movie);
        Task UpdateNewMovieAsync(NewMovie movie);
    }
}
