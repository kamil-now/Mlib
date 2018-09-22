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
        public int ID { get; set; }
        public string Id => ID.ToString();
        public int Length => Tracks?.Count ?? 0;
        public string Name { get; set; } 
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