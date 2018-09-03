using Caliburn.Micro;
using Mlib.Domain;
using Mlib.Domain.Infrastructure;
using Mlib.UI.ViewModels.Interfaces;

using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class AudioControlsViewModel : Screen, IViewModel
    {
        AudioPlayer audioPlayer;
        public AudioControlsViewModel(AudioPlayer audioPlayer)
        {
            audioPlayer.TrackChanged += () => NotifyOfPropertyChange(() => NowPlaying);
            this.audioPlayer = audioPlayer;
            audioPlayer.PlaybackPaused += () => NotifyOfPropertyChange(() => IsPlaying);
            audioPlayer.PlaybackResumed += () => NotifyOfPropertyChange(() => IsPlaying);
            audioPlayer.PlaybackStopped += () => NotifyOfPropertyChange(() => IsPlaying);

            TogglePlayPauseCommand = new Command(q => audioPlayer.TogglePlayPause(VolumeLevel));
        }
        public double VolumeLevel { get; set; } = 1;
        public Track NowPlaying => audioPlayer.NowPlaying;
        public bool IsPlaying => audioPlayer.IsPlaying;
        public ICommand TogglePlayPauseCommand { get; }
    }
}
