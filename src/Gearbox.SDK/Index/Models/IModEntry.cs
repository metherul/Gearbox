using System.Collections.Generic;
using Gearbox.Sdk.Index.Models;

namespace Gearbox.Sdk.Index
{
    public interface IModEntry
    {
        string Name { get; set; }
        string Directory { get; set; }
        string FilesystemHash { get; set; }
        List<IFileEntry> FileEntries { get; set; }
    }
}