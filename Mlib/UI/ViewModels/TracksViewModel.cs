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
        private static readonly int maximizedWindowBorderThickness = 5;
        private static readonly int minimizedWindowBorderThickness = 0;
        private Window window;
        private bool mRestoreForDragMove;
        public bool IsMaximized => window?.WindowState == WindowState.Maximized;
        public void OnLoad(FrameworkElement element)
        {
            while (!(element is Window))
            {
                element = element.Parent as FrameworkElement;
            }
            window = element as Window;

            element.Style = (Style)element.FindResource(typeof(Window));

            //SetWindowSize();
            //SetWindowMargin();

            window.StateChanged += (s, e) =>
            {
                window.BorderThickness = IsMaximized ? new Thickness(maximizedWindowBorderThickness) : new Thickness(minimizedWindowBorderThickness);
                //NotifyOfPropertyChange(() => IsMaximized);
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
        public new void MouseMove(MouseEventArgs e)
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
        public new void MouseUp() => mRestoreForDragMove = false;
        public void ToggleWindowState() => window.WindowState = IsMaximized ? WindowState.Normal : WindowState.Maximized;
        public void Minimize() => window.WindowState = WindowState.Minimized;
        public void Close() => window.Close();
    }
}
