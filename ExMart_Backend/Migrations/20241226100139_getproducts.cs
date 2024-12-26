using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class getproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 1, 39, 20, DateTimeKind.Utc).AddTicks(2478), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 1, 39, 20, DateTimeKind.Utc).AddTicks(2481) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 9, 59, 55, 924, DateTimeKind.Utc).AddTicks(7793), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 9, 59, 55, 924, DateTimeKind.Utc).AddTicks(7796) });
        }
    }
}
