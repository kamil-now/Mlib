namespace Mlib.UI.Interfaces
{
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMainViewModel : IViewModel, IContextMenuAgent
    {
        IViewModel BottomPanel { get; set; }
        IViewModel MainPanel { get; set; }
        IViewModel LeftSidePanel { get; set; }
        IViewModel RightSidePanel { get; set; }
    }
}
