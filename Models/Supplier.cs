using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{

    [Table("supplier")]
    public class Supplier
    {

       public Supplier(string companyName, string address, int phoneNumber)
        {
            CompanyName = companyName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("companyID", TypeName = "int(10)")]
        public int CompanyID { get; set; }

        [Column("companyName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string CompanyName { get; set; }

        [Column("address", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string Address { get; set; }

        [Column("phoneNumber", TypeName = "int(10)")]
        [Required]
        public int PhoneNumber { get; set; }
        

        // set foreign keys here if and when needed.
        //[InverseProperty(nameof(Models.Order.Customer))]

        //public virtual ICollection<Order> Orders { get; set; }

    }
}
