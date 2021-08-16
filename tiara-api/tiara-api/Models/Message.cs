using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.Models
{
    /// <summary>
    /// This may need changing depending on the data that the chat component requires
    /// </summary>
    public class Message : BaseEntity
    {
        public string Description { get; set; }
    }
}
