using Mlib.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class MlibData:DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public MlibData():base("MlibData")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>().
              HasMany(c => c.Tracks).
              WithMany(p => p.Playlists).
              Map(
               m =>
               {
                   m.MapLeftKey("Name");
                   m.MapRightKey("TrackId");
                   m.ToTable("PlaylistTracks");
               });
        }
    }
}
