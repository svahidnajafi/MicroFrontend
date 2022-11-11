using MicroFrontend.Api.Common.Interfaces;
using MicroFrontend.Api.Domain.Entities;
using MicroFrontend.Api.IntegrationTest.Mocks;
using MicroFrontend.Api.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;

namespace MicroFrontend.Api.IntegrationTest;

[SetUpFixture]
public class Testing
{
    private static MicroFrontendApplication _application = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static IConfiguration _configuration = null!;
    private static Checkpoint _checkpoint = null!;
    
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _application = new MicroFrontendApplication();
        _scopeFactory = _application.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _application.Services.GetRequiredService<IConfiguration>();
        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
        };
    }
    
    public static HttpClient GetClient() => _application.CreateClient();
    
    public static IServiceScopeFactory ScopeFactory => _scopeFactory;
    
    public static async Task ResetStateAsync()
    {
        await _checkpoint.Reset(_configuration.GetConnectionString("TestAppConnectionString"));
    }
    
    public static async Task SeedDefaultDataAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.SeedAsync(async (context) =>
        {
            foreach (User user in UsersMock.Users)
                await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        });
    }
}