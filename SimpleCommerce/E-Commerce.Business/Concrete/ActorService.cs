using E_Commerce.Business.Abstract;
using E_Commerce.Core.Entitites;
using E_Commerce.DAL.DALContext;
using E_Commerce.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class ActorService :BaseRepository<Actor>,IActorService
    {
        public ActorService(CommerceContext commerceContext):base(commerceContext) { }

    }
}
