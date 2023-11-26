using E_Commerce.Core.Entitites;
using E_Commerce.DAL.Abstract;
using E_Commerce.DAL.DALContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly CommerceContext _commerceContext;

        public BaseRepository(CommerceContext commerceContext)
        {
            _commerceContext = commerceContext;
        }

        public async Task AddAsync(T actor)
        {
            await _commerceContext.Set<T>().AddAsync(actor);
            await _commerceContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _commerceContext.Set<T>().FirstOrDefaultAsync(c => c.ID == id);
            EntityEntry entry = _commerceContext.Entry<T>(entity);
            entry.State = EntityState.Deleted;
            await _commerceContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result=await _commerceContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] predicate)
        {
            IQueryable<T> query = _commerceContext.Set<T>();
            query = predicate.Aggregate(query, (current, includePropery) => current.Include(includePropery));
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var result=await _commerceContext.Set<T>().FirstOrDefaultAsync(c=>c.ID==id);
            return result;
        }

        public async Task UpdateAsync(int id, T actor)
        {
            EntityEntry entry = _commerceContext.Entry<T>(actor);
            entry.State= EntityState.Modified;
            await _commerceContext.SaveChangesAsync();
        }
    }
}
