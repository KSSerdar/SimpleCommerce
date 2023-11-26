using E_Commerce.Core.Data;
using E_Commerce.Core.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.DALContext
{
    public class CommerceContext:IdentityDbContext<ApplicationUser>
    {
        public CommerceContext(DbContextOptions<CommerceContext>options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(c => new
            {
                c.ActorID,
                c.MovieID
            });
            modelBuilder.Entity<Actor_Movie>().HasOne(c => c.Movie).WithMany(c => c.Actor_Movies).HasForeignKey(c => c.MovieID);
            modelBuilder.Entity<Actor_Movie>().HasOne(c => c.Actor).WithMany(c => c.Actor_Movies).HasForeignKey(c => c.ActorID);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor_Movie> Actor_Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<_PaymentData> PayData { get; set; }

    }
}
