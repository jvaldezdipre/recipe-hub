using api.Models;

namespace api.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Description { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
        public int Servings { get; set; }
        public int CookingTime { get; set; }
        public int PrepTime { get; set; }
        public string? Cuisine { get; set; }
        
        // Nutrition info
        public NutritionDto? Nutrition { get; set; }
        
        // Author info
        public UserDto Author { get; set; } = null!;
        
        // Flag to indicate if the recipe is saved by the current user
        public bool IsSaved { get; set; }
    }

    public class NutritionDto
    {
        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
    }

    public class CreateRecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Description { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
        public int Servings { get; set; }
        public int CookingTime { get; set; }
        public int PrepTime { get; set; }
        public string? Cuisine { get; set; }
        public NutritionDto? Nutrition { get; set; }
    }

    public class UpdateRecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Description { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
        public int Servings { get; set; }
        public int CookingTime { get; set; }
        public int PrepTime { get; set; }
        public string? Cuisine { get; set; }
        public NutritionDto? Nutrition { get; set; }
    }

    public class RecipeListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Cuisine { get; set; }
        public int CookingTime { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public bool IsSaved { get; set; }
    }
} 