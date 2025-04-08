using AutoMapper;
using LinkDev.IKEA.BLL.Profiles;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Data.DbInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentService, DepartmentService>();
            //services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddAutoMapper(P => P.AddProfile(new MappingProfiles()));
            return services;
        }
    }
}
