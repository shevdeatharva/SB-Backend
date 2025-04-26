using Dapper;

namespace Shubham_Bhawtu.API.Utilities
{
    public interface IDatabaseUtilities
    {
        Task<List<T>> QueryAllAsync<T>(string sqlQuery, object parameters);
        Task<T> QuerySingleAsync<T>(string sqlQuery, object parameters);
        Task<int> ExecuteCommandAsync(string sqlCommand, DynamicParameters parameters);
    }
}
