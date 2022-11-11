using MicroFrontend.Api.Common.Interfaces;
using MicroFrontend.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroFrontend.Api.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    
    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
}