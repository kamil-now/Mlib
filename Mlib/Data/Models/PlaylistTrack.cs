using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data.Models
{
    public class PlaylistTrack : IDataEntity
    {
        public string Id => EntityId.ToString();
        [NotMapped]
        public EntityType Type => EntityType.Album;
        [Required]
        [Key]
        public int EntityId { get; set; }
        [Required]
        public Track Track { get; set; }
        [Required]
        public long Number { get; set; }
        [Required]
        public Playlist Playlist { get; set; }
        public PlaylistTrack()
        {

        }
        public PlaylistTrack(Playlist playlist, Track track, long number)
        {
            Playlist = playlist;
            Track = track;
            Number = number;
        }
    }
}
