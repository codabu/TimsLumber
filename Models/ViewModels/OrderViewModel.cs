﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public List<Order> Orders { get; set; }

        public int OrderID { get; set; }
    }
}
