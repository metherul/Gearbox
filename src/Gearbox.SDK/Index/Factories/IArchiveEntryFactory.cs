using System.Threading.Tasks;
using Gearbox.Sdk.Index.Models;

namespace Gearbox.Sdk.Index.Factories
{
    public interface IArchiveEntryFactory
    {
        Task<IArchiveEntry> Create(string archivePath);
    }
}