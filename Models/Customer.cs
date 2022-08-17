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
<<<<<<< HEAD

    public class Customer

    {
        public Customer(string firstName, string lastName, string address)
=======
  
    public class Customer

    {
        public Customer(string firstName, string lastName, string address, string phoneNumber)
>>>>>>> master
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
<<<<<<< HEAD
            // PhoneNumber = phoneNumber;

        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customerID", TypeName = "int(10)")]

=======
           PhoneNumber = phoneNumber;

        }

        
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Column("customerID", TypeName = "int(10)")] 
     
>>>>>>> master
        public int CustomerID { get; set; }

        [Column("FirstName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string? FirstName { get; set; }


        [Column("LastName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string? LastName { get; set; }

        [Column("Address", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string? Address { get; set; }
<<<<<<< HEAD
        /**
          [Column("PhoneNumber", TypeName = "long(10)")]
        [Required]
        public long PhoneNumber { get; set; }
        */



=======
        
          [Column("PhoneNumber", TypeName = "varchar(10)")]
          [Required]
          public string PhoneNumber { get; set; }
       


   
>>>>>>> master
        [InverseProperty(nameof(Models.Order.Customer))]
   
        public virtual ICollection<Order> Orders { get; set; }
    }
}