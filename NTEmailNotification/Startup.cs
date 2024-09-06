using EmailNotifications.DataLayer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(NTEmailNotifications.Startup))]

namespace NTEmailNotifications
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Retrieve the configuration
            var config = builder.GetContext().Configuration;

            // Bind SMTP settings from the configuration
            var smtpSettings = new SmtpSettings
            {
                Host = config.GetValue<string>("EmailSettings:MailServer"),
                Port = config.GetValue<int>("EmailSettings:MailPort"),
                Username = config.GetValue<string>("EmailSettings:EmailUsername"),
                Password = config.GetValue<string>("EmailSettings:Password"),
                Sender = config.GetValue<string>("EmailSettings:Sender"),
                SenderName = config.GetValue<string>("EmailSettings:SenderName")
            };

            // Register SmtpSettings for dependency injection
            builder.Services.AddSingleton(smtpSettings);

            // Register the DbContext with the connection string
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("SqlConnectionString environment variable is not set.");
            }

            builder.Services.AddDbContext<EmailNotificationsDBContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }
    }
}
