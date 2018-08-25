using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;
using System.Windows;

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
        public void OnLoad(Window view)
        {
            var windowHeightScale = 0.8;//TODO external setting
            var windowWidthScale = 0.8;//TODO external setting
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            view.Width = screenWidth * windowWidthScale;
            view.Height = screenHeight * windowHeightScale ;

            view.Left = (screenWidth / 2) - (view.Width / 2);
            view.Top = (screenHeight / 2) - (view.Height / 2);
        }
    }
}
