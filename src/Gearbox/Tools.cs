using SystemHandle;
using Gearbox.Managers;
using Gearbox.Sdk;

namespace Gearbox
{
    public class Tools : ITools
    {
        public IManager Manager { get; }
        public ISdk Sdk { get; }
        public ISystemHandle SystemHandle { get; }

        public Tools(IManager manager, ISdk sdk, ISystemHandle systemHandle)
        {
            Manager = manager;
            Sdk = sdk;
            SystemHandle = systemHandle;
        }
    }
}
