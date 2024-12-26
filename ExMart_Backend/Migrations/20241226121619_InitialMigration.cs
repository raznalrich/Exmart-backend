using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExMart_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addToCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    IconPath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addToCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<List<string>>(type: "text[]", nullable: false),
                    Color = table.Column<List<string>>(type: "text[]", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Color", "CreatedAt", "CreatedBy", "Description", "Name", "Price", "Size", "UpdatedAt", "VendorId", "Weight" },
                values: new object[,]
                {
                    { 1, "Experion", "GAR001", new List<string> { "red", "black" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Experion branded t-shirt", "Experion Tshirt", 1499.00m, new List<string> { "15.6 inches" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 500m },
                    { 2, "Experion", "GAR001", new List<string> { "white" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Experion branded hoodie", "Hoody Experion brand", 399.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 600m },
                    { 3, "Experion", "GAR001", new List<string> { "black", "red" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Experion branded jersey", "Jersey Experion branded", 399.99m, new List<string> { "40mm", "44mm" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 400m },
                    { 4, "Swiss Military", "APP001", new List<string> { "white", "blue" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Swiss military branded earpods", "Earpods Swiss military", 349.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 50m },
                    { 5, "VAFS", "STA001", new List<string> { "blue", "white", "green" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Reusable water bottle", "Water Bottle", 399.00m, new List<string> { "41mm", "45mm" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 300m },
                    { 6, "VAFS", "STA001", new List<string> { "Black", "red" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Insulated flask", "Flask", 349.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 400m },
                    { 8, "Nike", "STA001", new List<string> { "White", "Black", "Red" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Nike Air Force 1 sneakers", "Nike Air Force 1", 99.99m, new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 800m },
                    { 9, "Samsung", "APP001", new List<string> { "black", "White" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Samsung wireless earbuds", "Samsung Galaxy Buds 2 Pro", 1999.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 50m },
                    { 10, "VAFS", "STA001", new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Personal diary", "Diary", 149.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, 200m },
                    { 11, "VAFS", "STA001", new List<string> { "Black", "White" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Multi-purpose backpack", "BackPack", 149.99m, new List<string> { "Standard" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, 700m },
                    { 12, "VAFS", "C001", new List<string> { "Blue", "Green" }, new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc), 1, "Ergonomic wireless mouse with 2.4 GHz connectivity", "Wireless Mouse", 25m, new List<string> { "XS", "S", "M" }, new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc), 1, 250m }
                });

            migrationBuilder.InsertData(
                table: "addToCategories",
                columns: new[] { "Id", "CategoryName", "IconPath" },
                values: new object[,]
                {
                    { 1, "Garments", "iconURL" },
                    { 8, "Stationary", "iconUrl" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addToCategories");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
