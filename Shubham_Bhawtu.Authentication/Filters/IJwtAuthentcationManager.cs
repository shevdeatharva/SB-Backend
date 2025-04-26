using Shubham_Bhawtu.Authentication.Models;

namespace Shubham_Bhawtu.Authentication.Filters
{
    public interface IJwtAuthentcationManager
    {
        string Authenticate(LoginInfoModel user, bool SamalFlag);
        string RefreshTokenExpirationTime(string token, bool SamalFlag);
    }
}
