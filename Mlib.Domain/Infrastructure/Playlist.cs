using Mlib.Domain.Extensions;
using Mlib.Domain.Infrastructure.Interfaces;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mlib.Domain.Infrastructure
{
    public class Playlist : IDatabaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Length => Tracks?.Count ?? 0;
        public string Name { get; set; } 
        [ManyToMany(typeof(PlaylistData))]
        public List<Track> Tracks { get; set; }

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