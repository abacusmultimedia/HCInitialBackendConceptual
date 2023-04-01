namespace DAO.PlanningPortal.Domain.Enums;

public enum ServiceWorkerStatusEnum : byte
{
    NotSignedIn = 1,
    SignedIn = 2,
    LoggedOut = 3,
    Unavailabe = 4,
    Blocked = 5
}