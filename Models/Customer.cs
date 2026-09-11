using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(150)]
        [DisplayName("Email Address")]
        public string? Email { get; set; }

        [DisplayName("Joined On")]
        [DataType(DataType.Date)]
        public DateTime JoinedOn { get; set; } = DateTime.Now;

        // Navigation property: one customer has many rentals
        public List<Rental> Rentals { get; set; } = new();
    }
}
