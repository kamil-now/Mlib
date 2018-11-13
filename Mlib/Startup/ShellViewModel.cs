using Caliburn.Micro;
using Mlib.Properties;
using Mlib.UI.ViewModels;
using Mlib.UI.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Mlib.ViewModels
{
    public class ShellViewModel : Screen
    {
        private static readonly int maximizedWindowBorderThickness = 5;
        private static readonly int minimizedWindowBorderThickness = 0;
        private bool mRestoreForDragMove;
        private bool settingsMenuVisible;
        private Timer timer;
        private bool closeCanceled = true;
        private Window window;
        IWindowManager windowManager;

        public bool IsMaximized => window?.WindowState == WindowState.Maximized;
        public IMainViewModel MainView { get; }
        public IViewModel SettingsMenuView { get; }

        public bool SettingsMenuVisible
        {
            get => settingsMenuVisible;
            set
            {
                settingsMenuVisible = value;
                NotifyOfPropertyChange();
            }
        }


        public ShellViewModel(IMainViewModel mainView, IWindowManager windowManager, SettingsMenuViewModel settingsMenuView)
        {
            MainView = mainView;
            SettingsMenuView = settingsMenuView;
            this.windowManager = windowManager;
        }

        public void ToggleSettingsMenu() => SettingsMenuVisible = !SettingsMenuVisible;
        public void CancelCloseSettingsMenu() => closeCanceled = true;
        public void CloseSettingsMenu()
        {
            closeCanceled = false;
            timer = new Timer(1500);
            timer.Elapsed += (s, e) =>
            {
                if (!closeCanceled)
                {
                    timer.Stop();
                    SettingsMenuVisible = false;
                }
            };
            timer.Start();
        }
        public void OnLoad(Window window)
        {
            this.window = window;
            window.Style = (Style)window.FindResource(typeof(Window));

            SetWindowSize();
            SetWindowMargin();

            window.StateChanged += (s, e) =>
            {
                window.BorderThickness = IsMaximized ? new Thickness(maximizedWindowBorderThickness) : new Thickness(minimizedWindowBorderThickness);
                NotifyOfPropertyChange(() => IsMaximized);
            };
        }
        public void OnClosed()
        {
            Settings.Default.Save();
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
            window.MinHeight = window.Height * 0.7;
            window.MinWidth = window.Width * 0.7;
        }
        private void SetWindowMargin()
        {
            //TODO external setting - last position and size on close
            window.Left = (SystemParameters.PrimaryScreenWidth / 2) - (window.Width / 2);
            window.Top = (SystemParameters.PrimaryScreenHeight / 2) - (window.Height / 2);
        }
    }
}
