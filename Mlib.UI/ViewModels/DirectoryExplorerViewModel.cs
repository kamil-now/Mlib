using Caliburn.Micro;
using Mlib.Domain;
using Mlib.Domain.Infrastructure;
using Mlib.Domain.Infrastructure.Interfaces;
using Mlib.UI.ViewModels.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class DirectoryExplorerViewModel : Screen, IViewModel
    {
        AudioPlayer audioPlayer;
        PlaylistViewModel playlistVM;
        public BindableCollection<FileInfo> Files { get; set; }
        public DirectoryExplorerViewModel(AudioPlayer audioPlayer,PlaylistViewModel playlistVM)
        {
            this.audioPlayer = audioPlayer;
            this.playlistVM = playlistVM;
            Files = new BindableCollection<FileInfo>(new DirectoryInfo(@"D:\MBLibrary\Playlists").GetFiles("*.m3u"));
        }
        public ICommand SelectDirectory => new Command(a =>
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                Files = new BindableCollection<FileInfo>(new DirectoryInfo(dialog.SelectedPath).GetFiles("*.m3u"));
                NotifyOfPropertyChange(() => Files);
            }
        });
        public ICommand Select => new Command(fileInfo =>
           {
               playlistVM.SetPlaylist(new Playlist(fileInfo as FileInfo));
               //audioPlayer.SetNowPlaying(fileInfo as FileInfo);
               
           });
    }
}
