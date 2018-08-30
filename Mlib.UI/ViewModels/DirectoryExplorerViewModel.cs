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
        public BindableCollection<FileInfo> Files { get; set; }
        public DirectoryExplorerViewModel(AudioPlayer audioPlayer)
        {
            this.audioPlayer = audioPlayer;
            //Files = new BindableCollection<FileInfo>();
            Files = new BindableCollection<FileInfo>(new DirectoryInfo(@"C:\Users\Kamil\Downloads").GetFiles("*.mp3"));
        }
        public ICommand SelectDirectory => new Command(a =>
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowDialog();
                Files = new BindableCollection<FileInfo>(new DirectoryInfo(dialog.SelectedPath).GetFiles("*.mp3"));
                NotifyOfPropertyChange(() => Files);
            }
        });
        public ICommand Select => new Command(fileInfo =>
           {
               audioPlayer.SetFile(fileInfo as FileInfo);
           });
    }
}
