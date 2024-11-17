namespace CrmBackend.Api.Dtos;

public record OneAccountDto(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? PhoneNumber,
    string? TelegramLink
);

public record CreateAccountDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    string? PhoneNumber,
    string? TelegramLink
);