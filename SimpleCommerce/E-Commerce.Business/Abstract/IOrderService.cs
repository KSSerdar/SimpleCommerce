using E_Commerce.Core.Entitites;
using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userID, string userEmailAdress); 
        Task<List<Order>>GetOrderByUserIDAndRoleAsync(string userID,string role);

        
    }
}
