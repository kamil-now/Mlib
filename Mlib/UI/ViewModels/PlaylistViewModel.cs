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
        AudioPlayer audioPlayer;
        public Playlist Playlist { get; }
        public PlaylistViewModel(Playlist playlist, AudioPlayer audioPlayer)
        {
            Playlist = playlist;
            this.audioPlayer = audioPlayer;
        }
        public ICommand Select => new Command(track =>
        {
            audioPlayer.Play(track as Track, Playlist);
        });
    }
}
