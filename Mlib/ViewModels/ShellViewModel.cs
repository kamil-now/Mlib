using Caliburn.Micro;
using Mlib.UI.ViewModels.Interfaces;
using System.Windows;
using System.Windows.Input;

namespace Mlib.ViewModels
{
    public class ShellViewModel : Screen
    {
        private bool mRestoreForDragMove;
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
        public bool IsMaximized => window?.WindowState == WindowState.Maximized;

        public ShellViewModel(IMainViewModel main)
        {
            mainView = main;
        }
      
        public void OnLoad(Window window)
        {
            this.window = window;
            window.Style = (Style)window.FindResource(typeof(Window));

            SetWindowSize();
            SetWindowMargin();

            window.StateChanged += (s, e) => NotifyOfPropertyChange(() => IsMaximized);
        }

        public void Drag(MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (window.ResizeMode != ResizeMode.CanResize && window.ResizeMode != ResizeMode.CanResizeWithGrip)
                {
                    return;
                }

                ToggleWindowState();
            }

            else
            {
                mRestoreForDragMove = IsMaximized;
                window.DragMove();
            }

        }
        public void MouseMove(MouseEventArgs e)
        {
            if (mRestoreForDragMove)
            {
                mRestoreForDragMove = false;

                var point = window.PointToScreen(e.MouseDevice.GetPosition(window));

                window.Left = point.X - (window.RestoreBounds.Width * 0.5);
                window.Top = point.Y;

                window.WindowState = WindowState.Normal;

                window.DragMove();
            }
        }
        public void MouseUp() => mRestoreForDragMove = false;
        public void ToggleWindowState() => window.WindowState = IsMaximized ? WindowState.Normal : WindowState.Maximized;
        public void Minimize() => window.WindowState = WindowState.Minimized;
        public void Close() => Application.Current.Shutdown();

        private void SetWindowSize()
        {
            var windowHeightScale = 0.8;//TODO external setting
            var windowWidthScale = 0.82;//TODO external setting

            window.Width = SystemParameters.PrimaryScreenWidth * windowWidthScale;
            window.Height = SystemParameters.PrimaryScreenHeight * windowHeightScale;
        }
        private void SetWindowMargin()
        {
            window.Left = (SystemParameters.PrimaryScreenWidth / 2) - (window.Width / 2);
            window.Top = (SystemParameters.PrimaryScreenHeight / 2) - (window.Height / 2);
        }
    }
}
