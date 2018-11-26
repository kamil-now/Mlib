namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
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

    public class PlaylistViewModel : Screen, IViewModel
    {
        public AudioPlayer AudioPlayer { get; }
        public ObservableCollection<Track> Tracks
        {
            get => _tracks;
            set
            {
                _tracks = value;
                NotifyOfPropertyChange();
            }
        }
        public PlaylistViewModel(AudioPlayer audioPlayer)
        {
            AudioPlayer = audioPlayer;
            Tracks = new ObservableCollection<Track>(AudioPlayer.CurrentPlaylist.Tracks);
        }
        ListView listView;
        private ObservableCollection<Track> _tracks;
        void Drag(object sender, MouseButtonEventArgs args)
        {
            try
            {
                if (sender is ListViewItem)
                {
                    ListViewItem draggedItem = sender as ListViewItem;
                    DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                    draggedItem.IsSelected = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


        void Drop(object sender, DragEventArgs args)
        {
            try
            {
                var droppedData = args.Data.GetData(typeof(Track)) as Track;
                var target = ((ListViewItem)(sender)).DataContext as Track;

                int removedIdx = listView.Items.IndexOf(droppedData);
                int targetIdx = listView.Items.IndexOf(target);

                if (removedIdx < targetIdx)
                {
                    Tracks.Insert(targetIdx + 1, droppedData);
                    Tracks.RemoveAt(removedIdx);
                }
                else
                {
                    int remIdx = removedIdx + 1;
                    if (Tracks.Count + 1 > remIdx)
                    {
                        Tracks.Insert(targetIdx, droppedData);
                        Tracks.RemoveAt(remIdx);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
        public void InitListView(object sender)
        {
            listView = sender as ListView;


            Style itemContainerStyle = new Style(typeof(ListViewItem));
            itemContainerStyle.BasedOn = (Style)listView.FindResource(typeof(ListViewItem));
            itemContainerStyle.Setters.Add(new Setter(UIElement.AllowDropProperty, true));
            itemContainerStyle.Setters.Add(new EventSetter(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Drag)));
            itemContainerStyle.Setters.Add(new EventSetter(UIElement.DropEvent, new DragEventHandler(Drop)));
            listView.ItemContainerStyle = itemContainerStyle;
        }
        public ICommand Select => new Command(track =>
        {
            AudioPlayer.Play(track as Track);
        });
    }
}
