﻿// <auto-generated />
using System;
using System.Collections.Generic;
using ExMart_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExMart_Backend.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241226121444_mergingthedb")]
    partial class mergingthedb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExMart_Backend.Model.ColourMaster", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ColorId"));

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ColorId");

                    b.ToTable("ColourMaster");

                    b.HasData(
                        new
                        {
                            ColorId = 1,
                            ColorCode = "#FF0000",
                            ColorName = "red"
                        },
                        new
                        {
                            ColorId = 2,
                            ColorCode = "#0000FF",
                            ColorName = "blue"
                        },
                        new
                        {
                            ColorId = 3,
                            ColorCode = "#FFFFFF",
                            ColorName = "white"
                        },
                        new
                        {
                            ColorId = 4,
                            ColorCode = "#000000",
                            ColorName = "black"
                        },
                        new
                        {
                            ColorId = 5,
                            ColorCode = "#008000",
                            ColorName = "green"
                        },
                        new
                        {
                            ColorId = 6,
                            ColorCode = "#8F00FF",
                            ColorName = "violet"
                        },
                        new
                        {
                            ColorId = 7,
                            ColorCode = "#FFFF00",
                            ColorName = "yellow"
                        });
                });

            modelBuilder.Entity("ExMart_Backend.Model.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Order_ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Product_StatusId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ExMart_Backend.Model.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("ColorId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductRateId")
                        .HasColumnType("integer");

                    b.Property<int>("SizeId")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ExMart_Backend.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Color")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<List<string>>("Size")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("VendorId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 12,
                            Brand = "VAFS",
                            CategoryId = "C001",
                            Color = new List<string> { "Blue", "Green" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Ergonomic wireless mouse with 2.4 GHz connectivity",
                            Name = "Wireless Mouse",
                            Price = 25m,
                            Size = new List<string> { "XS", "S", "M" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 250m
                        },
                        new
                        {
                            Id = 1,
                            Brand = "Experion",
                            CategoryId = "GAR001",
                            Color = new List<string> { "red", "black" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Experion branded t-shirt",
                            Name = "Experion Tshirt",
                            Price = 1499.00m,
                            Size = new List<string> { "15.6 inches" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 500m
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Experion",
                            CategoryId = "GAR001",
                            Color = new List<string> { "white" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Experion branded hoodie",
                            Name = "Hoody Experion brand",
                            Price = 399.99m,
                            Size = new List<string> { "Standard" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 600m
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Experion",
                            CategoryId = "GAR001",
                            Color = new List<string> { "black", "red" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Experion branded jersey",
                            Name = "Jersey Experion branded",
                            Price = 399.99m,
                            Size = new List<string> { "40mm", "44mm" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 400m
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Swiss Military",
                            CategoryId = "APP001",
                            Color = new List<string> { "white", "blue" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Swiss military branded earpods",
                            Name = "Earpods Swiss military",
                            Price = 349.99m,
                            Size = new List<string> { "Standard" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 50m
                        },
                        new
                        {
                            Id = 5,
                            Brand = "VAFS",
                            CategoryId = "STA001",
                            Color = new List<string> { "blue", "white", "green" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Reusable water bottle",
                            Name = "Water Bottle",
                            Price = 399.00m,
                            Size = new List<string> { "41mm", "45mm" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 300m
                        },
                        new
                        {
                            Id = 6,
                            Brand = "VAFS",
                            CategoryId = "STA001",
                            Color = new List<string> { "Black", "red" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Insulated flask",
                            Name = "Flask",
                            Price = 349.99m,
                            Size = new List<string> { "Standard" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 400m
                        },
                        new
                        {
                            Id = 8,
                            Brand = "Nike",
                            CategoryId = "STA001",
                            Color = new List<string> { "White", "Black", "Red" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Nike Air Force 1 sneakers",
                            Name = "Nike Air Force 1",
                            Price = 99.99m,
                            Size = new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 800m
                        },
                        new
                        {
                            Id = 9,
                            Brand = "Samsung",
                            CategoryId = "APP001",
                            Color = new List<string> { "black", "White" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Samsung wireless earbuds",
                            Name = "Samsung Galaxy Buds 2 Pro",
                            Price = 1999.99m,
                            Size = new List<string> { "Standard" },
                            UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 50m
                        },
                        new
                        {
                            Id = 10,
                            Brand = "VAFS",
                            CategoryId = "STA001",
                            Color = new List<string> { "Black", "White" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Personal diary",
                            Name = "Diary",
                            Price = 149.99m,
                            Size = new List<string> { "Standard" },
                            UpdatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 200m
                        },
                        new
                        {
                            Id = 11,
                            Brand = "VAFS",
                            CategoryId = "STA001",
                            Color = new List<string> { "Black", "White" },
                            CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            CreatedBy = 1,
                            Description = "Multi-purpose backpack",
                            Name = "BackPack",
                            Price = 149.99m,
                            Size = new List<string> { "Standard" },
                            UpdatedAt = new DateTime(2023, 11, 22, 13, 37, 0, 0, DateTimeKind.Utc),
                            VendorId = 1,
                            Weight = 700m
                        });
                });

            modelBuilder.Entity("ExMart_Backend.Model.SizeMaster", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SizeId"));

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SizeId");

                    b.ToTable("SizeMaster");

                    b.HasData(
                        new
                        {
                            SizeId = 1,
                            Size = "XS"
                        },
                        new
                        {
                            SizeId = 2,
                            Size = "S"
                        },
                        new
                        {
                            SizeId = 3,
                            Size = "M"
                        },
                        new
                        {
                            SizeId = 4,
                            Size = "L"
                        },
                        new
                        {
                            SizeId = 5,
                            Size = "XL"
                        },
                        new
                        {
                            SizeId = 6,
                            Size = "XXL"
                        },
                        new
                        {
                            SizeId = 7,
                            Size = "XXXL"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ExMart_Backend.Model.OrderItem", b =>
                {
                    b.HasOne("ExMart_Backend.Model.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExMart_Backend.Model.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
