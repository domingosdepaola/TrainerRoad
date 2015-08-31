using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Domain.Entity;
using Domain.Interfaces;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Factory
{
    public class Factory
    {

        static volatile Factory _instance;
        static object _lock = new object();

        static Factory()
        {
        }

        public static Factory Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Factory();
                        }
                    }

                return _instance;
            }
        }
        static IWindsorContainer container;

        public void InstallIoC()
        {
            container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.Register(Component.For<IBikeRepository>().ImplementedBy<BikeRepository>());
            container.Register(Component.For<IRepository<Line,int>>().ImplementedBy<LineRepository>());
            container.Register(Component.For<IRepository<OrderLine, int>>().ImplementedBy<OrderLineRepository>());
            container.Register(Component.For<IOrderRepository>().ImplementedBy<OrderRepository>());
            container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>());

            // this is to use xml in config files. For this exemple i am using mapping in code.
            // container.Install(Castle.Windsor.Installer.Configuration.FromAppConfig());

            container.Install();

        }
        public T Resolve<T>()
        {
            var consumer = container.Resolve<T>();
            return consumer;
        }
    }
}
