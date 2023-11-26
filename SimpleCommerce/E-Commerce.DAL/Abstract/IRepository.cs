using E_Commerce.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Abstract
{
    public interface IRepository<T> where T : class, IEntity,new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>>GetAllAsync(params Expression<Func<T, object>>[] predicate);
        Task<T> GetAsync(int id);
        Task AddAsync(T actor);
        Task UpdateAsync(int id, T actor);
        Task DeleteAsync(int id);
    }
}
