using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Product5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "red", "black" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), new List<string> { "15.6 inches" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), new List<string> { "XS", "S", "M" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Color", "CreatedAt", "CreatedBy", "Description", "Name", "Price", "Size", "UpdatedAt", "VendorId", "Weight" },
                values: new object[,]
                {
                    { 2, "Experion", "GAR001", new List<string> { "white" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Experion branded hoodie", "Hoody Experion brand", 399.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 600m },
                    { 3, "Experion", "GAR001", new List<string> { "black", "red" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Experion branded jersey", "Jersey Experion branded", 399.99m, new List<string> { "40mm", "44mm" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 400m },
                    { 4, "Swiss Military", "APP001", new List<string> { "white", "blue" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Swiss military branded earpods", "Earpods Swiss military", 349.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 50m },
                    { 5, "VAFS", "STA001", new List<string> { "blue", "white", "green" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Reusable water bottle", "Water Bottle", 399.00m, new List<string> { "41mm", "45mm" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 300m },
                    { 6, "VAFS", "STA001", new List<string> { "Black", "red" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Insulated flask", "Flask", 349.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 400m },
                    { 8, "Nike", "STA001", new List<string> { "White", "Black", "Red" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Nike Air Force 1 sneakers", "Nike Air Force 1", 99.99m, new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 800m },
                    { 9, "Samsung", "APP001", new List<string> { "black", "White" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Samsung wireless earbuds", "Samsung Galaxy Buds 2 Pro", 1999.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 50m },
                    { 10, "VAFS", "STA001", new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Personal diary", "Diary", 149.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, 200m },
                    { 11, "VAFS", "STA001", new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Multi-purpose backpack", "BackPack", 149.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, 700m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "red", "black" }, null, new List<string> { "15.6 inches" }, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, null, new List<string> { "XS", "S", "M" }, null });
        }
    }
}
