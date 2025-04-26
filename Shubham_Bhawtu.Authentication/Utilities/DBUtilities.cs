using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Shubham_Bhawtu.Authentication.Utilities
{
    public class DBUtilities:IDBUtilities
    {
        private readonly IConfiguration _configuration;

        public DBUtilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("IED_DEV_CON"));
        }

        public async Task<T> QuerySingleAsync<T>(string procedureName, object parameter = null)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(
                        procedureName,
                        parameter,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<List<T>> QueryAllAsync<T>(string procedureName, object parameter = null)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    var result = await connection.QueryAsync<T>(
                        procedureName,
                        parameter,
                        commandType: CommandType.StoredProcedure
                    );
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> ExecuteCommandAsync(string procedureName, DynamicParameters parameter = null)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(
                        procedureName,
                        parameter,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
