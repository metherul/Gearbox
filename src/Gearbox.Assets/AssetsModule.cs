using Autofac;

namespace Gearbox.Assets
{
    public class AssetsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AssetService>().As<IAssetService>();
        }
    }
}
