using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FlexateWebApi.Application
{
    public static class DependencyInjection
    {    
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<ICarsService, CarsService>();
            services.AddTransient<IOfficesService, OfficesService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
