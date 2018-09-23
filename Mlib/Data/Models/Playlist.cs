using Mlib.Extensions;
using Mlib.Infrastructure;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mlib.Data.Models
{
    public class Playlist : IDataEntity
    {
        public string Id => Name;

        public string Name{ get; set; }
        public string ImageId { get; set; }

        public ICollection<Track> Tracks { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public ICollection<Album> Albums { get; set; }
        

        public Playlist()
        {
            Tracks = new List<Track>();
           
        }
        public Playlist(FileInfo file) : this()
        {
            Name =  file.Name.Substring(0,file.Name.LastIndexOf('.'));
            Tracks =  M3UReader.GetFiles(file).Select(n=>new Track(n)).ToList();
        }
        
    }
}