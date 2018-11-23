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

    public class LibraryViewModel : Screen, IViewModel
    {
        UnitOfWork unitOfWork;
        AudioPlayer audioPlayer;
        MusicLibrary library;
        PlaylistViewModel playlistVM;
        TracksViewModel tracksVM;
        private IDataEntity selected;

        public BindableCollection<IDataEntity> Collection { get; set; }
        public IDataEntity Selected
        {
            get => selected;
            set
            {
                selected = value;
                Select.Execute(selected);
            }
        }
        public BindableCollection<IDataEntity> Playlists { get; }
        public BindableCollection<IDataEntity> Tracks { get; }
        public BindableCollection<IDataEntity> Artists { get; }
        public BindableCollection<IDataEntity> Albums { get; }

        public LibraryViewModel(UnitOfWork unitOfWork, MusicLibrary library, AudioPlayer audioPlayer, PlaylistViewModel playlistVM, TracksViewModel tracksVM)
        {
            this.unitOfWork = unitOfWork;
            this.library = library;
            this.playlistVM = playlistVM;
            this.audioPlayer = audioPlayer;
            this.tracksVM = tracksVM;


            var playlists = unitOfWork.Playlists.GetAll().ToList();
            var tracks = unitOfWork.Tracks.GetAll().ToList();
            var albums = unitOfWork.Albums.GetAll().ToList();
            var artists = unitOfWork.Artists.GetAll().ToList();

            //TODO observer
            unitOfWork.DbContextChanged += DbStateChanged;

            Playlists = !playlists.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(playlists) : new BindableCollection<IDataEntity>();
            Tracks = !tracks.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(tracks) : new BindableCollection<IDataEntity>();
            Albums = !albums.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(albums) : new BindableCollection<IDataEntity>();
            Artists = !artists.IsNullOrEmpty() ? new BindableCollection<IDataEntity>(artists) : new BindableCollection<IDataEntity>();

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
        public ICommand SetCollection => new Command(type =>
         {
             //TODO enum switch
             var t = type as Type;
             if (t == typeof(Album))
             {
                 Collection = Albums;

             }
             else if (t == typeof(Track))
             {
                 Collection = Tracks;
             }
             else if (t == typeof(Artist))
             {
                 Collection = Artists;
             }
             else if (t == typeof(Playlist))
             {
                 Collection = Playlists;
             }
             NotifyOfPropertyChange(() => Collection);
         });
        public ICommand Select => new Command(item =>
        {
            if (item is Playlist)
                playlistVM.SetPlaylist(item as Playlist);
            if (item is Track)
                audioPlayer.SetNowPlaying(item as Track);
        });
        public ICommand AddNew => new Command(() =>
        {
            tracksVM.Tracks = new BindableCollection<Track>(Tracks.Select(x => x as Track));
            dynamic settings = new ExpandoObject();
            settings.Width = 500;
            settings.Height = 300;
            AppWindowManager.ShowWindow(tracksVM, settings);
        });

    }
}
