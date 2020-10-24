using ForElva.DAL.Interfaces;
using ForElva.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ForElva.DAL.DependencyInjections
{    public static class DataDependencyRegistrator
    {
        public static void RegisterDataRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository, OSMRepository>();
        }
    }
}