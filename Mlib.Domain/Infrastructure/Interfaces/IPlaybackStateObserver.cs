using NAudio.Wave;

namespace Mlib.Domain.Infrastructure.Interfaces
{
    public interface IPlaybackStateObserver
    {
        void UpdatePlaybackState(PlaybackState currentPlaybackState);
    }
}
