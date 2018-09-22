using System;
using System.Configuration;
using System.IO;

namespace Mlib.Infrastructure

{
    public static class ConnectionStrings
    {
        public static readonly string ProgramData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Mlib");
        public static readonly string Log = Path.Combine(ProgramData, "log.txt");
        public static readonly string DatabaseFilePath = Path.Combine(ProgramData, "data.db3");
    }
}
