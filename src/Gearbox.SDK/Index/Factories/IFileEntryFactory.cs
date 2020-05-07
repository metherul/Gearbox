using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Models;

namespace Gearbox.Sdk.Index.Writer
{
    public interface IFileEntryFactory
    {
        Task<IFileEntry> Create(string filePath);
        Task<IFileEntry> Create(string filePath, string relativeTo);
        Task<IFileEntry> Create(string filePath, string relativeTo, FileHashType fileHashType);
    }
}