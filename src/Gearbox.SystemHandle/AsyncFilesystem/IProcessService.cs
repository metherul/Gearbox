using System.Collections.Generic;

namespace SystemHandle.AsyncFilesystem
{
    public interface IProcessService
    {
        IAsyncEnumerable<string> RunAndYieldOutput(string fileName, string args);
    }
}