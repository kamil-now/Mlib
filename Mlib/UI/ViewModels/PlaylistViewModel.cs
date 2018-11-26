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

    public class PlaylistViewModel : Screen, IViewModel, IDragSource
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
        private ObservableCollection<Track> _tracks;
      
        public void StartDrag(IDragInfo dragInfo)
        {
            Track track = (Track)dragInfo.SourceItem;

            dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
            dragInfo.Data = track;

        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            throw new NotImplementedException();
        }

        public void Dropped(IDropInfo dropInfo)
        {
            throw new NotImplementedException();
        }

        public void DragCancelled()
        {
            throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        
        public ICommand Select => new Command(track =>
        {
            AudioPlayer.Play(track as Track);
        });
    }
}
