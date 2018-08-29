using Mlib.Domain.Infrastructure.Interfaces;
using NAudio.Wave;
using System.IO;

namespace Mlib.Domain.Infrastructure
{
    public class AudioPlayer : PropertyChangedBase, IAudioPlayer
    {
        WaveOutEvent output;
        FileInfo file;
        public bool IsPlaying => output?.PlaybackState == PlaybackState.Playing;

        public FileInfo File
        {
            get => file;
            set
            {
                file = value;
                SetFile(file);
            }
        }

        public AudioPlayer()
        {
            output = new WaveOutEvent();
        }
        public void UnPause()
        {
            if (output.PlaybackState == PlaybackState.Paused)
                output.Play();
        }
        public void Play()//FileInfo file)
        {
            output.Play();
            NotifyOfPropertyChange();
        }

        public void Stop()
        {
            output.Stop();
            NotifyOfPropertyChange();
        }

        public void Pause()
        {
            output.Pause();
            NotifyOfPropertyChange();
        }
        void SetFile(FileInfo file)
        {
            Mp3FileReader mp3 = new Mp3FileReader(file.FullName);
            if (output.PlaybackState == PlaybackState.Playing)
            {
                output.Stop();
                output.Init(mp3);
                output.Play();
            }
            else if (output.PlaybackState == PlaybackState.Paused)
            {
                output.Stop();
                output.Init(mp3);
            }
            else
            {
                output.Init(mp3);
            }

        }
    }
}
