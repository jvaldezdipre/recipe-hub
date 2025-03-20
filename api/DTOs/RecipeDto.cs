using api.Models;

namespace api.DTOs
{
    /// <summary>
    /// Data Transfer Object for complete recipe details. Used when returning a single recipe
    /// with all its information through the API.
    /// </summary>
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
        public NutritionDto? Nutrition { get; set; }
        public UserDto Author { get; set; } = null!;
        public bool IsSaved { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for nutrition information.
    /// </summary>
    public class NutritionDto
    {
        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for creating a new recipe. Contains all the information
    /// required to create a recipe.
    /// </summary>
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

    /// <summary>
    /// Data Transfer Object for updating an existing recipe. Contains all the information
    /// that can be updated for a recipe.
    /// </summary>
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

    /// <summary>
    /// Data Transfer Object for simplified recipe information in list views.
    /// Contains only essential information for displaying in a list.
    /// </summary>
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