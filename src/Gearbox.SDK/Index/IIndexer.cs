using System.Threading.Tasks;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Reader;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk.Index
{
    public interface IIndexer
    {
        Task<IIndexReader> OpenRead(string indexDir);
        Task<IIndexWriter> OpenWrite(string indexDir);
        Task<IArchiveEntry> IndexArchive(string archivePath);
        Task<IModEntry> IndexMod(string modDir);
    }
}
