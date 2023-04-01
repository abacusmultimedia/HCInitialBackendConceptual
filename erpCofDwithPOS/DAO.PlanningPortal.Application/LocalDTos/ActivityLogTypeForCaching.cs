using System;
namespace DAO.PlanningPortal.Application.LocalDTos
{
    [Serializable]
    public class ActivityLogTypeForCaching
    {
        public int Id { get; set; }
        public string SystemKeyword { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Template { get; set; }
    }
}
