﻿// <auto-generated />
using System;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClassroomStart.Migrations
{
    [DbContext(typeof(DatabaseContext))]
<<<<<<<< HEAD:Migrations/20220817202747_intialMigrations.Designer.cs
    [Migration("20220817202747_intialMigrations")]
    partial class intialMigrations
========
    [Migration("20220818164011_seedData")]
    partial class seedData
>>>>>>>> Nick:Migrations/20220818164011_seedData.Designer.cs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ClassroomStart.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("customerID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FirstName")
                        .UseCollation("utf8mb4_general_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("FirstName"), "utf8mb4");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LastName")
                        .UseCollation("utf8mb4_general_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("LastName"), "utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            CustomerID = -1,
                            Address = "12345-123st North, Cincinatti, OH, 87542, ",
                            FirstName = "John",
                            LastName = "Bonjovi",
                            PhoneNumber = "7804564561"
                        },
                        new
                        {
                            CustomerID = -2,
                            Address = "Apt.3478, 57 West Park Avenue, New York, NY, 87754",
                            FirstName = "Sarah",
                            LastName = "Rafferty",
                            PhoneNumber = "8007635541"
                        },
                        new
                        {
                            CustomerID = -3,
                            Address = "457 Wolverine Creek, Penascola, FL, 58742",
                            FirstName = "Harvey",
                            LastName = "Spector",
                            PhoneNumber = "4035571234"
                        },
                        new
                        {
                            CustomerID = -4,
                            Address = "16345-191st East, Chicago, IL, 77752",
                            FirstName = "Tony",
                            LastName = "Montana",
                            PhoneNumber = "7808456455"
                        },
                        new
                        {
                            CustomerID = -5,
                            Address = "Apt.7578, 88 West Park Avenue, New York, NY, 85754",
                            FirstName = "Harrison",
                            LastName = "Ford",
                            PhoneNumber = "8005552248"
                        },
                        new
                        {
                            CustomerID = -6,
                            Address = "Suite 2500, 275 Palm Beach Cove, Miami, FL, 59542",
                            FirstName = "Jorge",
                            LastName = "DeSilva",
                            PhoneNumber = "5874892330"
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("orderID");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int(10)")
                        .HasColumnName("customerID");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DateTime")
                        .HasColumnName("Date");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("SalePrice");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("TotalAmount");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID")
                        .HasDatabaseName("FK_Order_Customer");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            OrderID = -1,
                            CustomerID = -1,
                            Date = new DateTime(2021, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 0m,
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -2,
                            CustomerID = -4,
                            Date = new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 0m,
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -3,
                            CustomerID = -1,
                            Date = new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 0m,
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -4,
                            CustomerID = -6,
                            Date = new DateTime(2022, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 0m,
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -5,
                            CustomerID = -2,
                            Date = new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalePrice = 0m,
                            TotalAmount = 75.42m
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("OrderDetailID");

                    b.Property<int>("OrderID")
                        .HasColumnType("int(10)")
                        .HasColumnName("OrderID");

                    b.Property<int>("ProductID")
                        .HasColumnType("int(10)")
                        .HasColumnName("ProductID");

                    b.Property<int>("QuantityOrdered")
                        .HasColumnType("int(10)")
                        .HasColumnName("QuantityOrdered");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("OrderID")
                        .HasDatabaseName("FK_OrderDetail_Order");

                    b.HasIndex("ProductID")
                        .HasDatabaseName("FK_OrderDetail_Product");

                    b.ToTable("OrderDetail");

                    b.HasData(
                        new
                        {
                            OrderDetailID = -1,
                            OrderID = -1,
                            ProductID = -4,
                            QuantityOrdered = 10
                        },
                        new
                        {
                            OrderDetailID = -2,
                            OrderID = -1,
                            ProductID = -2,
                            QuantityOrdered = 15
                        },
                        new
                        {
                            OrderDetailID = -3,
                            OrderID = -1,
                            ProductID = -3,
                            QuantityOrdered = 8
                        },
                        new
                        {
                            OrderDetailID = -4,
                            OrderID = -2,
                            ProductID = -4,
                            QuantityOrdered = 30
                        },
                        new
                        {
                            OrderDetailID = -5,
                            OrderID = -3,
                            ProductID = -1,
                            QuantityOrdered = 17
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("ProductID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Description")
                        .UseCollation("utf8mb4_general_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("Description"), "utf8mb4");

                    b.Property<bool>("Discontinued")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Discontinued");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ProductName")
                        .UseCollation("utf8mb4_general_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("ProductName"), "utf8mb4");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int(4)")
                        .HasColumnName("QuantityInStock");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("SalePrice");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int(10)")
                        .HasColumnName("SupplierID");

                    b.HasKey("ProductID");

                    b.HasIndex("SupplierID")
                        .HasDatabaseName("FK_Product_Supplier");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = -1,
                            Description = "4 L jugs of 2% Milk from Beatrice",
                            Discontinued = false,
                            ProductName = "milk, 2%",
                            QuantityInStock = 175,
                            SalePrice = 4.50m,
                            SupplierID = -1
                        },
                        new
                        {
                            ProductID = -2,
                            Description = "4 L jugs of Skim Milk from Beatrice",
                            Discontinued = true,
                            ProductName = "milk, skim",
                            QuantityInStock = 94,
                            SalePrice = 4.65m,
                            SupplierID = -1
                        },
                        new
                        {
                            ProductID = -3,
                            Description = "4 L jugs of Chocolate Milk from Beatrice",
                            Discontinued = false,
                            ProductName = "milk, chocolate",
                            QuantityInStock = 90,
                            SalePrice = 4.70m,
                            SupplierID = -1
                        },
                        new
                        {
                            ProductID = -4,
                            Description = "Loaf of white bread from Weston Bakeries",
                            Discontinued = false,
                            ProductName = "White Bread",
                            QuantityInStock = 40,
                            SalePrice = 2.85m,
                            SupplierID = -2
                        },
                        new
                        {
                            ProductID = -5,
                            Description = "Loaf of whole wheat bread from Weston Bakeries",
                            Discontinued = false,
                            ProductName = "Whole wheat bread",
                            QuantityInStock = 75,
                            SalePrice = 3.25m,
                            SupplierID = -2
                        },
                        new
                        {
                            ProductID = -6,
                            Description = "3 lb bag of fresh Mandarin Oranges",
                            Discontinued = false,
                            ProductName = "Mandarin Oranges 3 lb bag",
                            QuantityInStock = 30,
                            SalePrice = 8.65m,
<<<<<<<< HEAD:Migrations/20220817202747_intialMigrations.Designer.cs
                            SupplierID = -3
========
                            SupplierID = -2
>>>>>>>> Nick:Migrations/20220818164011_seedData.Designer.cs
                        },
                        new
                        {
                            ProductID = -7,
                            Description = "3lb bag of Gala Apples",
                            Discontinued = true,
                            ProductName = "Gala Apples",
                            QuantityInStock = 25,
                            SalePrice = 6.50m,
<<<<<<<< HEAD:Migrations/20220817202747_intialMigrations.Designer.cs
                            SupplierID = -3
========
                            SupplierID = -2
>>>>>>>> Nick:Migrations/20220818164011_seedData.Designer.cs
                        },
                        new
                        {
                            ProductID = -8,
                            Description = "3 lb bag of carrots from Redcliff, AB",
                            Discontinued = true,
                            ProductName = "Carrots",
                            QuantityInStock = 15,
                            SalePrice = 3.65m,
<<<<<<<< HEAD:Migrations/20220817202747_intialMigrations.Designer.cs
                            SupplierID = -3
========
                            SupplierID = -2
>>>>>>>> Nick:Migrations/20220818164011_seedData.Designer.cs
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("SupplierID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Address");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FirstName")
                        .UseCollation("utf8mb4_general_ci");

                    MySqlPropertyBuilderExtensions.HasCharSet(b.Property<string>("CompanyName"), "utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("SupplierID");

                    b.ToTable("Supplier");

                    b.HasData(
                        new
                        {
                            SupplierID = -1,
<<<<<<<< HEAD:Migrations/20220817202747_intialMigrations.Designer.cs
========
                            Address = "Athabascan Ave, Edmonton, AB T8N7T7",
                            CompanyName = "G&L Distributors",
                            PhoneNumber = "7804643186"
                        },
                        new
                        {
                            SupplierID = -2,
                            Address = "14225 130 Ave NW, Edmonton, AB T5L 4K8",
                            CompanyName = "Wholesale food store",
                            PhoneNumber = "7804241234"
                        },
                        new
                        {
                            SupplierID = -3,
                            Address = "7331 104 Street Edmonton, AB T6E 4B9",
                            CompanyName = "Sabroso Foods",
                            PhoneNumber = "7804825026"
                        },
                        new
                        {
                            SupplierID = -4,
>>>>>>>> Nick:Migrations/20220818164011_seedData.Designer.cs
                            Address = "12345-Yellowhead Trail",
                            CompanyName = "Gordon Food Services",
                            PhoneNumber = "7804552213"
                        },
                        new
                        {
<<<<<<<< HEAD:Migrations/20220817202747_intialMigrations.Designer.cs
                            SupplierID = -2,
                            Address = "12275-155 street",
                            CompanyName = "Weston Bakeries",
                            PhoneNumber = "7804338877"
                        },
                        new
                        {
                            SupplierID = -3,
                            Address = "12355-154 street",
                            CompanyName = "Eberhardt Foods",
                            PhoneNumber = "7804555230"
========
                            SupplierID = -5,
                            Address = "12275-155 street",
                            CompanyName = "Weston Bakeries",
                            PhoneNumber = "7804338877"
>>>>>>>> Nick:Migrations/20220818164011_seedData.Designer.cs
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.Order", b =>
                {
                    b.HasOne("ClassroomStart.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Order_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ClassroomStart.Models.OrderDetail", b =>
                {
                    b.HasOne("ClassroomStart.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Order");

                    b.HasOne("ClassroomStart.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ClassroomStart.Models.Product", b =>
                {
                    b.HasOne("ClassroomStart.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Product_Supplier");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ClassroomStart.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ClassroomStart.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ClassroomStart.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ClassroomStart.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
