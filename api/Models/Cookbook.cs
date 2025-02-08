using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Cookbook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Image { get; set; }

        // Relationship
        public List<Recipe> Recipes { get; set; } = new();

        // Foreign Key for User (Owner)
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}