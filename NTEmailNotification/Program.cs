using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using EmailNotifications.DataLayer;

namespace NTEmailNotifications
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration["SqlConnectionString"];
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new ArgumentNullException(nameof(connectionString), "SqlConnectionString cannot be null or empty.");
                    }

                    services.AddDbContext<EmailNotificationsDBContext>(options =>
                        options.UseSqlServer(connectionString));
                });
    }
}
