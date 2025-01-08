using CrmBackend.Database.Enums;

namespace CrmBackend.Api.Dtos;

public record UserRegisterDto(string Email, string Password, RolesEnum Role);

public record UserLoginDto(string Email, string Password);
