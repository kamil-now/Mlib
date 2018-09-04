using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Domain.Infrastructure.Interfaces
{
    public interface ICurrentTrackObserver
    {
        void UpdateCurrentTrack(Track currentTrack);
    }
}
