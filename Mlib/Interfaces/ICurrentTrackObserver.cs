using Mlib.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Interfaces
{
    public interface ICurrentTrackObserver
    {
        void UpdateCurrentTrack(Track currentTrack);
    }
}
