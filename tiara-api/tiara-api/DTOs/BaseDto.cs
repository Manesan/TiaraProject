using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.DTOs
{
    public class BaseDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int CreatedById { get; set; }
        public UserDto CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
