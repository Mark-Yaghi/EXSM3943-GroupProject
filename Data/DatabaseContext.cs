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

        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DbContext> options) : base(options)
        {
             public virtual DbSet<Customer>Customers { get; set; }
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
                       .HasColumnType("varchar(50)")
                       .HasColumnName("LastName")
                       .HasMaxLength(50);

                        entity.Property(e => e.Address)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Address")
                        .HasMaxLength(50);
                
                        entity.Property(e => e.PhoneNumber)
                        .HasColumnType("int(10)")
                        .HasColumnName("PhoneNumber")
                        .HasMaxLength(10);

                        entity.HasData(
                            new Customer[]
                            {
                                new Customer ("John", "Bonjovi","12345-123st North, Cincinatti, OH, 87542", 7804564561){CustomerID=-1},
                                new Customer ("Sarah","Rafferty","Apt.3478, 57 West Park Avenue, New York, NY, 87754", 8007635541){ClientID=-2},
                                new Customer ("Harvey", "Spector", "457 Wolverine Creek, Penascola, FL, 58742",4035571234){ClientID=-3},
                                new Customer ("Tony", "Montana","16345-191st East, Chicago, IL, 77752", 7808456455){CustomerID=-4},
                                new Customer ("Harrison","Ford","Apt.7578, 88 West Park Avenue, New York, NY, 85754", 8005552248){ClientID=-5},
                                new Customer ("Jorge", "DeSilva", "Suite 2500, 275 Palm Beach Cove, Miami, FL, 59542", 5874892330){ClientID=-6},
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



                    });        


                }

    }








}