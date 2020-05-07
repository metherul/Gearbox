using SystemHandle;
using Gearbox.Managers;
using Gearbox.Sdk;

namespace Gearbox
{
    public interface ITools
    {
          IManager Manager { get; }
          ISdk Sdk { get; }
          ISystemHandle SystemHandle { get; }
    }
}
