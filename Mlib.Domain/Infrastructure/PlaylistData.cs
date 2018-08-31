using SQLiteNetExtensions.Attributes;

namespace Mlib.Domain.Infrastructure
{
    public class PlaylistData
    {
        [ForeignKey(typeof(Playlist))]
        public int PlaylistID { get; set; }

        [ForeignKey(typeof(Track))]
        public int TrackID { get; set; }

    }
}
