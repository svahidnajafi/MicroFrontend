using AutoMapper;
using MicroFrontend.Api.Common.Models;
using MicroFrontend.Api.Domain.Entities;

namespace MicroFrontend.Api.Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}