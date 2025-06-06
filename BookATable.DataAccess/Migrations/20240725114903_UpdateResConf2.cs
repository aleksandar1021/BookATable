﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookATable.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResConf2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeInterval",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 10,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeInterval",
                table: "Restaurants",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 10);
        }
    }
}
