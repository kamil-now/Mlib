using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;

namespace Mlib.UI.ViewModels
{
    public class MainViewModel : Screen, IViewModel, IMainViewModel
    {
        public AudioControlsViewModel AudioControls { get; }
        public PlaylistViewModel Playlist { get; }
        public LibraryViewModel Library { get; }
        public MainViewModel(PlaylistViewModel playlist, AudioControlsViewModel audioControls, LibraryViewModel library)
        {
            Playlist = playlist;
            AudioControls = audioControls;
            Library = library;
        }
    }
}
