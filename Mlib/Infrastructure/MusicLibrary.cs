using Microsoft.Win32;
using Mlib.Data;
using Mlib.Data.Models;
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

                    if (fileInfo.Extension == ".m3u")
                        unitOfWork.AddOrUpdate(new Playlist(fileInfo), true);
                    else
                        unitOfWork.AddOrUpdate(new Track(fileInfo), true);
                }
            }
        }
        string[] ShowDialog(string filter, string defaultExtension)
        {
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
