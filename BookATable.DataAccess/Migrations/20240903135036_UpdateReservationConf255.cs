using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservationConf255 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeHour",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TimeMinute",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Reservations");

            migrationBuilder.AddColumn<byte>(
                name: "TimeHour",
                table: "Reservations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "TimeMinute",
                table: "Reservations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
