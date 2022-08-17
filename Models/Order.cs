using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{

    [Table("Order")]

    public class Order
    {

        public Order(int customerID, DateTime date, decimal totalAmount)
        {

            Date = date;
            TotalAmount = totalAmount;
            CustomerID = customerID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("orderID", TypeName = "int(10)")]
        public int OrderID { get; set; }


        [Column("customerID", TypeName = "int(10)")]
        [Required]
        public int CustomerID { get; set; }


        [Column("TotalAmount", TypeName = "decimal(10,2)")]
        [Required]
        public decimal TotalAmount { get; set; }

        [Column("Date", TypeName = "DateTime")]

        public DateTime Date { get; set; }
        [Required]

        [Column("SalePrice", TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }
        [Required]


        [ForeignKey(nameof(CustomerID))]

        [InverseProperty(nameof(Models.Customer.Orders))]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}