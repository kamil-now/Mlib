using Mlib.Domain.Infrastracture;
namespace Mlib.Domain.Infrastructure
{
    public class PlaylistCreator
    {
        IDatabaseService database;
        public PlaylistCreator(IDatabaseService database)
        {
            this.database = database;
        }
        public void AddTrack(Playlist playlist, Track track)
        {
            playlist.Tracks.Add(track);
            database.Update(playlist);
        }
    }
}
