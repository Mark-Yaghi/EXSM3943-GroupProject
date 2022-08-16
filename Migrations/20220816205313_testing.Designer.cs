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
    [Migration("20220816205313_testing")]
    partial class testing
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
                        .HasColumnName("CustomerID");

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

                    b.Property<long>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("long(10)")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            CustomerID = -1,
                            Address = "12345-123st North, Cincinatti, OH, 87542",
                            FirstName = "John",
                            LastName = "Bonjovi",
                            PhoneNumber = 7804564561L
                        },
                        new
                        {
                            CustomerID = -2,
                            Address = "Apt.3478, 57 West Park Avenue, New York, NY, 87754",
                            FirstName = "Sarah",
                            LastName = "Rafferty",
                            PhoneNumber = 8007635541L
                        },
                        new
                        {
                            CustomerID = -3,
                            Address = "457 Wolverine Creek, Penascola, FL, 58742",
                            FirstName = "Harvey",
                            LastName = "Spector",
                            PhoneNumber = 4035571234L
                        },
                        new
                        {
                            CustomerID = -4,
                            Address = "16345-191st East, Chicago, IL, 77752",
                            FirstName = "Tony",
                            LastName = "Montana",
                            PhoneNumber = 7808456455L
                        },
                        new
                        {
                            CustomerID = -5,
                            Address = "Apt.7578, 88 West Park Avenue, New York, NY, 85754",
                            FirstName = "Harrison",
                            LastName = "Ford",
                            PhoneNumber = 8005552248L
                        },
                        new
                        {
                            CustomerID = -6,
                            Address = "Suite 2500, 275 Palm Beach Cove, Miami, FL, 59542",
                            FirstName = "Jorge",
                            LastName = "DeSilva",
                            PhoneNumber = 5874892330L
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("CustomerID");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int(10)")
                        .HasColumnName("CustomerID");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateTime");

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
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -2,
                            CustomerID = -1,
                            Date = new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -3,
                            CustomerID = -2,
                            Date = new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -4,
                            CustomerID = -3,
                            Date = new DateTime(2022, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalAmount = 75.42m
                        },
                        new
                        {
                            OrderID = -5,
                            CustomerID = -3,
                            Date = new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TotalAmount = 75.42m
                        });
                });

            modelBuilder.Entity("ClassroomStart.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)")
                        .HasColumnName("OrderDetailID");

                    b.Property<int?>("OrderDetailID1")
                        .HasColumnType("int(10)");

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

                    b.HasIndex("OrderDetailID1");

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
                        .HasColumnType("bool(1)")
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

                    b.HasKey("ProductID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = -1,
                            Description = "4 L jugs of 2% Milk from Beatrice",
                            Discontinued = false,
                            ProductName = "milk, 2%",
                            QuantityInStock = 175,
                            SalePrice = 4.50m
                        },
                        new
                        {
                            ProductID = -2,
                            Description = "4 L jugs of Skim Milk from Beatrice",
                            Discontinued = true,
                            ProductName = "milk, skim",
                            QuantityInStock = 94,
                            SalePrice = 4.65m
                        },
                        new
                        {
                            ProductID = -3,
                            Description = "4 L jugs of Chocolate Milk from Beatrice",
                            Discontinued = false,
                            ProductName = "milk, chocolate",
                            QuantityInStock = 90,
                            SalePrice = 4.70m
                        },
                        new
                        {
                            ProductID = -4,
                            Description = "Loaf of white bread from Weston Bakeries",
                            Discontinued = false,
                            ProductName = "White Bread",
                            QuantityInStock = 40,
                            SalePrice = 2.85m
                        },
                        new
                        {
                            ProductID = -5,
                            Description = "Loaf of whole wheat bread from Weston Bakeries",
                            Discontinued = false,
                            ProductName = "Whole wheat bread",
                            QuantityInStock = 75,
                            SalePrice = 3.25m
                        },
                        new
                        {
                            ProductID = -6,
                            Description = "3 lb bag of fresh Mandarin Oranges",
                            Discontinued = false,
                            ProductName = "Mandarin Oranges 3 lb bag",
                            QuantityInStock = 30,
                            SalePrice = 8.65m
                        },
                        new
                        {
                            ProductID = -7,
                            Description = "3lb bag of Gala Apples",
                            Discontinued = true,
                            ProductName = "Gala Apples",
                            QuantityInStock = 25,
                            SalePrice = 6.50m
                        },
                        new
                        {
                            ProductID = -8,
                            Description = "3 lb bag of carrots from Redcliff, AB",
                            Discontinued = true,
                            ProductName = "Carrots",
                            QuantityInStock = 15,
                            SalePrice = 3.65m
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
                    b.HasOne("ClassroomStart.Models.OrderDetail", null)
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderDetailID1");

                    b.HasOne("ClassroomStart.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassroomStart.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ClassroomStart.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ClassroomStart.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ClassroomStart.Models.OrderDetail", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ClassroomStart.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
