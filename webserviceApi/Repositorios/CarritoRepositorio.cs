using Dapper;
using Microsoft.Data.SqlClient;
using webserviceApi.Servicios.Externos;
using System.Data;
namespace webserviceApi.Repositorios
{
    public class CarritoRepositorio:ICarritoRepositorio
    {
        private readonly string _configuration;


        public CarritoRepositorio(IConfiguration configuration )
        {
            _configuration =  configuration.GetConnectionString("ConnectionString")!;
        }

        public async Task<string> Post(string xmlString, string Id)
        {
            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_carritoByUser]",
                                     new { Carrito = xmlString, UsuarioId = Id }, commandType: CommandType.StoredProcedure);

            return xmlRespuesta ?? string.Empty;
        }
        public async Task<string> GetById(int Id, string IdUsuario)
        {
            var xmlString = $@"<CarritoItems>
                            <CarritoItem>
                            <Id>{Id}</Id>
                             </CarritoItem>
                            </CarritoItems>"; 

            var con = new SqlConnection(_configuration);

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_getCarritoByUser]"
                                     , new {Carrito= xmlString, usuarioId= IdUsuario}, commandType: CommandType.StoredProcedure);

            return xmlRespuesta ?? string.Empty;

        }

        public async Task<string> Delete(int id, string UsuarioId)
        {
            var xmlString = $@"<CarritoItems>
                            <CarritoItem>
                            <id>{id}</id>
                             </CarritoItem>
                            </CarritoItems>";

            var con = new SqlConnection(_configuration);

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_deleteCarritoByUser]"
                                     , new { Carrito = xmlString, usuarioId = UsuarioId }, commandType: CommandType.StoredProcedure);

            return xmlRespuesta ?? string.Empty;

        }



        public async Task<string> Put(string Carrito,string Id )
        {

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_UpdateCarritoByUser]",
                                      new {Carrito= Carrito, UsuarioId = Id}, commandType: CommandType.StoredProcedure);

            return xmlRespuesta ?? string.Empty;    

        }
    }

 
}
