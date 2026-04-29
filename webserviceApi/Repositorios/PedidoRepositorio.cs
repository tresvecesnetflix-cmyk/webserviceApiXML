using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public class PedidoRepositorio:IPedidoRepositorio
    {
        private readonly string _configuration;
        public PedidoRepositorio(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString")!;   
        }


        public async Task<string> Post(string xmlDocument, string usuarioId)
        {
            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PedidoDetallePostByUser]",
                    new { PedidoDetalle = xmlDocument, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }

        public async Task<string> Get(int Id, string usuarioId, string UsuarioEmail)
        {
            var con = new SqlConnection(_configuration);

            var xmlString = $@"<PedidoDetalles>
                              <PedidoDetalle>
                              <Id>{Id}</Id>
                              </PedidoDetalle>
                              </PedidoDetalles>";

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetPedidoDetalleById]",
                    new { PedidoDetalle = xmlString, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }

        public async Task<string> Delete(int Id , string usuarioId)
        {

            var con = new SqlConnection(_configuration);

            var xmlString = $@"<PedidosDetalles>
                              <PedidoDetalle>
                              <Id>{Id}</Id>
                              </PedidoDetalle>
                              </PedidosDetalles>";
            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_deletePedidoDetalleByUser]",
                    new { DetallePedido = xmlString, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }
    }
}
