using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiara_api.DTOs
{
    public class AlbumDto: BaseDto
    {
        public string Title { get; set; }
        public bool PlayOnBooth { get; set; }

        public virtual IEnumerable<PhotoDto> Photos { get; set; }
    }
}
