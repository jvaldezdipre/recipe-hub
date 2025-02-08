using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
   public class Rating
    {
        // Primary Key
        [Key]
        public int Id { get; set; } 

        // Rating (1-5 stars)
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Score { get; set; }

        // Optional user review
        public string? Comment { get; set; } 

        // Foreign Key to Recipe
        [Required]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        // Foreign Key to User (Reviewer)
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}