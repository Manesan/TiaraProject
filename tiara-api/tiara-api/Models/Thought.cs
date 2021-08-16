using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.Models
{
    public class Thought : BaseEntity
    {
        public string Description { get; set; }
    }
}
