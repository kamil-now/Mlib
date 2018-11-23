namespace Mlib.UI.Controls
{
    using Caliburn.Micro;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public partial class ContextMenu : UserControl
    {
        public ContextMenu()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
            List.MouseLeave += (s, e) => CloseSettingsMenu();
            List.MouseEnter += (s, e) => CancelCloseSettingsMenu();
        }

        public static readonly DependencyProperty MenuItemsProperty =
        DependencyProperty.RegisterAttached("MenuItems", typeof(BindableCollection<IContextMenuItem>), typeof(ContextMenu), new PropertyMetadata(null, OnMenuItemsPropertyChanged));

        public static new readonly DependencyProperty IsVisibleProperty =
        DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(ContextMenu), new PropertyMetadata(false, new PropertyChangedCallback(OnIsVisblePropertyChanged)));

        public BindableCollection<IContextMenuItem> MenuItems
        {
            get { return (BindableCollection<IContextMenuItem>)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }
        public new bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        private static void OnMenuItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contextMenu = d as ContextMenu;
            contextMenu.List.ItemsSource = contextMenu.MenuItems;
        }
        private static void OnIsVisblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contextMenu = d as ContextMenu;
            contextMenu.Visibility = contextMenu.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }

        private Timer timer;
        private bool closeCanceled = true;
        private void CancelCloseSettingsMenu() => closeCanceled = true;
        private void CloseSettingsMenu()
        {
            closeCanceled = false;
            timer = new Timer(1500);
            timer.Elapsed += (s, e) =>
            {
                if (!closeCanceled)
                {
                    timer.Stop();
                    try
                    {
                        Application.Current.Dispatcher.Invoke(()=>IsVisible = false);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            };
            timer.Start();
        }
    }
}
