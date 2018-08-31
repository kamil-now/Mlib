using SQLite;
using System.Collections.Generic;

namespace Mlib.Domain.Infrastructure
{
    public class Playlist
    {
        [Ignore]
        public List<Track> Tracks { get; set; }
        public Playlist(string path)
        {
            Tracks = new List<Track>();
        }
    }
}
