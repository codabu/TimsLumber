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
        [Key]
        public int OrderItemId { get; set; }

        public int LumberItemId { get; set; }
        public LumberItem LumberItem { get; set; }

        public int Length { get; set; }
        public double Cost { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
        
    }
}
