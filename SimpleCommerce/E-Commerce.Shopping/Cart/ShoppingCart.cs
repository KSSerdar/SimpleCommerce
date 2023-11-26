using E_Commerce.Core.Entitites;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shopping.Cart
{
    public class ShoppingCart
    {
        public CommerceContext _context { get; set; }
        public string ShoppingCartID { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(CommerceContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context=services.GetService<CommerceContext>();
            string cartID=session.GetString("CartID")??Guid.NewGuid().ToString();
            session.SetString("CartID", cartID);
            return new ShoppingCart(context) { ShoppingCartID = cartID };
        }
        public void AddItemToCart(Movie movie)
        {
            var item=_context.ShoppingCartItems.FirstOrDefault(c=>c.Movie.ID == movie.ID&&c.ShoppingCartID==ShoppingCartID);
            if (item==null) 
            {
                item = new ShoppingCartItem()
                {
                    Movie = movie,
                    ShoppingCartID = ShoppingCartID,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(item);
            }
            else
            {
                item.Amount++;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var item = _context.ShoppingCartItems.FirstOrDefault(c => c.Movie.ID == movie.ID && c.ShoppingCartID == ShoppingCartID);
            if (item != null)
            {
                if (item.Amount>1)
                {
                    item.Amount--;
                }
                else
                {

                _context.ShoppingCartItems.Remove(item);
                }
            }
           
            _context.SaveChanges();
        }
        public List<ShoppingCartItem>GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartID == ShoppingCartID).Include(c => c.Movie).ToList());
        }
        public double GetShoppingCartTotal()=>
        
             _context.ShoppingCartItems.Where(c => c.ShoppingCartID == ShoppingCartID).Select(c => c.Movie.Price * c.Amount).Sum();
        
        public async Task ClearShoppingCartAsync() {
            var items =await _context.ShoppingCartItems.Where(n => n.ShoppingCartID == ShoppingCartID).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
