using E_Commerce.Core.Data;
using E_Commerce.Core.Entitites;
using E_Commerce.Core.Static;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Concrete
{
    public class CommDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var builder = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = builder.ServiceProvider.GetService<CommerceContext>();
                context.Database.EnsureCreated();
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                    new Cinema
                    {
                        Name = "ASDF",
                        Description = "asdasdasdasd",

                        Logo = "https://www.datocms-assets.com/64859/1648643624-bir-sinema-filmi-nasil-ortaya-cikar.jpg?q=70&auto=format&w=1280&fit=max&iptc=allow"
                    },
                    new Cinema
                    {
                        Name = "ASDF212",
                        Description = "asdasdasdas123123123123d",
                        Logo = "https://tr.web.img3.acsta.net/newsv7/21/06/01/00/43/4844878.jpg"
                    }
                });

                    context.SaveChanges();
                }
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                         new Producer
                    {
                        Name = "Bahadir",
                        Bio = "ASDASDASDS",

                        ProfilePicture = "https://publish.one37pm.net/wp-content/uploads/2022/10/highest-gross-actor_mobile.jpeg?resize=720%2C780"
                    },
                    new Producer
                    {
                        Name = "Ekrem",
                        Bio = "ASDAasdfasdfSDASDS",
                        ProfilePicture = "https://cdn.britannica.com/92/215392-050-96A4BC1D/Australian-actor-Chris-Hemsworth-2019.jpg"
                    }
                });

                    context.SaveChanges();
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                         new Movie
                    {
                        Name = "ScENE",
                        Description = "asdasdasdasd",
                        Price = 15,
                        CinemaID = 1,
                        ProducerID = 1,
                        Category=MovieCategory.Action,
                        StartTime = DateTime.Now.AddDays(1),
                        EndTime = DateTime.Now.AddDays(25),
                        ImageURL = "https://www.datocms-assets.com/64859/1648643624-bir-sinema-filmi-nasil-ortaya-cikar.jpg?q=70&auto=format&w=1280&fit=max&iptc=allow"
                    },
                    new Movie
                    {
                        Name = "John Wick",
                        Description = " A",
                        Price = 15,
                        CinemaID=2  , ProducerID = 2,
                        Category=MovieCategory.Drama,
                        StartTime = DateTime.Now.AddDays(1),
                        EndTime = DateTime.Now.AddDays(25),
                        ImageURL = "https://image.kanald.com.tr/i/kanald/100/0x0/5a54d0f78685761848714237.jpg"
                    }
                });

                    context.SaveChanges();
                }
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor
                    {
                        Name = "Bahadir",
                        Bio = "ASDASDASDS",

                        ProfilePicture = "https://publish.one37pm.net/wp-content/uploads/2022/10/highest-gross-actor_mobile.jpeg?resize=720%2C780"
                    },
                    new Actor
                    {
                        Name = "Ekrem",
                        Bio = "ASDAasdfasdfSDASDS",
                        ProfilePicture = "https://cdn.britannica.com/92/215392-050-96A4BC1D/Australian-actor-Chris-Hemsworth-2019.jpg"
                    }
                });

                    context.SaveChanges();
                }

                if (!context.Actor_Movies.Any())
                {
                    context.Actor_Movies.AddRange(new List<Actor_Movie>()
                    {
                         new Actor_Movie
                    {
                        ActorID = 1,
                        MovieID = 2,
                    },
                    new Actor_Movie
                    {
                        ActorID = 2,
                        MovieID = 2,
                    },
                    new Actor_Movie
                    {
                        ActorID = 2,
                        MovieID = 1,
                    },
                    new Actor_Movie
                    {
                        ActorID = 1,
                        MovieID = 1,
                    }
                });

                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var rManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await rManager.RoleExistsAsync(Roles.Admin))
                {
                    await rManager.CreateAsync(new IdentityRole(Roles.Admin));
                }
                if (!await rManager.RoleExistsAsync(Roles.User))
                {
                    await rManager.CreateAsync(new IdentityRole(Roles.User));
                }
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminMail = "admin@commerce.com";
                var adminUser = await userManager.FindByEmailAsync(adminMail);
                if (adminUser == null)
                {
                    var newAdmin = new ApplicationUser
                    {
                        FullName = "Admin User",
                        Email = adminMail,
                        UserName = "admin",
                        City = "Istanbul",
                        Country = "Turkey",
                        IdentityNumber = "12345678911",
                        PhoneNumber = "0421212022",
                        Name = "admin",
                        SurName = "admin",
                        ZipCode = "34",
                        RegistrationDate = DateTime.Now.ToString(),
                        RegistrationAdress="A",
                        LastLoginDate= DateTime.Now.ToString(),
                        EmailConfirmed = true
                        
                    };
                    await userManager.CreateAsync(newAdmin, "ASDFG@?1");
                    await userManager.AddToRoleAsync(newAdmin, Roles.Admin);

                }

                string userMail = "user@commerce.com";
                var appUser = await userManager.FindByEmailAsync(userMail);
                if (appUser == null)
                {
                    var newUser = new ApplicationUser
                    {
                        FullName = "User User",
                        Email = userMail,
                        UserName = "user",
                        City = "Istanbul",
                        Country = "Turkey",
                        IdentityNumber = "12345678911",
                        PhoneNumber = "0421212022",
                        Name = "user",
                        SurName = "user",
                        ZipCode = "34",
                        RegistrationDate = DateTime.Now.ToString(),
                        RegistrationAdress = "A",
                        LastLoginDate = DateTime.Now.ToString(),
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(newUser, "ASDFG@?1");
                    await userManager.AddToRoleAsync(newUser, Roles.User);
                }
            }
        }
    }
}
