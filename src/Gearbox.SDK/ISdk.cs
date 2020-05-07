using Gearbox.Sdk.Compiler;
using Gearbox.Sdk.Index;

namespace Gearbox.Sdk
{
    public interface ISdk
    {
        IPackCompiler PackCompiler { get; }
        IIndexer Indexer { get; }
    }
}
