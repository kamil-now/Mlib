using Caliburn.Micro;
using Mlib.Infrastructure;
using Mlib.UI.Additional;
using Mlib.UI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Mlib.UI.ViewModels
{
    public class SettingsMenuViewModel : Screen, IViewModel
    {
       
        public List<ListActionItem> Settings { get; }
        public SettingsMenuViewModel(MusicLibrary library)
        {
            Settings = new List<ListActionItem>()
            {
                new ListActionItem("Add files to library", new Command(library.AddMusicFiles))
            };
        }
        
    }

}
