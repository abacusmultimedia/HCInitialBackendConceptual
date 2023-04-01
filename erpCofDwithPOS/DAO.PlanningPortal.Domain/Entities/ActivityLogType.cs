using DAO.PlanningPortal.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Domain.Entities
{
    [Table(nameof(ActivityLogType))]
    public partial class ActivityLogType : IBaseEntity<int>
    { 
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar(250)")]
        public string SystemKeyword { get; set; }
        [Required, Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; } 
        public bool Enabled { get; set; }
        [Required, Column(TypeName = "nvarchar(1000)")]
        public string Template { get; set; }
        public int GroupId { get; set; } 
        public virtual ICollection<ActivityLog> AdminActivityLogs { get; set; }
    }
}
