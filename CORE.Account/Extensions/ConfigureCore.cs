using CORE.Account.Application;
using CORE.Account.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Extensions
{
    public static class ConfigureCore
    {
        public static IServiceCollection UseCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IClientesService, ClientesService>();
            services.AddTransient<ICuentasService, CuentasService>();
            
            return services;
        }
    }
}
