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
        public LibraryViewModel Library { get; }

        public BindableCollection<IContextMenuItem> ContextMenuItems { get; }

        public MainViewModel(PlaylistViewModel playlist, AudioControlsViewModel audioControls, LibraryViewModel library, MusicLibrary musicLibrary)
        {
            Playlist = playlist;
            AudioControls = audioControls;
            Library = library;
            ContextMenuItems = new BindableCollection<IContextMenuItem>()
            {
                new ListActionItem("Add files to library", new Command(musicLibrary.AddMusicFiles))
            };
        }
    }
}
