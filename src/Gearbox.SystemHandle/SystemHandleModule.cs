using System.IO.Abstractions;
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
            containerBuilder.RegisterType<FileSystem>().As<IFileSystem>();

            containerBuilder.RegisterType<AsyncDirectory>().As<IAsyncDirectory>();
            containerBuilder.RegisterType<AsyncFile>().As<IAsyncFile>();
            containerBuilder.RegisterType<RegistryHandle.RegistryHandle>().As<IRegistryHandle>();
            containerBuilder.RegisterType<AsyncArchive>().As<IAsyncArchive>();
        }
    }
}