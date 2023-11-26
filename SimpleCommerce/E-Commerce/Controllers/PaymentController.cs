using E_Commerce.Business.Concrete;
using E_Commerce.Core.Data;
using E_Commerce.Shopping.Cart;
using E_Commerce.Shopping.Model;
using E_Commerce.Shopping.PaymentData;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOptions<ApiKeyConfig> _apiKeyConfig;
        private readonly IOptions<SecretKeyConfig> _secretKeyConfig;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserService _userService;
        private readonly _PaymentService _paymentService;
        Iyzipay.Options options = new Iyzipay.Options();
        public PaymentController(ShoppingCart shoppingCart,UserService userService, _PaymentService paymentService,IOptions<ApiKeyConfig> apiKeyConfig ,IOptions<SecretKeyConfig> secretKeyConfig)
        {
            _shoppingCart = shoppingCart;
            _userService = userService;
            _paymentService = paymentService;
            _secretKeyConfig = secretKeyConfig;
            _apiKeyConfig = apiKeyConfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentMethod()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> PaymentMethod(string cardholdername, string cardnumber, string expiremonth, string expireyear, string cvc)
        {


            options.ApiKey = _apiKeyConfig.Value.ApiKey;
            options.SecretKey = _secretKeyConfig.Value.SecretKey;
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInfo=_userService.GetAsync(userID);
            
           
            var ipAdress =Response.HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ipAdress=="::1")
            {
                ipAdress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            }
            Guid guid = Guid.NewGuid();

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = guid.ToString();
            request.Price = _shoppingCart.GetShoppingCartTotal().ToString();
            request.PaidPrice = _shoppingCart.GetShoppingCartTotal().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = _shoppingCart.ShoppingCartID;
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();


            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = cardholdername;
            paymentCard.CardNumber = cardnumber;
            paymentCard.ExpireMonth = expiremonth;
            paymentCard.ExpireYear = expireyear;
            paymentCard.Cvc = cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;


            Buyer buyer = new Buyer();
            buyer.Id = userID;
            buyer.Name = userInfo.Name;
            buyer.Surname = userInfo.SurName;
            buyer.GsmNumber = userInfo.PhoneNumber;
            buyer.Email = userInfo.Email;
            buyer.IdentityNumber = userInfo.IdentityNumber;
            buyer.LastLoginDate = userInfo.LastLoginDate;
            buyer.RegistrationDate = userInfo.RegistrationDate;
            buyer.RegistrationAddress = userInfo.RegistrationAdress;
            buyer.Ip = ipAdress;
            buyer.City = userInfo.City;
            buyer.Country = userInfo.Country;
            buyer.ZipCode = userInfo.ZipCode;
            request.Buyer = buyer;


            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;
            request.BillingAddress = shippingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            var item = _shoppingCart.GetShoppingCartItems();
            for (int i = 0; i < item.Count; i++)
            {
                var basket = new BasketItem()
                {
                    Name = item[i].Movie.Name,
                    Id = item[i].Movie.ID.ToString(),
                    Price = item[i].Movie.Price.ToString(),
                    Category1 = item[i].Movie.Category.ToString(),
                    Category2 = item[i].Movie.Category.ToString(),
                    ItemType = BasketItemType.VIRTUAL.ToString()
                };
                basketItems.Add(basket);
            }
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
            
            if (payment.Status=="success")
            {
                var pData = new _PaymentData()
                {
                    PaymentID = payment.PaymentId,
                    IpAddress = ipAdress
                };
               await _paymentService.Add(pData);
                return RedirectToAction("CompleteOrder", "Orders");
            }
            else
            {
                return Content(payment.Status);
            }

            
        }
       
    }
}
