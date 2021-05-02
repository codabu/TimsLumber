using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models
{
    public class EditViewModel
    {
        public List<LumberItem> LumberItems { get; set; }

        public int[] LumberItemIds { get; set; }

        public int[] Lengths { get; set; }

        public Order ThisOrder { get; set; }
    }
}
