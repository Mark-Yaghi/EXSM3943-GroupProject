using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClassroomStart.Models
{

    public partial class DatabaseContext : DbContext
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DatabaseContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DatabaseContext(DbContextOptions<DbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }   
            
            public virtual DbSet<Customer> Customers { get; set; }
            public virtual DbSet<Order> Orders { get; set; }
            public virtual DbSet<OrderDetail> OrderDetails { get; set; }
            public virtual DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=CSharp3_Assignment_Peasants", new MySqlServerVersion(new Version(10, 4, 24)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>                                //declare the "Customer" table and its columns/attributes
            {
                entity.Property(e => e.CustomerID)
                      .HasColumnType("int(10)")
                      .HasColumnName("CustomerID");

                entity.Property(e => e.FirstName)
                      .HasCharSet("utf8mb4")
                      .UseCollation("ut8fmb4_general_ci")
                      .HasColumnType("varchar(50)")
                      .HasColumnName("FirstName")
                      .HasMaxLength(50);

                entity.Property(e => e.LastName)
                      .HasCharSet("utf8mb4")
                      .UseCollation("ut8fmb4_general_ci")
                      .HasColumnType("varchar(50)")
                      .HasColumnName("LastName")
                      .HasMaxLength(50);

                entity.Property(e => e.Address)
                      .HasColumnType("varchar(50)")
                      .HasColumnName("Address")
                      .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                      .HasColumnType("long(10)")
                      .HasColumnName("PhoneNumber")
                      .HasMaxLength(10);

                entity.HasData(
                new Customer[]
                {
                  new Customer ("John", "Bonjovi","12345-123st North, Cincinatti, OH, 87542", 7804564561){CustomerID=-1},
                  new Customer ("Sarah","Rafferty","Apt.3478, 57 West Park Avenue, New York, NY, 87754", 8007635541){CustomerID=-2},
                  new Customer ("Harvey", "Spector", "457 Wolverine Creek, Penascola, FL, 58742",4035571234){CustomerID=-3},
                  new Customer ("Tony", "Montana","16345-191st East, Chicago, IL, 77752", 7808456455){CustomerID=-4},
                  new Customer ("Harrison","Ford","Apt.7578, 88 West Park Avenue, New York, NY, 85754", 8005552248){CustomerID=-5},
                  new Customer ("Jorge", "DeSilva", "Suite 2500, 275 Palm Beach Cove, Miami, FL, 59542", 5874892330){CustomerID=-6},
               });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderID)
                      .HasColumnType("int(10)")
                      .HasColumnName("CustomerID");

                entity.HasIndex(e => e.CustomerID)
                      .HasDatabaseName("FK_" + nameof(Order) + "_" + nameof(Customer));

                entity.Property(e => e.CustomerID)
                      .HasColumnType("int(10)")
                      .HasColumnName("CustomerID");

                entity.Property(e => e.Date)
                      .HasColumnType("DateTime")
                      .HasColumnName("DateTime");

                entity.Property(e => e.TotalAmount)
                      .HasColumnType("decimal(10,2)")
                      .HasColumnName("TotalAmount");

                entity.HasOne(x => x.Customer)
                      .WithMany(y => y.Orders)
                      .HasForeignKey(x => x.CustomerID)
                      .HasConstraintName("FK_" + nameof(Order) + "_" + nameof(Customer))
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new Order[]
                    {
                                //custID,       date/time,          amount
                        new Order (-1, DateTime.Parse("07-24-2021"), 75.42m){OrderID=-1},
                        new Order (-1, DateTime.Parse("08-12-2022"), 75.42m){OrderID=-2},
                        new Order (-2, DateTime.Parse("06-14-2021"), 75.42m){OrderID=-3},
                        new Order (-3, DateTime.Parse("12-04-2022"), 75.42m){OrderID=-4},
                        new Order (-3, DateTime.Parse("07-11-2022"), 75.42m){OrderID=-5},

                    });

            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {

                entity.Property(e => e.OrderDetailID)
                      .HasColumnType("int(10)")
                      .HasColumnName("OrderDetailID");

                entity.HasIndex(e => e.OrderID)
                      .HasDatabaseName("FK_" + nameof(OrderDetail) + "_" + nameof(Order));

                entity.Property(e => e.OrderID)
                      .HasColumnType("int(10)")
                      .HasColumnName("OrderID");

                entity.HasIndex(e => e.ProductID)
                      .HasDatabaseName("FK_" + nameof(OrderDetail) + "_" + nameof(Product));

                entity.Property(e => e.ProductID)
                      .HasColumnType("int(10)")
                      .HasColumnName("ProductID");

                entity.Property(e => e.QuantityOrdered)
                      .HasColumnType("int(10)")
                      .HasColumnName("QuantityOrdered");

                entity.HasData(
                    new OrderDetail[]
                    {         //orderID,ProdID, quantity
                       new OrderDetail (-1, -4, 10){OrderDetailID=-1},
                       new OrderDetail (-1, -2, 15){OrderDetailID=-2},
                       new OrderDetail (-1, -3, 8) {OrderDetailID=-3},
                       new OrderDetail (-2, -4, 30){OrderDetailID=-4},
                       new OrderDetail (-3, -1, 17){OrderDetailID=-5},

                    });
            });

            modelBuilder.Entity<Product>(entity =>
            {

                entity.Property(e => e.ProductID)
                    .HasColumnType("int(10)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .HasCharSet("utf8mb4")
                    .UseCollation("ut8fmb4_general_ci")
                    .HasColumnType("varchar(50)")
                    .HasColumnName("ProductName")
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasCharSet("utf8mb4")
                    .UseCollation("ut8fmb4_general_ci")
                    .HasColumnType("varchar(100)")
                    .HasColumnName("Description")
                    .HasMaxLength(50);

                entity.Property(e => e.QuantityInStock)
                    .HasColumnType("int(4)")
                    .HasColumnName("QuantityInStock");

                entity.Property(e => e.Discontinued)
                    .HasColumnType("bool")
                    .HasColumnName("Discontinued");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("decimal(5,2)")
                    .HasColumnName("SalePrice");

                entity.HasData(
                    new Product[]
                    {             //prod name, prod description,   quantity on hand,discontinued, price
                      new Product("milk, 2%", "4 L jugs of 2% Milk from Beatrice", 175, false, 4.50m){ProductID=-1},
                      new Product("milk, skim", "4 L jugs of Skim Milk from Beatrice", 94, true, 4.65M){ProductID=-2},
                      new Product("milk, chocolate", "4 L jugs of Chocolate Milk from Beatrice", 90, false, 4.70m){ProductID=-3},
                      new Product("White Bread", "Loaf of white bread from Weston Bakeries", 40, false, 2.85M){ProductID=-4},
                      new Product("Whole wheat bread", "Loaf of whole wheat bread from Weston Bakeries", 75, false, 3.25m){ProductID=-5},
                      new Product("Mandarin Oranges 3 lb bag", "3 lb bag of fresh Mandarin Oranges", 30, false, 8.65M){ProductID=-6},
                      new Product("Gala Apples", "3lb bag of Gala Apples", 25, true, 6.50m){ProductID=-7},
                      new Product("Carrots", "3 lb bag of carrots from Redcliff, AB", 15, true, 3.65M){ProductID=-8},

                    });

            });


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
       
}