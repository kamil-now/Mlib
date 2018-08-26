using Mlib.Domain.Infrastructure.Interfaces;
using NAudio.Wave;
using System.IO;

namespace Mlib.Domain.Infrastructure
{
    public class AudioPlayer : IAudioPlayer
    {
        WaveOutEvent output;
        public AudioPlayer()
        {
            output = new WaveOutEvent();
        }
        public void UnPause()
        {
            if (output.PlaybackState == PlaybackState.Paused)
                output.Play();
        }
        public void Play(FileInfo file)
        {
            Mp3FileReader mp3 = new Mp3FileReader(file.FullName);

            output.Init(mp3);
            output.Play();
        }

        public void Stop()
        {
            output.Stop();
        }

        public void Pause()
        {
            output.Pause();
        }
    }
}
