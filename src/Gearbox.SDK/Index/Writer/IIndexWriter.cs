using System.Threading.Tasks;
using Gearbox.Sdk.Index.Models;

namespace Gearbox.Sdk.Index.Writer
{
    public interface IIndexWriter
    {
        Task Load(string indexDir);
        void Push(IModEntry indexedMod);
        void Push(IArchiveEntry indexedArchive);
        Task Flush();
    }
}