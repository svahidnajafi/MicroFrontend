using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Domain.Entities;

namespace MicroFrontend.Api.Common.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAsync(Predicate<User>? predicate = null);
    Task<UserDto?> GetByIdAsync(string id);
    Task<string> UpsertAsync(UserDto dto);
    Task<string> DeleteAsync(UserDto dto);
    Task<string> DeleteAsync(string id);
}