namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using GongSolutions.Wpf.DragDrop;
    using Mlib.Data.Models;
    using Mlib.Infrastructure;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class PlaylistViewModel : Screen, IViewModel, IDropTarget, IDragSource
    {
        public AudioPlayer AudioPlayer { get; }
        public ObservableCollection<Track> Tracks
        {
            get => _tracks;
            set
            {
                _tracks = value;
                AudioPlayer.CurrentPlaylist.Tracks = value;
                NotifyOfPropertyChange();
            }
        }
        public PlaylistViewModel(AudioPlayer audioPlayer)
        {
            AudioPlayer = audioPlayer;
            Tracks = new ObservableCollection<Track>(AudioPlayer.CurrentPlaylist.Tracks);
        }
        private ObservableCollection<Track> _tracks;


        public ICommand Select => new Command(track =>
        {
            AudioPlayer.Play(track as Track);
        });
        Separator previous;
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.Effects = DragDropEffects.Move;
            var dragOverBrush = Application.Current.FindResource("GlobalAccent") as SolidColorBrush;
            var defaultBrush = Application.Current.FindResource("GlobalLightDim") as SolidColorBrush;


            var separator = AppWindowManager.FindVisualChild<Separator>(dropInfo.VisualTargetItem as ListViewItem);
            if (separator != null)
                separator.BorderBrush = dragOverBrush;
            if (previous != null && previous != separator)
            {
                previous.BorderBrush = defaultBrush;
            }
            previous = separator;
        }

        public void Drop(IDropInfo dropInfo)
        {
            Track sourceItem = dropInfo.Data as Track;
            Track targetItem = dropInfo.TargetItem as Track;
            if (targetItem != null && sourceItem != null)
            {

                var targetIndex = Tracks.IndexOf(Tracks.First(n => n.Id == targetItem.Id));
                var sourceIndex = Tracks.IndexOf(Tracks.First(n => n.Id == sourceItem.Id));

                Tracks.RemoveAt(sourceIndex);
                Tracks.Insert(targetIndex, sourceItem);
                var separator = AppWindowManager.FindVisualChild<Separator>(dropInfo.VisualTargetItem as ListViewItem);
                separator.BorderBrush = Application.Current.FindResource("GlobalLightDim") as SolidColorBrush;
                if (AudioPlayer?.NowPlaying?.Id == sourceItem?.Id)
                {
                    (dropInfo.VisualTarget as ListView).SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedItemProperty, sourceItem);
                }
            }

        }
        public void StartDrag(IDragInfo dragInfo)
        {
            Track track = (Track)dragInfo.SourceItem;

            dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
            dragInfo.Data = track;

        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            return true;
        }

        public void Dropped(IDropInfo dropInfo)
        {
        }

        public void DragCancelled()
        {
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            return false;
        }
    }
}
