using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApplication.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
               IConfiguration configuration)
        {
            services.AddDbContext<TruckApplicationContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TruckApplicationContext"),
                    b => b.MigrationsAssembly(typeof(TruckApplicationContext)
                            .Assembly.FullName)));
            //services.AddDatabaseDeveloperPageExceptionFilter();
            return services;
        }
    }
}
