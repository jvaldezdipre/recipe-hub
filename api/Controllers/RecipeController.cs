using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.DTOs;
using api.Models;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetRecipes()
        {
            // Get the current user ID if authenticated
            int? currentUserId = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    currentUserId = userId;
                }
            }

            var recipes = await _context.Recipes
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
                    IsSaved = currentUserId.HasValue && 
                        _context.Set<Dictionary<string, object>>("UserSavedRecipes")
                            .Any(usr => EF.Property<int>(usr, "UserId") == currentUserId.Value && 
                                  EF.Property<int>(usr, "RecipeId") == r.Id)
                })
                .ToListAsync();

            return recipes;
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Author)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Get the current user ID if authenticated
            int? currentUserId = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    currentUserId = userId;
                }
            }

            // Check if recipe is saved by current user
            bool isSaved = false;
            if (currentUserId.HasValue)
            {
                isSaved = await _context.Set<Dictionary<string, object>>("UserSavedRecipes")
                    .AnyAsync(usr => EF.Property<int>(usr, "UserId") == currentUserId.Value && 
                                    EF.Property<int>(usr, "RecipeId") == recipe.Id);
            }

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
                Nutrition = recipe.Nutrition != null ? new NutritionDto
                {
                    Calories = recipe.Nutrition.Calories,
                    Carbohydrates = recipe.Nutrition.Carbohydrates,
                    Protein = recipe.Nutrition.Protein,
                    Fat = recipe.Nutrition.Fat
                } : null,
                Author = new UserDto
                {
                    Id = recipe.Author.Id,
                    Email = recipe.Author.Email,
                    FullName = recipe.Author.FullName ?? string.Empty,
                    ProfilePicture = recipe.Author.ProfilePicture,
                    Bio = recipe.Author.Bio
                },
                IsSaved = isSaved
            };

            return recipeDto;
        }

        // POST: api/Recipe
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RecipeDto>> CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            var recipe = new Recipe
            {
                Title = createRecipeDto.Title,
                Image = createRecipeDto.Image,
                Description = createRecipeDto.Description,
                Ingredients = createRecipeDto.Ingredients,
                Instructions = createRecipeDto.Instructions,
                Servings = createRecipeDto.Servings,
                CookingTime = createRecipeDto.CookingTime,
                PrepTime = createRecipeDto.PrepTime,
                Cuisine = createRecipeDto.Cuisine,
                UserId = userId,
                Nutrition = createRecipeDto.Nutrition != null ? new NutritionFacts
                {
                    Calories = createRecipeDto.Nutrition.Calories,
                    Carbohydrates = createRecipeDto.Nutrition.Carbohydrates,
                    Protein = createRecipeDto.Nutrition.Protein,
                    Fat = createRecipeDto.Nutrition.Fat
                } : null
            };

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
                Nutrition = createRecipeDto.Nutrition,
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

        // PUT: api/Recipe/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int id, UpdateRecipeDto updateRecipeDto)
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            // Check if the user is the author of the recipe
            if (recipe.UserId != userId)
            {
                return Forbid();
            }

            // Update recipe properties
            recipe.Title = updateRecipeDto.Title;
            recipe.Image = updateRecipeDto.Image;
            recipe.Description = updateRecipeDto.Description;
            recipe.Ingredients = updateRecipeDto.Ingredients;
            recipe.Instructions = updateRecipeDto.Instructions;
            recipe.Servings = updateRecipeDto.Servings;
            recipe.CookingTime = updateRecipeDto.CookingTime;
            recipe.PrepTime = updateRecipeDto.PrepTime;
            recipe.Cuisine = updateRecipeDto.Cuisine;
            
            // Update nutrition facts
            if (updateRecipeDto.Nutrition != null)
            {
                recipe.Nutrition = new NutritionFacts
                {
                    Calories = updateRecipeDto.Nutrition.Calories,
                    Carbohydrates = updateRecipeDto.Nutrition.Carbohydrates,
                    Protein = updateRecipeDto.Nutrition.Protein,
                    Fat = updateRecipeDto.Nutrition.Fat
                };
            }
            else
            {
                recipe.Nutrition = null;
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            // Check if the user is the author of the recipe
            if (recipe.UserId != userId)
            {
                return Forbid();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Recipe/5/save
        [HttpPost("{id}/save")]
        [Authorize]
        public async Task<IActionResult> SaveRecipe(int id)
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            // Check if recipe is already saved
            var isSaved = await _context.Set<Dictionary<string, object>>("UserSavedRecipes")
                .AnyAsync(usr => EF.Property<int>(usr, "UserId") == userId && 
                               EF.Property<int>(usr, "RecipeId") == id);

            if (isSaved)
            {
                return BadRequest("Recipe is already saved.");
            }

            // Add the recipe to saved recipes
            var savedRecipe = new Dictionary<string, object>
            {
                ["UserId"] = userId,
                ["RecipeId"] = id
            };

            _context.Set<Dictionary<string, object>>("UserSavedRecipes").Add(savedRecipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Recipe/5/save
        [HttpDelete("{id}/save")]
        [Authorize]
        public async Task<IActionResult> UnsaveRecipe(int id)
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            // Check if recipe exists
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            // Get the saved recipe entry
            var savedRecipe = await _context.Set<Dictionary<string, object>>("UserSavedRecipes")
                .FirstOrDefaultAsync(usr => EF.Property<int>(usr, "UserId") == userId && 
                                         EF.Property<int>(usr, "RecipeId") == id);

            if (savedRecipe == null)
            {
                return BadRequest("Recipe is not saved.");
            }

            // Remove the recipe from saved recipes
            _context.Set<Dictionary<string, object>>("UserSavedRecipes").Remove(savedRecipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Recipe/saved
        [HttpGet("saved")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetSavedRecipes()
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            // Get the saved recipe IDs for the current user
            var savedRecipeIds = await _context.Set<Dictionary<string, object>>("UserSavedRecipes")
                .Where(usr => EF.Property<int>(usr, "UserId") == userId)
                .Select(usr => EF.Property<int>(usr, "RecipeId"))
                .ToListAsync();

            // Get the saved recipes
            var savedRecipes = await _context.Recipes
                .Include(r => r.Author)
                .Where(r => savedRecipeIds.Contains(r.Id))
                .Select(r => new RecipeListItemDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Image = r.Image,
                    Description = r.Description,
                    Cuisine = r.Cuisine,
                    CookingTime = r.CookingTime,
                    AuthorName = r.Author.FullName ?? r.Author.Email,
                    IsSaved = true
                })
                .ToListAsync();

            return savedRecipes;
        }

        // GET: api/Recipe/user
        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RecipeListItemDto>>> GetUserRecipes()
        {
            // Get the current user ID
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            // Get the recipes created by the current user
            var userRecipes = await _context.Recipes
                .Include(r => r.Author)
                .Where(r => r.UserId == userId)
                .Select(r => new RecipeListItemDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Image = r.Image,
                    Description = r.Description,
                    Cuisine = r.Cuisine,
                    CookingTime = r.CookingTime,
                    AuthorName = r.Author.FullName ?? r.Author.Email,
                    IsSaved = false
                })
                .ToListAsync();

            return userRecipes;
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
} 