using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.DTOs
{
    public class MilestoneContainerDto : BaseDto
    {
        public List<MileStoneDto> todo { get; set; }
        public List<MileStoneDto> done { get; set; }
    }
}
