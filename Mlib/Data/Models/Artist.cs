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
        [Required]
        [Key]
        public string Name { get; set; }
        public string ImageId { get; set; }
        ICollection<Track> Tracks { get; set; }
        ICollection<Album> Albums { get; set; }
        public Artist()
        {
            Tracks = new List<Track>();
            Albums = new List<Album>();
        }

    }
}
