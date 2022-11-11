using Microsoft.Extensions.DependencyInjection;

namespace MicroFrontend.Api.IntegrationTest;

using static Testing;

[TestFixture]
public abstract class TestBase
{
    protected HttpClient HttpClient = null!;
    protected IServiceScope Scope = null!;
    
    [SetUp]
    public async Task BaseSetUp()
    {
        await ResetStateAsync();
        await SeedDefaultDataAsync();
        HttpClient = GetHttpClient();
        Scope = GetScopeFactory().CreateScope();
    }
    
    protected IServiceScopeFactory GetScopeFactory() => ScopeFactory;
    
    protected HttpClient GetHttpClient() => GetClient();
}