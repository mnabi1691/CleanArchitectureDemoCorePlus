using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigurationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>());

            return services;
        }
    }
}
