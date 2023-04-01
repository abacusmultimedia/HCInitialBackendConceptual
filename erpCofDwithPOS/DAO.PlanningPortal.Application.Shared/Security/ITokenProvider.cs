using zero.Shared.Models.Security;
using zero.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace zero.Shared.Security;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> Authenticate(string username, string password, Dictionary<string, object> parameters);

    Task<Result<bool>> ChangePassword(ChangePasswordDTO parameter);
}