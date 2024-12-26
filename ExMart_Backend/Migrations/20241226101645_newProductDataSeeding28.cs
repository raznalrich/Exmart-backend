using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class newProductDataSeeding28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "red", "black" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "15.6 inches" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "red" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "40mm", "44mm" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white", "blue" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "blue", "white", "green" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "41mm", "45mm" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "red" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "White", "Black", "Red" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "White" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "Standard" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc), new List<string> { "Standard" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 16, 45, 206, DateTimeKind.Utc).AddTicks(4390), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 16, 45, 206, DateTimeKind.Utc).AddTicks(4393) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "red", "black" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2338), new List<string> { "XS,L,XL" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2339) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2343), new List<string>(), new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2344) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "red" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2349), new List<string> { "40mm", "44mm" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white", "blue" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2427), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2427) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "blue", "white", "green" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2432), new List<string> { "41mm", "45mm" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2433) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "red" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2438), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2439) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "White", "Black", "Red" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2445), new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2445) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "White" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2450), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2451) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2456), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2456) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2461), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2462) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2327), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 11, 41, 867, DateTimeKind.Utc).AddTicks(2331) });
        }
    }
}
