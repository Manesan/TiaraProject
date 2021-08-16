using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.Models
{
    /// <summary>
    /// Inherited by all classes
    /// </summary>
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        [ForeignKey("CreatedBy")]
        public int? CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
