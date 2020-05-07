using System.Collections.Generic;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Reader;

namespace Gearbox.Sdk.Index
{
    public class IndexRoot : IIndexRoot
    {
        public string ManagerPath { get; set; }
        public List<ModEntry> ModEntries { get; set; }
        public List<ArchiveEntry> ArchiveEntries { get; set; }
    }
}