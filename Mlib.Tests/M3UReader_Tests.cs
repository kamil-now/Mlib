using Mlib.Data.Models;
using Mlib.Infrastructure;
using System;
using System.IO;
using Xunit;

namespace Mlib.Tests
{
    public class M3UReader_Tests
    {
        [Fact]
        public void InvalidExtension()
        {
            string filePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".mp3";
            var fileInfo = new FileInfo(filePath);
            var result = M3UReader.GetFiles(fileInfo);

            Assert.Empty(result);
        }
        [Fact]
        public void NotExisitingFile()
        {
            string filePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".m3u";
            Assert.False(File.Exists(filePath));
            var fileInfo = new FileInfo(filePath);
            var result = M3UReader.GetFiles(fileInfo);

            Assert.Empty(result);
        }
        [Fact]
        public void EmptyFile()
        {
            string filePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".m3u";

            Assert.False(File.Exists(filePath));
            File.Create(filePath).Close();
            Assert.True(File.Exists(filePath));

            var fileInfo = new FileInfo(filePath);
            var result = M3UReader.GetFiles(fileInfo);

            Assert.Empty(result);
        }
    }
}
