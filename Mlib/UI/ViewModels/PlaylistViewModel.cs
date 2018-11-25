namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using Mlib.Infrastructure;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System.Linq;
    using System.Windows.Input;

    public class PlaylistViewModel : Screen, IViewModel
    {
        public AudioPlayer AudioPlayer { get; }
        public PlaylistViewModel(AudioPlayer audioPlayer)
        {
            AudioPlayer = audioPlayer;
        }
        public ICommand Select => new Command(track =>
        {
            AudioPlayer.Play(track as Track);
        });
    }
}
