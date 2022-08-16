using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomStart.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<long>(type: "long(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantityInStock = table.Column<int>(type: "int(4)", nullable: false),
                    Discontinued = table.Column<bool>(type: "bool(1)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(type: "int(10)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
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
                        principalColumn: "OrderId",
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
                columns: new[] { "CustomerID", "Address", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { -6, "Suite 2500, 275 Palm Beach Cove, Miami, FL, 59542", "Jorge", "DeSilva", 5874892330L },
                    { -5, "Apt.7578, 88 West Park Avenue, New York, NY, 85754", "Harrison", "Ford", 8005552248L },
                    { -4, "16345-191st East, Chicago, IL, 77752", "Tony", "Montana", 7808456455L },
                    { -3, "457 Wolverine Creek, Penascola, FL, 58742", "Harvey", "Spector", 4035571234L },
                    { -2, "Apt.3478, 57 West Park Avenue, New York, NY, 87754", "Sarah", "Rafferty", 8007635541L },
                    { -1, "12345-123st North, Cincinatti, OH, 87542", "John", "Bonjovi", 7804564561L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "Discontinued", "ProductName", "QuantityInStock", "SalePrice" },
                values: new object[,]
                {
                    { -8, "3 lb bag of carrots from Redcliff, AB", true, "Carrots", 15, 3.65m },
                    { -7, "3lb bag of Gala Apples", true, "Gala Apples", 25, 6.50m },
                    { -6, "3 lb bag of fresh Mandarin Oranges", false, "Mandarin Oranges 3 lb bag", 30, 8.65m },
                    { -5, "Loaf of whole wheat bread from Weston Bakeries", false, "Whole wheat bread", 75, 3.25m },
                    { -4, "Loaf of white bread from Weston Bakeries", false, "White Bread", 40, 2.85m },
                    { -3, "4 L jugs of Chocolate Milk from Beatrice", false, "milk, chocolate", 90, 4.70m },
                    { -2, "4 L jugs of Skim Milk from Beatrice", true, "milk, skim", 94, 4.65m },
                    { -1, "4 L jugs of 2% Milk from Beatrice", false, "milk, 2%", 175, 4.50m }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CustomerID", "DateTime", "TotalAmount" },
                values: new object[,]
                {
                    { -5, -3, new DateTime(2022, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.42m },
                    { -4, -3, new DateTime(2022, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.42m },
                    { -3, -2, new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.42m },
                    { -2, -1, new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.42m },
                    { -1, -1, new DateTime(2021, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.42m }
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
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "FK_OrderDetail_Order",
                table: "OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "FK_OrderDetail_Product",
                table: "OrderDetail",
                column: "ProductID");
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
        }
    }
}
