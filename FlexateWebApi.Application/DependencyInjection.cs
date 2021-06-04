using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Application
{
    public static class DependencyInjection
    {    
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();

            return services;
        }
    }
}
