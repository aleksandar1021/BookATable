using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class insertIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealCategoryRestaurants_MealCategoryId",
                table: "MealCategoryRestaurants");

            migrationBuilder.CreateIndex(
                name: "IX_MealCategoryRestaurants_MealCategoryId_RestaurantId",
                table: "MealCategoryRestaurants",
                columns: new[] { "MealCategoryId", "RestaurantId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealCategoryRestaurants_MealCategoryId_RestaurantId",
                table: "MealCategoryRestaurants");

            migrationBuilder.CreateIndex(
                name: "IX_MealCategoryRestaurants_MealCategoryId",
                table: "MealCategoryRestaurants",
                column: "MealCategoryId");
        }
    }
}
