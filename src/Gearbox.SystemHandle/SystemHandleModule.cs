using SystemHandle.AsyncFilesystem;
using SystemHandle.RegistryHandle;
using Autofac;

namespace SystemHandle
{
    public class SystemHandleModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SystemHandle>().As<ISystemHandle>();

            containerBuilder.RegisterType<AsyncFilesystem.AsyncFilesystem>().As<IAsyncFilesystem>();
            containerBuilder.RegisterType<AsyncDirectory>().As<IAsyncDirectory>();
            containerBuilder.RegisterType<RegistryHandle.RegistryHandle>().As<IRegistryHandle>();
            containerBuilder.RegisterType<ArchiveHandle>().As<IArchiveHandle>();
        }
    }
}