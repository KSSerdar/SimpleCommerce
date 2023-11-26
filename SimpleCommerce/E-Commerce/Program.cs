
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Concrete;
using E_Commerce.Core.Entitites;
using E_Commerce.Shopping.Cart;
using E_Commerce.DAL.Concrete;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Shopping.PaymentData;
using E_Commerce.Core.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CommerceContext>(c=>c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<ApiKeyConfig>(builder.Configuration.GetSection("Api_Key"));
builder.Services.Configure<SecretKeyConfig>(builder.Configuration.GetSection("Secret_Key"));
builder.Services.AddScoped<IActorService,ActorService>();
builder.Services.AddScoped<IProducerService,ProducerService>();
builder.Services.AddScoped<ICinemaService,CinemaService>();
builder.Services.AddScoped<IMovieService,MovieService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped(sc=>ShoppingCart.GetShoppingCart(sc));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<_PaymentService>();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<CommerceContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(c =>
{
    c.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
CommDbInitializer.Seed(app);
CommDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();
