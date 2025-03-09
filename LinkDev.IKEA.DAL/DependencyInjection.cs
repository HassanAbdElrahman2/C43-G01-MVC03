using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
               // contextLifetime: ServiceLifetime.Scoped,optionsLifetime: ServiceLifetime.Scoped,
               (optionsBuilder) => {
                   optionsBuilder.UseSqlServer(connectionString: configuration.GetConnectionString("DefultConnection"));
               }
              


              );
            return services;
        }
    }
}
