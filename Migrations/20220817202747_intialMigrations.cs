using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomStart.Migrations
{
    public partial class intialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customerID = table.Column<int>(type: "int(10)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "DateTime", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.customerID,
                        principalTable: "Customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SupplierID = table.Column<int>(type: "int(10)", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantityInStock = table.Column<int>(type: "int(4)", nullable: false),
                    Discontinued = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Supplier",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderID = table.Column<int>(type: "int(10)", nullable: false),
                    ProductID = table.Column<int>(type: "int(10)", nullable: false),
                    QuantityOrdered = table.Column<int>(type: "int(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "customerID", "Address", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { -6, "Suite 2500, 275 Palm Beach Cove, Miami, FL, 59542", "Jorge", "DeSilva", "5874892330" },
                    { -5, "Apt.7578, 88 West Park Avenue, New York, NY, 85754", "Harrison", "Ford", "8005552248" },
                    { -4, "16345-191st East, Chicago, IL, 77752", "Tony", "Montana", "7808456455" },
                    { -3, "457 Wolverine Creek, Penascola, FL, 58742", "Harvey", "Spector", "4035571234" },
                    { -2, "Apt.3478, 57 West Park Avenue, New York, NY, 87754", "Sarah", "Rafferty", "8007635541" },
                    { -1, "12345-123st North, Cincinatti, OH, 87542, ", "John", "Bonjovi", "7804564561" }
                });

            migrationBuilder.InsertData(
                table: "Supplier",
                columns: new[] { "SupplierID", "Address", "FirstName", "PhoneNumber" },
                values: new object[,]
                {
                    { -3, "12355-154 street", "Eberhardt Foods", "7804555230" },
                    { -2, "12275-155 street", "Weston Bakeries", "7804338877" },
                    { -1, "12345-Yellowhead Trail", "Gordon Food Services", "7804552213" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "orderID", "customerID", "Date", "SalePrice", "TotalAmount" },
                values: new object[,]
                {
                    { -5, -2, new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 75.42m },
                    { -4, -6, new DateTime(2022, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 75.42m },
                    { -3, -1, new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 75.42m },
                    { -2, -4, new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 75.42m },
                    { -1, -1, new DateTime(2021, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 75.42m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "Discontinued", "ProductName", "QuantityInStock", "SalePrice", "SupplierID" },
                values: new object[,]
                {
                    { -8, "3 lb bag of carrots from Redcliff, AB", true, "Carrots", 15, 3.65m, -3 },
                    { -7, "3lb bag of Gala Apples", true, "Gala Apples", 25, 6.50m, -3 },
                    { -6, "3 lb bag of fresh Mandarin Oranges", false, "Mandarin Oranges 3 lb bag", 30, 8.65m, -3 },
                    { -5, "Loaf of whole wheat bread from Weston Bakeries", false, "Whole wheat bread", 75, 3.25m, -2 },
                    { -4, "Loaf of white bread from Weston Bakeries", false, "White Bread", 40, 2.85m, -2 },
                    { -3, "4 L jugs of Chocolate Milk from Beatrice", false, "milk, chocolate", 90, 4.70m, -1 },
                    { -2, "4 L jugs of Skim Milk from Beatrice", true, "milk, skim", 94, 4.65m, -1 },
                    { -1, "4 L jugs of 2% Milk from Beatrice", false, "milk, 2%", 175, 4.50m, -1 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "OrderDetailID", "OrderID", "ProductID", "QuantityOrdered" },
                values: new object[,]
                {
                    { -5, -3, -1, 17 },
                    { -4, -2, -4, 30 },
                    { -3, -1, -3, 8 },
                    { -2, -1, -2, 15 },
                    { -1, -1, -4, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "FK_Order_Customer",
                table: "Order",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "FK_OrderDetail_Order",
                table: "OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "FK_OrderDetail_Product",
                table: "OrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "FK_Product_Supplier",
                table: "Products",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
