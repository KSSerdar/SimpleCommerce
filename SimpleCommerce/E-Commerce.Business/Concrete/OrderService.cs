using E_Commerce.Business.Abstract;
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
    public class OrderService : IOrderService
    {
        private readonly CommerceContext _commerceContext;

        public OrderService(CommerceContext commerceContext)
        {
            _commerceContext = commerceContext;
        }

        public async Task<List<Order>> GetOrderByUserIDAndRoleAsync(string userID,string role)
        {
            var result=await _commerceContext.Orders.Include(c=>c.OrderItems).ThenInclude(c=>c.Movie).Include(c=>c.ApplicationUser).ToListAsync();
            if (role!="Admin")
            {
                result = result.Where(c => c.UserID == userID).ToList();
            }
            return result;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userID, string userEmailAdress)
        {
            var order = new Order()
            {
                UserID = userID,
                Email = userEmailAdress,

            };
            await _commerceContext.Orders.AddAsync(order);
            await _commerceContext.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieID=item.Movie.ID,
                    OrderID=order.ID,
                    Price=item.Movie.Price
                };
                await _commerceContext.OrderItems.AddAsync(orderitem);
            }
               await _commerceContext.SaveChangesAsync();
        }
    }
}
