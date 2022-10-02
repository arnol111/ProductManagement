using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Infrastructure.Persistence;
using WebApi;

namespace ApplicationCoreTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((builder, services) =>
            {

                services
                    .AddDbContext<ApplicationDbContext>((sp, options) =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            });
        }
    }
}
