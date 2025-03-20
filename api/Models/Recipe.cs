using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace api.Models
{
    /// <summary>
    /// Represents a recipe in the application.
    /// </summary>
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
        public NutritionFacts? NutritionFacts { get; set; }
        public int UserId { get; set; }
        public User Author { get; set; } = null!;
        public List<User> SavedByUsers { get; set; } = new();
    }

    /// <summary>
    /// Represents nutrition facts for a recipe. This is an owned entity type that is stored
    /// within the Recipe table.
    /// </summary>
    [Owned]
    public class NutritionFacts
    {
        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
    }
}