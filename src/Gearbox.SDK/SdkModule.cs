using Autofac;
using Gearbox.Sdk.Compiler;
using Gearbox.Sdk.Index;
using Gearbox.Sdk.Index.Factories;
using Gearbox.Sdk.Index.Reader;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk
{
    public class SdkModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Sdk>().As<ISdk>();

            containerBuilder.RegisterType<PackCompiler>().As<IPackCompiler>();
            containerBuilder.RegisterType<CompiledPack>().As<ICompiledPack>();

            containerBuilder.RegisterType<Indexer>().As<IIndexer>();
            containerBuilder.RegisterType<IndexReader>().As<IIndexReader>();
            containerBuilder.RegisterType<IndexWriter>().As<IIndexWriter>();
            containerBuilder.RegisterType<ArchiveEntryFactory>().As<IArchiveEntryFactory>();
            containerBuilder.RegisterType<ModEntryFactory>().As<IModEntryFactory>();
            containerBuilder.RegisterType<FileEntryFactory>().As<IFileEntryFactory>();
        }
    }
}
