using MovieApp.Data;
using MovieApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MoviesDBContext _context;

        public HomeController(MoviesDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;

            var model = new DashboardViewModel
            {
                TotalMovies = _context.Movies.Count(),
                TotalCopies = _context.Movies.Sum(m => m.QuantityInStock),
                CopiesOut = _context.Rentals.Count(r => !r.IsReturned),
                OverdueRentals = _context.Rentals.Count(r => !r.IsReturned && r.DueOn < now),
                TotalCustomers = _context.Customers.Count(),
                ActiveCustomers = _context.Customers
                    .Count(c => c.Rentals.Any(r => !r.IsReturned)),
                RecentRentals = _context.Rentals
                    .Include(r => r.Customer)
                    .Include(r => r.Movie)
                    .OrderByDescending(r => r.RentedOn)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
