using Microsoft.Practices.Unity;
using MyApp.ServiceContracts.Services;
using MyApp.Services;

namespace MyApp.DependencyResolution
{
    public class UnityContainerFactory
    {
        public IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<INumberConverterService, NumberConverterService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISentenceConverterService, SentenceConverterService>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
