using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.DTOs;
using api.Models;
using System.Security.Claims;

namespace api.Controllers
{
    /// <summary>
    /// Controller for managing recipe-related operations in the application.
    /// Provides endpoints for creating, retrieving, updating, and deleting recipes,
    /// as well as saving and unsaving recipes for users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeController"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing recipe and user data.</param>
        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all recipes with optional filtering by cuisine type.
        /// </summary>
        /// <param name="cuisine">Optional cuisine filter parameter.</param>
        /// <returns>A list of recipes matching the filter criteria.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetRecipes([FromQuery] string? cuisine)
        {
            var userId = User.Identity?.IsAuthenticated == true
                ? int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0")
                : 0;

            var query = _context.Recipes.AsQueryable();

            // Apply cuisine filter if provided
            if (!string.IsNullOrEmpty(cuisine))
            {
                query = query.Where(r => r.Cuisine == cuisine);
            }

            var recipes = await query
                .Include(r => r.Author)
                .Include(r => r.SavedByUsers)
                .Select(r => new RecipeListItemDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Image = r.Image,
                    Description = r.Description,
                    Cuisine = r.Cuisine,
                    CookingTime = r.CookingTime,
                    AuthorName = r.Author.FullName ?? r.Author.Email,
                    IsSaved = userId != 0 && r.SavedByUsers.Any(u => u.Id == userId)
                })
                .ToListAsync();

