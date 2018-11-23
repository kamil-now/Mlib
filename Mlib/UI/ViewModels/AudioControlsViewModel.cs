namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using Mlib.Infrastructure;
    using Mlib.UI.Interfaces;
    using NAudio.Wave;
    using System.Windows.Input;

    public class AudioControlsViewModel : Screen, IViewModel, IPlaybackStateObserver, ICurrentTrackObserver
    {
        public AudioControlsViewModel(AudioPlayer audioPlayer)
        {
            this.audioPlayer = audioPlayer;
            audioPlayer.Attach(this as IPlaybackStateObserver);
            audioPlayer.Attach(this as ICurrentTrackObserver);

            TogglePlayPauseCommand = new Command(q => audioPlayer.TogglePlayPause(VolumeLevel));
            NextTrackCommand = new Command(q => audioPlayer.NextTrack());
            PreviousTrackCommand = new Command(q => audioPlayer.PreviousTrack());
        }

        AudioPlayer audioPlayer;
        public double VolumeLevel { get; set; } = 1;
        public Track NowPlaying => audioPlayer.NowPlaying;
        public bool IsPlaying => audioPlayer.IsPlaying;

        public ICommand TogglePlayPauseCommand { get; }
        public ICommand PreviousTrackCommand { get; }
        public ICommand NextTrackCommand { get; }

        public void UpdateCurrentTrack(Track currentTrack)
        {
            NotifyOfPropertyChange(() => NowPlaying);
        }

        public void UpdatePlaybackState(PlaybackState currentPlaybackState)
        {
            NotifyOfPropertyChange(() => IsPlaying);
        }
    }
}
