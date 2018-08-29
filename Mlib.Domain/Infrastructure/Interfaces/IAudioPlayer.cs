using System.ComponentModel;
using System.IO;

namespace Mlib.Domain.Infrastructure.Interfaces
{
    public interface IAudioPlayer : INotifyPropertyChanged
    {
        bool IsPlaying { get; }
        FileInfo File { get; set; }
        void Play();// FileInfo file);
        void UnPause();
        void Pause();
        void Stop();
    }
}
