using System;
using System.Linq;
using System.Reflection;
using EstimationApp.Services;
using EstimationApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace EstimationApp.IoC
{
    public static class IocUtil
    {
        private static ServiceCollection _container = new ServiceCollection();
        private static ServiceProvider _serviceProvider;

        public static void BuildIocContainer()
        {
            RegisterCommonDependencies();
            _serviceProvider = _container.BuildServiceProvider();
        }

        public static T Resolve<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        private static void RegisterCommonDependencies()
        {
            RegisterAssemblyTypes(typeof(App).Assembly, "Page", typeof(ContentPage), ServiceLifetime.Transient);
            RegisterAssemblyTypes(typeof(App).Assembly, "ViewModel", typeof(BasePageViewModel), ServiceLifetime.Transient);
            RegisterAssemblyTypes(typeof(App).Assembly, "Service", ServiceLifetime.Singleton);
            RegisterAssemblyTypes(typeof(App).Assembly, "DataAccess", ServiceLifetime.Singleton);
        }

        private static void RegisterAssemblyTypes(Assembly asm, string typeEndsWith, Type baseType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var types = from tp in asm.GetTypes()
                        where tp.IsClass && tp.Name.EndsWith(typeEndsWith, StringComparison.Ordinal) && tp.IsSubclassOf(baseType) && !tp.IsAbstract
                        select tp;

            foreach (var itemType in types)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        _container.AddTransient(Type.GetType(itemType.FullName));
                        break;
                    case ServiceLifetime.Scoped:
                        _container.AddScoped(Type.GetType(itemType.FullName));
                        break;
                    default:
                        _container.AddSingleton(Type.GetType(itemType.FullName));
                        break;
                }
            }
        }

        private static void RegisterAssemblyTypes(Assembly asm, string typeEndsWith, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var types = from tm in asm.GetTypes()
                        where tm.IsClass && tm.Name.EndsWith(typeEndsWith, StringComparison.Ordinal)
                        select tm;
            foreach (var itemType in types)
            {
                var itemInterface = itemType.FindInterfaces(WfmInterfaceFilter, itemType.Name).FirstOrDefault();
                if (itemInterface == null) continue;

                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        _container.AddTransient(Type.GetType(itemInterface.FullName), Type.GetType(itemType.FullName));
                        break;
                    case ServiceLifetime.Scoped:
                        _container.AddScoped(Type.GetType(itemInterface.FullName), Type.GetType(itemType.FullName));
                        break;
                    default:
                        _container.AddSingleton(Type.GetType(itemInterface.FullName), Type.GetType(itemType.FullName));
                        break;
                }
            }
        }

        private static bool WfmInterfaceFilter(Type type, object filterCriteria)
        {
            return type.ToString().EndsWith("I" + filterCriteria, StringComparison.OrdinalIgnoreCase);
        }
    }
}
