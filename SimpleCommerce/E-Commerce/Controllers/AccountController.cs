using E_Commerce.Core.Data;
using E_Commerce.Core.Entitites;
using E_Commerce.Core.Static;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CommerceContext _commerceContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CommerceContext commerceContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _commerceContext = commerceContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users()
        {
            var users=await _commerceContext.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login()=>View(new LoginVM());
        [HttpPost]
        public async Task<IActionResult>Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user!=null)
            {
                var check=await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (check)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        var a = _commerceContext.Users.FirstOrDefault(x => x.Email == loginVM.EmailAddress);
                        a.LastLoginDate= DateTime.UtcNow.AddTicks(0).ToString("yyyy-mm-dd HH:mm:ss");
                        _commerceContext.Users.Update(a);
                        _commerceContext.SaveChanges();
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong Inputs Try Again Later";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong Inputs Try Again Later";
            return View(loginVM);
        }
        public IActionResult Register() => View(new RegisterVM());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user!=null)
            {
                TempData["Error"] = "Email Address in Already Use";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                Email = registerVM.EmailAddress,
                FullName = registerVM.Name+registerVM.SurName,
                UserName = registerVM.EmailAddress,
                RegistrationDate = DateTime.UtcNow.AddTicks(0).ToString("yyyy-mm-dd HH:mm:ss"),
                City=registerVM.City,
                Country=registerVM.Country,
                IdentityNumber=registerVM.IdentityNumber,
                Name = registerVM.Name,
                SurName=registerVM.SurName,
                ZipCode=registerVM.ZipCode,
                PhoneNumber=registerVM.PhoneNumber,
                RegistrationAdress="Kutahya",
                LastLoginDate= DateTime.UtcNow.AddTicks(0).ToString("yyyy-mm-dd HH:mm:ss"),
                
        };
            var newUserRespponse=await _userManager.CreateAsync(newUser,registerVM.Password);
            if (newUserRespponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Roles.User);
            }
            return View("RegisterCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Movies");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
