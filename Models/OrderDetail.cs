using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{
    [Table("orderDetail")]
    public class OrderDetail
    {

        public OrderDetail(int quantityOrdered)
        {
            QuantityOrdered = quantityOrdered;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("orderDetailId", TypeName = "int(10)")]
        public int OrderDetailID { get; set; }

        [Column("orderID", TypeName = "int(10)")]
        [Required]
        public int OrderID { get; set; }



        [Column("productID", TypeName = "int(10)")]
        [Required]
        public int ProductID { get; set; }

        [Column("quantityOrdered", TypeName = "int(10)")]
        [Required]
        public int QuantityOrdered { get; set; }

        [NotMapped]
        public bool InsufficientStoke
        {
            get { return QuantityOrdered < Product.QuantityInStoke; }
        }


        [ForeignKey(nameof(ProductID))]
        [InverseProperty(nameof(Models.Product.OrderDetails))]
        public virtual Product Product { get; set; }



        [ForeignKey(nameof(OrderID))]
        [InverseProperty(nameof(Models.Order.OrderDetails))]

        public virtual Order Order { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}