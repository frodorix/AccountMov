using CORE.Account.Application;
using CORE.Account.Application.Interfaces;
using CORE.Account.Application.Services;
using CORE.Account.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace CORE.Account.Extensions
{
    public static class ConfigureCore
    {
        public static IServiceCollection UseCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IClientesService, ClientesService>();
            services.AddScoped<ICuentasService, CuentasService>();
            services.AddScoped<IMovimientosService, MovimientosService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
            
            return services;
        }
    }
}
