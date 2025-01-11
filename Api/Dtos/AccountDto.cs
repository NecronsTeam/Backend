using CrmBackend.Database.Models;
﻿using CrmBackend.Api.Helpers;

namespace CrmBackend.Api.Dtos;

public record OneAccountDto(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? PhoneNumber,
    string? TelegramLink,
    string? AvatarLink
);


public record CreateAccountDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    string? PhoneNumber,
    string? TelegramLink
);


public record PatchAccountDto
(
    string? FirstName,
    string? LastName,
    string? MiddleName,
    string? PhoneNumber,
    string? TelegramLink
) : BasePatchDto<Account>;


public record AttachAvatarDto(
    [OnlyPhoto]
    IFormFile Avatar
);