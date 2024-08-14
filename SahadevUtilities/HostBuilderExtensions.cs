using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevUtilities
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            //For error user Log.LogError methods
            //For warning user Log.LogWarning methods
            //For information user Log.LogInformation methods
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            SerilogHostBuilderExtensions.UseSerilog(builder);
            return builder;
        }
    }
}
