namespace CrmBackend.Database.Enums;

public enum ActivityStatus
{
    Registered = 0,
    WaitingTest = 1,
    PassedTest = 2,
    Rejected = 3,
    WaitingToJoinChat = 4,
    JoinedChat = 5,
    PassingActivity = 6,
    ActivityFinished = 7,
}
