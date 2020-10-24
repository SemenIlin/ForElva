using ForElva.BLL.Interfaces;
using ForElva.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
namespace ForElva.BLL.DependencyInjections
{
    public static class BussinessDependencyRegistrator
    {
        public static void RegisterBussinessRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IService, OSMSevice>();
        }
    }
}
