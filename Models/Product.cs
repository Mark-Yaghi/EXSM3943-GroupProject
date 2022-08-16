﻿using System;
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
        public Product(string productName, string description, int quantityInStoke, decimal price)
        {
            ProductName = productName;
            Description = description;
            QuantityInStoke = quantityInStoke;
            Price = price;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("productId", TypeName = "int(10)")]
        public int ProductID { get; set; }


        [Column("productName", TypeName = "varchar(50)")]
        [StringLength(50)]
        [Required]
        public string ProductName { get; set; }


        [Column("description", TypeName = "varchar(100)")]
        [StringLength(50)]
        [Required]
        public string Description { get; set; }



        [Column("QuantityInStock", TypeName = "int(4)")]
        [Required]
        public int QuantityInStock { get; set; }



        [Column("SalePrice", TypeName = "decimal(5,2)")]
        [Required]
        public decimal SalePrice { get; set; }


        [InverseProperty(nameof(Models.OrderDetail.Product))]

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
