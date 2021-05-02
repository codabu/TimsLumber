using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimsLumber.Models;

namespace TimsLumber.Models
{
    public class AddViewModel
    {

        public List<LumberItem> LumberItems { get; set; }

        public int LumberItemId { get; set; }

        [Required]
        [Range(1, 30, ErrorMessage ="Min length = 1 ft. Maximum length = 30ft")]
        public int Length { get; set; }

        public Order ThisOrder { get; set; }
    }
}
