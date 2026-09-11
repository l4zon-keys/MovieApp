using MovieApp.Data;
using MovieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MovieApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MoviesDBContext _context;

        public CustomersController(MoviesDBContext context)
        {
            _context = context;
        }

        // GET: /Customers
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        // GET: /Customers/Details/5
        public IActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Rentals)
                .ThenInclude(r => r.Movie)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();
            return View(customer);
        }

        // GET: /Customers/Create
        public IActionResult Create()
        {
            var customer = new Customer();  // initializes JoinedOn to DateTime.Now
            return View(customer);
        }

        // POST: /Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Customers/Edit/5
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // POST: /Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            var existing = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.FullName = customer.FullName;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Email = customer.Email;
            existing.JoinedOn = customer.JoinedOn;

            _context.SaveChanges();
            TempData["Success"] = "Customer details updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Customers/Delete/5
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // POST: /Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers
     .Include(c => c.Rentals)
     .FirstOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();

            if (customer.Rentals.Any(r => !r.IsReturned))
            {
                // Refuse to delete — customer still has outstanding rentals
                TempData["Error"] = "Cannot delete: customer has unreturned rentals.";
                return RedirectToAction(nameof(Index));
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
