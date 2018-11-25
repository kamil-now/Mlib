using Mlib.Data.Models;
using Mlib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mlib.UI.Controls
{
    /// <summary>
    /// Interaction logic for AudioProgressBar.xaml
    /// </summary>
    public partial class AudioProgressBar : UserControl
    {
        public AudioProgressBar()
        {
            InitializeComponent();
        }
        public static new readonly DependencyProperty AudioPlayerProperty =
        DependencyProperty.RegisterAttached("AudioPlayer", typeof(AudioPlayer), typeof(AudioProgressBar), new PropertyMetadata(null, new PropertyChangedCallback(OnAudioPlayerPropertyChanged)));

        public AudioPlayer AudioPlayer
        {
            get { return (AudioPlayer)GetValue(AudioPlayerProperty); }
            set { SetValue(AudioPlayerProperty, value); }
        }
        private static void OnAudioPlayerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
