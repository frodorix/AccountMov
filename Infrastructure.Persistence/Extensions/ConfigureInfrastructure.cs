using AutoMapper;
using CORE.Account.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Mapping;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Extensions
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection UseInfrastructurePersistence(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("AccountsDB") 
                ?? throw new InvalidOperationException("Connection string 'AccountsDB' not found.");

            services.AddDbContext<MyContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            // Configure AutoMapper once as a singleton for better performance
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EntityToModelProfile>();
            });

            services.AddScoped<IClientesRepository, ClientesRepository>();
            services.AddScoped<ICuentasRepository, CuentasRepository>();
            services.AddScoped<IMovimientosRepository, MovimientosRepository>();

            return services;
        }
    }
}
