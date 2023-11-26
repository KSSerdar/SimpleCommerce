using E_Commerce.Shopping.Cart;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
    public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var a=_shoppingCart.GetShoppingCartItems();
            return View(a.Count);
        }
    }
}
