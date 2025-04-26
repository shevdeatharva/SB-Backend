using Shubham_Bhawtu.Authentication.Models;
using Shubham_Bhawtu.Authentication.Repositories.Interface;
using Shubham_Bhawtu.Authentication.Services.Interface;

namespace Shubham_Bhawtu.Authentication.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginInfoModel> GetUserDetails(LoginInfoModel loginInfo)
        {
            return await _userRepository.GetUserDetails(loginInfo);
        }
    }
}
