using Caliburn.Micro;
using Mlib.Data.Models;
using Mlib.UI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class TracksViewModel : Screen, IViewModel
    {
        public BindableCollection<Track> Tracks { get; set; }
        public Track Selected { get; set; }
        public TracksViewModel()
        {
        }
    }
}
