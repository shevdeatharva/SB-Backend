using Dapper;

namespace Shubham_Bhawtu.Authentication.Utilities
{
    public interface IDBUtilities
    {
        Task<List<T>> QueryAllAsync<T>(string sqlQuery, object parameters);
        Task<T> QuerySingleAsync<T>(string sqlQuery, object parameters);
        Task<int> ExecuteCommandAsync(string sqlCommand, DynamicParameters parameters);
    }
}
