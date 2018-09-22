using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Interfaces
{
   public interface ICurrentTrackSubject
    {
        void Attach(ICurrentTrackObserver observer);
        void Detach(ICurrentTrackObserver observer);
        void NotifyOfCurrentTrackChange();
    }
}
