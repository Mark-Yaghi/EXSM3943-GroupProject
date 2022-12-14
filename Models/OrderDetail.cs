using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {

        public OrderDetail(int orderID, int productID, int quantityOrdered)
        {
            OrderID = orderID;
            ProductID = productID;
            QuantityOrdered = quantityOrdered;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("OrderDetailID", TypeName = "int(10)")]
        public int OrderDetailID { get; set; }

        [Column("OrderID", TypeName = "int(10)")]
        [Required]
        public int OrderID { get; set; }


        [Column("ProductID", TypeName = "int(10)")]
        [Required]
        public int ProductID { get; set; }

        [Column("QuantityOrdered", TypeName = "int(10)")]
        [Required]
        public int QuantityOrdered { get; set; }

        [NotMapped]
        public bool InsufficientStock
        {
            get { return QuantityOrdered < Product.QuantityInStock; }
        }




        [ForeignKey(nameof(ProductID))]
        [InverseProperty(nameof(Models.Product.OrderDetails))]
        public virtual Product Product { get; set; }


        [ForeignKey(nameof(OrderID))]
        [InverseProperty(nameof(Models.Order.OrderDetails))]

        public virtual Order Order { get; set; }


    }
}