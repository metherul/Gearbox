using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public interface IAsyncArchive
    {
        Task Extract(string directory, string archivePath);

        IAsyncEnumerable<string> ExtractAndYieldOutput(string directory, string archivePath);
    }
}