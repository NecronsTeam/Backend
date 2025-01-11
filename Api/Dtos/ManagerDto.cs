using CrmBackend.Database.Enums;

namespace CrmBackend.Api.Dtos;

public record StudentsAppliedOnActivityDto(List<AppliedStudentDto> AppliedStudents);

public record AppliedStudentDto(int StudentAccountId, string FullName, ActivityStatus Status, string AvatarLink);
