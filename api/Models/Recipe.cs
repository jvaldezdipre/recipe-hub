using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Image { get; set; }
        public string? Description { get; set; }

        [Required]
        public List<string> Ingredients { get; set; } = new();

        [Required]
        public List<string> Instructions { get; set; } = new();

        public int Servings { get; set; }
        public int CookingTime { get; set; }
        public int PrepTime { get; set; }
        public string? Cuisine { get; set; }

        // Nutrition Facts (Embedded Value Object)
        public NutritionFacts? Nutrition { get; set; }

        // Ratings
        public List<Rating> Ratings { get; set; } = new();

        // Foreign Key for User (Author)
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

    public class NutritionFacts
    {
        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
    }
}