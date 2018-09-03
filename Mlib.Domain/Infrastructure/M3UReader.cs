using System.Collections.Generic;
using System.IO;

namespace Mlib.Domain.Infrastructure
{
    public static class M3UReader
    {
        public static IEnumerable<FileInfo> GetFiles(FileInfo file)
        {
            if (file.Extension == ".m3u")
            {
                string line;
                FileInfo retval = null;
                using (var stream = file.OpenRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            retval = GetFile(line);
                            if (retval != null)
                                yield return retval;
                            else
                                continue;
                        }
                    }
                }
            }

        }
        static FileInfo GetFile(string path)
        {
            var retval = new FileInfo(path);
           
            if (!retval.Exists)
                retval = FindMatchingFile(path);

            return retval ;
        }
        static FileInfo FindMatchingFile(string path)
        {
            var parentDirectoryFile = GetAbsolutePathFromLibrary(path);
            var rootDirectoryFile = FindOnAnotherDrive(path);

            return parentDirectoryFile ?? rootDirectoryFile;
        }

        static FileInfo FindOnAnotherDrive(string path)
        {
            FileInfo retval = null;
            foreach (var drive in DriveInfo.GetDrives())
            {
                var fullPath = drive.Name.Remove(drive.Name.Length-1) + path;
                retval = new FileInfo(fullPath);
                if (retval.Exists)
                    return retval;
            }
            return null;
        }

        static FileInfo GetAbsolutePathFromLibrary(string path)
        {
            //TODO
            return null;
        }
    }
}
