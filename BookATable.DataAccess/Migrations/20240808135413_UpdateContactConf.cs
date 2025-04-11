using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_Email_Name",
                table: "Contacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email_Name",
                table: "Contacts",
                columns: new[] { "Email", "Name" });
        }
    }
}
