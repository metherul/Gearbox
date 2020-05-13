using SystemHandle.AsyncFilesystem;
using SystemHandle.RegistryHandle;

namespace SystemHandle
{
    public interface ISystemHandle
    {
        IAsyncFile AsyncFile { get; set; }
        IAsyncDirectory AsyncDirectory { get; set; }
        IRegistryHandle RegistryHandle { get; set; }
    }
}