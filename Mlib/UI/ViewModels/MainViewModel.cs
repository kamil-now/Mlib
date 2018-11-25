namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Infrastructure;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System.Collections.Generic;

    public class MainViewModel : Screen, IMainViewModel
    {
        private IViewModel mainPanel;
        private IViewModel leftSidePanel;
        private IViewModel rightSidePanel;
        private IViewModel bottomPanel;

        public IViewModel BottomPanel
        {
            get => bottomPanel;
            set
            {
                bottomPanel = value;
                NotifyOfPropertyChange();
            }
        }
        public IViewModel LeftSidePanel
        {
            get => leftSidePanel;
            set
            {
                leftSidePanel = value;
                NotifyOfPropertyChange();
            }
        }
        public IViewModel RightSidePanel
        {
            get => rightSidePanel;
            set
            {
                rightSidePanel = value;
                NotifyOfPropertyChange();
            }
        }
        public IViewModel MainPanel
        {
            get => mainPanel;
            set
            {
                mainPanel = value;
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<IContextMenuItem> ContextMenuItems { get; }


        public MainViewModel(BottomPanelViewModel bottomPanel, SidePanelViewModel sidePanel, MusicLibrary musicLibrary)
        {
            BottomPanel = bottomPanel;
            LeftSidePanel = sidePanel;
            ContextMenuItems = new BindableCollection<IContextMenuItem>()
            {
                new ListCommandItem("Add files to library", new Command(musicLibrary.AddMusicFiles))
            };
        }
    }
}
