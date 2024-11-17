using AutoMapper;

using CrmBackend.Api.Dtos;
using CrmBackend.Database.Models;

namespace CrmBackend.Api.Services;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Account, OneAccountDto>();
        CreateMap<CreateAccountDto, Account>();
    }
}
