using SystemHandle.AsyncFilesystem;
using SystemHandle.RegistryHandle;

namespace SystemHandle
{
    public interface ISystemHandle
    {
        IAsyncFilesystem AsyncFilesystem { get; set; }
        IRegistryHandle RegistryHandle { get; set; }
    }
}