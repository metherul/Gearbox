using System;

namespace Gearbox.Sdk.Index.Models
{
    public interface IFileEntry
    {
        string Name { get; set; }
        string FilePath { get; set; }
        DateTime LastWriteTimeUtc { get; set; }
        string Hash { get; set; }
        long Length { get; set; }
    }
}