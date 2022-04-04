using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entites
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [MaxLength(50)]
        public string CustomerEmail { get; set; }

        [Required]
        [MaxLength(15)]
        public string CustomerMobile { get; set; }

        [MaxLength(250)]
        public string CustomerMessage { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }
      
        public int Status { get; set; }

        public int CustommerId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual User User { set; get; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
