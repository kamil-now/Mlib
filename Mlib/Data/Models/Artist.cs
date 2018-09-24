using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data.Models
{
    public class Artist:IDataEntity
    {
        [NotMapped]
        public string Id => Name;
        [NotMapped]
        public EntityType Type => EntityType.Artist;
        [Required]
        [Key]
        public string Name { get; set; }
        public string ImageId { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public Artist()
        {
            Tracks = new List<Track>();
            Albums = new List<Album>();
        }

    }
}
