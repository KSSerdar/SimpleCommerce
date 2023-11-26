using E_Commerce.Business.Abstract;
using E_Commerce.Shopping.Cart;
using E_Commerce.Shopping.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        public OrdersController(IMovieService movieService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders=await _orderService.GetOrderByUserIDAndRoleAsync(userId,userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items=_shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems=items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }
        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            var item=await _movieService.GetMovieByID(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _movieService.GetMovieByID(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _orderService.StoreOrderAsync(items, userID, userEmail);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
       
    }
}
