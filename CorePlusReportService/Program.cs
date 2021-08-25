using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace CorePlusReportService
{
    public class Program
    {
        private static readonly string AppName = typeof(Program).Namespace;

        public static IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = CreateSerilogLogger(Configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);

                IWebHost host = BuildWebHost(args);

                Log.Information("Starting web host ({ApplicationContext})...", AppName);

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).
            ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
            {
                IWebHostEnvironment env = webHostBuilderContext.HostingEnvironment;
                configurationbuilder.SetBasePath(env.ContentRootPath);
                configurationbuilder.AddJsonFile("appsettings.json", false, true);
                configurationbuilder.AddEnvironmentVariables();
            })
            .UseStartup<Startup>()
            .UseSerilog()
            .Build();
    }
}
