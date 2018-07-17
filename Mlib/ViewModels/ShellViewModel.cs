using Caliburn.Micro;
using Mlib.Domain;
using Mlib.Domain.Infrastracture;
using Mlib.UI.ViewModels;
using Mlib.UI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.ViewModels
{
    public class ShellViewModel : Screen
    {
        private IMainViewModel mainView;
        public IMainViewModel MainView
        {
            get => mainView;
            set
            {
                mainView = value;
                NotifyOfPropertyChange();
            }
        }
        public ShellViewModel(IMainViewModel main)
        {
            mainView = main;
        }

    }
}
