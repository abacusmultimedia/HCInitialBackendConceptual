using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.LocalDTos
{
    [Serializable]
    public class OrganizationalUnitTypeForCaching
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsSelected { get; set; }
    }
}
