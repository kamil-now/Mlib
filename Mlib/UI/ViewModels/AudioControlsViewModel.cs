namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using Mlib.Infrastructure;
    using Mlib.UI.Interfaces;
    using NAudio.Wave;
    using System.Windows.Input;

    public class AudioControlsViewModel : Screen, IViewModel, IPlaybackStateObserver
    {
        public AudioControlsViewModel(AudioPlayer audioPlayer)
        {
            this.AudioPlayer = audioPlayer;
            audioPlayer.Attach(this as IPlaybackStateObserver);

            TogglePlayPauseCommand = new Command(q => audioPlayer.TogglePlayPause(VolumeLevel));
            NextTrackCommand = new Command(q => audioPlayer.PlayNextTrack());
            PreviousTrackCommand = new Command(q => audioPlayer.PlayPreviousTrack());
        }

        public AudioPlayer AudioPlayer { get; }
        public double VolumeLevel { get; set; } = 1;
        
        public bool IsPlaying => AudioPlayer.IsPlaying;

        public ICommand TogglePlayPauseCommand { get; }
        public ICommand PreviousTrackCommand { get; }
        public ICommand NextTrackCommand { get; }

       

        public void UpdatePlaybackState(PlaybackState currentPlaybackState)
        {
            NotifyOfPropertyChange(() => IsPlaying);
        }
    }
}
