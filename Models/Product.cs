using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{
    public class Product
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Product(string productName, string description, int quantityInStock, bool discontinued, decimal salePrice)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductName = productName;
            Description = description;
            QuantityInStock = quantityInStock;
            Discontinued = discontinued;
            SalePrice = salePrice;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductID", TypeName = "int(10)")]
        public int ProductID { get; set; }


        [Column("ProductName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string ProductName { get; set; }


        [Column("Description", TypeName = "varchar(100)")]
        [StringLength(50)]
        [Required]
        public string Description { get; set; }



        [Column("QuantityInStock", TypeName = "int(4)")]
        [Required]
        public int QuantityInStock { get; set; }

        [Column("Discontinued", TypeName = "bool")]
        [Required]
        public bool Discontinued { get; set; }



        [Column("SalePrice", TypeName = "decimal(5,2)")]
        [Required]
        public decimal SalePrice { get; set; }


        [InverseProperty(nameof(Models.OrderDetail.Product))]

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
