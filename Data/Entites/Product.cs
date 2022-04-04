using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    [Table("Products")]
    public class Product
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public Decimal Price { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int Status { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}