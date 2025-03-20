using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    /// <summary>
    /// Database context for the Recipe Hub application. Provides access to all database tables
    /// and configures relationships between entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// <param name="dbContextOptions">The options to be used by this DB context.</param>
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        /// <summary>
        /// Configures the database model and relationships between entities.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to construct the model for this context.</param>
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