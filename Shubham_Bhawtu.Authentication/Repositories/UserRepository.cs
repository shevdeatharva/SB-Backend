using Dapper;
using Shubham_Bhawtu.Authentication.Models;
using Shubham_Bhawtu.Authentication.Repositories.Interface;
using Shubham_Bhawtu.Authentication.Utilities;
using System.Data;

namespace Shubham_Bhawtu.Authentication.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IDBUtilities _dbConnection;
        public UserRepository( IDBUtilities dBUtilities)
        {
            _dbConnection = dBUtilities;
        }
        public async Task<LoginInfoModel> GetUserDetails(LoginInfoModel loginInfo)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Username", loginInfo.Username);
                parameter.Add("@Password", loginInfo.Password);
                var userdetails = await _dbConnection.QuerySingleAsync<LoginInfoModel>("sp_ins_Login_master", parameter);
                return userdetails;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
