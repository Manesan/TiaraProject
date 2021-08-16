using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.DTOs
{
    public class PhotoDto : BaseDto
    {
        public string Image { get; set; }
        public int PhotoAlbumId { get; set; }
        public AlbumDto PhotoAlbum { get; set; }
    }
}
