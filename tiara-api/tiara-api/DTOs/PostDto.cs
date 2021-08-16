using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.DTOs
{
    public class PostDto : BaseDto
    {
        public string Caption { get; set; }
        public string Photo { get; set; }
        public string Location { get; set; }
    }
}
