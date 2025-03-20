using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}

        // Dbsets allows us to query and save instances of these types to the database
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many for SavedRecipes
            modelBuilder.Entity<User>()
                .HasMany(u => u.SavedRecipes)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserSavedRecipes",
                    j => j
                .HasOne<Recipe>()
                .WithMany()
                .HasForeignKey("RecipeId")
                  // You can keep cascade on the Recipe side if desired
                .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                  // Set to NoAction (or Restrict) to avoid multiple cascade paths
                  .OnDelete(DeleteBehavior.NoAction)
            );

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Author)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}