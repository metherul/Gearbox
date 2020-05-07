using System.Threading.Tasks;

namespace Gearbox.Sdk.Compiler
{
    public interface ICompiledPack
    {
        Task Publish();
    }
}