using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumber.Areas.Admin.Models
{
    public class AdminOrderViewModel
    {
        public Order Order { get; set; }

        public List<Order> Orders { get; set; }

        public List<LumberItem> LumberItems { get; set; }

        public int OrderID { get; set; }

        public List<string> UserNames { get; set; }

        public List<string> Emails { get; set; }
    }
}
