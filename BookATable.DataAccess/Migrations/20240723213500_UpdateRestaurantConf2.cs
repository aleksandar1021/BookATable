using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRestaurantConf2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_Name",
                table: "Restaurants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Name",
                table: "Restaurants",
                column: "Name");
        }
    }
}
