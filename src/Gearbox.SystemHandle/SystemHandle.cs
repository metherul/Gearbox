using SystemHandle.AsyncFilesystem;
using SystemHandle.RegistryHandle;

namespace SystemHandle
{
    public class SystemHandle : ISystemHandle
    {
        public IAsyncFile AsyncFile { get; set; }
        public IAsyncDirectory AsyncDirectory { get; set; }
        public IRegistryHandle RegistryHandle { get; set; }

        public SystemHandle(IAsyncFile asyncFile, IAsyncDirectory asyncDirectory, IRegistryHandle registryHandle)
        {
            AsyncFile = asyncFile;
            AsyncDirectory = asyncDirectory;
            RegistryHandle = registryHandle;
        }
    }
}