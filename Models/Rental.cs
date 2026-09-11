using MovieApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Display(Name = "Rented On")]
        [DataType(DataType.Date)]
        public DateTime RentedOn { get; set; } = DateTime.Now;

        [Display(Name = "Due On")]
        [DataType(DataType.Date)]
        public DateTime DueOn { get; set; }

        [Display(Name = "Returned On")]
        [DataType(DataType.Date)]
        public DateTime? ReturnedOn { get; set; }

        [Display(Name = "Is Returned")]
        public bool IsReturned { get; set; } = false;

        // Calculated property (not stored in DB)
        [NotMapped]
        public bool IsOverdue => !IsReturned && DueOn < DateTime.Now;
    }
}
