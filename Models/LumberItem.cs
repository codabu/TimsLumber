using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models
{
    public class LumberItem
    {
        public int LumberItemId { get; set; }
        public string NominalSize { get; set; }

        public string ActualSize { get; set; }

        public double PricePerFt { get; set; }
    }
}
