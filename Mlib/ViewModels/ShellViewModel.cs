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
            view.Height = screenHeight * windowHeightScale;

            view.Left = (screenWidth / 2) - (view.Width / 2);
            view.Top = (screenHeight / 2) - (view.Height / 2);
        }
        public void Close()
        {
            Application.Current.Shutdown();
        }
        private Point startPoint;
        public void Drag(Window view)
        {
            view.DragMove();
        }
        //private void toggleButton_PreviewMouseLeftButtonDown(
        //    object sender, MouseButtonEventArgs e)
        //{
        //    startPoint = e.GetPosition(toggleButton);
        //}

        //private void toggleButton_PreviewMouseMove(object sender, MouseEventArgs e)
        //{
        //    var currentPoint = e.GetPosition(toggleButton);
        //    if (e.LeftButton == MouseButtonState.Pressed &&
        //        toggleButton.IsMouseCaptured &&
        //        (Math.Abs(currentPoint.X - startPoint.X) >
        //            SystemParameters.MinimumHorizontalDragDistance ||
        //        Math.Abs(currentPoint.Y - startPoint.Y) >
        //            SystemParameters.MinimumVerticalDragDistance))
        //    {
        //        // Prevent Click from firing
        //        toggleButton.ReleaseMouseCapture();
        //        DragMove();
        //    }
        //}
    }
}
