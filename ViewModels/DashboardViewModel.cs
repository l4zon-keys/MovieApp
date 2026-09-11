
using MovieApp.Models;

namespace MovieApp.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalMovies { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesOut { get; set; }
        public int OverdueRentals { get; set; }
        public int TotalCustomers { get; set; }
        public int ActiveCustomers { get; set; }
        public List<Rental> RecentRentals { get; set; } = new();
    }
}