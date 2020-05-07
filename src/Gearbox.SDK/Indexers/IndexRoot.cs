using System.Collections.Generic;

namespace Gearbox.Sdk.Indexers
{
    public class IndexRoot
    {
        public string ModOrganizerPath { get; set; }
        public List<ModEntry> ModEntries { get; set; }
        public List<ArchiveEntry> ArchiveEntries { get; set; }
    }
}
