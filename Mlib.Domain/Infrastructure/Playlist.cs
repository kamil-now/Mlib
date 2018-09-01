using Mlib.Domain.Extensions;
using Mlib.Domain.Infrastructure.Interfaces;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.IO;

namespace Mlib.Domain.Infrastructure
{
    public class Playlist : IDatabaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Length => Tracks?.Count ?? 0;
        [ManyToMany(typeof(PlaylistData))]
        public List<Track> Tracks { get; set; }

        public Playlist()
        {
            Tracks = new List<Track>();
           
        }
        public Playlist(FileInfo file) : this()
        {
        }
        
    }
}