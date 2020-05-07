using System;
using System.Collections.Generic;

namespace Gearbox.Sdk.Index.Models
{
    public interface IArchiveEntry
    {
        string Name { get; set; }
        string ArchivePath { get; set; }
        long Length { get; set; }
        DateTime LastModified { get; set; }
        string FilesystemHash { get; set; }
        string Hash { get; set; }
        List<IFileEntry> FileEntries { get; set; }
    }
}