using Mlib.Extensions;
using Mlib.Infrastructure;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Mlib.Data.Models
{
    public class Playlist : IDataEntity
    {
        [NotMapped]
        public string Id => Name;
        [NotMapped]
        public EntityType Type => EntityType.Playlist;
        [Key]
        [Required]
        public string Name{ get; set; }
        public string ImageId { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        

        public Playlist()
        {
            //Tracks = new List<Track>();
            //Playlists = new List<Playlist>();
            //Albums = new List<Album>();
           
        }
        public Playlist(FileInfo file) : this()
        {
            Name =  file.Name.Substring(0,file.Name.LastIndexOf('.'));
            Tracks =  M3UReader.GetFiles(file).Select(n=>new Track(n)).ToList();
        }
        
    }
}