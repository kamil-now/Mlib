using SQLiteNetExtensions.Attributes;

namespace Mlib.Data.Models
{
    public class PlaylistData
    {
        [ForeignKey(typeof(Playlist))]
        public int PlaylistID { get; set; }

        [ForeignKey(typeof(Track))]
        public int TrackID { get; set; }

    }
}
