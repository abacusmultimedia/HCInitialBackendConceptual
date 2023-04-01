using System.Collections.Generic;

namespace zero.Shared.Models
{
    public class OrganizationalUnitsWrtTenantsDTO
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public bool IsSelected { get; set; }
        public List<OrganizationalUnitDTO> OrganizationalUnits { get; set; }
    }
}
