using System.Data;
using System.Data.SqlClient;

namespace API_SPEEDTONER.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection(string connectionName) => new SqlConnection(_configuration[$"ConnectionStrings:{connectionName}"]);
    }
}
