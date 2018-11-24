namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Infrastructure;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System.Collections.Generic;

    public class MainViewModel : Screen, IMainViewModel
    {
        public AudioControlsViewModel AudioControls { get; }
        public PlaylistViewModel Playlist { get; }
        public SidePanelViewModel Library { get; }

        public BindableCollection<IContextMenuItem> ContextMenuItems { get; }

        public MainViewModel(PlaylistViewModel playlist, AudioControlsViewModel audioControls, SidePanelViewModel library, MusicLibrary musicLibrary)
        {
            Playlist = playlist;
            AudioControls = audioControls;
            Library = library;
            ContextMenuItems = new BindableCollection<IContextMenuItem>()
            {
                new ListCommandItem("Add files to library", new Command(musicLibrary.AddMusicFiles))
            };
        }
    }
}
