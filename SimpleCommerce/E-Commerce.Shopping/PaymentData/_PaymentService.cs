using E_Commerce.Core.Data;
using E_Commerce.Core.Entitites;
using E_Commerce.DAL.DALContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shopping.PaymentData
{
    public class _PaymentService
    {
        private readonly CommerceContext _commerceContext;


        public _PaymentService(CommerceContext commerceContext)
        {
            _commerceContext = commerceContext;
        }

        public async Task Add(_PaymentData payData)
        {
             await _commerceContext.Set<_PaymentData>().AddAsync(payData);
             await _commerceContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<_PaymentData>> GetAllAsync(params Expression<Func<_PaymentData, object>>[] predicate)
        {
            IQueryable<_PaymentData> query = _commerceContext.Set<_PaymentData>();
            query = predicate.Aggregate(query, (current, includePropery) => current.Include(includePropery));
            return  await query.ToListAsync();
        }
        public async Task<_PaymentData>  GetAsync(string id)
        {
            var result = await _commerceContext.Set<_PaymentData>().FirstOrDefaultAsync(c => c.PaymentID == id);
            return result;
        }
    }
}
