using Microsoft.Win32;
using Mlib.Data;
using Mlib.Data.Models;
using Mlib.Extensions;
using Mlib.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Infrastructure
{
    public class MusicLibrary
    {
        UnitOfWork unitOfWork;
        public MusicLibrary(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddMusicFiles()
        {
            var defaultExt = ".m3u";
            var filter = "M3U files (*.m3u)|*.m3u|MP3 files (*.mp3)|*.mp3";
            var fileNames = ShowDialog(filter, defaultExt);

            if (fileNames != null)
            {
                foreach (var filename in fileNames)
                {
                    var fileInfo = new FileInfo(filename);
                    var tracks = M3UReader.GetFiles(fileInfo)?.Select(n => new Track(n))?.ToList();
                    var playlistName = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));

                    if (fileInfo.Extension == ".m3u")
                    {
                        var playlist = new Playlist(playlistName);

                        unitOfWork.AddOrUpdate(playlist, true);

                        uint x = 0;
                        var playlistTracks = tracks.Select(n => new PlaylistTrack(playlist, n, x++));
                        playlistTracks.ForEach(n=>
                        {
                            unitOfWork.AddOrUpdate(n, true);
                        });
                    }
                    tracks.ForEach(n => unitOfWork.AddOrUpdate(n, true));
                }
            }
        }
        string[] ShowDialog(string filter, string defaultExtension)
        {
            //TODO folders selection
            OpenFileDialog dlg = new OpenFileDialog();
            var directory = Settings.Default.AddNewLastDirectory;
            dlg.InitialDirectory = string.IsNullOrEmpty(directory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) : directory;

            dlg.Multiselect = true;
            dlg.ReadOnlyChecked = true;

            var result = dlg.ShowDialog();

            if ((bool)result)
            {
                Settings.Default.AddNewLastDirectory = new FileInfo(dlg.FileNames?.First())?.Directory?.FullName ?? "";
            }

            return dlg.FileNames;
        }
    }
}
