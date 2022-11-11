using MicroFrontend.Api.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MicroFrontend.Api.IntegrationTest;

internal class MicroFrontendApplication : WebApplicationFactory<Program>
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
        
        builder.ConfigureServices((host, services) =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(host.Configuration.GetConnectionString("TestAppConnectionString"), 
                    optionBuilder => optionBuilder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        });
    }
}