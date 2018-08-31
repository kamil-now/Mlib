using Mlib.Domain.Infrastructure.Interfaces;
using System.IO;
using System.Linq;

namespace Mlib.Domain.Infrastructure
{
    public class Track : IDatabaseEntity
    {
        public int ID { get; set; }

        public string Title { get; }
        public string Artist { get; }
        public string Album { get; }
        public uint Year { get; }
        public long Length { get; }

        public Track(FileInfo mp3File)
        {
            TagLib.File taggedFile = TagLib.File.Create(mp3File.FullName);
            Title = taggedFile.Tag.Title;
            Artist = taggedFile.Tag.AlbumArtists.FirstOrDefault();
            Album = taggedFile.Tag.Album;
            Year = taggedFile.Tag.Year;
            Length = taggedFile.Length;
        }

    }
}
