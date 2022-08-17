using System;
using System.Collections.Generic;
using System.Linq;
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

        public Product(int supplierID,string productName, string description, int quantityInStock, bool discontinued,  decimal salePrice)

        {
            SupplierID = supplierID;
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

        [Column("SupplierID", TypeName = "int(10)")]
        [Required]
        public int SupplierID { get; set; }


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

        [Column("Discontinued", TypeName = "tinyint(1)")]
        [Required]
       public bool Discontinued { get; set; }


        [Column("SalePrice", TypeName = "decimal(5,2)")]
        [Required]
        public decimal SalePrice { get; set; }


        public bool IsEmptyStock
        {
            get { return QuantityInStock == 0; }
        }

        public int AddToStock(int amount)
        {
            QuantityInStock += amount;

            return QuantityInStock;
        }

        public int SellProduct(int amount)
        {

            QuantityInStock -= amount;
            return QuantityInStock;
        }

        [ForeignKey(nameof(SupplierID))]

        [InverseProperty(nameof(Models.Supplier.Products))]
        public virtual Supplier Supplier { get; set; }

        [InverseProperty(nameof(Models.OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }



    }
}
