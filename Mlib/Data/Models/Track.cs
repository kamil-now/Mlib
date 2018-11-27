using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Mlib.Data.Models
{
    public class Track : IDataEntity
    {
        [NotMapped]
        public string Id => TrackId.ToString();
        [NotMapped]
        public EntityType Type => EntityType.Track;
        [NotMapped]
        public ICollection<Playlist> Playlists => PlaylistTracks.Select(n => n.Playlist).ToList();
        [Key]
        [Required]
        public int TrackId { get; set; }
        [Required]
        public string FullPath { get; set; }
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public long Number { get; set; }
        public long Year { get; set; }
        public long Length { get; set; }
       
        [NotMapped]
        public bool IsCopy => TrackId == -1;

        public Track()
        {
        }
        public Track(FileInfo mp3File)
        {
            TagLib.File taggedFile = TagLib.File.Create(mp3File.FullName);
            Number = taggedFile.Tag. DiscCount;
            Title = taggedFile.Tag.Title;
            Artist = taggedFile.Tag.FirstPerformer;
            Album = taggedFile.Tag.Album;
            Year = taggedFile.Tag.Year;
            Length = taggedFile.Length;
            FullPath = mp3File.FullName;
        }
        public Track Copy()
        {
            return new Track()
            {
                TrackId = -1,
                Title = Title,
                Artist = Artist,
                Album = Album,
                Year = Year,
                Length = Length
            };
        }
    }
}
