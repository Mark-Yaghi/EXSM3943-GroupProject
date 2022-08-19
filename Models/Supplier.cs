
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomStart.Models
{

    [Table("Supplier")]
    public class Supplier
    {

        public Supplier(string companyName, string address, string phoneNumber)
        {
            CompanyName = companyName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("SupplierID", TypeName = "int(10)")]
        public int SupplierID { get; set; }

        [Column("CompanyName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string CompanyName { get; set; }

        [Column("Address", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string Address { get; set; }

        [Column("PhoneNumber", TypeName = "varchar(10)")]
        [Required]
        public string PhoneNumber { get; set; }


        
        [InverseProperty(nameof(Models.Product.Supplier))]

        public virtual ICollection<Product> Products { get; set; }

    }
}