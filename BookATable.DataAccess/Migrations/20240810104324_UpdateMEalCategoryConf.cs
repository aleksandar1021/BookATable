using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMEalCategoryConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "MealCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "MealCategories");
        }
    }
}
