using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
       
        [Required]
        [StringLength(100)]
        public string Genre { get; set; }= string.Empty; 

        [DisplayName("Release Year")]
        [Range (1900,2100, ErrorMessage = "Release year must be between 1900 and 2100.")]
        public int ReleaseYear { get; set; }

        public string? Rating { get; set; }

        [Required]
        [DisplayName("Copies Owned")]
        [Range (0, 1000)]
        public int QuantityInStock { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
