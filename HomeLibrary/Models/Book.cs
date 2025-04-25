using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Author { get; set; }

        [Range(0,9999)]
        public int Year { get; set; }

        [Required]
        public required string Publisher { get; set; }

        [Required]
        public required string Genre { get; set; }

        public string? Description { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        public bool wishlist { get; set; }
    }
}
