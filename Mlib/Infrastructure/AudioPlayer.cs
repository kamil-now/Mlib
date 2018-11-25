namespace Mlib.Infrastructure
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using NAudio.Wave;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    public class AudioPlayer : PropertyChangedBase, IPlaybackStateSubject, ICurrentTrackSubject
    {
        private WaveStream audioFileReader;

        private IWavePlayer output;
        private int trackNumber;
        private List<IPlaybackStateObserver> playbackStateObservers = new List<IPlaybackStateObserver>();
        private List<ICurrentTrackObserver> currentTrackObservers = new List<ICurrentTrackObserver>();

        public bool IsPlaying => output?.PlaybackState == PlaybackState.Playing;
        public Track NowPlaying { get; private set; }
        public Playlist CurrentPlaylist { get; private set; }
        public void SetPlaylist(Playlist playlist)
        {
            CurrentPlaylist = playlist;
            AppWindowManager.SetPlaylistPanel(this);
        }
        private void SetNowPlaying(Track track)
        {
            NowPlaying = track;
            NotifyOfPropertyChange(() => NowPlaying);
            trackNumber = CurrentPlaylist?.Tracks?.ToList().IndexOf(track) + 1 ?? -1;
            NotifyOfCurrentTrackChange();
        }

        public void PlayNextTrack()
        {
            if (trackNumber > 0)
            {
                if (trackNumber == CurrentPlaylist.Tracks.Count)
                    trackNumber = 1;
                else
                    trackNumber++;

                ChangeNowPlaying(CurrentPlaylist.Tracks.ElementAt(trackNumber - 1));

            }
        }
        public void PreviousTrack()
        {
            if (trackNumber > 0)
            {
                if (trackNumber == 1)
                    trackNumber = CurrentPlaylist.Tracks.Count;
                else
                    trackNumber--;

                ChangeNowPlaying(CurrentPlaylist.Tracks.ElementAt(trackNumber - 1));
            }
        }
        public void Play(Track track)
        {
            SetNowPlaying(track);
            Play();
        }
        public void Stop()
        {
            DisposeWavePlayer();
            NotifyOfPlaybackStateChange();
        }
        public void Pause()
        {
            output?.Pause();
            NotifyOfPlaybackStateChange();
        }
        public void TogglePlayPause(double currentVolumeLevel)
        {
            if (output?.PlaybackState == PlaybackState.Playing)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        public double CurrentTrackLenght => audioFileReader?.TotalTime.TotalSeconds ?? 0;
        public string TotalTime => audioFileReader?.TotalTime.ToString("mm\\:ss");

        public string CurrentTime => audioFileReader?.CurrentTime.ToString("mm\\:ss");
        public double CurrentTrackPosition
        {
            get => audioFileReader?.CurrentTime.TotalSeconds ?? 0;
            set
            {
                if (audioFileReader != null)
                    audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
            }
        }

        private void ChangeNowPlaying(Track track)
        {
            SetNowPlaying(track);
            if (IsPlaying)
                Play();
            else
                DisposeWavePlayer();
        }
        private void Play()
        {
            if (output?.PlaybackState == PlaybackState.Paused)
            {
                output.Play();
            }
            else if (NowPlaying != null)
            {
                DisposeWavePlayer();
                InitOutput();
                output.Play();
            }
            NotifyOfPlaybackStateChange();
        }
        private void DisposeWavePlayer()
        {
            if (output != null)
            {
                timer.Stop();
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    output.Stop(); ;
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
        private void InitOutput()
        {
            output = new WaveOut();
            audioFileReader = new MediaFoundationReader(NowPlaying.FullPath);

            var wc = new WaveChannel32(audioFileReader);

            output.Init(wc);
            NotifyOfPropertyChange(() => CurrentTrackLenght);
            NotifyOfPropertyChange(() => TotalTime);
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += (s, e) =>
            {
                NotifyOfPropertyChange(() => CurrentTrackPosition);
                NotifyOfPropertyChange(() => CurrentTime);
                if ((int)CurrentTrackPosition == (int)CurrentTrackLenght)
                {
                    PlayNextTrack();
                }
            };
            timer.Start();
        }
        Timer timer;
        public void Attach(IPlaybackStateObserver observer)
        {
            if (!playbackStateObservers.Contains(observer))
                playbackStateObservers.Add(observer);
        }

        public void Detach(IPlaybackStateObserver observer)
        {
            if (playbackStateObservers.Contains(observer))
                playbackStateObservers.Remove(observer);
        }

        public void NotifyOfPlaybackStateChange()
        {
            if (output != null)
                playbackStateObservers.ForEach(n => n.UpdatePlaybackState(output.PlaybackState));
        }

        public void Attach(ICurrentTrackObserver observer)
        {
            if (!currentTrackObservers.Contains(observer))
                currentTrackObservers.Add(observer);
        }

        public void Detach(ICurrentTrackObserver observer)
        {
            if (currentTrackObservers.Contains(observer))
                currentTrackObservers.Remove(observer);
        }

        public void NotifyOfCurrentTrackChange()
        {
            currentTrackObservers.ForEach(n => n.UpdateCurrentTrack(NowPlaying));
        }
    }
}
