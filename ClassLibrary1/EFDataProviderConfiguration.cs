using Infrastructure.Repositories;
using SimpleInjector;

namespace EFDataProvider
{
    public static class EFDataProviderConfiguration
    {
        public static void Initialize(Container container)
        {

            //var assembly = typeof(EFDataProviderConfiguration).Assembly;
            container.Register(typeof(IRepository<>), typeof(EFRepository<>), Lifestyle.Scoped);
        }
    }
}
