using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDishConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dishs_Name",
                table: "Dishs");

            migrationBuilder.CreateIndex(
                name: "IX_Dishs_Name",
                table: "Dishs",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dishs_Name",
                table: "Dishs");

            migrationBuilder.CreateIndex(
                name: "IX_Dishs_Name",
                table: "Dishs",
                column: "Name",
                unique: true);
        }
    }
}
