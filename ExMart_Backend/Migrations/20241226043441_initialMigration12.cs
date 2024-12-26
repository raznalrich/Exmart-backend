using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 4, 34, 41, 563, DateTimeKind.Utc).AddTicks(1634), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 4, 34, 41, 563, DateTimeKind.Utc).AddTicks(1638) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 4, 31, 46, 610, DateTimeKind.Utc).AddTicks(9036), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 4, 31, 46, 610, DateTimeKind.Utc).AddTicks(9039) });
        }
    }
}
