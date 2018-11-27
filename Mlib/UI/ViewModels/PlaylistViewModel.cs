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
            Separator separator;

            separator = FindVisualChild<Separator>(dropInfo.VisualTargetItem as ListViewItem);
            if (separator != null)
                separator.BorderBrush = dragOverBrush;
            if (previous != null && previous != separator)
            {
                previous.BorderBrush = defaultBrush;
            }
            previous = separator;
        }
        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }
        public static T FindVisualParent<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(depObj);
                if (parent != null && parent is T)
                {
                    return (T)parent;
                }

                T parentItem = FindVisualParent<T>(parent);
                if (parentItem != null) return parentItem;

            }
            return null;
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
                var separator = FindVisualChild<Separator>(dropInfo.VisualTargetItem as ListViewItem);
                separator.BorderBrush = Application.Current.FindResource("GlobalLightDim") as SolidColorBrush;
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
            return true;//throw new NotImplementedException();
        }

        public void Dropped(IDropInfo dropInfo)
        {
            //throw new NotImplementedException();
        }

        public void DragCancelled()
        {
            //throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            return false;//throw new NotImplementedException();
        }
    }
}
