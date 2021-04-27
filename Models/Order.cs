using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public double Subtotal { get; set; }

        public double Tax { get; set; }

        public double Total { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
