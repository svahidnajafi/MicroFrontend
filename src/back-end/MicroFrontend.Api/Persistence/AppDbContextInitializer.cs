using Microsoft.EntityFrameworkCore;

namespace MicroFrontend.Api.Persistence;

public class AppDbContextInitializer
{
    private readonly ILogger<AppDbContextInitializer> _logger;
    private readonly AppDbContext _context;

    public AppDbContextInitializer(ILogger<AppDbContextInitializer> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    
    public async Task SeedAsync(Func<AppDbContext, Task> seedDataAsync)
    {
        try
        {
            await seedDataAsync(_context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}