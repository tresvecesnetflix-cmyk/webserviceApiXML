using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Data;

namespace webserviceApi.Repositorios
{
    public class DireccionesRepositorio:IDireccionesRepositorio
    {
        private readonly string _configuration;
        public DireccionesRepositorio(IConfiguration configuration)          
        {
            _configuration = configuration.GetConnectionString("ConnectionString")!;
        }

        public async Task<string> GetAll()
        {

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();


                var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetAllDirecciones]",
                commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;
        }

        public async Task<string> GetById(int Id, string UsuarioId)
        {

            var con = new SqlConnection(_configuration);

            var xmlString = $@"<Direcciones>
                              <Direccion>
                              <Id>{Id}</Id>
                              </Direccion>
                              </Direcciones>";

            await con.OpenAsync();


            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetDireccionById]",
                                                    new { xmlDirecciones = xmlString, usuarioId=UsuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;
        }

        public async Task<string> Delete(int Id, string usuarioId)
        {
            var con = new SqlConnection(_configuration);

            var xmlString = $@"<Direcciones>
                              <Direccion>
                              <Id>{Id}</Id>
                              </Direccion>
                              </Direcciones>";

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_DeleteDirecciones]",
                              new { xmlDirecciones = xmlString, UsuarioId= usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }


        public async Task<int> PostById(string Direccion, string usuarioId)
        {
            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].[spu_postDireccionesUser]",
                       new { xmlDirecciones = Direccion,UsuarioId= usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult;

        }

        public async Task<int> Post(string Direccion)
        {
            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].[spu_postDirecciones]",
                       new { xmlDirecciones = Direccion }, commandType: CommandType.StoredProcedure);

            return xmlResult;

        }

        public async Task<string>Put(string xmlDocument, string usuarioId)
        {

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PutDireccionesByUser]",
                new { xmlDirecciones = xmlDocument, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }
    }
}
