using MovieApp.Data;
using MovieApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace MovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesDBContext _context;

        public MoviesController(MoviesDBContext context)
        {
            _context = context;
        }

        // GET: /Movies
        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        // GET: /Movies/Details/5
        public IActionResult Details(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // GET: /Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Movies/Edit/5
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // POST: /Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            var existing = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (existing == null) return NotFound();

            existing.Title = movie.Title;
            existing.Genre = movie.Genre;
            existing.ReleaseYear = movie.ReleaseYear;
            existing.Rating = movie.Rating;
            existing.QuantityInStock = movie.QuantityInStock;

            _context.SaveChanges();
            TempData["Success"] = "Movie details updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Movies/Delete/5
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}