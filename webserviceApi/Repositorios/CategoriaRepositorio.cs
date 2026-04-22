using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using webserviceApi.DTOs;

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

        public async Task<string> GetById(int Id)
        {
            using var con = new SqlConnection(_con);

           await con.OpenAsync();

            var xmlString = $@"<Categorias>
                <Categoria>
                <Id>{Id}</Id>
                </Categoria>
                </Categorias>";

    
                var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[sp_GetCategoriaByid]", new { xmlCategoria = xmlString }, commandType: CommandType.StoredProcedure);
                return xmlRespuesta ?? string.Empty;
    
        }

        public async Task<int> Post(string xmlCategoria)
        {

            using var con = new SqlConnection(_con);

             await con.OpenAsync();

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<int>("sp_InsertarCategoria",new {xmlCategoria},commandType:CommandType.StoredProcedure);
            return xmlRespuesta;
        
        }

        public async Task<int> Put(string xmlCategoria)
        {
            using var con = new SqlConnection(_con);

            await con.OpenAsync();

                var xmlRespuesta = await con.QueryFirstOrDefaultAsync<int>("sp_updateCategoria", new { xmlCategoria }, commandType: CommandType.StoredProcedure);
                return xmlRespuesta;
          
        }

        public async Task<string> delete(int Id)
        {
            using var con=  new SqlConnection(_con);
          var xmlString=$@"
                <Categoria>
                <Id>{Id}</Id>
                </Categoria>
               ";

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_DeleteCategoria]", new { xmlCategoria = xmlString }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }

        public async Task<string> PostFoto(string xmlCategoria)
        {
            using var con = new SqlConnection(_con);

           await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PotsCategoriaFoto]", new { xmlCategoria = xmlCategoria }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }

    }
}
