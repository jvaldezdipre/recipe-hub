using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FixNutritionFactsMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nutrition_Protein",
                table: "Recipes",
                newName: "NutritionFacts_Protein");

            migrationBuilder.RenameColumn(
                name: "Nutrition_Fat",
                table: "Recipes",
                newName: "NutritionFacts_Fat");

            migrationBuilder.RenameColumn(
                name: "Nutrition_Carbohydrates",
                table: "Recipes",
                newName: "NutritionFacts_Carbohydrates");

            migrationBuilder.RenameColumn(
                name: "Nutrition_Calories",
                table: "Recipes",
                newName: "NutritionFacts_Calories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NutritionFacts_Protein",
                table: "Recipes",
                newName: "Nutrition_Protein");

            migrationBuilder.RenameColumn(
                name: "NutritionFacts_Fat",
                table: "Recipes",
                newName: "Nutrition_Fat");

            migrationBuilder.RenameColumn(
                name: "NutritionFacts_Carbohydrates",
                table: "Recipes",
                newName: "Nutrition_Carbohydrates");

            migrationBuilder.RenameColumn(
                name: "NutritionFacts_Calories",
                table: "Recipes",
                newName: "Nutrition_Calories");
        }
    }
}
