using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    /// <summary>
    /// Represents a user in the recipe application.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string? FullName { get; set; }
        [MaxLength(255)]
        public string? ProfilePicture { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Bio { get; set; } = string.Empty;
        public List<Recipe> Recipes { get; set; } = new();
        public List<Recipe> SavedRecipes { get; set; } = new();
    }
} 