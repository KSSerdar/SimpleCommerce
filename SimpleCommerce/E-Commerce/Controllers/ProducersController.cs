using E_Commerce.Business.Abstract;
using E_Commerce.Business.Concrete;
using E_Commerce.Core.Entitites;
using E_Commerce.Core.Static;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Authorize(Roles =Roles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var a = await _producerService.GetAllAsync();
            return View(a);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,ProfilePicture,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                return View(producer);
            }

            await _producerService.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var getByID = await _producerService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ProfilePicture,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                return View(producer);
            }

            await _producerService.UpdateAsync(id, producer);

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var getByID = await _producerService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            return View(getByID);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var getByID = await _producerService.GetAsync(id);
            if (getByID == null)
            {
                return View("NotFound");
            }
            await _producerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var a = await _producerService.GetAsync(id);
            if (a == null) return View("NotFound"); 
            return View(a);
        }

    }
}
