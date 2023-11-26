using E_Commerce.Core.Entitites;
using E_Commerce.DAL.DALContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class UserService
    {
        private readonly CommerceContext _commerceContext;

        public UserService(CommerceContext commerceContext)
        {
            _commerceContext = commerceContext;
        }
        public  ApplicationUser GetAsync(string id)
        {
            var result =_commerceContext.Set<ApplicationUser>().FirstOrDefault(c => c.Id == id);
            return result;
        }
    }
}
