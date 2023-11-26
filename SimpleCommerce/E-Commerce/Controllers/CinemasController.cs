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
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;

        public CinemasController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var a=await _cinemaService.GetAllAsync();
            return View(a);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                return View(cinema);
            }

            await _cinemaService.AddAsync(cinema);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var getByID = await _cinemaService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Logo,Description")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                return View(cinema);
            }

            await _cinemaService.UpdateAsync(id, cinema);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var getByID = await _cinemaService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var getByID = await _cinemaService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            await _cinemaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var a = await _cinemaService.GetAsync(id);
            if (a == null) return View("NotFound");
            return View(a);
        }
    }
}
