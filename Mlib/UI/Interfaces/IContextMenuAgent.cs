namespace Mlib.UI.Interfaces
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IContextMenuAgent
    {
        BindableCollection<IContextMenuItem> ContextMenuItems { get; }
    }
}
