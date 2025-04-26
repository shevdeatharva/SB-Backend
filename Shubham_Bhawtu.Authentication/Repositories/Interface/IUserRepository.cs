using Shubham_Bhawtu.Authentication.Models;

namespace Shubham_Bhawtu.Authentication.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<LoginInfoModel> GetUserDetails(LoginInfoModel loginInfo);
    }
}
