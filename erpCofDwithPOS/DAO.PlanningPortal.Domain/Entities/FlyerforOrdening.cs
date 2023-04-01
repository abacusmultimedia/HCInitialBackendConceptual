using DAO.PlanningPortal.Domain.Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(AlternativeServiceWorkersforOrdeningGroup))]
    public class AlternativeServiceWorkersforOrdeningGroup : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int OrdeningGroupId { get; set; }
        public int ServiceWorkerId { get; set; }

        [ForeignKey("OrdeningGroupId")]
        public OrdeningGroup OrdeningGroup { get; set; }

        [ForeignKey("ServiceWorkerId")]
        public ServiceWorker ServiceWorker { get; set; }
    }
}