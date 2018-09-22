using NAudio.Wave;

namespace Mlib
{
    public interface IPlaybackStateObserver
    {
        void UpdatePlaybackState(PlaybackState currentPlaybackState);
    }
}
