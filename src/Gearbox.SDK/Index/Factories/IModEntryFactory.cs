using System.Threading.Tasks;

namespace Gearbox.Sdk.Index.Factories
{
    public interface IModEntryFactory
    {
        Task<IModEntry> Create(string modDir);
    }
}