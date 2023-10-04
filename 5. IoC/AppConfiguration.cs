using AppRunEnvVar._2._Domain._2._4_Interfaces;
using AppRunEnvVar._3._Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppRunEnvVar._5._IoC;

public static class AppConfiguration
{
    public static ServiceProvider ConfigureServices()
    {
        ServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IProcessEnvVarService, ProcessEnvVarService>();
        serviceCollection.AddTransient<IStorageService,  StorageService>();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider;
    }
}
