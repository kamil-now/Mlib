namespace Mlib.UI
{
    using Caliburn.Micro;
    using Mlib.Extensions;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using Mlib.UI.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    public static class AppWindowManager
    {
        static AppWindowManager()
        {
            //TODO external settings + last position and size on close
            var windowHeightScale = 0.8;
            var windowWidthScale = 0.82;
            var minSizeProportionPercent = 0.7;

            var height = SystemParameters.PrimaryScreenHeight * windowHeightScale;
            var width = SystemParameters.PrimaryScreenWidth * windowWidthScale;
            var left = (SystemParameters.PrimaryScreenWidth / 2) - (width / 2);
            var top = (SystemParameters.PrimaryScreenHeight / 2) - (height / 2);

            defaultSettings.Add(nameof(Window.Width), width);
            defaultSettings.Add(nameof(Window.Height), height);
            defaultSettings.Add(nameof(Window.MinHeight), height * minSizeProportionPercent);
            defaultSettings.Add(nameof(Window.MinWidth), width * minSizeProportionPercent);
            defaultSettings.Add(nameof(Window.Left), left);
            defaultSettings.Add(nameof(Window.Top), top);


        }
        static Dictionary<string, object> defaultSettings = new Dictionary<string, object>()
            {
                { nameof(Window.SizeToContent), SizeToContent.Manual},
                { nameof(Window.WindowStartupLocation), WindowStartupLocation.CenterScreen},
                { nameof(Window.BorderThickness), new Thickness(1)},
                { nameof(Window.ResizeMode), ResizeMode.CanResize}
            };
        public static void ShowWindow<T>(T viewModel,IDictionary<string, object> windowSettings = null) where T : IViewModel
        {
            windowSettings?.ForEach(n =>
            {
                if (defaultSettings.ContainsKey(n.Key))
                {
                    defaultSettings[n.Key] = n.Value;
                }
            });
            var window = new ShellViewModel(viewModel);
            IoC.Get<IWindowManager>().ShowWindow(window, null, windowSettings);
        }
        public static void ShowWindow<T>(IDictionary<string, object> settings = null) where T : IViewModel
        {
            ShowWindow((T)IoC.GetInstance(typeof(T), null), settings);
        }
    }
}
