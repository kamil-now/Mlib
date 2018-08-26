using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Domain.Infrastructure.Interfaces
{
    public interface IAudioPlayer
    {
        void Play(FileInfo file);
        void UnPause();
        void Pause();
        void Stop();
    }
}
