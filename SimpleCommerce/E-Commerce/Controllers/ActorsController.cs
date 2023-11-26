using E_Commerce.Business.Abstract;
using E_Commerce.Core.Entitites;
using E_Commerce.Core.Static;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorService _actorService;

     
        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var a=await _actorService.GetAllAsync();
            return View(a);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,ProfilePicture,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                return View(actor);
            }
           
               await _actorService.AddAsync(actor);
            
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult>Detail(int id)
        {
            var getByID=await _actorService.GetAsync(id);
            if (getByID==null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var getByID = await _actorService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("ID,Name,ProfilePicture,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                return View(actor);
            }

            await _actorService.UpdateAsync(id,actor);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var getByID = await _actorService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var getByID = await _actorService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            await _actorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
