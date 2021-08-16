using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace tiara_api.Models
{
    public class Album : BaseEntity
    {
        private ILazyLoader lazyLoader;
        private IEnumerable<Photo> photos;

        public Album(ILazyLoader _lazyLoader)
        {
            lazyLoader = _lazyLoader;
        }

        public Album() { }


        public string Title { get; set; }
        public bool PlayOnBooth { get; set; }
        //public ICollection<Photo> Photos { get; set; }

        public virtual IEnumerable<Photo> Photos
        {
            get => lazyLoader.Load(this, ref photos);
            set => photos = value;
        }
    }
}
