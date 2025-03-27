using API_SPEEDTONER.Data;
using API_SPEEDTONER.Models;
using Dapper;
using System.Data;

namespace API_SPEEDTONER.Services
{
    public class DapperService : IDapperService
    {
        private readonly DapperContext _dapperContext;

        public DapperService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DapperServiceResponse> ExecuteQueryAsync<T>(string sql, DynamicParameters parameters, string connectionId, bool hasArray = false)
        {
            try
            {
                dynamic? response;
                using (IDbConnection connection = _dapperContext.CreateConnection(connectionId))
                {
                    if (hasArray)
                    {
                        //Devuelve una lista de objetos
                        response = await connection.QueryAsync<T>(sql, parameters);
                    }
                    else
                    {
                        //Devueleve un solo objeto o null
                        response = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
                    }

                    return new DapperServiceResponse
                    {
                        Results = response
                    };
                }
            }
            catch (Exception ex)
            {
                return new DapperServiceResponse
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DapperServiceResponse> ExecuteStoredProcedureAsync<T>(StoredProcedureData qData, DynamicParameters parameters, bool hasArray = false)
        {
            try
            {
                dynamic response;
                var spName = $"{qData.SchemaName}.{qData.Name}";
                using (IDbConnection connection = _dapperContext.CreateConnection(qData.IdConnectionString))
                {

                    if (hasArray)
                    {
                        response = await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        response = await connection.QuerySingleOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
                    }

                    return new DapperServiceResponse
                    {
                        Results = response
                    };
                }
            }
            catch (Exception ex)
            {
                return new DapperServiceResponse
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }
    }
}
