using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumber.Models
{
    public class AddViewModel
    {
        
        public List<LumberItem> LumberItems { get; set; }

        public int LumberItemId { get; set; }

        public int Length { get; set; }

        public Order ThisOrder { get; set; }
    }
}
