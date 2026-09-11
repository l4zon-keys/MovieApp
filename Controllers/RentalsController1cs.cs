using MovieApp.Data;
using MovieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace MovieApp.Controllers
{
    public class RentalsController : Controller
    {
        private readonly MoviesDBContext _context;

        public RentalsController(MoviesDBContext context)
        {
            _context = context;
        }

        // GET: /Rentals
        public IActionResult Index()
        {
            var rentals = _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .OrderByDescending(r => r.RentedOn)
                .ToList();

            return View(rentals);
        }

        // GET: /Rentals/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: /Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {
            // 1. Basic form validation (Required fields, etc.)
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(rental);
            }

            // 2. Does the movie exist?
            var movie = _context.Movies.FirstOrDefault(m => m.Id == rental.MovieId);
            if (movie == null) return NotFound();

            // 3. Are there copies available?
            var copiesOut = _context.Rentals
                .Count(r => r.MovieId == rental.MovieId && !r.IsReturned);

            if (copiesOut >= movie.QuantityInStock)
            {
                ModelState.AddModelError("", $"No copies of '{movie.Title}' are available right now.");
                PopulateDropdowns();
                return View(rental);
            }

            // 4. NEW RULE: Does the customer have overdue rentals?
            var hasOverdue = _context.Rentals
                .Any(r => r.CustomerId == rental.CustomerId
                          && !r.IsReturned
                          && r.DueOn < DateTime.Now);

            if (hasOverdue)
            {
                var customer = _context.Customers.Find(rental.CustomerId);
                ModelState.AddModelError("",
                    $"{customer?.FullName} has overdue rentals and cannot rent more until they're returned.");
                PopulateDropdowns();
                return View(rental);
            }

            // 5. All checks passed — save the rental
            rental.RentedOn = DateTime.Now;
            rental.DueOn = DateTime.Now.AddDays(7);
            rental.IsReturned = false;

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            // 6. Success message and redirect
            var rentedTo = _context.Customers.Find(rental.CustomerId);
            TempData["Success"] = $"'{movie.Title}' rented to {rentedTo?.FullName}.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Rentals/Return/5
        public IActionResult Return(int id)
        {
            var rental = _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .FirstOrDefault(r => r.Id == id);

            if (rental == null) return NotFound();
            return View(rental);
        }

        // POST: /Rentals/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public IActionResult ReturnConfirmed(int id)
        {
            var rental = _context.Rentals.FirstOrDefault(r => r.Id == id);
            if (rental == null) return NotFound();

            if (!rental.IsReturned)
            {
                rental.IsReturned = true;
                rental.ReturnedOn = DateTime.Now;
                _context.SaveChanges();
                TempData["Success"] = "Rental marked as returned.";
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: /Rentals/Overdue
        public IActionResult Overdue()
        {
            var now = DateTime.Now;
            var overdue = _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .Where(r => !r.IsReturned && r.DueOn < now)
                .OrderBy(r => r.DueOn)
                .ToList();

            return View(overdue);
        }

        private void PopulateDropdowns()
        {
            ViewBag.Customers = new SelectList(
                _context.Customers.OrderBy(c => c.FullName).ToList(),
                "Id", "FullName");

            // Only movies with at least one copy available
            var availableMovies = _context.Movies
                .Where(m => m.QuantityInStock > _context.Rentals
                    .Count(r => r.MovieId == m.Id && !r.IsReturned))
                .OrderBy(m => m.Title)
                .ToList();

            // Use a custom text field with availability count
            var movieItems = availableMovies.Select(m => new
            {
                Id = m.Id,
                Display = $"{m.Title} ({m.QuantityInStock - _context.Rentals.Count(r => r.MovieId == m.Id && !r.IsReturned)} available)"
            }).ToList();

            ViewBag.Movies = new SelectList(movieItems, "Id", "Display");
        }
    }
}
