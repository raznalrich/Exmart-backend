using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the ProductImages table
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", "1"), // Auto-increment ID
                    ImageUrl = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Assuming each product can have multiple images
                });

            // Example update data logic after the table is created
            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductId",
                value: 3);

            // Example update for the Products table
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "red", "black" }, new List<string> { "15.6 inches" } });

            // Continue your update for other products as you have in your original migration
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the ProductImages table if the migration is rolled back
            migrationBuilder.DropTable(
                name: "ProductImages");

            // Keep the existing update logic for Products in case you want to revert it
            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductId",
                value: 1);

            // Continue your rollback logic for Products
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Size" },
                values: new object[] { new List<string> { "red", "black" }, new List<string> { "15.6 inches" } });

            // Continue for other products...
        }
    }
}
