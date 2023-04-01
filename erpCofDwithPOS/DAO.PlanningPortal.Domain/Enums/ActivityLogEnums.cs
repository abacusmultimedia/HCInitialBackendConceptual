 
namespace DAO.PlanningPortal.Domain.Enums
{
    public enum ActivityLogTypeEnum
    {

        NewOrderCreated = 2,
        OrderMovedToInPreparationStatus = 3,
        OrderMovedToDispatchedToBKStatus = 4,
        OrderMovedToReceivedByBKStatus = 5,
        OrderMovedToDispatchedToDistributorStatus = 6,
        OrderCompleted = 7,
        OrderCancelled = 8

    }

}
