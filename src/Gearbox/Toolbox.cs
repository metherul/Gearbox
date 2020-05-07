using SystemHandle;
using Autofac;
using Gearbox.Managers;
using Gearbox.Sdk;

namespace Gearbox
{
    public static class Toolbox
    {
        private static IContainer _container;
        
        private static ITools _toolsInstance;
        private static bool _hasConstructed;

        public static ITools Open()
        {
            if (_hasConstructed)
            {
                return _toolsInstance;
            }

            // Construct Gearbox dependencies.
            var container = new ContainerBuilder();
            container.RegisterModule(new SdkModule());
            container.RegisterModule(new ManagerModule());
            container.RegisterModule(new SystemHandleModule());

            container.RegisterType<Tools>().As<ITools>();

            _container = container.Build();
            
            _hasConstructed = true;
            _toolsInstance = _container.Resolve<ITools>();
                
            return _toolsInstance;
        }
    }
}