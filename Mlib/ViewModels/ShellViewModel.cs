using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;
using System.Windows;

namespace Mlib.ViewModels
{
    public class ShellViewModel : Screen
    {
        private Window window;
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
        public void OnLoad(Window window)
        {
            this.window = window;
            var windowHeightScale = 0.8;//TODO external setting
            var windowWidthScale = 0.8;//TODO external setting
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            window.Width = screenWidth * windowWidthScale;
            window.Height = screenHeight * windowHeightScale;

            window.Left = (screenWidth / 2) - (window.Width / 2);
            window.Top = (screenHeight / 2) - (window.Height / 2);
        }
        public void Close()
        {
            Application.Current.Shutdown();
        }
        public void Drag() => window.DragMove();

        public void Minimize() => window.WindowState = WindowState.Minimized;
    }
}
