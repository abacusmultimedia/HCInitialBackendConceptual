using System.Collections.Generic;

namespace zero.Shared.Models.User
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int? TenantId { get; set; }
        public int? OrganizationUnitId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public List<ApplicationRoleDto> UserRoles { get; set; }
        public List<TenantDto> UserAreas { get; set; }
        //public List<OrganizationalUnitDTO> OrganizationalUnits { get; set; }
        public List<OrganizationalUnitsWrtTenants> OrganizationalUnitsWrtTenant { get; set; }
    }
}