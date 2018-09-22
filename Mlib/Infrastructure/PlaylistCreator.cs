using Mlib.Data;
using Mlib.Data.Models;
using System.Data.Entity;

namespace Mlib.Infrastructure
{
    public class PlaylistCreator
    {
        UnitOfWork unitOfWork;
        public PlaylistCreator(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddTrack(Playlist playlist, Track track)
        {
            //playlist.Tracks.Add(track);
            //database.Update(playlist);
        }
    }
}
