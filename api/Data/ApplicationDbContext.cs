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
                public DbSet<Rating> Ratings { get; set; }
                public DbSet<Cookbook> Cookbooks { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many for SavedRecipes
            modelBuilder.Entity<User>()
                .HasMany(u => u.SavedRecipes)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>("UserSavedRecipes",
                    j => j.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Author)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many for Cookbooks
            modelBuilder.Entity<Cookbook>()
                .HasMany(c => c.Recipes)
                .WithMany()
                .UsingEntity(j => j.ToTable("CookbookRecipes"));

            // Configure One-to-Many: User -> Ratings
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes ratings if user is deleted

            // Configure One-to-Many: Recipe -> Ratings
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Recipe)
                .WithMany(re => re.Ratings)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes ratings if recipe is deleted
        }
    }
}