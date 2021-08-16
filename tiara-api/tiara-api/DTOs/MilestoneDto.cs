using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.DTOs
{
    public class MileStoneDto : BaseDto
    {
        public string Description { get; set; }
        public bool Achieved { get; set; }
    }
}
