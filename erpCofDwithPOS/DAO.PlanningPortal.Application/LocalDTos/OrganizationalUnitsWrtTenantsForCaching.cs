using zero.Shared.Models;
using System;
using System.Collections.Generic;

namespace DAO.PlanningPortal.Application.LocalDTos
{
    [Serializable]
    public class OrganizationalUnitsWrtTenantsForCaching
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public bool IsSelected { get; set; }
        public List<OrganizationalUnitTypeForCaching> OrganizationalUnits { get; set; }
    }
}
