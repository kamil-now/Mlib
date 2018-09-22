using NAudio.Wave;

namespace Mlib.Interfaces
{
    public interface IPlaybackStateObserver
    {
        void UpdatePlaybackState(PlaybackState currentPlaybackState);
    }
}
