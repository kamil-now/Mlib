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
        [Key]
        [Required]
        public int TrackId { get; set; }
        [Required]
        public string FullPath { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public uint Year { get; set; }
        public long Length { get; set; }
       
        [NotMapped]
        public bool IsCopy => TrackId == -1;

        public Track()
        {
            Playlists = new List<Playlist>();
        }
        public Track(FileInfo mp3File)
        {
            TagLib.File taggedFile = TagLib.File.Create(mp3File.FullName);
            Title = taggedFile.Tag.Title;
            Artist = taggedFile.Tag.AlbumArtists.FirstOrDefault();
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
