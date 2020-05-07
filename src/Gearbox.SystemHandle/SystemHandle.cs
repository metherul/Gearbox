using SystemHandle.AsyncFilesystem;
using SystemHandle.RegistryHandle;

namespace SystemHandle
{
    public class SystemHandle : ISystemHandle
    {
        public IAsyncFilesystem AsyncFilesystem { get; set; }
        public IRegistryHandle RegistryHandle { get; set; }

        public SystemHandle(IAsyncFilesystem asyncFilesystem, IRegistryHandle registryHandle)
        {
            AsyncFilesystem = asyncFilesystem;
            RegistryHandle = registryHandle;
        }
    }
}