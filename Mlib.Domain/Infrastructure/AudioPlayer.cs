using NAudio.Wave;
using System;
using System.IO;

namespace Mlib.Domain.Infrastructure
{
    public class AudioPlayer : PropertyChangedBase
    {
        public bool IsPlaying => output?.PlaybackState == PlaybackState.Playing;
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser,
            PlaybackStoppedReachingEndOfFile
        }

        public PlaybackStopTypes PlaybackStopType { get; set; }

        private AudioFileReader audioFileReader;

        private DirectSoundOut output;

        private FileInfo nowPlaying;

        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;

        public AudioPlayer()
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
        }
        public void SetFile(FileInfo file)
        {
            if(output==null)
            {
                output = new DirectSoundOut(200);
                output.PlaybackStopped += (s, e) =>
                {
                    Dispose();
                    PlaybackStopped?.Invoke();
                };
            }
            nowPlaying = file;
            audioFileReader = new AudioFileReader(nowPlaying.FullName) { Volume = GetVolume() };
            var wc = new WaveChannel32(audioFileReader);
            wc.PadWithZeroes = false;

            output.Init(wc);
        }
        public void Play(FileInfo file, double currentVolumeLevel)
        {
            SetFile(file);
            Play(currentVolumeLevel);

        }
        public void Play(double currentVolumeLevel)
        {
            if (output?.PlaybackState == PlaybackState.Stopped || output?.PlaybackState == PlaybackState.Paused)
            {
                output.Play();
            }
            SetVolume((float)currentVolumeLevel);

            PlaybackResumed?.Invoke();
        }


        public void Stop() => output?.Stop();

        public void Pause()
        {
            if (output != null)
            {
                output.Pause();

                PlaybackPaused?.Invoke();
            }
        }

        public void TogglePlayPause(double currentVolumeLevel)
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    Pause();
                }
                else
                {
                    Play(currentVolumeLevel);
                }
            }
        }

        public void Dispose()
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    output.Stop();
                }
                output.Dispose();
                output = null;
            }
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
        }

        public double GetLenghtInSeconds()
        {
            if (audioFileReader != null)
            {
                return audioFileReader.TotalTime.TotalSeconds;
            }
            else
            {
                return 0;
            }
        }

        public double GetPositionInSeconds()
        {
            return audioFileReader != null ? audioFileReader.CurrentTime.TotalSeconds : 0;
        }

        public float GetVolume()
        {
            if (audioFileReader != null)
            {
                return audioFileReader.Volume;
            }
            return 1;
        }

        public void SetPosition(double value)
        {
            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
            }
        }

        public void SetVolume(float value)
        {
            if (output != null && audioFileReader != null)
            {
                audioFileReader.Volume = value;
            }
        }
    }
}
