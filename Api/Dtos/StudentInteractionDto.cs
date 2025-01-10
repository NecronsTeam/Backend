namespace CrmBackend.Api.Dtos;

public record AddStudentTestResultDto(
    int StudentUserId,
    int ActivityId,
    double Score,
    bool IsStudentPassedTest
);

public record JoinChatDto(int ActivityId);