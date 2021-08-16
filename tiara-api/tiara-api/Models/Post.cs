using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.Models
{
    public class Post : BaseEntity
    {
        public string Caption { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
    }
}
