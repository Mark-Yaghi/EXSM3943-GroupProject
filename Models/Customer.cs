using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{

    [Table("Customer")]
    public class Customer
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Customer(string firstName, string lastName, string address, long phoneNumber)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("CustomerID", TypeName = "int(10)")]
        public int CustomerID { get; set; }

        [Column("FirstName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }


        [Column("LastName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Column("Address", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string Address { get; set; }


        [Column("PhoneNumber", TypeName = "long(10)")]
        [Required]
        public long PhoneNumber { get; set; }

        [InverseProperty(nameof(Models.Order.Customer))]

        public virtual ICollection<Order> Orders { get; set; }


    }
}