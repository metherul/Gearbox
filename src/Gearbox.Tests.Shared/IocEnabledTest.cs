using System;
using Autofac;
using Autofac.Core;

namespace Gearbox.Tests.Shared
{
    public class IocEnabledTest<T> where T : IModule, new()
    {
        private readonly IContainer _container;

        public IocEnabledTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new T());

            _container = builder.Build();
        }

        protected T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        protected void Shutdown()
        {
            _container.Dispose();
        }
    }
}