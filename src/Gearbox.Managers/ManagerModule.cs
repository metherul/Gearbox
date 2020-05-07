using Gearbox.Managers.ModOrganizer;
using Autofac;

namespace Gearbox.Managers
{
    public class ManagerModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Manager>().As<IManager>();

            containerBuilder.RegisterType<ManagerReader>().As<IManagerReader>();
        }
    }
}