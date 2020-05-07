using System;

namespace Gearbox.Sdk.Index.Models
{
    public class FileEntry : IFileEntry
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public string Hash { get; set; }
        public long Length { get; set; }
    }
}