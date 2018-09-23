using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;

namespace Mlib.UI.ViewModels
{
    public class MainViewModel : Screen, IViewModel, IMainViewModel
    {
        public DirectoryExplorerViewModel DirectoryExplorer { get; }
        public AudioControlsViewModel AudioControls { get; }
        public PlaylistViewModel Playlist { get; }
        public LibraryViewModel Library { get; }
        public MainViewModel(PlaylistViewModel playlist, DirectoryExplorerViewModel directoryExplorer, AudioControlsViewModel audioControls, LibraryViewModel library)
        {
            Playlist = playlist;
            DirectoryExplorer = directoryExplorer;
            AudioControls = audioControls;
            Library = library;
        }
    }
}
