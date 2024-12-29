using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedingStatusMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "red", "black" }, new List<string> { "15.6 inches" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "white" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "black", "red" }, new List<string> { "40mm", "44mm" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "white", "blue" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "blue", "white", "green" }, new List<string> { "41mm", "45mm" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Black", "red" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "White", "Black", "Red" }, new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "black", "White" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Black", "White" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Black", "White" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Blue", "Green" }, new List<string> { "XS", "S", "M" } });

            migrationBuilder.InsertData(
                table: "StatusMaster",
                columns: new[] { "Product_StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Shipped" },
                    { 3, "Delivered" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusMaster",
                keyColumn: "Product_StatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatusMaster",
                keyColumn: "Product_StatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StatusMaster",
                keyColumn: "Product_StatusId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "red", "black" }, new List<string> { "15.6 inches" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "white" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "black", "red" }, new List<string> { "40mm", "44mm" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "white", "blue" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "blue", "white", "green" }, new List<string> { "41mm", "45mm" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Black", "red" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "White", "Black", "Red" }, new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "black", "White" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Black", "White" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Black", "White" }, new List<string> { "Standard" } });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "Blue", "Green" }, new List<string> { "XS", "S", "M" } });
        }
    }
}
