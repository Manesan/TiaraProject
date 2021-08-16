using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.Models
{
    public class Photo : BaseEntity
    {
        public string Image { get; set; }
        [ForeignKey("PhotoAlbum")]
        public int PhotoAlbumId { get; set; }
        public Album PhotoAlbum { get; set; }
    }
}
