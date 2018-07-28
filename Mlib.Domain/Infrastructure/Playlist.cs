using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Domain.Infrastructure
{
    public class Playlist
    {
        [Ignore]
        public List<Track> Tracks { get; set; } = new List<Track>();
        public Playlist(string path)
        {

        }
    }
}
