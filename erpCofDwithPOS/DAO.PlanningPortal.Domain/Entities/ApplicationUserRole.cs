using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace DAO.PlanningPortal.Domain.Entities;

[DisplayName("UserRoles")]
public class ApplicationUserRole : IdentityUserRole<int>
{
}