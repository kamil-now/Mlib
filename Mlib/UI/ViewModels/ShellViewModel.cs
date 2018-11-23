namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Properties;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System.Collections.Generic;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Shell;

    public class ShellViewModel : Screen, IViewModel
    {
        public IViewModel ViewModel { get; set; }
        private static readonly int maximizedWindowBorderThickness = 5;
        private static readonly int minimizedWindowBorderThickness = 1;
        private bool mRestoreForDragMove;
        private bool contextMenuVisible;
        private Window window;

        public bool IsMaximized => window?.WindowState == WindowState.Maximized;


        public bool ContextMenuVisible
        {
            get => contextMenuVisible && ViewModel is IContextMenuAgent && (ViewModel as IContextMenuAgent).ContextMenuItems != null;
            set
            {
                contextMenuVisible = value;
                NotifyOfPropertyChange();
            }
        }

        public ShellViewModel(IViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void ToggleSettingsMenu() => ContextMenuVisible = !ContextMenuVisible;

        public void OnLoad(Window window)
        {
            this.window = window;
            window.Style = (Style)window.FindResource(typeof(Window));


            window.StateChanged += (s, e) =>
            {
                window.BorderThickness = IsMaximized ? new Thickness(maximizedWindowBorderThickness) : new Thickness(minimizedWindowBorderThickness);
                NotifyOfPropertyChange(() => IsMaximized);
            };

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
        public void Close() => TryClose();


    }
}
