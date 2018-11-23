using Caliburn.Micro;
using Mlib.UI.ViewModels;
using Mlib.UI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Mlib.Infrastructure
{
    public static class MlibWindowManager
    {
        static Dictionary<string, object> settings = new Dictionary<string, object>()
            {
                { nameof(Window.Height), 400},
                { nameof(Window.Width), 800},
                { nameof(Window.SizeToContent), SizeToContent.Manual},
                { nameof(Window.WindowStyle), WindowStyle.None},
                { nameof(Window.WindowStartupLocation), WindowStartupLocation.CenterScreen},
                { nameof(Window.BorderBrush), new SolidColorBrush(Colors.Black)},
                { nameof(Window.BorderThickness), new Thickness(1)},
                { nameof(Window.WindowState),WindowState.Normal},
                { nameof(Window.ResizeMode), ResizeMode.CanResize},
                { nameof(Window.AllowsTransparency), true}
            };
        public static void ShowWindow(IViewModel viewModel)
        {
            var window = new WindowViewModel() { ViewModel = viewModel };
            IoC.Get<IWindowManager>().ShowWindow(window, null, settings);
        }
    }
}
