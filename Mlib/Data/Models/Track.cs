using Mlib.Interfaces;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mlib.Data.Models
{
    public class Track : IDatabaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ManyToMany(typeof(PlaylistData))]
        List<Playlist> Playlists { get; set; }

        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public uint Year { get; private set; }
        public long Length { get; private set; }
        public string FullPath { get; private set; }
        [Ignore]
        public bool IsCopy => ID == -1;

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
        public Track() { }
        public Track Copy()
        {
            return new Track()
            {
                ID = -1,
                Title = Title,
                Artist = Artist,
                Album = Album,
                Year = Year,
                Length = Length
            };
        }
    }
}
