using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class newProductDataSeeding26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "red", "black" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9258), new List<string> { "XS,L,XL" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9259) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9264), new List<string>(), new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9265) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "red" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9269), new List<string> { "40mm", "44mm" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white", "blue" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9275), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9276) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "blue", "white", "green" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9281), new List<string> { "41mm", "45mm" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9282) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "red" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9314), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9315) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "White", "Black", "Red" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9321), new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9322) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "White" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9327), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9327) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9332), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9333) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9338), new List<string> { "Standard" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9338) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9247), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 8, 43, 895, DateTimeKind.Utc).AddTicks(9250) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "red", "black" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "XS,L,XL" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string>(), new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "red" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "40mm", "44mm" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "white", "blue" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "blue", "white", "green" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "41mm", "45mm" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "red" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "White", "Black", "Red" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "black", "White" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "Standard" }, new DateTime(2023, 11, 23, 20, 52, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "Standard" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 19, 7, 0, 0, DateTimeKind.Local), new List<string> { "Standard" }, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "CreatedAt", "Size", "UpdatedAt" },
                values: new object[] { new List<string> { "Blue", "Green" }, new DateTime(2024, 12, 26, 10, 4, 3, 879, DateTimeKind.Utc).AddTicks(6173), new List<string> { "XS", "S", "M" }, new DateTime(2024, 12, 26, 10, 4, 3, 879, DateTimeKind.Utc).AddTicks(6177) });
        }
    }
}
