using System;
using System.Collections.Generic;

namespace Gearbox.Sdk.Index.Models
{
    public class ArchiveEntry : IArchiveEntry
    {
       public string Name { get; set; }
       public string ArchivePath { get; set; }
       public long Length { get; set; }
       public DateTime LastModified { get; set; }
       public string FilesystemHash { get; set; }
       public string Hash { get; set; }
       public List<IFileEntry> FileEntries { get; set; } 
    }
}