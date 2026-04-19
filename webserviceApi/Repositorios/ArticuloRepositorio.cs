using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace webserviceApi.Repositorios
{
    public class ArticuloRepositorio: IArticuloRespositorio
    {
        private readonly string _connectionString;

        public ArticuloRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString")!;
        }

        public async Task<string> GetAll()
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            var xml = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetAllArticulos]",
                commandType: CommandType.StoredProcedure);

            return xml ?? string.Empty;
        }
    }
}
