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
            CustomerID=customerID;
            CustomerID = customerID;
            Date = date;
            TotalAmount = totalAmount;
        }

        [Column("OrderId", TypeName = "int(10)")]
        public int OrderID { get; set; }

        [Column("CustomerID", TypeName = "int(10)")]
        [Required]
        public int CustomerID { get; set; }


        [Column("TotalAmount", TypeName = "decima(10,2)")]

        public decimal TotalAmount { get; set; }

        [Column("Date", TypeName = "DateTime")]

        public DateTime Date { get; set; }



        [ForeignKey(nameof(CustomerID))]
        public virtual Customer Customer { get; set; }


        [InverseProperty(nameof(Models.OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
