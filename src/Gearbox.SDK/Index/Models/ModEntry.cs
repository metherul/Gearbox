using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk.Index
{
    public class ModEntry : IModEntry
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public string FilesystemHash { get; set; }
        public List<IFileEntry> FileEntries { get; set; }
    }
}