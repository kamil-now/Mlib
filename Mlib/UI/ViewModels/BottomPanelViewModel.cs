namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using Mlib.Infrastructure;
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BottomPanelViewModel : Screen, IViewModel
    {
        public AudioPlayer AudioPlayer { get; }
        public IViewModel AudioControls { get; }

        public BottomPanelViewModel(AudioControlsViewModel audioControls, AudioPlayer audioPlayer)
        {
            AudioControls = audioControls;
            AudioPlayer = audioPlayer;
        }
    }
}
