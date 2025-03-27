using API_SPEEDTONER.Models;
using Dapper;

namespace API_SPEEDTONER.Services
{
    public interface IDapperService
    {
        Task<DapperServiceResponse> ExecuteStoredProcedureAsync<T>(StoredProcedureData qData, DynamicParameters parameters, bool hasArray = false);
        Task<DapperServiceResponse> ExecuteQueryAsync<T>(string sql, DynamicParameters parameters, string connectionId, bool hasArray = false);
    }
}
