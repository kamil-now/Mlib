using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib
{
    public interface IPlaybackStateSubject
    {
        void Attach(IPlaybackStateObserver observer);
        void Detach(IPlaybackStateObserver observer);
        void NotifyOfPlaybackStateChange();
    }
}
