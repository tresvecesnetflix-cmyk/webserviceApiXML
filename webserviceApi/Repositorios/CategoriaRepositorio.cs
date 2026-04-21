using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace webserviceApi.Repositorios
{
    public class CategoriaRepositorio:ICategoriaRepositorio
    {
        private readonly string _con;
        public CategoriaRepositorio(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("ConnectionString")!;  
        }

        public async Task<string> ListaCategoria()
        {
           using var con =  new SqlConnection(_con);

            con.Open();

            try
            {
             
                var xml = await con.QueryFirstOrDefaultAsync<string>(
                    "[dbo].[SPU_GetAllCategoria]", commandType: CommandType.StoredProcedure);

                return xml;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }

        }
    }
}
