using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;

namespace Mlib.UI.ViewModels
{
    public class MainViewModel : Screen, IViewModel, IMainViewModel
    {
        public DirectoryExplorerViewModel DirectoryExplorer { get; }
        public AudioControlsViewModel AudioControls { get; }
        public PlaylistViewModel Playlist { get; }
        public MainViewModel(PlaylistViewModel playlist, DirectoryExplorerViewModel directoryExplorer, AudioControlsViewModel audioControls)
        {
            Playlist = playlist;
            DirectoryExplorer = directoryExplorer;
            AudioControls = audioControls;
        }
    }
}
