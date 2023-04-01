using System.Collections.Generic;

namespace zero.Shared.Models.User
{
    public class OrganizationalUnitsWrtTenants
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public List<OrganizationalUnits> OrganizationalUnit { get; set; }
    }
}
