using Shubham_Bhawtu.Authentication.Models;

namespace Shubham_Bhawtu.Authentication.Services.Interface
{
    public interface IUserService
    {
        Task<LoginInfoModel> GetUserDetails(LoginInfoModel loginInfo);
    }
}