            return recipes;
        }

        /// <summary>
        /// Gets a specific recipe by its ID.
        /// </summary>
        /// <param name="id">The ID of the recipe to retrieve.</param>
        /// <returns>The requested recipe details.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var userId = User.Identity?.IsAuthenticated == true
                ? int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0")
                : 0;

            var recipe = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.SavedByUsers)
                .Where(r => r.Id == id)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Image = r.Image,
                    Description = r.Description,
                    Ingredients = r.Ingredients,
                    Instructions = r.Instructions,
                    Servings = r.Servings,
                    CookingTime = r.CookingTime,
                    PrepTime = r.PrepTime,
                    Cuisine = r.Cuisine,
                    Nutrition = r.NutritionFacts != null ? new NutritionDto
                    {
                        Calories = r.NutritionFacts.Calories,
                        Carbohydrates = r.NutritionFacts.Carbohydrates,
                        Protein = r.NutritionFacts.Protein,
                        Fat = r.NutritionFacts.Fat
                    } : null,
                    Author = new UserDto
                    {
                        Id = r.Author.Id,
                        Email = r.Author.Email,
                        FullName = r.Author.FullName ?? string.Empty,
                        ProfilePicture = r.Author.ProfilePicture,
                        Bio = r.Author.Bio
                    },
                    IsSaved = userId != 0 && r.SavedByUsers.Any(u => u.Id == userId)
                })
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        /// <summary>
        /// Creates a new recipe for the authenticated user.
        /// </summary>
        /// <param name="dto">The recipe data to create.</param>
        /// <returns>The newly created recipe.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RecipeDto>> CreateRecipe(CreateRecipeDto dto)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            // Debug log to see the received image value
            Console.WriteLine($"Received image value: {dto.Image ?? "null"}");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            var recipe = new Recipe
            {
                Title = dto.Title,
                Image = dto.Image,
                Description = dto.Description,
                Ingredients = dto.Ingredients,
                Instructions = dto.Instructions,
                Servings = dto.Servings,
                CookingTime = dto.CookingTime,
                PrepTime = dto.PrepTime,
                Cuisine = dto.Cuisine,
                UserId = userId,
                Author = user
            };

            if (dto.Nutrition != null)
            {
                recipe.NutritionFacts = new NutritionFacts
                {
                    Calories = dto.Nutrition.Calories,
                    Carbohydrates = dto.Nutrition.Carbohydrates,
                    Protein = dto.Nutrition.Protein,
                    Fat = dto.Nutrition.Fat
                };
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            var recipeDto = new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Image = recipe.Image,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                Servings = recipe.Servings,
                CookingTime = recipe.CookingTime,
                PrepTime = recipe.PrepTime,
                Cuisine = recipe.Cuisine,
                Nutrition = recipe.NutritionFacts != null ? new NutritionDto
                {
                    Calories = recipe.NutritionFacts.Calories,
                    Carbohydrates = recipe.NutritionFacts.Carbohydrates,
                    Protein = recipe.NutritionFacts.Protein,
                    Fat = recipe.NutritionFacts.Fat
                } : null,
                Author = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName ?? string.Empty,
                    ProfilePicture = user.ProfilePicture,
                    Bio = user.Bio
                },
                IsSaved = false
            };

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipeDto);
        }

        /// <summary>
        /// Updates an existing recipe owned by the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the recipe to update.</param>
        /// <param name="dto">The updated recipe data.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int id, UpdateRecipeDto dto)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.UserId != userId)
            {
                return Forbid();
            }

            // Debug log to see the received image value
            Console.WriteLine($"Updating recipe {id} with image value: {dto.Image ?? "null"}");

            recipe.Title = dto.Title;
            recipe.Image = dto.Image;
            recipe.Description = dto.Description;
            recipe.Ingredients = dto.Ingredients;
            recipe.Instructions = dto.Instructions;
            recipe.Servings = dto.Servings;
            recipe.CookingTime = dto.CookingTime;
            recipe.PrepTime = dto.PrepTime;
            recipe.Cuisine = dto.Cuisine;

            if (dto.Nutrition != null)
            {
                recipe.NutritionFacts ??= new NutritionFacts();
                recipe.NutritionFacts.Calories = dto.Nutrition.Calories;
                recipe.NutritionFacts.Carbohydrates = dto.Nutrition.Carbohydrates;
                recipe.NutritionFacts.Protein = dto.Nutrition.Protein;
                recipe.NutritionFacts.Fat = dto.Nutrition.Fat;
            }
            else
            {
                recipe.NutritionFacts = null;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a recipe owned by the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the recipe to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.UserId != userId)
            {
                return Forbid();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Saves a recipe for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the recipe to save.</param>
        /// <returns>No content if the save operation is successful.</returns>
        [HttpPost("{id}/save")]
        [Authorize]
        public async Task<IActionResult> SaveRecipe(int id)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users
                .Include(u => u.SavedRecipes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            if (user.SavedRecipes?.Any(r => r.Id == id) == true)
            {
                return BadRequest("Recipe already saved");
            }

            user.SavedRecipes ??= new List<Recipe>();
            user.SavedRecipes.Add(recipe);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Unsaves (removes from saved recipes) a recipe for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the recipe to unsave.</param>
        /// <returns>No content if the unsave operation is successful.</returns>
        [HttpDelete("{id}/save")]
        [Authorize]
        public async Task<IActionResult> UnsaveRecipe(int id)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users
                .Include(u => u.SavedRecipes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var recipe = user.SavedRecipes?.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            user.SavedRecipes?.Remove(recipe);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets all recipes saved by the authenticated user.
        /// </summary>
        /// <returns>A list of saved recipes.</returns>
        [HttpGet("saved")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetSavedRecipes()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users
                .Include(u => u.SavedRecipes!)
                    .ThenInclude(r => r.Author)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var recipes = user.SavedRecipes?
                .Select(r => new RecipeListItemDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Image = r.Image,
                    Description = r.Description,
                    Cuisine = r.Cuisine,
                    CookingTime = r.CookingTime,
                    AuthorName = r.Author?.FullName ?? r.Author?.Email ?? string.Empty,
                    IsSaved = true
                })
                .ToList() ?? new List<RecipeListItemDto>();

            return recipes;
        }

        /// <summary>
        /// Gets all recipes created by the authenticated user.
        /// </summary>
        /// <returns>A list of the user's created recipes.</returns>
        [HttpGet("my")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetMyRecipes()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var recipes = await _context.Recipes
                .Where(r => r.UserId == userId)
                .Include(r => r.Author)
                .Select(r => new RecipeListItemDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Image = r.Image,
                    Description = r.Description,
                    Cuisine = r.Cuisine,
                    CookingTime = r.CookingTime,
                    AuthorName = r.Author.FullName ?? r.Author.Email,
                    IsSaved = false // These are the user's own recipes, not saved recipes
                })
                .ToListAsync();

            return recipes;
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
} 