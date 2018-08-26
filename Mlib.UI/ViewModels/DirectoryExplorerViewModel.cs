using Caliburn.Micro;
using Mlib.Domain;
using Mlib.Domain.Infrastructure.Interfaces;
using Mlib.UI.ViewModels.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class DirectoryExplorerViewModel : Screen, IViewModel
    {
        IAudioPlayer audioPlayer;
        string path = @"C:\Users\Kamil\Downloads";
        public BindableCollection<FileInfo> Files { get; set; }
        public DirectoryExplorerViewModel(IAudioPlayer audioPlayer)
        {
            this.audioPlayer =audioPlayer;

            DirectoryInfo taskDirectory = new DirectoryInfo(path);
            Files = new BindableCollection<FileInfo>(taskDirectory.GetFiles("*.mp3"));
        }
        public ICommand Select => new Command(fileInfo =>
           {
               audioPlayer.Play(fileInfo as FileInfo);
               Debug.WriteLine((fileInfo as FileInfo).Name);
           });
    }
}
