using FlexateWebApi.Domain.Interfaces;
using FlexateWebApi.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPeopleRepository, PeopleRepository>();

            return services;
        }

    }
}
