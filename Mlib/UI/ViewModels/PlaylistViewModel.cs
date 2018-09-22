using Caliburn.Micro;
using Mlib.Data.Models;
using Mlib.Infrastructure;
using Mlib.UI.ViewModels.Interfaces;
using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class PlaylistViewModel : Screen, IViewModel
    {
        private AudioPlayer audioPlayer;
        public Playlist SelectedPlaylist { get; set; }

        public PlaylistViewModel(AudioPlayer audioPlayer)
        {
            this.audioPlayer = audioPlayer;
        }
        public ICommand Select => new Command(track =>
        {
            audioPlayer.Play(track as Track, SelectedPlaylist);
        });
        public void SetPlaylist(Playlist playlist)
        {
            SelectedPlaylist = playlist;
            NotifyOfPropertyChange(() => SelectedPlaylist);
        }
    }
}
