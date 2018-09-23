using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data.Models
{
    public class Album : IDataEntity
    {
        public string Id => AlbumId.ToString();
        [NotMapped]
        public EntityType Type => EntityType.Album;
        [Required]
        [Key]
        public int AlbumId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Artist Artist { get; set; }
        public string Year { get; set; }
        public string ImageId { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public Album()
        {
            Tracks = new List<Track>();
        }
    }
}
