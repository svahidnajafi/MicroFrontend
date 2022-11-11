using MicroFrontend.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroFrontend.Api.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync();
}