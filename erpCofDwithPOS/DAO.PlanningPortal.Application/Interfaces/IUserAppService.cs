using zero.Shared.Models;
using zero.Shared.Models.User;
using zero.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAO.PlanningPortal.Domain.Entities;
using zero.Shared.Models.GDPRAccess;

namespace DAO.PlanningPortal.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<Result<PaginatedList<ApplicationUserDto>>> GetAllUsers(UserSearchDto request);

        Task<Result<ApplicationUserDto>> GetUserById(int id);

        Task<Result<string>> CreateUser(CreateUserRequestDto request);

        Task<Result<string>> UpdateUser(UpdateRequestUserDto request);

        Task<Result<string>> DeleteUser(int id);

        Task<Result<string>> ToggleActiveStatus(int id);

        Task<Result<List<ApplicationRoleDto>>> GetAllRoles();
        Task<Result<List<LookupDto>>> GetUserLookUp();
    }
}