using System;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public interface IArchiveHandle
    {
        event EventHandler<string> FileExtractedEvent;
        
        Task Extract(string directory, string archivePath);
    }
}