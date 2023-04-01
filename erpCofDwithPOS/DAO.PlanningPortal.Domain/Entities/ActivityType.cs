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
    [Table(nameof(ActivityType))]
    public class ActivityType : IBaseEntity<int>
    {
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

    }
}
