using NAudio.Wave;
using System;
using System.Linq;

namespace Mlib.Domain.Infrastructure
{
    public class AudioPlayer : PropertyChangedBase
    {
        public event Action TrackChanged;
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser,
            PlaybackStoppedReachingEndOfFile
        }
        public AudioPlayer()
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
            
        }
        private AudioFileReader audioFileReader;

        private Playlist currentPlaylist;
        private DirectSoundOut output;
        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;
        public bool IsPlaying => output?.PlaybackState == PlaybackState.Playing;


        public PlaybackStopTypes PlaybackStopType { get; set; }

        public Track NowPlaying { get; private set; }
        int trackNumber;
        public void NextTrack()
        {
            if (trackNumber > 0)
            {
                if (trackNumber == currentPlaylist.Tracks.Count)
                    trackNumber = 1;
                else
                    trackNumber++;
                SetNowPlaying(currentPlaylist.Tracks.ElementAt(trackNumber - 1));
            }
        }
        public void PreviousTrack()
        {
            if (trackNumber > 0)
            {
                if (trackNumber == 1)
                    trackNumber = currentPlaylist.Tracks.Count;
                else
                    trackNumber--;
                SetNowPlaying(currentPlaylist.Tracks.ElementAt(trackNumber - 1));
            }
        }
        public void SetCurrentPlaylist(Playlist playlist)
        {
            currentPlaylist = playlist;

            SetNowPlaying(currentPlaylist.Tracks.First());
            Play(output.Volume);
        }
        public void SetNowPlaying(Track track)
        {
            trackNumber = currentPlaylist?.Tracks?.IndexOf(track) + 1 ?? -1;
            if (output == null)
            {
                output = new DirectSoundOut(200);
                output.PlaybackStopped += (s, e) =>
                {
                    NextTrack();
                    Play(output.Volume);
                    //Dispose();
                    //PlaybackStopped?.Invoke();
                };
            }
            NowPlaying = track;
            audioFileReader = new AudioFileReader(NowPlaying.FullPath) { Volume = GetVolume() };
            var wc = new WaveChannel32(audioFileReader);
            wc.PadWithZeroes = false;

            output.Init(wc);
            TrackChanged?.Invoke();
        }
        public void Play(Track track, double currentVolumeLevel)
        {
            SetNowPlaying(track);
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
