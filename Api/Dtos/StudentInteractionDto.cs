﻿namespace CrmBackend.Api.Dtos;

public record AddStudentTestResultDto(
    int StudentUserId,
    int ActivityId,
    double Score
);

public record JoinChatDto(int ActivityId);