using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class newProductSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 10, 56, 582, DateTimeKind.Utc).AddTicks(354), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 10, 56, 582, DateTimeKind.Utc).AddTicks(357) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Color", "CreatedAt", "CreatedBy", "Description", "Name", "Price", "Size", "UpdatedAt", "VendorId", "Weight" },
                values: new object[] { 2, "ABC", "C003", new List<string> { "Black", "Green", "Purple" }, new DateTime(2024, 12, 26, 10, 10, 56, 582, DateTimeKind.Utc).AddTicks(363), 5, "Men's tshirt", "Tshirt", 250m, new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 10, 56, 582, DateTimeKind.Utc).AddTicks(363), 2, 250m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 1, 39, 20, DateTimeKind.Utc).AddTicks(2478), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 1, 39, 20, DateTimeKind.Utc).AddTicks(2481) });
        }
    }
}
