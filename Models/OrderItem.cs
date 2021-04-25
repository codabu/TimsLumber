using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public LumberItem LItem { get; set; }

        public int Length { get; set; }
        public double Cost { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
    }
}
