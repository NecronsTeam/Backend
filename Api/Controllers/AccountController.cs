using AutoMapper;

using CrmBackend.Api.Dtos;
using CrmBackend.Api.Helpers;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("account")]
[Authorize]
public class AccountController(IMapper mapper, UserRepository userRepository, AccountRepository accountRepository) : Controller
{
    [HttpGet]
    public async Task<OneAccountDto> GetUserAccountAsync()
    {
        var user = await HttpContext.User.GetUser(userRepository);
        if (user.Account is null)
            throw new InvalidOperationException("У пользователя не создан аккаунт");

        return mapper.Map<OneAccountDto>(user.Account);
    }

    [HttpPost]
    public async Task CreateAccountForUser([FromBody] CreateAccountDto accountDto)
    {
        var user = await HttpContext.User.GetUser(userRepository);
        if (user.Account is not null)
            throw new InvalidOperationException("У пользователя уже создан аккаунт");

        var account = mapper.Map<Account>(accountDto);
        await userRepository.AddAccountToUserEntity(user, account);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<OneAccountDto> GetOtherPersonAccount([FromRoute] int id)
    {
        var account = await accountRepository.GetEntityByIdAsync(id);
        return mapper.Map<OneAccountDto>(account);
    }
}
