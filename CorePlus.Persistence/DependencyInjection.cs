using CorePlus.Report.Application.Interfaces;
using CorePlus.Report.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Report.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IHostEnvironment currentEnvironment)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ConnectionStrings connectionStrings =
               serviceProvider.GetService(typeof(ConnectionStrings))
               as ConnectionStrings;

            if (currentEnvironment.IsEnvironment("Test"))
            {
                services.AddDbContext<CorePlusReportDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            }
            else
            {
                services.AddDbContext<CorePlusReportDbContext>(options =>
             options.UseSqlServer(connectionStrings.DatabaseConnectionString));
            }

            services.AddScoped<ICorePlusReportDbContext>(provider => provider.GetService<CorePlusReportDbContext>());
            return services;
        }
    }
}
