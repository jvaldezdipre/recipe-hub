using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? ProfilePicture { get; set; }

        public string? Bio { get; set; }

        // Relationships
        public List<Recipe> Recipes { get; set; } = new();
        public List<Recipe> SavedRecipes { get; set; } = new();
        public List<Cookbook> Cookbooks { get; set; } = new();
    }
} 