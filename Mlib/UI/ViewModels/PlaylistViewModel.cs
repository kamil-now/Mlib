namespace Mlib.UI.ViewModels
{
    using Caliburn.Micro;
    using Mlib.Data.Models;
    using Mlib.Infrastructure;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    public class DragDropDecorator<T>
    {
        public DragDropDecorator()
        {
        }
        public T Item { get; set; }
    }
    public class PlaylistViewModel : Screen, IViewModel
    {
        public AudioPlayer AudioPlayer { get; }
        public ObservableCollection<DragDropDecorator<Track>> Tracks
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
            Tracks = new ObservableCollection<DragDropDecorator<Track>>(
                AudioPlayer.CurrentPlaylist.Tracks
                .Select(n => new DragDropDecorator<Track>() { Item = n }));
        }
        ListView listView;
        private ObservableCollection<DragDropDecorator<Track>> _tracks;
        void Drag(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem)
            {
                ListViewItem draggedItem = sender as ListViewItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        void Drop(object sender, DragEventArgs e)
        {
            var droppedData = e.Data.GetData(typeof(DragDropDecorator<Track>)) as DragDropDecorator<Track>;
            var target = ((ListViewItem)(sender)).DataContext as DragDropDecorator<Track>;

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
            AudioPlayer.Play((track as DragDropDecorator<Track>).Item);
        });
    }
}
