using Caliburn.Micro;
using Mlib.Data;
using Mlib.Data.Models;
using Mlib.Extensions;
using Mlib.Infrastructure;
using Mlib.UI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class LibraryViewModel : Screen, IViewModel
    {
        UnitOfWork unitOfWork;
        PlaylistViewModel playlistVM;
        private FileInfo selected;

        public BindableCollection<FileInfo> Files { get; set; }
        public FileInfo Selected
        {
            get => selected;
            set
            {
                selected = value;
                Select.Execute(selected);
            }
        }
        public BindableCollection<Playlist> Playlists { get; }
        public BindableCollection<Track> Tracks { get; }
        public BindableCollection<Artist> Artists { get; }
        public BindableCollection<Album> Albums { get; }

        public LibraryViewModel(UnitOfWork unitOfWork, PlaylistViewModel playlistVM)
        {
            this.unitOfWork = unitOfWork;
            this.playlistVM = playlistVM;

            var playlists = unitOfWork.GetAll<Playlist>();
            var tracks = unitOfWork.GetAll<Track>();

            Playlists = !playlists.IsNullOrEmpty() ? new BindableCollection<Playlist>(playlists) : new BindableCollection<Playlist>();
            Tracks = !tracks.IsNullOrEmpty() ? new BindableCollection<Track>(tracks) : new BindableCollection<Track>();
        }
        public ICommand Select => new Command(fileInfo => playlistVM.SetPlaylist(new Playlist(fileInfo as FileInfo)));
        public ICommand AddNew => new Command(() =>
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".m3u";
            dlg.Filter = "M3U files (*.m3u)|*.m3u";
            dlg.InitialDirectory = "D:\\MBlibrary";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                var fileInfo = new FileInfo(filename);
                playlistVM.SetPlaylist(new Playlist(fileInfo as FileInfo));
            }
        });
    }
}
