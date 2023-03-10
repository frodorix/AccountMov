using CORE.Account.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Extensions
{
    public static class ConfigureInfrastructure
    {

        public static IServiceCollection UseInfrastructurePersistence(this IServiceCollection services, IConfiguration config )
        {

            services.AddDbContext<MyContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("AccountsDB"));
            });

            services.AddTransient<IClientesRepository, ClientesRepository>();
            services.AddTransient<ICuentasRepository, CuentasRepository>();
            services.AddTransient<IMovimientosRepository, MovimientosRepository>();


            return services;
        }
    }
}
