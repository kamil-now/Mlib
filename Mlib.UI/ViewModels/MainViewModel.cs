using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;

namespace Mlib.UI.ViewModels
{
    public class MainViewModel : Screen, IViewModel, IMainViewModel
    {
        public DirectoryExplorerViewModel DirectoryExplorer { get; }
        public AudioControlsViewModel AudioControls { get; }
        public MainViewModel(DirectoryExplorerViewModel directoryExplorer, AudioControlsViewModel audioControls)
        {
            DirectoryExplorer = directoryExplorer;
            AudioControls = audioControls;
        }
    }
}
