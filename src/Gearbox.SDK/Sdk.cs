using Gearbox.Sdk.Compiler;
using Gearbox.Sdk.Index;

namespace Gearbox.Sdk
{
    public class Sdk : ISdk
    {
        public IPackCompiler PackCompiler { get; }
        public IIndexer Indexer { get; }

        public Sdk(IPackCompiler packCompiler, IIndexer indexer)
        {
            PackCompiler = packCompiler;
            Indexer = indexer;
        }
    }
}
