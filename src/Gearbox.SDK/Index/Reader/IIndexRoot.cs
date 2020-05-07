using System.Collections.Generic;
using Gearbox.Sdk.Index.Models;

namespace Gearbox.Sdk.Index.Reader
{
    public interface IIndexRoot
    {
        string ManagerPath { get; set; }
        List<ModEntry> ModEntries { get; set; }
        List<ArchiveEntry> ArchiveEntries { get; set; }
    }
}