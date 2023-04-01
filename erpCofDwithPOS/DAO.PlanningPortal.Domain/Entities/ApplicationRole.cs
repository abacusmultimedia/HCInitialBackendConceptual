using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace DAO.PlanningPortal.Domain.Entities;

[DisplayName("Roles")]
public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}