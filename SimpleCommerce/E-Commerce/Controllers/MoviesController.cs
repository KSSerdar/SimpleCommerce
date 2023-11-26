using E_Commerce.Business.Abstract;
using E_Commerce.Core.Data;
using E_Commerce.Core.Static;
using E_Commerce.DAL.DALContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var a = await _movieService.GetAllAsync(c=>c.Cinema);
            return View(a);
        }
        [AllowAnonymous]
        public async Task<IActionResult>Detail(int id)
        {
            var a= await _movieService.GetMovieByID(id);
            if (a==null)
            {
                return View("NotFound");
            }
            return View(a);
        }
        [AllowAnonymous]
        public async Task<IActionResult>Filter(string searchString)
        {
            var getallMovies = await _movieService.GetAllAsync(c => c.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filtered = getallMovies.Where(c => c.Name.Contains(searchString,StringComparison.OrdinalIgnoreCase) || c.Description.Contains(searchString,StringComparison.OrdinalIgnoreCase)).ToList();
                return View("Index",filtered);
            }
            return View("Index",getallMovies);
        }
        public async Task<IActionResult> Create()
        {
            var dropDown =await _movieService.GetDropDown();
            ViewBag.Cinema = new SelectList(dropDown.Cinemas, "ID", "Name");
            ViewBag.Producer = new SelectList(dropDown.Producers, "ID", "Name");
            ViewBag.Actor = new SelectList(dropDown.Actors, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(NewMovie newMovie)
        {
            if (!ModelState.IsValid)
            {
                var dropDown = await _movieService.GetDropDown();
                ViewBag.Cinema = new SelectList(dropDown.Cinemas, "ID", "Name");
                ViewBag.Producer = new SelectList(dropDown.Producers, "ID", "Name");
                ViewBag.Actor = new SelectList(dropDown.Actors, "ID", "Name");
                return View(newMovie);
            }
            await _movieService.AddNewMovieAsync(newMovie);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var movie=await _movieService.GetMovieByID(id);
            if (movie == null)return View("NotFound");
            var response = new NewMovie()
            {
                ID=movie.ID,
                Name=movie.Name,
                Description=movie.Description,
                Category=movie.Category,
                EndTime=movie.EndTime,
                StartTime=movie.StartTime,
                Price=movie.Price,
                CinemaID=movie.CinemaID,
                ProducerID=movie.ProducerID,
                ImageURL=movie.ImageURL,
                ActorIDs = movie.Actor_Movies.Select(c=>c.ActorID).ToList()
            };
            var dropDown = await _movieService.GetDropDown();
            ViewBag.Cinema = new SelectList(dropDown.Cinemas, "ID", "Name");
            ViewBag.Producer = new SelectList(dropDown.Producers, "ID", "Name");
            ViewBag.Actor = new SelectList(dropDown.Actors, "ID", "Name");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovie newMovie)
        {
            if (id!=newMovie.ID)
            {
                return View("NotFound");
            }
            if (!ModelState.IsValid)
            {
                var dropDown = await _movieService.GetDropDown();
                ViewBag.Cinema = new SelectList(dropDown.Cinemas, "ID", "Name");
                ViewBag.Producer = new SelectList(dropDown.Producers, "ID", "Name");
                ViewBag.Actor = new SelectList(dropDown.Actors, "ID", "Name");
                return View(newMovie);
            }
            await _movieService.UpdateNewMovieAsync(newMovie);
            return RedirectToAction(nameof(Index));
        }
    }
}
