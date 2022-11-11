using AutoMapper;
using MicroFrontend.Api.Common.Interfaces;
using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroFrontend.Api.Repositories;

public class UserService : IUserService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAsync(Predicate<User>? predicate = null)
    {
        IQueryable<User> queryable = _context.Users.AsNoTracking();
        if (predicate != null)
            queryable = queryable.Where(e => predicate(e));
        IEnumerable<UserDto> result = _mapper.Map<IEnumerable<UserDto>>(await queryable.ToListAsync());
        return result;
    }

    public async Task<UserDto?> GetByIdAsync(string id)
    {
        User? drink = await _context.Users.AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
        if (drink == null) return null;
        return _mapper.Map<UserDto>(drink);
    }

    public async Task<string> UpsertAsync(UserDto dto)
    {
        User? entity;
        if (dto.Id == null)
        {
            entity = _mapper.Map<User>(dto);
            entity.Id = Guid.NewGuid().ToString(); 
            _context.Users.Add(entity);
        }
        else
        {
            entity = await _context.Users.FindAsync(dto.Id);
            _mapper.Map(dto, entity);
        }
        
        await _context.SaveChangesAsync();
        return entity!.Id;
    }

    public Task<string> DeleteAsync(UserDto dto)
    {
        if (dto.Id == null)
            throw new ArgumentNullException();
        return DeleteAsync(dto.Id);
    }

    public async Task<string> DeleteAsync(string id)
    {
        User? entity = await _context.Users.SingleOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            throw new KeyNotFoundException();
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return id;
    }
}