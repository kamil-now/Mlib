using Caliburn.Micro;
using Mlib.Extensions;
using Mlib.Infrastructure;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Mlib.Data.Models
{
    public class Playlist : IDataEntity
    {
        [NotMapped]
        public string Id => Name;
        [NotMapped]
        public EntityType Type => EntityType.Playlist;
        [NotMapped]
        public ICollection<Track> Tracks
        {
            get => PlaylistTracks.Select(n => n.Track).ToList();//.OrderBy(n => n.Number).ToList();
            set
            {
                //value.ForEach(n =>
                //{
                //    var entity = PlaylistTracks.First(x => x.Track.Id == n.Id);
                //    entity.Number = (uint)value.ToList().IndexOf(n);
                //    IoC.Get<UnitOfWork>().AddOrUpdate(entity, true);

                //});
            }
        }
        [Key]
        [Required]
        public string Name { get; set; }
        public string ImageId { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }

        public Playlist()
        {

        }
        public Playlist(string name)
        {
            Name = name;
        }

    }
}