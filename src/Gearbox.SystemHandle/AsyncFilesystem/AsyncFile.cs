using System.IO.Abstractions;

namespace SystemHandle.AsyncFilesystem
{
    public class AsyncFile : FileWrapper, IAsyncFile
    {
        public AsyncFile(IFileSystem fileSystem) : base(fileSystem)
        {
        }
    }
}