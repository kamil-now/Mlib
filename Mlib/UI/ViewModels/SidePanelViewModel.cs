namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Data;
    using Mlib.Data.Models;
    using Mlib.Extensions;
    using Mlib.Infrastructure;
    using Mlib.Properties;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class SidePanelViewModel : Screen, IViewModel
    {
        UnitOfWork unitOfWork;
        AudioPlayer audioPlayer;
        MusicLibrary library;
        private IDataEntity selected;
        private ListCommandItem selectedGroup;
        private BindableCollection<IDataEntity> collection;

        public BindableCollection<IDataEntity> Collection
        {
            get => collection; set
            {
                collection = value;
                NotifyOfPropertyChange();
            }
        }
        public ListCommandItem SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                selectedGroup.Command.Execute(null);
                selectedGroup.IsSelected = true;
                NotifyOfPropertyChange();
            }
        }
        public IDataEntity Selected
        {
            get => selected;
            set
            {
                selected = value;
                Select.Execute(selected);
            }
        }
        public BindableCollection<ListCommandItem> Groups { get; set; }
        public BindableCollection<IDataEntity> Playlists { get; private set; }
        public BindableCollection<IDataEntity> Tracks { get; private set; }
        public BindableCollection<IDataEntity> Artists { get; private set; }
        public BindableCollection<IDataEntity> Albums { get; private set; }

        public SidePanelViewModel(UnitOfWork unitOfWork, MusicLibrary library, AudioPlayer audioPlayer)
        {
            this.unitOfWork = unitOfWork;
            this.library = library;
            this.audioPlayer = audioPlayer;

            //TODO observer
            unitOfWork.DbContextChanged += DbStateChanged;

            SetCollections();
            SetGroups();

        }
        void SetCollections()
        {
            var playlists = unitOfWork.Playlists.GetAll().ToList();
            var tracks = unitOfWork.Tracks.GetAll().ToList();
            var albums = unitOfWork.Albums.GetAll().ToList();
            var artists = unitOfWork.Artists.GetAll().ToList();
            Playlists = !playlists.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(playlists) : new BindableCollection<IDataEntity>();
            Tracks = !tracks.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(tracks) : new BindableCollection<IDataEntity>();
            Albums = !albums.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(albums) : new BindableCollection<IDataEntity>();
            Artists = !artists.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(artists) : new BindableCollection<IDataEntity>();

        }
        void SetGroups()
        {
            Groups = new BindableCollection<ListCommandItem>()
            {
                new ListCommandItem("All tracks", new Command(()=>Collection=Tracks)),
                new ListCommandItem("Albums", new Command(()=>Collection=Albums)),
                new ListCommandItem("Artists", new Command(()=>Collection=Artists)),
                new ListCommandItem("Playlists", new Command(()=>Collection=Playlists)){ IsSelected=true},
            };
            Collection = Playlists;
        }
        private void DbStateChanged(object sender, EventArgs e)
        {
            var args = e as DbContextChangedEventArgs;
            var tuple = GetCollection(args.Entity.Type);
            ICollection<IDataEntity> collection = tuple.Collection;

            switch (args.State)
            {
                case EntityState.Deleted: collection.Remove(collection.First(x => x.Id == args.Entity.Id)); break;
                case EntityState.Added: collection.Add(args.Entity); break;
                case EntityState.Modified: collection.Swap(collection.First(x => x.Id == args.Entity.Id), args.Entity); break;
            }
            tuple.NotifyAction.Invoke();
            NotifyOfPropertyChange(() => Collection);
        }
        private (ICollection<IDataEntity> Collection, System.Action NotifyAction) GetCollection(EntityType type)
        {
            switch (type)
            {
                case EntityType.Playlist: return (Playlists, () => NotifyOfPropertyChange(() => Playlists));
                case EntityType.Track: return (Tracks, () => NotifyOfPropertyChange(() => Tracks));
                case EntityType.Album: return (Albums, () => NotifyOfPropertyChange(() => Albums));
                case EntityType.Artist: return (Artists, () => NotifyOfPropertyChange(() => Artists));
            }
            return (null, null);
        }

        public ICommand Select => new Command(item =>
        {
            if (item is Playlist)
                audioPlayer.SetPlaylist(item as Playlist);
            if (item is Track)
                audioPlayer.SetNowPlaying(item as Track);
        });
        public ICommand AddNew => new Command(() =>
        {
            var tracks = new TracksViewModel() { Tracks = new BindableCollection<Track>(Tracks.Select(x => x as Track)) };

            var settings = new Dictionary<DependencyProperty, object>()
            {
                {Window.WidthProperty, 500d },
                {Window.HeightProperty, 500d }
            };
            AppWindowManager.SetMainPanel(tracks);
        });

    }
}
