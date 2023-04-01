using zero.Shared.Models.Security;
using zero.Shared.Response;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DAO.PlanningPortal.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<Result<TokenResponse>> LoginUser(UserLogin userLogin, HttpRequest httpRequest);

        Task<Result<bool>> ChangePassword(ChangePasswordDTO parameter);
    }
}